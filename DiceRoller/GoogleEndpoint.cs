using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using XPlat.VUI.Models;

namespace DiceRoller
{
    public class GoogleEndpoint
    {
        private ILoggableAssistant Assistant { get; }

        public GoogleEndpoint(ILoggableAssistant assistant)
        {
            Assistant = assistant;
        }

        [FunctionName(nameof(GoogleEndpoint))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req, ILogger log)
        {
            Assistant.Logger = log;
            var response = await Assistant.RespondAsync(req, Platform.GoogleAssistant);
            return new OkObjectResult(response.ToGoogleAssistantResponse());
        }
    }
}
