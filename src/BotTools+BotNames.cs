using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;

namespace BotTools
{
    public partial class BotTools : BasePlugin
    {
        private void ChangeBotName(CCSPlayerController player)
        {
            if (!player.IsBot) return;
            if (Config.BotNames.Count == 0) return;
            List<string> BotNamesCopy = [.. Config.BotNames];
            foreach (CCSPlayerController entry in Utilities.GetPlayers())
            {
                if (!entry.IsBot) continue;
                // remove already used bot names
                if (BotNamesCopy.Contains(entry.PlayerName))
                {
                    BotNamesCopy.Remove(entry.PlayerName);
                }
            }
            if (Config.BotNames.Count == 0) return;
            // select random bot name
            player.PlayerName = Config.BotNames[_random.Next(Config.BotNames.Count)];
            // set bot clantag
            player.ClanName = Config.BotClantag;
        }
    }
}