﻿#if UNITY_EDITOR
using UnityEditor;
using Crosstales.BWF.EditorUtil;

namespace Crosstales.BWF.EditorIntegration
{
   /// <summary>Editor component for the "Hierarchy"-menu.</summary>
   public static class BWFGameObject
   {
      [MenuItem("GameObject/" + Util.Constants.ASSET_NAME + "/" + Util.Constants.MANAGER_SCENE_OBJECT_NAME, false, EditorHelper.GO_ID)]
      private static void AddRadioPlayer()
      {
         EditorHelper.InstantiatePrefab(Util.Constants.MANAGER_SCENE_OBJECT_NAME);
      }

      [MenuItem("GameObject/" + Util.Constants.ASSET_NAME + "/" + Util.Constants.MANAGER_SCENE_OBJECT_NAME, true)]
      private static bool AddBWFValidator()
      {
         return !EditorHelper.isBWFInScene;
      }
   }
}
#endif
// © 2017-2021 crosstales LLC (https://www.crosstales.com)