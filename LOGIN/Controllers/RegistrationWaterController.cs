using LOGIN.Dtos.ScheduleDtos.RegistrationWater;
using LOGIN.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/registration")]
public class RegistrationWaterController : ControllerBase
{
    private readonly IRegistrationWaterService _registrationWaterService;

    public RegistrationWaterController(IRegistrationWaterService registrationWaterService)
    {
        _registrationWaterService = registrationWaterService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ResponseDto<RegistrationWaterDto>>> GetByIdRegistrationAsync(Guid id)
    {
        var result = await _registrationWaterService.GetByIdAsync(id);
        if (result.Status)
        {
            return Ok(result);
        }
        return StatusCode(result.StatusCode, result);
    }


    [HttpGet]
    public async Task<ActionResult<ResponseDto<IEnumerable<RegistrationWaterDto>>>> GetAllRegistrationAsync()
    {
        var result = await _registrationWaterService.GetAllAsync();
        return StatusCode(result.StatusCode, result);
    }

    [HttpPost]
    public async Task<ActionResult<ResponseDto<RegistrationWaterDto>>> CreateRegistration([FromBody] RegistrationWaterCreateDto createDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ResponseDto<string>
            {
                Status = false,
                StatusCode = 400,
                Message = "El modelo es inválido",
                Data = ModelState.ToString()
            });
        }

        var result = await _registrationWaterService.CreateAsync(createDto);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] RegistrationWaterCreateDto updateDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var response = await _registrationWaterService.UpdateAsync(id, updateDto);
        if (!response.Status)
        {
            return NotFound(response.Message);
        }

        return Ok(response.Data);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var response = await _registrationWaterService.DeleteAsync(id);
        if (!response.Status)
        {
            return NotFound(response.Message);
        }

        return Ok(response.Message);
    }
}
