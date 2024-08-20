using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LOGIN.Services.Interfaces;
using LOGIN.Dtos;
using FireSharp.Interfaces;
using FireSharp.Config;
using FireSharp;

namespace LOGIN.Controllers
{
    [Route("api/subscribers")]
    [ApiController]
    public class ApiSubscribersControllers : ControllerBase
    {

        private readonly IAPiSubscriberServices _apiSubscriberServices;
        private readonly IFirebaseClient _firebaseClient;

        private readonly HttpContext _httpContext;
        private readonly string _USER_ID;


        public ApiSubscribersControllers(IAPiSubscriberServices apiSubscriberServices, IHttpContextAccessor httpContextAccessor)
        {
            _apiSubscriberServices = apiSubscriberServices;

            var idClaim = _httpContext.User.Claims.Where(x => x.Type == "UserId")
            .FirstOrDefault();
            _USER_ID = idClaim?.Value;

            IFirebaseConfig config = new FirebaseConfig
            {
                AuthSecret = "75d2Hsnb7kvdy8eoAU5XY0W1DxNGVH0GxPN5DsuP",
                BasePath = "https://fir-bdii-default-rtdb.firebaseio.com/"
            };

            _firebaseClient = new FirebaseClient(config);

        }

        [HttpGet]
        public async Task<IActionResult> GetAbonados()
        {
            var json = await _apiSubscriberServices.GetUserAsync();
            var abonados = JsonConvert.DeserializeObject<List<Suscriber>>(json);
            return Ok(abonados);
        }

        //buscar por clave catastral abonado y mostrar solo el nombre y el saldo
        [HttpGet("buscar-abonado/{clave}")]
        public async Task<IActionResult> GetAbonado(string clave)
        {
            var json = await _apiSubscriberServices.GetUserAsync();
            var abonados = JsonConvert.DeserializeObject<List<Suscriber>>(json);
            var abonado = abonados.FirstOrDefault(x => x.clave_catastral == clave);

            if (abonado == null)
            {
                return NotFound();
            }

            var result = new
            {
                abonado.clave_catastral,
                abonado.nombre_abonado,
                abonado.Saldo_actual,
            };

            return Ok(result);
        }

        //buscar abonado y mostrar todos los datos que pertenecen al abonado
        [HttpGet("buscar-abonado-completo/{clave}")]
        public async Task<IActionResult> GetAbonadoCompleto(string clave)
        {
            var json = await _apiSubscriberServices.GetUserAsync();
            var abonados = JsonConvert.DeserializeObject<List<Suscriber>>(json);
            var abonado = abonados.FirstOrDefault(x => x.clave_catastral == clave);

            if (abonado == null)
            {
                return NotFound();
            }

            return Ok(abonado);
        }
        
    }
}
