using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using IPA;
using IPA.Config;
using IPA.Config.Stores;
using UnityEngine.SceneManagement;
using UnityEngine;
using HarmonyLib;
using System.Reflection;
using IPALogger = IPA.Logging.Logger;

namespace No360HUD
{

    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin
    {
        internal static Harmony HarmonyInstance { get; private set; }
        internal static string Name => "No360HUD";

        [Init]
        public void Init(IPALogger logger)
        {
            Logger.log = logger;
        }

        [OnStart]
        public void OnApplicationStart()
        {
            HarmonyInstance = new Harmony("com.rugtveit.no360hud");
            HarmonyInstance.PatchAll(Assembly.GetExecutingAssembly());
        }

        [OnExit]
        public void OnApplicationQuit()
        {
            HarmonyInstance.UnpatchAll("com.rugtveit.no360hud");
        }
    }
}
