using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Memory;
using CounterStrikeSharp.API.Modules.Memory.DynamicFunctions;
using CounterStrikeSharp.API.Modules.Utils;

namespace BotTools
{
    public partial class BotTools : BasePlugin, IPluginConfig<PluginConfig>
    {
        public override string ModuleName => "Bot Tools";
        public override string ModuleAuthor => "Kalle <kalle@kandru.de>";

        Random _random = new Random(Guid.NewGuid().GetHashCode());

        public override void Load(bool hotReload)
        {
            // initialize configuration
            LoadConfig();
            UpdateConfig();
            SaveConfig();
            // register listeners
            VirtualFunctions.CCSPlayer_ItemServices_CanAcquireFunc.Hook(OnWeaponCanAcquire, HookMode.Pre);
            RegisterEventHandler<EventPlayerSpawn>(OnPlayerSpawn);
            RegisterEventHandler<EventPlayerTeam>(OnPlayerTeam);
            // print message if hot reload
            if (hotReload)
            {
                Console.WriteLine(Localizer["core.hotreload"]);
            }
        }

        public override void Unload(bool hotReload)
        {
            // unregister listeners
            DeregisterEventHandler<EventPlayerSpawn>(OnPlayerSpawn);
            DeregisterEventHandler<EventPlayerTeam>(OnPlayerTeam);
            Console.WriteLine(Localizer["core.unload"]);
        }

        private HookResult OnPlayerSpawn(EventPlayerSpawn @event, GameEventInfo info)
        {
            DebugPrint("OnPlayerSpawn");
            // get player
            CCSPlayerController? bot = @event.Userid;
            if (bot == null
                || !bot.IsValid
                || !bot.IsBot) return HookResult.Continue;
            SetBotLoadout(bot);
            // continue event
            return HookResult.Continue;
        }

        public HookResult OnWeaponCanAcquire(DynamicHook hook)
        {
            if (!Config.Enabled) return HookResult.Continue;
            if (Config.EnableBuyZone) return HookResult.Continue;
            if (Config.BotProfiles.Count == 0) return HookResult.Continue;
            CCSPlayerController bot = hook.GetParam<CCSPlayer_ItemServices>(0).Pawn.Value!.Controller.Value!.As<CCSPlayerController>();
            if (bot == null
                || !bot.IsValid
                || !bot.IsBot) return HookResult.Continue;
            hook.SetReturn(AcquireResult.AlreadyPurchased);
            return HookResult.Continue;
        }

        private HookResult OnPlayerTeam(EventPlayerTeam @event, GameEventInfo info)
        {
            DebugPrint("OnPlayerTeam");
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
