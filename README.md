
# OfflineStaticProtection Plugin for Space Engineers (Torch Server)

![Build Status](https://img.shields.io/badge/build-passing-brightgreen)

Author: mamba  
Current Version: INIT Buildable START point  
Torch Version: 1.3.1.328-master  
Space Engineers Version: 1.208.15

## Overview
This plugin is designed to implement offline protection for players' grids.  
Currently, the plugin is in an **initial buildable state**:

- Code structure is in place.
- Core classes and services are created.
- The plugin compiles without errors.
- Logging and configuration skeletons are ready.

**Status:** 🚧 Initial Buildable Version

---

## What has been implemented so far
1. **Plugin skeleton**
   - 'OfflineStaticProtectionPlugin.cs' implements 'ITorchPlugin' interface (initially empty).
   - Configuration class 'OfflineProtectionConfig' exists with placeholders for settings.

2. **Services skeletons**
   - 'GridLockService.cs' handles locking/unlocking grids (methods are placeholders, minimal code to avoid build errors).
   - 'OfflinePlayerTracker.cs' intended to track players' online/offline status (methods are empty for now).

3. **Utilities**
   - 'ChatUtils.cs' exists to handle chat messages (stubbed, minimal implementation for now).

4. **Build system**
   - 'build.bat' works and produces 'OfflineStaticProtection.dll'.
   - Zip packaging of the plugin is automated.
   - '.gitignore' excludes 'bin/', 'obj/', 'Dependencies/*.dll' and '*.zip'.

---

## Next Steps (To Be Implemented)
1. **Offline Detection**
   - Detect when a player disconnects from the server.
   - Track players’ online/offline status using Torch API.

2. **Grid Locking**
   - Implement real grid lock/unlock logic in 'GridLockService'.
   - Respect configuration options like 'DisableProduction' and 'UnlockDelaySeconds'.
   - Ensure grid state is correctly saved and restored.

3. **Configuration**
   - Add booleans for 'Debug' mode and 'PluginEnabled'.
   - Optionally allow admins to configure behavior per player or per world.

4. **Logging**
   - All important actions should be logged to Torch log.
   - Include debug logs when 'Debug = true'.

5. **Full Integration**
   - Wire 'OfflinePlayerTracker' to call 'GridLockService' on offline events.
   - Ensure all asynchronous operations are safe and thread-protected.

---

## Configuration Example
Example 'OfflineProtectionConfig.json' settings:

```json
{
  "PluginEnabled": true,
  "Debug": false,
  "DisableProduction": true,
  "UnlockDelaySeconds": 30
}
```

---

## File Structure (Current)
```
OfflineStaticProtection/
├── Dependencies/          # External DLLs needed for build
│   └── (NLog.dll, Torch.API.dll, etc.)
├── Plugin/
│   └── OfflineStaticProtectionPlugin.cs
├── Services/
│   ├── GridLockService.cs
│   └── OfflinePlayerTracker.cs
├── Utils/
│   └── ChatUtils.cs
├── build.bat
├── .gitignore
└── README.md
```

---

## Example Usage (Stub)
```csharp
// Example usage after implementation
OfflineStaticProtectionPlugin.Instance.GridLockService.LockPlayerGrids(player);
OfflineStaticProtectionPlugin.Instance.GridLockService.UnlockPlayerGridsWithDelay(player.SteamId);
```

---

## Dependencies
- NLog.dll  
- Torch.API.dll  
- Torch.dll  
- Sandbox.Common.dll  
- Sandbox.Game.dll  
- VRage.Game.dll  
- VRage.Library.dll  
- VRage.Math.dll  
- VRage.dll

> Ensure all DLL versions match your Torch server version.

---

## Troubleshooting
- Make sure your Torch server matches the dependencies version.  
- For build errors regarding missing types (e.g., 'IPlayer'), check that 'Torch.API.dll' is referenced.  
- Always build with .NET Framework 4.6 to avoid compatibility issues.  
- If grids are not locking/unlocking, debug logs will show plugin activity if 'Debug = true'.

---

## How to Build
1. Open terminal in the plugin root directory.
2. Run:

```bash
./build.bat
```

3. After build, the DLL will be in 'bin\Release\OfflineStaticProtection.dll'.  
4. The zip 'OfflineStaticProtection.zip' can be uploaded to your Torch plugins folder.

---

