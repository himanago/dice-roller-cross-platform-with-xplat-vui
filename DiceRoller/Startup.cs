using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using XPlat.VUI;

[assembly: FunctionsStartup(typeof(DiceRoller.Startup))]
namespace DiceRoller
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddAssistant<ILoggableAssistant, DiceAssistant>();
        }
    }
}