using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;

namespace BotTools
{
    public partial class BotTools : BasePlugin, IPluginConfig<PluginConfig>
    {
        public override string ModuleName => "Bot Tools";
        public override string ModuleAuthor => "Kalle <kalle@kandru.de>";

        private string _currentMap = "";
        Random _random = new Random(Guid.NewGuid().GetHashCode());

        public override void Load(bool hotReload)
        {
            // initialize configuration
            LoadConfig();
            UpdateConfig();
            SaveConfig();
            // register listeners
            RegisterEventHandler<EventPlayerTeam>(OnPlayerTeam);
            // print message if hot reload
            if (hotReload)
            {
                // set current map
                _currentMap = Server.MapName;
                // initialize configuration
                InitializeConfig(_currentMap);
                Console.WriteLine(Localizer["core.hotreload"]);
            }
        }

        public override void Unload(bool hotReload)
        {
            // unregister listeners
            DeregisterEventHandler<EventPlayerTeam>(OnPlayerTeam);
            Console.WriteLine(Localizer["core.unload"]);
        }

        private HookResult OnPlayerTeam(EventPlayerTeam @event, GameEventInfo info)
        {
            DebugPrint("PlayerTeamChange");
            // get player
            CCSPlayerController? bot = @event.Userid;
            if (bot == null
                || !bot.IsValid
                || !bot.IsBot) return HookResult.Continue;
            // change bot name
            ChangeBotName(bot);
            // continue event
            return HookResult.Continue;
        }
    }
}
