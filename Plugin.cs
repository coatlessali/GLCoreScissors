using BepInEx;
using HarmonyLib;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;


namespace GLCoreScissors
{
	[BepInPlugin("com.coatlessali.glcorescissors", "GLCoreScissors", "1.0.0")]
	public class Plugin : BaseUnityPlugin
	{
		public static Plugin Instance;
		private void Awake()
		{
			Instance = this;
			Logger.LogInfo("This is going to cause memory leaks. You have been warned.");
			Harmony har = new Harmony("com.coatlessali.glcorescissors");
			har.PatchAll();
		}
	}
	
	[HarmonyPatch(typeof(GLCoreBandaid), nameof(GLCoreBandaid.OnEnable))]
	class GLCoreScissors : MonoBehaviour
	{	
		// private string videoName;
		
		static void Postfix(ref GameObject ___optionsToHide, ref GameObject ___dialogToShow)
		{
			___optionsToHide.SetActive(true);
			___dialogToShow.SetActive(false);
		}
	}

	[HarmonyPatch(typeof(PostProcessV2_Handler), nameof(PostProcessV2_Handler.Start))]
	class EnableOutlines : MonoSingleton<PostProcessV2_Handler>
	{
		static void Postfix()
		{
			Shader.DisableKeyword("NOOUTLINES");
		}
	}
}
