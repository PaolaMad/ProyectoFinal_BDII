using LOGIN.Dtos.Communicates;
using LOGIN.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LOGIN.Controllers
{
    [Route("api/communicate")]
    [ApiController]
    public class CommunicateController : ControllerBase
    {
        private readonly IComunicateServices _comunicateServices;

        public CommunicateController(IComunicateServices comunicateServices)
        {
            _comunicateServices = comunicateServices;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCommunicate([FromBody] CreateCommunicateDto model)
        {
            var response = await _comunicateServices.CreateCommunicate(model);

            if (response.Status)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCommunicates()
        {
            var response = await _comunicateServices.GetAllCommunicates();

            if (response.Status)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        //obtener comunicado por id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommunicateById(Guid id)
        {
            var response = await _comunicateServices.GetCommunicateById(id);

            if (response.Status)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }


        //editar comunicado
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCommunicate(Guid id,[FromBody] CommunicateDto model)
        {
            model.Id = id;
            var response = await _comunicateServices.UpdateCommunicate(model);

            if (response.Status)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        //eliminar comunicado
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCommunicate(Guid id)
        {
            var response = await _comunicateServices.DeleteCommunicate(id);

            if (response.Status)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

    }
}