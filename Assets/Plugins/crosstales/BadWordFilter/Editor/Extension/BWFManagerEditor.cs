﻿#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using Crosstales.BWF.EditorUtil;

namespace Crosstales.BWF.EditorExtension
{
   /// <summary>Custom editor for the 'BWFManager'-class.</summary>
   [InitializeOnLoad]
   [CustomEditor(typeof(BWFManager))]
   public class BWFManagerEditor : Editor
   {
      #region Variables

      private BWFManager script;

      private string inputText = "MARTIANS are asses.... => watch mypage.com";
      private string outputText;

      #endregion


      #region Static constructor

      static BWFManagerEditor()
      {
         EditorApplication.hierarchyWindowItemOnGUI += hierarchyItemCB;
      }

      #endregion


      #region Editor methods

      public void OnEnable()
      {
         script = (BWFManager)target;
      }

      public override void OnInspectorGUI()
      {
         DrawDefaultInspector();

         if (script.isActiveAndEnabled)
         {
            GUILayout.Label("Stats", EditorStyles.boldLabel);
            GUILayout.Label($"Ready:\t\t{(script.isReady ? "Yes" : "No")}");

            if (script.isReady)
            {
#if UNITY_2019_1_OR_NEWER
               GUILayout.Label($"Sources:\t{script.Sources().Count}");
#else
               GUILayout.Label($"Sources:\t\t{script.Sources().Count}");
#endif
               GUILayout.Label($"Regex Count:\t{script.TotalRegexCount}");
            }

            EditorHelper.SeparatorUI();

            GUILayout.Label("Test-Drive", EditorStyles.boldLabel);

            if (Util.Helper.isEditorMode)
            {
               inputText = EditorGUILayout.TextField(new GUIContent("Input Text", "Text to check."), inputText);

               EditorHelper.ReadOnlyTextField("Output Text", outputText);

               GUILayout.Space(8);

               GUILayout.BeginHorizontal();
               if (GUILayout.Button(new GUIContent(" Contains", EditorHelper.Icon_Contains, "Contains any bad words?")))
                  outputText = script.Contains(inputText).ToString();

               if (GUILayout.Button(new GUIContent(" Get", EditorHelper.Icon_Get, "Get all bad words.")))
                  outputText = string.Join(", ", script.GetAll(inputText).ToArray());

               if (GUILayout.Button(new GUIContent(" Replace", EditorHelper.Icon_Replace, "Check and replace all bad words.")))
                  outputText = script.ReplaceAll(inputText, Crosstales.BWF.Model.Enum.ManagerMask.All);

               if (GUILayout.Button(new GUIContent(" Mark", EditorHelper.Icon_Mark, "Mark all bad words.")))
                  outputText = script.Mark(inputText);

               GUILayout.EndHorizontal();
            }
            else
            {
               EditorHelper.SeparatorUI();
               EditorGUILayout.HelpBox("Disabled in Play-mode!", MessageType.Info);
            }
         }
         else
         {
            EditorHelper.SeparatorUI();
            EditorGUILayout.HelpBox("Script is disabled!", MessageType.Info);
         }
      }

      #endregion


      #region Private methods

      private static void hierarchyItemCB(int instanceID, Rect selectionRect)
      {
         if (EditorConfig.HIERARCHY_ICON)
         {
            //Color cc = GUI.contentColor;
            //Color bc = GUI.backgroundColor;

            //GUI.backgroundColor = Color.green;
            //GUI.contentColor = Color.yellow;

            GameObject go = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

            if (go != null && go.GetComponent<BWFManager>())
            {
               Rect r = new Rect(selectionRect);
               r.x = r.width - 4;

               GUI.Label(r, EditorHelper.Logo_Asset_Small);
            }

            //GUI.contentColor = cc;
            //GUI.backgroundColor = bc;
         }
      }

      #endregion
   }
}
#endif
// © 2016-2021 crosstales LLC (https://www.crosstales.com)