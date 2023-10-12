using chatbot_backend.Core.DataTransferObjects;

namespace chatbot_backend.Core.Services.Interfaces
{
    public interface IChatbotService
    {
        Task<List<InputOutputDTO>> GenerarRespuesta(List<InputOutputDTO> mensajes);
    }
}
