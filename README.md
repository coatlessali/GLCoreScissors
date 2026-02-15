# GLCoreScissors
Reenables enemy silhouettes on OpenGLCore for ULTRAKILL.

## NOTICE

These were disabled for a reason. I personally can't replicate any noticable issues, but still. **Use at your own peril.**

![20230525_21h48m48s_grim](https://github.com/coatlessali/GLCoreScissors/assets/61166135/f2741a9f-1ff4-4888-8bcb-a0a77ee75fdd)

## Installation

Copy/move `GLCoreScissors.dll` to your BepInEx plugins directory.

## Uninstallation

Remove `GLCoreScissors.dll` from your BepInEx plugins directory.

## Usage

Enable the enemy silhouettes checkbox in the Assist options. Moving the slider tanks the framerate, so set it once and then restart the game.

## Building

Note: I don't use Visual Studio, so I have no clue how to compile this on Windows, though using `msbuild` should be possible.

### Dependencies

* A copy of ULTRAKILL with BepInEx 5.4.21 installed.
* A stripped copy of `Assembly-CSharp.dll` from your copy of ULTRAKILL, located in `ULTRAKILL_Data/Managed/Stripped`.
* `msbuild` in your `$PATH`.

### Instructions

1. Clone the repo with git. (`git clone https://github.com/coatlessali/GLCoreScissors.git`)
2. Enter the directory. (`cd GLCoreScissors`)
3. Build project, with `ULTRAKILLPath` set to the path to your copy of ULTRAKILL. (`msbuild GLCoreScissors.csproj -p:ULTRAKILLPath=/path/to/your/ULTRAKILL/`)
4. Copy `bin/Debug/GLCoreScissors.dll` to your BepInEx folder, if `msbuild` doesn't do it for you.
