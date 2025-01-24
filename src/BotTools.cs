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
            RegisterEventHandler<EventPlayerConnectFull>(OnPlayerConnectFull);
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
            DeregisterEventHandler<EventPlayerConnectFull>(OnPlayerConnectFull);
            Console.WriteLine(Localizer["core.unload"]);
        }

        private HookResult OnPlayerConnectFull(EventPlayerConnectFull @event, GameEventInfo info)
        {
            DebugPrint("Player connected");
            // get player
            CCSPlayerController? player = @event.Userid;
            if (player == null
                || !player.IsValid) return HookResult.Continue;
            // change bot name
            ChangeBotName(player);
            // continue event
            return HookResult.Continue;
        }
    }
}
