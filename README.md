[![Actions Status](https://github.com/InfinityGhost/TabletDriver/workflows/Build/badge.svg)](https://github.com/InfinityGhost/TabletDriver/actions)

# Fork for additional Development

This repository is a fork from InfinityGhost, as he deprecated his fork for another OpenSource variant of a tablet driver.

# TabletDriver

This is a low latency graphics tablet driver that is meant to be used with rhythm game [osu!](https://osu.ppy.sh/home)

Currently the driver only works when the TabletDriverGUI is running.

The GUI minimizes to system tray / notification area. You can reopen the GUI by double clicking the system tray icon.

**If you have problems with the driver, please read the FAQ:**

**https://github.com/hawku/TabletDriver/wiki/FAQ**

## Download

### https://github.com/MaxKruse/TabletDriver/releases/download/v0.3.1a/TabletDriver.v0.3.1a.zip

#

### Supported operating systems:
  - Windows 10 64-bit
  - Windows 8.1 64-bit
  - Windows 8 64-bit
  - Windows 7 64-bit (Multiple monitors do not work in absolute mode)

#

### Supported tablets:
  - Wacom CTE-440
  - Wacom CTL-470
  - Wacom CTH-470
  - Wacom CTL-471
  - Wacom CTL-472
  - Wacom CTL-480
  - Wacom CTH-480
  - Wacom CTL-490
  - Wacom CTH-490
  - Wacom CTL-4100 USB
  - Wacom CTL-4100 Bluetooth
  - Wacom CTL-671
  - Wacom CTL-672
  - Wacom CTL-680
  - Wacom CTH-680
  - Wacom CTL-690
  - Wacom CTH-690
  - Wacom PTH-660
  - Wacom PTH-451
  - Wacom PTH-850
  - XP-Pen G430 (New 2017+ "Model B". Old G430 is not supported)
  - XP-Pen G430S
  - XP-Pen G430S_B
  - XP-Pen G540 Pro (G540 is not supported)
  - XP-Pen G640
  - XP-Pen G640S
  - XP-Pen Deco 01
  - XP-Pen Deco 01 v2
  - XP-Pen Deco 02
  - Huion 420
  - Huion H420
  - Huion H430P
  - Huion H610
  - Huion H640P
  - Huion HS64
  - Huion osu!tablet
  - Gaomon S56K
  - Gaomon S620
  - VEIKK S640
  
### Configured, but not properly tested:
> - Huion New 1060 Plus
> - Huion Inspiroy Q11K
  - Other Wacom tablets that might work: https://github.com/hawku/TabletDriver/blob/master/TabletDriverService/config/wacom.cfg

#

## Installation

1. You might need to install these libraries, but usually these are already installed:
   * https://www.microsoft.com/net/download/dotnet-framework-runtime
   * https://aka.ms/vs/15/release/vc_redist.x86.exe
2. Unzip the driver to a folder (Shorter path is recommended, for example `C:\Temp\TabletDriver`)
3. Start the TabletDriverGUI.exe
4. Open the VMulti dropdown menu -> VMulti Installer
5. Install the VMulti driver from the GUI.

## Updating to a new version

1. Unzip the new version.
2. Start the TabletDriverGUI.exe

## Uninstallation

1. Uncheck the "Run at Windows startup" option in the GUI.
2. Uninstall the VMulti driver through `TabletDriver\bin\VMulti Installer GUI.exe` or in the main window.
3. Delete the application files.

#

## Building TabletDriver
The easiest way to build TabletDriver is to use the included powershell script [build.ps1](build.ps1)

### Requirements:
If you want to compile the code and don't want to install anything from the TabletDriver binary package, you will need extract the missing drivers from these installation packages:

**VMulti driver:**
- https://www.xp-pen.com/upload/download/20181019/osuWin(20181019).zip

**Huion WinUSB driver:**
- https://www.huiontablet.com/drivers/WinDriver/HuionTablet_WinDriver_v14.7.60.zip

**7-zip x64 (optional):**
- https://www.7-zip.org/download.html

# Changelog

>**v0.3.1**
>- Fixed Gaomon S620 detection
>- XP-Pen G430S_B configuration by [Vestuvian (on reddit)](reddit.com/u/vestuvian) and [xCuri0](https://github.com/xCuri0)
>- XP-Pen Star 03v2 configuration by [SBAPKat](https://github.com/SBAPKat)

[**View all changelogs**](docs/changelog.md)
