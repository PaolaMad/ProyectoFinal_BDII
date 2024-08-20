using FireSharp.Config;
using FireSharp;
using FireSharp.Interfaces;
using LOGIN.Dtos.ReportDto;
using LOGIN.Services;
using LOGIN.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using FireSharp.Response;
using LOGIN.Dtos;
using LOGIN.Entities;
using FireSharp.Extensions;

namespace LOGIN.Controllers
{
    [Route("api/report")]
    [ApiController]
    public class ReportController : Controller
    {
        private readonly IReportService _reportServices;
        private readonly IFirebaseClient _firebaseClient;

        private readonly HttpContext _httpContext;

        public ReportController(IReportService reportServices, IHttpContextAccessor httpContextAccessor)
        {
            _reportServices = reportServices;

            IFirebaseConfig config = new FirebaseConfig
            {
                AuthSecret = "75d2Hsnb7kvdy8eoAU5XY0W1DxNGVH0GxPN5DsuP",
                BasePath = "https://fir-bdii-default-rtdb.firebaseio.com/"
            };

            _firebaseClient = new FirebaseClient(config);

        }

        [HttpPost]
        public async Task<IActionResult> CreateReport([FromForm] CreateReportDto model)
        {
            // Verificar que el modelo no sea nulo
            if (model == null)
            {
                return BadRequest("El modelo es nulo");
            }

            // Verificar que el archivo no sea nulo
            if (model.File == null)
            {
                return BadRequest("El archivo es nulo");
            }

            try
            {
                // Llamada al servicio para crear el reporte
                var response = await _reportServices.CreateReportAsync(model);

                var status = "";

                if (response.StatusCode == 200)
                {

                    status = "succes";

                }
                else
                {
                    status = "error";
                }

                LogEntity log = new LogEntity
                {
                    Id = Guid.NewGuid(),
                    Time = DateTime.UtcNow,
                    Action = response.ToJson(),
                    State = status,

                };

                SetResponse respuesta = await _firebaseClient.SetAsync<LogEntity>("logs/", log);

                if (response.Status)
                {
                    return CreatedAtAction(nameof(CreateReport), new { id = response.Data.Id }, response.Data);
                }
                else
                {
                    return StatusCode(response.StatusCode, response.Message);
                }


            }
            catch (Exception ex)
            {
                // Registrar la excepción con detalles
                Console.WriteLine($"Exception: {ex.Message}\nStack Trace: {ex.StackTrace}");
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }

            

        }

        [HttpGet]
        public async Task<IActionResult> GetAllReports()
        {
            var response = await _reportServices.GetAllReportsAsync();


            var status = "";

            if (response.StatusCode == 200)
            {

                status = "succes";

            }
            else
            {
                status = "error";
            }

            LogEntity log = new LogEntity
            {
                Id = Guid.NewGuid(),
                Time = DateTime.UtcNow,
                Action = response.ToJson(),
                State = status,

            };

            SetResponse respuesta = await _firebaseClient.SetAsync<LogEntity>("logs/", log);

            if (response.Status)
            {
                return Ok(response);
            }

            return BadRequest(response);


        }

        //obtener reporte por id
        [HttpGet("byid/{id}")]
        public async Task<IActionResult> GetReportById(Guid id)
        {
            var response = await _reportServices.GetReportByIdAsync(id);

            var status = "";

            if (response.StatusCode == 200)
            {

                status = "succes";

            }
            else
            {
                status = "error";
            }

            LogEntity log = new LogEntity
            {
                Id = Guid.NewGuid(),
                Time = DateTime.UtcNow,
                Action = response.ToJson(),
                State = status,

            };

            SetResponse respuesta = await _firebaseClient.SetAsync<LogEntity>("logs/", log);

            if (response.Status)
            {
                return Ok(response);
            }

            return BadRequest(response);


        }

        //editar reporte
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReport(Guid id, [FromForm] UpdateReportDto model)
        {
            model.Id = id;
            var response = await _reportServices.UpdateReportAsync(model);


            var status = "";

            if (response.StatusCode == 200)
            {

                status = "succes";

            }
            else
            {
                status = "error";
            }

            LogEntity log = new LogEntity
            {
                Id = Guid.NewGuid(),
                Time = DateTime.UtcNow,
                Action = response.ToJson(),
                State = status,

            };

            SetResponse respuesta = await _firebaseClient.SetAsync<LogEntity>("logs/", log);

            if (response.Status)
            {
                return Ok(response);
            }

            return BadRequest(response);


        }

        //eliminar reporte
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReport(Guid id)
        {
            var response = await _reportServices.DeleteReportAsync(id);

            var status = "";

            if (response.StatusCode == 200)
            {

                status = "succes";

            }
            else
            {
                status = "error";
            }

            LogEntity log = new LogEntity
            {
                Id = Guid.NewGuid(),
                Time = DateTime.UtcNow,
                Action = response.ToJson(),
                State = status,

            };

            SetResponse respuesta = await _firebaseClient.SetAsync<LogEntity>("logs/", log);

            if (response.Status)
            {
                return Ok(response);
            }

            return BadRequest(response);


        }

        //metodo necesario para obtener la url de la imagen OBLIGATORIO
        [HttpGet("image/{publicId}")]
        public async Task<IActionResult> GetImageUrl(string publicId)
        {
            var url = await _reportServices.GetImageUrl(publicId);

            //validar si la url es nula
            if (string.IsNullOrEmpty(url))
            {
                return NotFound("No se encontro la imagen");
            }

            return Ok(new { Url = url });

        }

        //metodo para cambiar el estado del reporte
        [HttpPut("{id}/state/{stateId}")]
        public async Task<IActionResult> ChangeStateReport(Guid id, [FromBody] Guid stateId)
        {
            var response = await _reportServices.ChangeStateReportAsync(id, stateId);

            var status = "";

            if (response.StatusCode == 200)
            {

                status = "succes";

            }
            else
            {
                status = "error";
            }

            LogEntity log = new LogEntity
            {
                Id = Guid.NewGuid(),
                Time = DateTime.UtcNow,
                Action = response.ToJson(),
                State = status,

            };

            SetResponse respuesta = await _firebaseClient.SetAsync<LogEntity>("logs/", log);

            if (response.Status)
            {
                return Ok(response);
            }

            return BadRequest(response);

        }
    }
}