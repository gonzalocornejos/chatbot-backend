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
        public async Task<MensajesDTO> GenerarRespuesta(MensajesDTO mensajes)
        {
            var mensaje = mensajes.Mensajes.Last();

            var respuesta = await _openAIIntegration.GenerarRespuesta(mensajes);
            mensajes.Mensajes.Remove(mensaje);

            mensaje.Output = respuesta;
            mensajes.Mensajes.Add(mensaje);

            return mensajes;
        }
    }
}
