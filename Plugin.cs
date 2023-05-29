using BepInEx;
using HarmonyLib;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

namespace GLCoreScissors
{
	[BepInPlugin("com.coatlessali.glcorescissors", "GLCoreScissors", "1.1.1")]
	public class Plugin : BaseUnityPlugin
	{
		public static Plugin Instance;
		private void Awake()
		{
			// MacOS is special once again
			string funny = (Application.platform == RuntimePlatform.OSXPlayer) ? "../.." : "..";
			// Name of original Prefs file
			string fileName = Path.Combine(Application.dataPath, funny, "Preferences", "Prefs.json");
			// Name of new copy
			string refCopy = Path.Combine(Application.dataPath, funny, "Preferences", "GLCoreScissors.json");

			// Make Copy to avoid shared access error
			try {
				File.Copy(fileName, refCopy, true);
			}
			catch (IOException iox) {
				Logger.LogError(iox.Message);
			}

			// Load Plugin
			Instance = this;
			Logger.LogInfo("Loading Plugin... Restart required after settings toggle.");
			Harmony har = new Harmony("com.coatlessali.glcorescissors");
			har.PatchAll();
		}
	}

	// Patching the GLCoreBandaid function
	[HarmonyPatch(typeof(GLCoreBandaid), nameof(GLCoreBandaid.OnEnable))]
	class GLCoreScissors : MonoBehaviour
	{	
		static void Postfix(ref GameObject ___optionsToHide, ref GameObject ___dialogToShow)
		{
			// Reenable toggles
			___optionsToHide.SetActive(true);
			___dialogToShow.SetActive(false);
		}
	}

	// Patching PostProcessV2_Handler
	[HarmonyPatch(typeof(PostProcessV2_Handler), nameof(PostProcessV2_Handler.Start))]
	class EnableOutlines : MonoSingleton<PostProcessV2_Handler>
	{
		static void Postfix()
		{
			// once again checking for MacOS and handling it accordingly
			string funny = (Application.platform == RuntimePlatform.OSXPlayer) ? "../.." : "..";
			// get the copy of the .json file we made earlier
			string fileName = Path.Combine(Application.dataPath, funny, "Preferences", "GLCoreScissors.json");
			// reading all of the text...? uh, okay moving on
			string options = File.ReadAllText(fileName);

			// why
			bool optionsCheck = options.Contains("\"simplifyEnemies\": true,");

			// check for option and enable, still requires a restart but at least it can be toggled in-game now
			if (optionsCheck == true) {
				Shader.DisableKeyword("NOOUTLINES");
			}
		}
	}
}
