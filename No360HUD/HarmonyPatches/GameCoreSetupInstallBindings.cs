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
		// GameplayCoreSceneSetup
		[HarmonyPatch(typeof(GameplayCoreInstaller))]
		[HarmonyPatch(nameof(GameplayCoreInstaller.InstallBindings))]
		class GameplayCoreSceneSetupInstallBindings
		{
			static bool Prefix(ref GameCoreSceneSetup __instance, ref GameplayCoreSceneSetupData ____sceneSetupData)
			{
				IDifficultyBeatmap difficultyBeatmap = ____sceneSetupData.difficultyBeatmap;
				IReadOnlyList<BeatmapEventData> beatmapEventData = difficultyBeatmap.beatmapData.beatmapEventsData;
				SpawnRotationProcessor rotProcessor = new SpawnRotationProcessor();
				bool is360 = false;

				foreach (BeatmapEventData beatmapEvent in beatmapEventData)
				{
					if (beatmapEvent.type != BeatmapEventType.Event14 && beatmapEvent.type != BeatmapEventType.Event15) 
						continue;

					if (rotProcessor.RotationForEventValue(beatmapEvent.value) != 0)
					{
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
