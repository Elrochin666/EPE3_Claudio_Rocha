
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
    public class ReservaController : ControllerBase
    {
        // Campo privado para almacenar la instancia de ConexionReserva
        private readonly ConexionReserva _consultaReserva;

        // Constructor que recibe la instancia de ConexionReserva a través de la inyección de dependencias
        public ReservaController(ConexionReserva consultaReserva)
        {
            _consultaReserva = consultaReserva;
        }

        // Endpoint para obtener todas las reservas
        [HttpGet]
        public async Task<IActionResult> GetAllReservas()
        {
            return Ok(await _consultaReserva.GetAllReserva());
        }

        // Endpoint para obtener detalles de una reserva por su ID
        [HttpGet("{idReserva}")]
        public async Task<IActionResult> GetReservaDetails(int idReserva)
        {
            return Ok(await _consultaReserva.GetReservaDetails(idReserva));
        }

        // Endpoint para crear una nueva reserva
        [HttpPost]
        public async Task<IActionResult> CreateReserva([FromBody] Reserva reserva)
        {
            if (reserva == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var creado = await _consultaReserva.InsertReserva(reserva);
            return Created("creado", creado);
        }

        // Endpoint para actualizar la información de una reserva por su ID
        [HttpPut("{idReserva}")]
        public async Task<IActionResult> UpdateReserva(int idReserva, [FromBody] Reserva reserva)
        {
            if (reserva == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            reserva.IdReserva = idReserva; // Asegurarse de que el ID coincida
            await _consultaReserva.UpdateReserva(reserva);
            return NoContent();
        }

        // Endpoint para eliminar una reserva por su ID
        [HttpDelete("{idReserva}")]
        public async Task<IActionResult> DeleteReserva(int idReserva)
        {
            await _consultaReserva.DeleteReserva(idReserva);
            return NoContent();
        }
    }
}