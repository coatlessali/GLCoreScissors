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
			
			// Load Plugin
			Instance = this;
			Logger.LogInfo("This will force \"isGLCore\" to be false. This may have unintended consequences. You have been warned.");
			Harmony har = new Harmony("com.coatlessali.glcorescissors");
			har.PatchAll();
		}
	}

	// Patching PostProcessV2_Handler
	[HarmonyPatch(typeof(PostProcessV2_Handler), nameof(PostProcessV2_Handler.Start))]
	class Start : MonoSingleton<PostProcessV2_Handler>
	{
		static void Postfix(ref bool ___isGLCore)
		{	
		    ___isGLCore = false;
		}
	}
}

