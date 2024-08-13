using LOGIN.Dtos;
using LOGIN.Dtos.ScheduleDtos.Lines;
using LOGIN.Services;
using LOGIN.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LOGIN.Controllers
{
    [ApiController]
    [Route("api/lines")]
    public class LinesController : ControllerBase
    {
        private readonly ILinesService _linesService;

        public LinesController(ILinesService linesService)
        {
            _linesService = linesService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto<LinesDto>>> GetById(Guid id)
        {
            var result = await _linesService.GetByIdLineAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDto<IEnumerable<LinesDto>>>> GetAll()
        {
            var result = await _linesService.GetAllLineAsync();
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto<LinesDto>>> Create(LinesCreateDto createDto)
        {
            var result = await _linesService.CreateLine(createDto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDto<LinesDto>>> Update(Guid id, LinesCreateDto updateDto)
        {
            var result = await _linesService.UpdateLine(id, updateDto);
            return StatusCode(result.StatusCode, result);
        }
    }
}