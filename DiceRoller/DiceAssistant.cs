using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using XPlat.VUI;
using XPlat.VUI.Models;

namespace DiceRoller
{
    public class DiceAssistant : AssistantBase, ILoggableAssistant
    {
        public ILogger Logger { get; set; }

        protected override Task OnLaunchRequestAsync(Dictionary<string, object> session, CancellationToken cancellationToken)
        {
            Logger.LogInformation("LaunchRequest");

            Response
                .Speak("サイコロをいくつ振りますか？")
                .KeepListening("サイコロを振る個数を言ってください。");

            return Task.CompletedTask;
        }

        protected override Task OnIntentRequestAsync(string intent, Dictionary<string, object> slots, Dictionary<string, object> session, CancellationToken cancellationToken)
        {
            Logger.LogInformation("IntentRequest");

            switch (intent)
            {
                case "ThrowDiceIntent":
                    if (slots.TryGetValue("diceCount", out var slot) &&
                        int.TryParse(slot.ToString(), out var count))
                    {
                        Response
                            .Play("https://actions.google.com/sounds/v1/impacts/plastic_object_dropping.ogg", Platform.GoogleAssistant) // plastic object dropping
                            .Play("soundbank://soundlibrary/toys_games/board_games/board_games_10", Platform.Alexa)     // ボードゲーム（10）
                            .Play("https://clova-soundlib.line-scdn.net/clova_dramatic_timpani.mp3", Platform.Clova)    // ティンパニロール
                            .Speak(Dice.ThrowDice(count));
                    }
                    else
                    {
                        Response
                            .Speak("個数がわかりませんでした。もう一度言ってください。")
                            .KeepListening("サイコロを振る個数を言ってください。");
                    }
                    break;

                case "Alexa.CancelIntent":
                case "Clova.CancelIntent":
                    Response.Speak("終了します。");
                    break;

                case "Default Fallback Intent":
                case "Alexa.HelpIntent":
                case "Clova.GuideIntent":
                default:
                    Response
                        .Speak("好きな数のサイコロを振って、合計をしゃべります。いくつ振りますか？")
                        .KeepListening("サイコロを振る個数を言ってください。");
                    break;
            }
            return Task.CompletedTask;
        }
    }
}
