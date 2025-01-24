using CounterStrikeSharp.API.Core;

namespace BotTools
{
    public partial class BotTools : BasePlugin
    {
        private void DebugPrint(string message)
        {
            if (Config.Debug)
            {
                Console.WriteLine(Localizer["core.debugprint"].Value.Replace("{message}", message));
            }
        }
    }
}