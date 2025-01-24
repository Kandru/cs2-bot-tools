using System.IO.Enumeration;
using System.Text.Json;
using System.Text.Json.Serialization;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Config;

namespace BotTools
{
    public class MapConfig
    {
        // disabled
        [JsonPropertyName("enabled")] public bool Enabled { get; set; } = true;
        // bot names
        [JsonPropertyName("bot_names")] public List<string> BotNames { get; set; } = [];
        [JsonPropertyName("bot_clantag")] public string BotClantag { get; set; } = "";
    }

    public class PluginConfig : BasePluginConfig
    {
        // disabled
        [JsonPropertyName("enabled")] public bool Enabled { get; set; } = true;
        // debug prints
        [JsonPropertyName("debug")] public bool Debug { get; set; } = false;
        // bot names
        [JsonPropertyName("bot_names")]
        public List<string> BotNames { get; set; } = [
            "Adam Sapfel", "Alex Miamorsch", "Al Dorado", "Ali Gator", "Ali Mente", "Andi Macht", "Andi Tür", "Andi Wand",
            "André Henn", "Andreas Kreuz","Anke Brandt", "Anna Bolicker", "Anna Lüse", "Anne Theke", "Axel Schweiß",
            "Bernhard Diener", "Christian Steiffen", "Christof Smaul", "Claire Grube", "Dennis Platz", "Dieter Pete",
            "Ellen Bogen", "Elli Fand", "Erik Zion", "Ernst Haft", "Frank Furt", "Frank Reich", "Franz Ohse", "Hans Wurst",
            "Harry Gehrsack", "Heinz Fiction", "Hugo Slawien", "Jens Seitz", "John Glöhr", "Kai Sehr", "Karl Auer",
            "Karl Mussmer", "Karl Nickel", "Klara Himmel", "Klaus Thaler", "Klaus Uhr", "Konstantin Opel", "Kristiane Latte",
            "Lee Ceur", "Leo Pard", "Lisa Bonn", "Mario Nette", "Mark Aroni", "Mark Reele", "Martha Pfahl", "Matt Eagle",
            "Matt Ritze", "Mike Rosofft", "Mira Bellenbaum", "Moni Thor", "Nico Tiehn", "Paul Enis", "Paul Lahner", "Peter Silie",
            "Pia Nist", "Rainer Hohn", "Rainer Wein", "Rob Otter", "Roman Ticker", "Sam Stag", "Shawn Steinfeger", "Sepp Tember",
            "Theo Loge", "Tom Bohler", "Tom Mate", "Volker Racho", "Wanda Lismus", "Wilma Bier", "Wilma Gern"
        ];
        [JsonPropertyName("bot_clantag")] public string BotClantag { get; set; } = "";
        // map configurations
        [JsonPropertyName("maps")] public Dictionary<string, MapConfig> MapConfigs { get; set; } = new Dictionary<string, MapConfig>();
    }

    public partial class BotTools : BasePlugin, IPluginConfig<PluginConfig>
    {
        public PluginConfig Config { get; set; } = null!;
        private MapConfig[] _currentMapConfigs = Array.Empty<MapConfig>();
        private string _configPath = "";

        private void LoadConfig()
        {
            Config = ConfigManager.Load<PluginConfig>("BotTools");
            _configPath = Path.Combine(ModuleDirectory, $"../../configs/plugins/BotTools/BotTools.json");
        }

        private void InitializeConfig(string mapName)
        {
            // select map configs whose regexes (keys) match against the map name
            _currentMapConfigs = (from mapConfig in Config.MapConfigs
                                  where FileSystemName.MatchesSimpleExpression(mapConfig.Key, mapName)
                                  select mapConfig.Value).ToArray();

            if (_currentMapConfigs.Length > 0)
            {
                if (Config.MapConfigs.TryGetValue("default", out var config))
                {
                    // add default configuration
                    _currentMapConfigs = new[] { config };
                    Console.WriteLine(Localizer["core.defaultconfig"].Value.Replace("{mapName}", mapName));
                }
                else
                {
                    // there is no config to apply
                    Console.WriteLine(Localizer["core.noconfig"].Value.Replace("{mapName}", mapName));
                }
            }
            else
            {
                Console.WriteLine(Localizer["core.defaultconfig"].Value.Replace("{mapName}", mapName));
                // create default configuration
                Config.MapConfigs.Add(mapName, new MapConfig());
            }
            Console.WriteLine(Localizer["core.foundconfig"].Value.Replace("{count}", _currentMapConfigs.Length.ToString()).Replace("{mapName}", mapName));
        }

        public void OnConfigParsed(PluginConfig config)
        {
            Config = config;
            Console.WriteLine("[BotTools] Initialized map configuration!");
        }

        private void UpdateConfig()
        {
        }

        private void SaveConfig()
        {
            var jsonString = JsonSerializer.Serialize(Config, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_configPath, jsonString);
        }
    }
}
