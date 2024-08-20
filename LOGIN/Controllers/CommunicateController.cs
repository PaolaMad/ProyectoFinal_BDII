using FireSharp.Config;
using FireSharp;
using FireSharp.Interfaces;
using LOGIN.Dtos.Communicates;
using LOGIN.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FireSharp.Response;
using LOGIN.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using FireSharp.Extensions;

namespace LOGIN.Controllers
{
    [Route("api/communicate")]
    [ApiController]
    public class CommunicateController : ControllerBase
    {
        private readonly IComunicateServices _comunicateServices;
        private readonly IFirebaseClient _firebaseClient;

        private readonly HttpContext _httpContext;

        public CommunicateController(IComunicateServices comunicateServices, IHttpContextAccessor httpContextAccessor)
        {
            _comunicateServices = comunicateServices;

            IFirebaseConfig config = new FirebaseConfig
            {
                AuthSecret = "75d2Hsnb7kvdy8eoAU5XY0W1DxNGVH0GxPN5DsuP",
                BasePath = "https://fir-bdii-default-rtdb.firebaseio.com/"
            };

            _firebaseClient = new FirebaseClient(config);

        }

        [HttpPost]
        public async Task<IActionResult> CreateCommunicate([FromBody] CreateCommunicateDto model)
        {
            var response = await _comunicateServices.CreateCommunicate(model);

            if (response.Status)
            {
                return Ok(response);
            }


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

            return BadRequest(response);


        }


        //editar comunicado
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCommunicate(Guid id,[FromBody] CommunicateDto model)
        {
            model.Id = id;
            var response = await _comunicateServices.UpdateCommunicate(model);

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

        //eliminar comunicado
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCommunicate(Guid id)
        {
            var response = await _comunicateServices.DeleteCommunicate(id);


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