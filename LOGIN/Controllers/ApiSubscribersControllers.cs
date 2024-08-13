using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LOGIN.Services.Interfaces;
using LOGIN.Dtos;

namespace LOGIN.Controllers
{
    [Route("api/subscribers")]
    [ApiController]
    public class ApiSubscribersControllers : ControllerBase
    {

        private readonly IAPiSubscriberServices _apiSubscriberServices;

        public ApiSubscribersControllers(IAPiSubscriberServices apiSubscriberServices)
        {
            _apiSubscriberServices = apiSubscriberServices;
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
