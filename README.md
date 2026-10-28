# Select to Center

Source by **X_Bean_X** for the Cities: Skylines II mod **Select to Center**.

[Paradox Mods listing](https://mods.paradoxplaza.com/mods/138166/Windows) — published version **1.0**.

The repository follows the `GPTsSelectToCenter` naming convention. The published mod, solution and assembly retain their existing names; `FollowZMouse` is the original project/assembly name.

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

The project was copied from the retained published-release project. Its publishing-configuration reference was removed because account-specific publishing profiles and listing artwork are not part of this source repository. The C# files are unchanged. SDK builds may deploy to the toolchain's configured local mod directory; inspect that configuration before building. A fresh build against the current game/toolchain has not been performed as part of this source-publication task.

## Release verification

All three C# files match the SHA-256 document checksums in the portable PDB distributed with public version 1.0. The downloaded `FollowZMouse.dll` also exactly matches the retained published-release DLL.

Published DLL SHA-256: `996d0670948bc01423b1637c9039bfe8e94362c92bce2f932f0ff874ec152f67`.

Game assemblies, generated build output, debug symbols, IDE state and account credentials are excluded. Source publication does not change the installed mod or publish a new binary release.

No additional reuse license is granted by this source publication.
