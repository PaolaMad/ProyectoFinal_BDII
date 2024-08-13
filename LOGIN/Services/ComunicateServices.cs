using AutoMapper;
using LOGIN.Dtos;
using LOGIN.Dtos.Communicates;
using LOGIN.Entities;
using LOGIN.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LOGIN.Services
{
    public class ComunicateServices : IComunicateServices
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public ComunicateServices(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }


        public async Task<ResponseDto<CommunicateDto>> CreateCommunicate(CreateCommunicateDto model)
        {
            var communicateEntity = _mapper.Map<CommunicateEntity>(model);
            communicateEntity.Date = DateTime.UtcNow;

            _dbContext.Communicates.Add(communicateEntity);
            await _dbContext.SaveChangesAsync();

            var communicateDto = _mapper.Map<CommunicateDto>(communicateEntity);

            return new ResponseDto<CommunicateDto>
            {
                Status = true,
                StatusCode = 201,
                Message = "Communiqué created successfully",
                Data = communicateDto
            };
        }

        //traer todos los comunicados
        public async Task<ResponseDto<List<CommunicateDto>>> GetAllCommunicates()
        {
            var communicates = await _dbContext.Communicates.ToListAsync();
            var communicatesDto = _mapper.Map<List<CommunicateDto>>(communicates);

            return new ResponseDto<List<CommunicateDto>>
            {
                Status = true,
                StatusCode = 200,
                Message = "List of communications",
                Data = communicatesDto
            };
        }

        //traer comunicado por id
        public async Task<ResponseDto<CommunicateDto>> GetCommunicateById(Guid id)
        {
            var communicateEntity = await _dbContext.Communicates.FirstOrDefaultAsync(x => x.Id == id);

            if (communicateEntity == null)
            {
                return new ResponseDto<CommunicateDto>
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "Communication not found"
                };
            }

            var communicateDto = _mapper.Map<CommunicateDto>(communicateEntity);

            return new ResponseDto<CommunicateDto>
            {
                Status = true,
                StatusCode = 200,
                Message = "Communication found",
                Data = communicateDto
            };
        }

        public async Task<ResponseDto<CommunicateDto>> UpdateCommunicate(CommunicateDto model)
        {
            var communicateEntity = await _dbContext.Communicates.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (communicateEntity == null)
            {
                return new ResponseDto<CommunicateDto>
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "Communication not found"
                };
            }

            communicateEntity.Tittle = model.Tittle;
            communicateEntity.Content = model.Content;
            communicateEntity.Date = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();

            var communicateDto = _mapper.Map<CommunicateDto>(communicateEntity);

            return new ResponseDto<CommunicateDto>
            {
                Status = true,
                StatusCode = 200,
                Message = "Communiqué updated correctly",
                Data = communicateDto
            };
        }
        //elimina comunicado por id
        public async Task<ResponseDto<CommunicateDto>> DeleteCommunicate(Guid id)
        {
            var communicateEntity = await _dbContext.Communicates.FirstOrDefaultAsync(x => x.Id == id);

            if (communicateEntity == null)
            {
                return new ResponseDto<CommunicateDto>
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "Communication not found"
                };
            }

            _dbContext.Communicates.Remove(communicateEntity);
            await _dbContext.SaveChangesAsync();

            var communicateDto = _mapper.Map<CommunicateDto>(communicateEntity);

            return new ResponseDto<CommunicateDto>
            {
                Status = true,
                StatusCode = 200,
                Message = "Communiqué successfully deleted",
                Data = communicateDto
            };
        }
    }
}