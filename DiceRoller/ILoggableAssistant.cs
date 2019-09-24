using Microsoft.Extensions.Logging;
using XPlat.VUI;

namespace DiceRoller
{
    public interface ILoggableAssistant : IAssistant
    {
        // ロガーを受け取るプロパティ
        ILogger Logger { get; set; }
    }
}