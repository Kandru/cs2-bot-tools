using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;

namespace BotTools
{
    public partial class BotTools : BasePlugin
    {
        private void ChangeBotName(CCSPlayerController bot)
        {
            DebugPrint("ChangeBotName");
            if (!bot.IsBot) return;
            if (Config.BotNames.Count == 0) return;
            List<string> BotNamesCopy = [.. Config.BotNames];
            // remove already used bot names
            foreach (CCSPlayerController entry in Utilities.GetPlayers())
            {
                if (!entry.IsBot) continue;
                if (BotNamesCopy.Contains(entry.PlayerName))
                {
                    BotNamesCopy.Remove(entry.PlayerName);
                }
            }
            DebugPrint($"{BotNamesCopy.Count} names found");
            if (BotNamesCopy.Count == 0) return;
            // select random bot name
            string name = BotNamesCopy[_random.Next(BotNamesCopy.Count)];
            DebugPrint($"{bot.PlayerName} is now named {name}");
            bot.PlayerName = name;
            // set bot clantag
            bot.ClanName = Config.BotClantag;
            // update accordingly
            Utilities.SetStateChanged(bot, "CBasePlayerController", "m_iszPlayerName");
        }
    }
}