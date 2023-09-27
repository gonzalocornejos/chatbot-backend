using chatbot_backend.Configuration;
using chatbot_backend.Core.DataTransferObjects;
using chatbot_backend.Core.Integrations.Interfaces;
using Microsoft.Extensions.Options;
using OpenAI_API.Models;
using System.Reflection;

namespace chatbot_backend.Core.Integrations
{
    public class OpenAIIntegration : IOpenAIIntegration
    {
        private readonly OpenAIConfiguration _openAIConfiguration;

        public OpenAIIntegration(IOptionsMonitor<OpenAIConfiguration> optionsMonitor)
        {
            _openAIConfiguration = optionsMonitor.CurrentValue;
        }
        public async Task<string> GenerarRespuesta(MensajesDTO mensajes)
        {
            // api instance
            var api = new OpenAI_API.OpenAIAPI(_openAIConfiguration.Key);
            var chat = api.Chat.CreateConversation();

            chat.Model = Model.ChatGPTTurbo;
            /// give instruction as System
            var system = "Sos un chatbot especializado en turismo y gastronomia de la Ciudad de Buenos Aires";
            chat.AppendSystemMessage(system);

            var pregunta = mensajes.Mensajes.Last();
            mensajes.Mensajes.Remove(pregunta);

            // give a few examples as user and assistant
            foreach (var mensaje in mensajes.Mensajes) {
                chat.AppendUserInput(mensaje.Input);
                chat.AppendExampleChatbotOutput(mensaje.Output);
            }


            // now let's ask it a question'

            chat.AppendUserInput(pregunta.Input);
            // and get the response
            var respuesta = await chat.GetResponseFromChatbotAsync();
            return (respuesta);
        }
    }
}
