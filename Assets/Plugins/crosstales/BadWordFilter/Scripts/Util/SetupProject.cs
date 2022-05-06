﻿using UnityEngine;

namespace Crosstales.BWF.Util
{
   /// <summary>Setup the project to use BWF.</summary>
#if UNITY_EDITOR
   [UnityEditor.InitializeOnLoadAttribute]
#endif
   public class SetupProject
   {
      #region Constructor

      static SetupProject()
      {
         setup();
      }

      #endregion


      #region Public methods

      [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
      private static void setup()
      {
         Crosstales.Common.Util.Singleton<BWFManager>.PrefabPath = "Prefabs/BWF";
         Crosstales.Common.Util.Singleton<Manager.BadWordManager>.PrefabPath = "Prefabs/BadWordManager";
         Crosstales.Common.Util.Singleton<Manager.CapitalizationManager>.PrefabPath = "Prefabs/CapitalizationManager";
         Crosstales.Common.Util.Singleton<Manager.DomainManager>.PrefabPath = "Prefabs/DomainManager";
         Crosstales.Common.Util.Singleton<Manager.PunctuationManager>.PrefabPath = "Prefabs/PunctuationManager";
      }

      #endregion
   }
}
// © 2020-2021 crosstales LLC (https://www.crosstales.com)