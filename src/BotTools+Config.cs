using System.Text.Json;
using System.Text.Json.Serialization;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Config;

namespace BotTools
{
    public class BotProfileConfig
    {
        [JsonPropertyName("name")] public string Name { get; set; } = "unnamed";
        [JsonPropertyName("health")] public int Health { get; set; } = 100;
        [JsonPropertyName("strip_old_weapons")] public bool StripOldWeapons { get; set; } = true;
        [JsonPropertyName("weapons")] public List<string> Weapons { get; set; } = ["weapon_aug", "weapon_deagle", "weapon_hegrenade", "weapon_smokegrenade"];
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
        // bot profiles
        [JsonPropertyName("bot_profiles")] public Dictionary<string, BotProfileConfig> BotProfiles { get; set; } = new Dictionary<string, BotProfileConfig>();
    }

    public partial class BotTools : BasePlugin, IPluginConfig<PluginConfig>
    {
        public PluginConfig Config { get; set; } = null!;
        private string _configPath = "";

        private void LoadConfig()
        {
            Config = ConfigManager.Load<PluginConfig>("BotTools");
            _configPath = Path.Combine(ModuleDirectory, $"../../configs/plugins/BotTools/BotTools.json");
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
