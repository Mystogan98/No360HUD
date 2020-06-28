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
        internal static Plugin instance { get; private set; }
        internal static Harmony HarmonyInstance { get; private set; }
        internal static IPALogger _logger { get; private set; }
        internal static string Name => "No360HUD";

        [Init]
        public void Init(IPALogger logger)
        {
            instance = this;
            _logger = logger;
            Logger.log = logger;
            Logger.log.Debug("Logger initialized.");
        }

        [OnStart]
        public void OnApplicationStart()
        {

            Logger.log.Debug("OnApplicationStart");
            HarmonyInstance = new Harmony("com.rugtveit.no360hud");
            HarmonyInstance.PatchAll(Assembly.GetExecutingAssembly());

        }

        [OnExit]
        public void OnApplicationQuit()
        {
            Logger.log.Debug("OnApplicationQuit");
            HarmonyInstance.UnpatchAll("com.rugtveit.no360hud");
        }
    }
}
