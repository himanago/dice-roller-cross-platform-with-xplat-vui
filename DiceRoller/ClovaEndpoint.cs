using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using XPlat.VUI.Models;

namespace DiceRoller
{
    public class ClovaEndpoint
    {
        private ILoggableAssistant Assistant { get; }

        public ClovaEndpoint(ILoggableAssistant assistant)
        {
            Assistant = assistant;
        }

        [FunctionName(nameof(ClovaEndpoint))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req, ILogger log)
        {
            Assistant.Logger = log;

            var response = await Assistant.RespondAsync(req, Platform.Clova);
            return new OkObjectResult(response.ToClovaResponse());
        }
    }
}
