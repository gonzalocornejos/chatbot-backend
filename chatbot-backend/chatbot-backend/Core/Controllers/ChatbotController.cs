using chatbot_backend.Core.DataTransferObjects;
using chatbot_backend.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace chatbot_backend.Core.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ChatbotController : ControllerBase
    {
        private readonly IChatbotService _chatbotService;

        public ChatbotController(IChatbotService chatbotService)
        {
            _chatbotService = chatbotService;
        }

        /// <summary>
        ///     Genera una respuesta a partir de una serie de mensajes
        /// </summary>
        /// <returns>
        ///     Lista de mensajes
        /// </returns>
        /// <param name="mensajes">datos del ejercicio a adaptar</param>
        /// <response code="204">Si se genero correctamente el texto y el audio a partir de la imagen</response>
        /// <response code="400">Si los parametros se enviaron incorrectamente</response>
        /// <response code="500">En el caso de haber un problema interno en el codigo</response>
        [HttpPost]
        [Route("generar-respuesta")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GenerarTextoYAudio([FromBody] List<InputOutputDTO> mensajes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Parametros enviados incorrectamente");
            }
            var respuesta = await _chatbotService.GenerarRespuesta(mensajes);
            return Ok(respuesta);
        }
    }
}
