# GPT's Select to Center by X_Bean_X

Source by **X_Bean_X** for the Cities: Skylines II mod **GPT's Select to Center by X_Bean_X**.

[Paradox Mods listing](https://mods.paradoxplaza.com/mods/138166/Windows) â€” version **1.0.3**.

The repository follows the `GPTsSelectToCenter` naming convention. The public name is "GPT's Select to Center by X_Bean_X" and the Options entry is "GPT's Select to Center". The solution and assembly retain their existing names; `FollowZMouse` is the original project/assembly name.

## Features in this source

- Centers the camera on selected objects.
- Enabled toggle, on by default.
- Optional hotkey-armed selection mode, off by default; default binding **Shift + Alt + Z**.
- Optional keep-focus mode, off by default. The original options describe this mode as unsupported with game controllers.
- Key-binding reset.

## Files and build setup

- `Mod.cs`: initialization, settings registration and selection arming.
- `Setting.cs`: settings, key binding and English UI strings.
- `AutoCenterSelectionSystem.cs`: selection events and camera focus behavior.
- `FollowZMouse.csproj` and `FollowZMouse.slnx`: Visual Studio project and solution.
- `source-manifest.json`: source checksums and release fingerprints.

Install Cities: Skylines II and its official Modding Toolchain. The toolchain must configure the user environment variables, including `CSII_TOOLPATH`, and supply the game/Unity references. Open `FollowZMouse.slnx` in a Visual Studio version supporting `.slnx`, or open `FollowZMouse.csproj` directly, then use the Release configuration. The SDK project imports the installed toolchain's `Mod.props` and `Mod.targets`; these supply the .NET Framework 4.8 target, references and processing steps.

The project was copied from the retained published-release project. Its publishing-configuration reference was removed because account-specific publishing profiles and listing artwork are not part of this source repository. Version 1.0.3 changes only the English settings and key-binding display strings; gameplay code and persistent settings identifiers are unchanged. SDK builds may deploy to the toolchain's configured local mod directory; inspect that configuration before building. The 1.0.2 release was rebuilt and officially postprocessed against the 1.6.4f1 toolchain.

## Historical 1.0 release verification

All three C# files match the SHA-256 document checksums in the portable PDB distributed with public version 1.0. The downloaded `FollowZMouse.dll` also exactly matches the retained published-release DLL.

Published DLL SHA-256: `996d0670948bc01423b1637c9039bfe8e94362c92bce2f932f0ff874ec152f67`.

Game assemblies, generated build output, debug symbols, IDE state and account credentials are excluded. Source publication does not change the installed mod or publish a new binary release.

No additional reuse license is granted by this source publication.

## 1.0.2 compatibility update

Built against Cities: Skylines II 1.6.4f1. Assembly, file and release versions now agree. Selection behavior and settings are unchanged. Runtime checks on 1.6.4f1 passed within the scope recorded in `validation-1.6.4f1.json`. The update was submitted to the existing Paradox Mods listing on November 13, 2026.

## 1.0.3 naming correction

Options and key-binding labels now use **GPT's Select to Center**. The full public name is **GPT's Select to Center by X_Bean_X**. Rebuilt and officially postprocessed against 1.6.4f1. This naming-only candidate has not had a new in-game test; the 1.0.2 gameplay validation above is historical. Publication is pending.
