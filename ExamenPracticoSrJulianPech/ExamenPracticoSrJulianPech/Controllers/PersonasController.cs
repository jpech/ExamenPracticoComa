using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Services.Models.Request;
using System.Threading;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ExamenPracticoSrJulianPech.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : ControllerBase
    {
        private readonly IPersonaService personaService;

        public PersonasController(IPersonaService _personaService)
        {
            personaService = _personaService;
        }

        // GET: api/<PersonasController>
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken ct)
        {
            var response = await personaService.SelPersonas(ct);
            if (response.Success == false)
                return BadRequest(response.Message);

            return Ok(response);
        }

        // GET api/<PersonasController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, CancellationToken ct)
        {
            var response = await personaService.SelPersonaPorId(id, ct);
            if(response.Success == false)
            {
                return BadRequest(response.Message);
            }

            return Ok(response);
        }

        // POST api/<PersonasController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PersonaNewRequest request, CancellationToken ct)
        {
            bool validate = TryValidateModel(request);
            if (validate)
            {
                var response = await personaService.AgregarPersona(request, ct);
                if(response.Success == false)
                {
                    return BadRequest(response.Message);
                }

                return Ok(response);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // PUT api/<PersonasController>/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] PersonaUpdateRequest request, CancellationToken ct)
        {
            bool validate = TryValidateModel(request);
            if (validate)
            {
                var response = await personaService.ActualizarPersona(request, ct);
                if(response.Success == false)
                {
                    return BadRequest(response.Message);
                }

                return Ok(response);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // DELETE api/<PersonasController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            var response = await personaService.EliminarPersona(id, ct);
            if(response.Success == false)
            {
                return BadRequest(response.Message);
            }

            return Ok(response);
        }
    }
}