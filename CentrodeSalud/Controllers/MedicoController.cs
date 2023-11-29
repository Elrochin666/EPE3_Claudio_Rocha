
// Importar los espacios de nombres necesarios
using Microsoft.AspNetCore.Mvc;
using Model.Database;
using Mysql.Data;
using Mysql.Database.Repositorio;
using System.Threading.Tasks;

// Definir el namespace y la clase del controlador
namespace CentrodeSalud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicoController : ControllerBase
    {
        // Campo privado para almacenar la instancia de ConexionMedico
        private readonly ConexionMedico _consultaMedico;

        // Constructor que recibe la instancia de ConexionMedico a través de la inyección de dependencias
        public MedicoController(ConexionMedico consultaMedico)
        {
            _consultaMedico = consultaMedico;
        }

        // Endpoint para obtener todos los médicos
        [HttpGet]
        public async Task<IActionResult> GetAllMedicos()
        {
            return Ok(await _consultaMedico.GetAllMedico());
        }

        // Endpoint para obtener detalles de un médico por su ID
        [HttpGet("{idMedico}")]
        public async Task<IActionResult> GetMedicoDetails(int idMedico)
        {
            return Ok(await _consultaMedico.GetMedicoDetails(idMedico));
        }

        // Endpoint para crear un nuevo médico
        [HttpPost]
        public async Task<IActionResult> CreateMedico([FromBody] Medico medico)
        {
            if (medico == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var creado = await _consultaMedico.InsertMedico(medico);
            return Created("creado", creado);
        }

        // Endpoint para actualizar la información de un médico por su ID
        [HttpPut("{idMedico}")]
        public async Task<IActionResult> UpdateMedico(int idMedico, [FromBody] Medico medico)
        {
            if (medico == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            medico.IdMedico = idMedico; // Asegurarse de que el ID coincida
            await _consultaMedico.UpdateMedico(medico);
            return NoContent();
        }

        // Endpoint para eliminar un médico por su ID
        [HttpDelete("{idMedico}")]
        public async Task<IActionResult> DeleteMedico(int idMedico)
        {
            await _consultaMedico.DeleteMedico(idMedico);
            return NoContent();
        }
    }
}