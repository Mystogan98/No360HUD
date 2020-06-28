using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using IPA.Utilities;
using IPAUtils = IPA.Utilities.ReflectionUtil;
using IPALogger = IPA.Logging.Logger;

namespace No360HUD.HarmonyPatches
{
	class GameCoreSetupInstallBindings
	{



		[HarmonyPatch(typeof(GameplayCoreSceneSetup))]
		[HarmonyPatch("InstallBindings", MethodType.Normal)]
		class GameplayCoreSceneSetupInstallBindings
		{

			static bool Prefix(ref GameplayCoreSceneSetup __instance)
			{
				IPALogger log = Plugin._logger;
				log.Debug("Started patch!");
				bool is360 = false;
				IDifficultyBeatmap difficultyBeatmap = IPAUtils.GetField<GameplayCoreSceneSetupData, GameplayCoreSceneSetup>(__instance, "_sceneSetupData").difficultyBeatmap;
				BeatmapEventData[] beatmapEventData = difficultyBeatmap.beatmapData.beatmapEventData;
				SpawnRotationProcessor rotProcessor = new SpawnRotationProcessor();
				foreach (BeatmapEventData beatmapEvent in beatmapEventData)
				{
					if (!(beatmapEvent.type == BeatmapEventType.Event14 || beatmapEvent.type == BeatmapEventType.Event15)) continue;
					if (rotProcessor.RotationForEventValue(beatmapEvent.value) != 0)
					{
						log.Debug("360 is true!");
						is360 = true;
						break;
					}
				}

				if (!is360) IPAUtils.SetProperty<BeatmapData, int>(difficultyBeatmap.beatmapData, "spawnRotationEventsCount", 0);
				return true;
			}
		}
	}
}
