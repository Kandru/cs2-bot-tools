using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;

namespace BotTools
{
    public partial class BotTools : BasePlugin
    {
        private void SetBotLoadout(CCSPlayerController bot)
        {
            DebugPrint("SetBotLoadout");
            if (!bot.IsBot) return;
            if (Config.BotProfiles.Count == 0) return;
            BotProfileConfig profile = Config.BotProfiles[Config.BotProfiles.Keys.ElementAt(_random.Next(Config.BotProfiles.Count))];
            DebugPrint($"{bot.PlayerName} gets profile {profile.Name}");
            // set loadout
            if (profile.StripOldWeapons) bot.RemoveWeapons();
            foreach (string weapon in profile.Weapons) bot.GiveNamedItem(weapon);
            bot.Health = profile.Health;
            bot.MaxHealth = profile.Health;
        }
    }
}