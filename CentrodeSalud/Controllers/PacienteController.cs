
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
    public class PacienteController : ControllerBase
    {
        // Campo privado para almacenar la instancia de ConexionPaciente
        private readonly ConexionPaciente _consultaPaciente;

        // Constructor que recibe la instancia de ConexionPaciente a través de la inyección de dependencias
        public PacienteController(ConexionPaciente consultaPaciente)
        {
            _consultaPaciente = consultaPaciente;
        }

        // Endpoint para obtener todos los pacientes
        [HttpGet]
        public async Task<IActionResult> GetAllPacientes()
        {
            return Ok(await _consultaPaciente.GetAllPaciente());
        }

        // Endpoint para obtener detalles de un paciente por su ID
        [HttpGet("{idPaciente}")]
        public async Task<IActionResult> GetPacienteDetails(int idPaciente)
        {
            return Ok(await _consultaPaciente.GetPacienteDetails(idPaciente));
        }

        // Endpoint para crear un nuevo paciente
        [HttpPost]
        public async Task<IActionResult> CreatePaciente([FromBody] Paciente paciente)
        {
            if (paciente == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var creado = await _consultaPaciente.InsertPaciente(paciente);
            return Created("creado", creado);
        }

        // Endpoint para actualizar la información de un paciente por su ID
        [HttpPut("{idPaciente}")]
        public async Task<IActionResult> UpdatePaciente(int idPaciente, [FromBody] Paciente paciente)
        {
            if (paciente == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            paciente.IdPaciente = idPaciente; // Asegurarse de que el ID coincida
            await _consultaPaciente.UpdatePaciente(paciente);
            return NoContent();
        }

        // Endpoint para eliminar un paciente por su ID
        [HttpDelete("{idPaciente}")]
        public async Task<IActionResult> DeletePaciente(int idPaciente)
        {
            await _consultaPaciente.DeletePaciente(idPaciente);
            return NoContent();
        }
    }
}