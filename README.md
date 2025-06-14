> [!CAUTION]
> Work in progress - DOES NOT WORK ON YOUR SERVER!

# CounterstrikeSharp - Bot Tools

[![UpdateManager Compatible](https://img.shields.io/badge/CS2-UpdateManager-darkgreen)](https://github.com/Kandru/cs2-update-manager/)
[![Discord Support](https://img.shields.io/discord/289448144335536138?label=Discord%20Support&color=darkgreen)](https://discord.gg/bkuF8xKHUt)
[![GitHub release](https://img.shields.io/github/release/Kandru/cs2-bot-tools?include_prereleases=&sort=semver&color=blue)](https://github.com/Kandru/cs2-bot-tools/releases/)
[![License](https://img.shields.io/badge/License-GPLv3-blue)](#license)
[![issues - cs2-bot-tools](https://img.shields.io/github/issues/Kandru/cs2-bot-tools?color=darkgreen)](https://github.com/Kandru/cs2-bot-tools/issues)
[![](https://www.paypalobjects.com/en_US/i/btn/btn_donateCC_LG.gif)](https://www.paypal.com/donate/?hosted_button_id=C2AVYKGVP9TRG)

This plugin changes aspects of the bots

## Current Features

- Change bot names

## Plugin Installation

1. Download and extract the latest release from the [GitHub releases page](https://github.com/Kandru/cs2-bot-tools/releases/).
2. Move the "BotTools" folder to the `/addons/counterstrikesharp/plugins/` directory of your gameserver.
3. Restart the server.

## Plugin Update

Simply overwrite all plugin files and they will be reloaded automatically or just use the [Update Manager](https://github.com/Kandru/cs2-update-manager/) itself for an easy automatic or manual update by using the *um update BotTools* command.

## Commands

There is currently no client-side command.

## Configuration

This plugin automatically creates a readable JSON configuration file. This configuration file can be found in `/addons/counterstrikesharp/configs/plugins/BotTools/BotTools.json`.

```json
{
  "enabled": true,
  "debug": false,
  "maps": {},
  "ConfigVersion": 1
}
```

You can either disable the complete BotTools Plugin by simply setting the *enable* boolean to *false* or specify a specific map where you want this plugin to be disabled. This allows for a maximum customizability.

## Compile Yourself

Clone the project:

```bash
git clone https://github.com/Kandru/cs2-bot-tools.git
```

Go to the project directory

```bash
  cd cs2-bot-tools
```

Install dependencies

```bash
  dotnet restore
```

Build debug files (to use on a development game server)

```bash
  dotnet build
```

Build release files (to use on a production game server)

```bash
  dotnet publish
```

## License

Released under [GPLv3](/LICENSE) by [@Kandru](https://github.com/Kandru).

## Authors

- [@derkalle4](https://www.github.com/derkalle4)
