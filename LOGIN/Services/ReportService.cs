using AutoMapper;
using LOGIN.Dtos.Communicates;
using LOGIN.Dtos;
using LOGIN.Entities;
using LOGIN.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using CloudinaryDotNet;
using LOGIN.Dtos.ReportDto;
using CloudinaryDotNet.Actions;

namespace LOGIN.Services
{
    public class ReportService : IReportService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        private readonly Cloudinary _cloudinary;

        public ReportService(ApplicationDbContext dbContext, IMapper mapper, IConfiguration configuration)
        {
            _cloudinary = new Cloudinary(Environment.GetEnvironmentVariable("CLOUDINARY_URL"));
            _cloudinary.Api.Secure = true;

            _dbContext = dbContext;
            _mapper = mapper;
        }


        public async Task<ResponseDto<ReportDto>> CreateReportAsync(CreateReportDto model)
        {
            try
            {

                using var stream = model.File.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(model.File.FileName, stream),
                    Transformation = new Transformation().Width(500).Height(500).Crop("fill")
                };

                // Subir imagen a Cloudinary
                var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                //buscar el estado del reporte
                var defaultState = await _dbContext.States.FirstOrDefaultAsync(x => x.Name == "no asignado");

                var reportEntity = new ReportEntity
                {
                    PublicId = uploadResult.PublicId,
                    Url = uploadResult.SecureUrl.ToString(),
                    Key = model.Key,
                    Name = model.Name,
                    DNI = model.DNI,
                    Cellphone = model.Cellphone,
                    Date = model.Date,
                    Report = model.Report,
                    Direction = model.Direction,
                    Observation = model.Observation,
                    StateId = defaultState?.Id ?? Guid.NewGuid() // Si no se encuentra el estado, se asigna un nuevo Guid
                };

                _dbContext.Reports.Add(reportEntity);
                await _dbContext.SaveChangesAsync();

                var reportDto = _mapper.Map<ReportDto>(reportEntity);

                return new ResponseDto<ReportDto>
                {
                    Status = true,
                    StatusCode = 201,
                    Message = "Reporte creado con exito",
                    Data = reportDto
                };
            }
            catch (DbUpdateException dbEx)
            {
                var innerException = dbEx.InnerException?.Message ?? dbEx.Message;

                return new ResponseDto<ReportDto>
                {
                    Status = false,
                    StatusCode = 500,
                    Message = "An error occurred while saving the entity changes. Details: " + innerException,
                    Data = null
                };
            }
            catch (Exception e)
            {
                return new ResponseDto<ReportDto>
                {
                    Status = false,
                    StatusCode = 500,
                    Message = e.Message,
                    Data = null
                };
            }
        }

        public async Task<ResponseDto<List<ReportDto>>> GetAllReportsAsync()
        {
            //se agrego el include para traer el estado
            var reports = await _dbContext.Reports.Include(r => r.State).ToListAsync();
            var reportsDto = _mapper.Map<List<ReportDto>>(reports);

            return new ResponseDto<List<ReportDto>>
            {
                Status = true,
                StatusCode = 200,
                Message = "Lista de reportes",
                Data = reportsDto
            };
        }

        //traer reporte por id
        public async Task<ResponseDto<ReportDto>> GetReportByIdAsync(Guid id)
        {
            var reportEntity = await _dbContext.Reports.Include(r => r.State).FirstOrDefaultAsync(x => x.Id == id);

            if (reportEntity == null)
            {
                return new ResponseDto<ReportDto>
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "Reporte no encontrado"
                };
            }

            var reportDto = _mapper.Map<ReportDto>(reportEntity);

            return new ResponseDto<ReportDto>
            {
                Status = true,
                StatusCode = 200,
                Message = "Reporte encontrado",
                Data = reportDto
            };
        }

        public async Task<ResponseDto<ReportDto>> UpdateReportAsync(UpdateReportDto model)
        {
            var reportEntity = await _dbContext.Reports.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (reportEntity == null)
            {
                return new ResponseDto<ReportDto>
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "Reporte no encontrado"
                };
            }

            reportEntity.Key = model.Key;
            reportEntity.Name = model.Name;
            reportEntity.DNI = model.DNI;
            reportEntity.Cellphone = model.Cellphone;
            reportEntity.Report = model.Report;
            reportEntity.Direction = model.Direction;
            reportEntity.Observation = model.Observation;
            reportEntity.StateId = model.StateId;

            // Verificar si se proporciona un nuevo archivo para actualizar la imagen
            if (model.File != null)
            {
                // Subir nueva imagen a Cloudinary
                using var stream = model.File.OpenReadStream();
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(model.File.FileName, stream),
                    Transformation = new Transformation().Width(500).Height(500).Crop("fill")
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                // Actualizar la URL y el PublicId de la imagen en la entidad
                reportEntity.PublicId = uploadResult.PublicId;
                reportEntity.Url = uploadResult.SecureUrl.ToString();
            }

            await _dbContext.SaveChangesAsync();

            var reportDto = _mapper.Map<ReportDto>(reportEntity);

            return new ResponseDto<ReportDto>
            {
                Status = true,
                StatusCode = 200,
                Message = "Reporte actualizado con exito",
                Data = reportDto
            };
        }
        //elimina comunicado por id
        public async Task<ResponseDto<ReportDto>> DeleteReportAsync(Guid id)
        {
            var reportEntity = await _dbContext.Reports.FirstOrDefaultAsync(x => x.Id == id);

            if (reportEntity == null)
            {
                return new ResponseDto<ReportDto>
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "Reporte no encontrado"
                };
            }

            _dbContext.Reports.Remove(reportEntity);
            await _dbContext.SaveChangesAsync();

            var reportDto = _mapper.Map<ReportDto>(reportEntity);

            return new ResponseDto<ReportDto>
            {
                Status = true,
                StatusCode = 200,
                Message = "Reporte eliminado con exito",
                Data = reportDto
            };

        }

        //metodo necesario para obtener la url de la imagen OBLIGATORIO
        public Task<string> GetImageUrl(string publicId)
        {
            return Task.FromResult(_cloudinary.Api.UrlImgUp.BuildUrl(publicId));
        }

        //metodo para cambiar el estado del reporte
        public async Task<ResponseDto<ReportDto>> ChangeStateReportAsync(Guid id, Guid stateId)
        {
            var reportEntity = await _dbContext.Reports.FirstOrDefaultAsync(x => x.Id == id);

            if (reportEntity == null)
            {
                return new ResponseDto<ReportDto>
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "Reporte no encontrado"
                };
            }

            var newState = await _dbContext.States.FirstOrDefaultAsync(x => x.Id == stateId);
            if (newState == null)
            {
                return new ResponseDto<ReportDto>
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "Estado no encontrado"
                };
            }

            reportEntity.StateId = stateId;

            await _dbContext.SaveChangesAsync();

            var reportDto = _mapper.Map<ReportDto>(reportEntity);

            return new ResponseDto<ReportDto>
            {
                Status = true,
                StatusCode = 200,
                Message = "Estado del reporte actualizado con exito",
                Data = reportDto
            };
        }
    }
}