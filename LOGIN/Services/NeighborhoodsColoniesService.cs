using AutoMapper;
using LOGIN.Dtos;
using LOGIN.Dtos.ScheduleDtos.NeighborhoodsColonies;
using LOGIN.Entities;
using LOGIN.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LOGIN.Services
{
    public class NeighborhoodsColoniesService : INeighborhoodsColoniesService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public NeighborhoodsColoniesService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResponseDto<NeighborhoodsColoniesDto>> GetByIdNCAsync(Guid id)
        {
            var entity = await _context.NeighborhoodsColonies.FindAsync(id);
            if (entity == null)
            {
                return new ResponseDto<NeighborhoodsColoniesDto>
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "Barrio/colonia no encontrada",
                    Data = null
                };
            }
            return new ResponseDto<NeighborhoodsColoniesDto>
            {
                Status = true,
                StatusCode = 200,
                Message = "Barrio/colonia obtenidas correctamente",
                Data = _mapper.Map<NeighborhoodsColoniesDto>(entity)
            };
        }

        public async Task<ResponseDto<IEnumerable<NeighborhoodsColoniesDto>>> GetAllNCAsync()
        {
            var entities = await _context.NeighborhoodsColonies.ToListAsync();
            return new ResponseDto<IEnumerable<NeighborhoodsColoniesDto>>
            {
                Status = true,
                StatusCode = 200,
                Message = "Barrio/colonia obtenidas correctamente",
                Data = _mapper.Map<IEnumerable<NeighborhoodsColoniesDto>>(entities)
            };
        }

        public async Task<ResponseDto<NeighborhoodsColoniesDto>> CreateNC(NeighborhoodsColoniesCreateDto createDto)
        {
            var entity = _mapper.Map<NeighborhoodsColoniesEntity>(createDto);
            _context.NeighborhoodsColonies.Add(entity);
            await _context.SaveChangesAsync();
            return new ResponseDto<NeighborhoodsColoniesDto>
            {
                Status = true,
                StatusCode = 201,
                Message = "Barrio/colonia creado correctamente",
                Data = _mapper.Map<NeighborhoodsColoniesDto>(entity)
            };
        }

        public async Task<ResponseDto<NeighborhoodsColoniesDto>> UpdateNC(Guid id, NeighborhoodsColoniesCreateDto updateDto)
        {
            var entity = await _context.NeighborhoodsColonies.FindAsync(id);
            if (entity == null)
            {
                return new ResponseDto<NeighborhoodsColoniesDto>
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "Barrio/colonia no encontrada",
                    Data = null
                };
            }

            _mapper.Map(updateDto, entity);
            await _context.SaveChangesAsync();
            return new ResponseDto<NeighborhoodsColoniesDto>
            {
                Status = true,
                StatusCode = 200,
                Message = "Barrio/colonia actualizada correctamente",
                Data = _mapper.Map<NeighborhoodsColoniesDto>(entity)
            };
        }
        public async Task<ResponseDto<IEnumerable<NeighborhoodsColoniesDto>>> GetByBlockIdAsync(Guid blockId)
        {
            var entities = await _context.NeighborhoodsColonies
                .Where(nc => nc.BlockId == blockId)
                .ToListAsync();
            return new ResponseDto<IEnumerable<NeighborhoodsColoniesDto>>
            {
                Status = true,
                StatusCode = 200,
                Message = "Barrios/colonias obtenidos correctamente",
                Data = _mapper.Map<IEnumerable<NeighborhoodsColoniesDto>>(entities)
            };
        }
    }
}