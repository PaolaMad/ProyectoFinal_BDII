using AutoMapper;
using CloudinaryDotNet;
using LOGIN.Dtos.ReportDto;
using LOGIN.Dtos;
using LOGIN.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using LOGIN.Dtos.States;

namespace LOGIN.Services
{
    public class StateService : IStateService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public StateService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        //traer todos los estados
        public async Task<ResponseDto<List<StateDto>>> GetAllStatesAsync()
        {
            var statesEntity = await _dbContext.States.ToListAsync();

            var statesDto = _mapper.Map<List<StateDto>>(statesEntity);

            return new ResponseDto<List<StateDto>>
            {
                Status = true,
                StatusCode = 200,
                Message = "Estados encontrados",
                Data = statesDto
            };
        }

        //traer estados por id
        public async Task<ResponseDto<StateDto>> GetStateByIdAsync(Guid id)
        {
            var stateEntity = await _dbContext.States.FirstOrDefaultAsync(x => x.Id == id);

            if (stateEntity == null)
            {
                return new ResponseDto<StateDto>
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "Estado no encontrado"
                };
            }

            var stateDto = _mapper.Map<StateDto>(stateEntity);

            return new ResponseDto<StateDto>
            {
                Status = true,
                StatusCode = 200,
                Message = "Reporte encontrado",
                Data = stateDto
            };
        }
    }
}
