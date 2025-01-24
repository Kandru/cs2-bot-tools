using CounterStrikeSharp.API.Core;

namespace BotTools
{
    public partial class BotTools : BasePlugin
    {
        private void SetBotLoadout(CCSPlayerController bot)
        {
            DebugPrint("SetBotLoadout");
            if (!bot.IsBot
                || bot.PlayerPawn == null
                || !bot.PlayerPawn.IsValid
                || bot.PlayerPawn.Value == null) return;
            if (Config.BotProfiles.Count == 0) return;
            string profileKey = Config.BotProfiles.Keys.ElementAt(_random.Next(Config.BotProfiles.Count));
            BotProfileConfig profile = Config.BotProfiles[profileKey];
            DebugPrint($"{bot.PlayerName} gets profile {profileKey}");
            // set loadout
            if (profile.StripOldWeapons) bot.RemoveWeapons();
            foreach (string weapon in profile.Weapons) bot.GiveNamedItem(weapon);
            bot.Health = profile.Health;
            bot.MaxHealth = profile.Health;
        }
    }
}