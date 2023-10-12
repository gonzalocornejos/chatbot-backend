using chatbot_backend.Core.DataTransferObjects;

namespace chatbot_backend.Core.Integrations.Interfaces
{
    public interface IOpenAIIntegration
    {
        Task<string> GenerarRespuesta(List<InputOutputDTO> mensajes);
    }
}
