using chatbot_backend.Core.DataTransferObjects;
using chatbot_backend.Core.Integrations.Interfaces;
using chatbot_backend.Core.Services.Interfaces;

namespace chatbot_backend.Core.Services
{
    public class ChatbotService : IChatbotService
    {
        private readonly IOpenAIIntegration _openAIIntegration;

        public ChatbotService(IOpenAIIntegration openAIIntegration)
        {
            _openAIIntegration = openAIIntegration;
        }
        public async Task<List<InputOutputDTO>> GenerarRespuesta(List<InputOutputDTO> mensajes)
        {
            var mensaje = mensajes.Last();

            var respuesta = await _openAIIntegration.GenerarRespuesta(mensajes);
            mensajes.Remove(mensaje);

            mensaje.Output = respuesta;
            mensajes.Add(mensaje);

            return mensajes;
        }
    }
}
