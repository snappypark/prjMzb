using UnityEngine;
using System.Linq;
using System.Collections;

namespace Crosstales.BWF.Manager
{
   /// <summary>Manager for for bad words.</summary>
   [DisallowMultipleComponent]
   [HelpURL("https://www.crosstales.com/media/data/assets/badwordfilter/api/class_crosstales_1_1_b_w_f_1_1_manager_1_1_bad_word_manager.html")]
   public class BadWordManager : BaseManager<BadWordManager, Filter.BadWordFilter>
   {
      #region Variables

      [UnityEngine.Serialization.FormerlySerializedAsAttribute("ReplaceChars")] [Header("Specific Settings")] [Tooltip("Replace characters for bad words (default: *)."), SerializeField]
      private string replaceChars = "*"; //e.g. "?#@*&%!$^~+-/<>:;=()[]{}"

      [UnityEngine.Serialization.FormerlySerializedAsAttribute("ReplaceLeetSpeak")] [Tooltip("Replace Leet speak in the input string (default: false)."), SerializeField]
      private bool replaceLeetSpeak;

      [UnityEngine.Serialization.FormerlySerializedAsAttribute("SimpleCheck")] [Tooltip("Use simple detection algorithm. This is the way to check for Chinese, Japanese, Korean and Thai bad words (default: false)."), SerializeField]
      private bool simpleCheck;

      [UnityEngine.Serialization.FormerlySerializedAsAttribute("BadWordProviderLTR")] [Header("Bad Word Providers")] [Tooltip("List of all left-to-right providers."), SerializeField]
      private System.Collections.Generic.List<Provider.BadWordProvider> badWordProviderLTR;

      [UnityEngine.Serialization.FormerlySerializedAsAttribute("BadWordProviderRTL")] [Tooltip("List of all right-to-left providers."), SerializeField]
      private System.Collections.Generic.List<Provider.BadWordProvider> badWordProviderRTL;


      [Header("Events")] public OnContainsCompleted OnContainsCompleted;
      public OnGetAllCompleted OnGetAllCompleted;
      public OnReplaceAllCompleted OnReplaceAllCompleted;

      private static bool loggedFilterIsNull;

#if !UNITY_WSA && !UNITY_WEBGL
      private System.Threading.Thread worker;
#endif

      #endregion


      #region Properties

      /// <summary>Replace characters for bad words.</summary>
      public string ReplaceChars
      {
         get => replaceChars;
         set => filter.ReplaceCharacters = replaceChars = value;
      }

      /// <summary>Replace Leet speak in the input string.</summary>
      public bool ReplaceLeetSpeak
      {
         get => replaceLeetSpeak;
         set => filter.ReplaceLeetSpeak = replaceLeetSpeak = value;
      }

      /// <summary>Use simple detection algorithm. This is the way to check for Chinese, Japanese, Korean and Thai bad words.</summary>
      public bool SimpleCheck
      {
         get => simpleCheck;
         set => filter.SimpleCheck = simpleCheck = value;
      }

      /// <summary>List of all left-to-right providers.</summary>
      public System.Collections.Generic.List<Crosstales.BWF.Provider.BadWordProvider> BadWordProviderLTR
      {
         get => badWordProviderLTR;
         set => badWordProviderLTR = value;
      }

      /// <summary>List of all right-to-left providers.</summary>
      public System.Collections.Generic.List<Crosstales.BWF.Provider.BadWordProvider> BadWordProviderRTL
      {
         get => badWordProviderRTL;
         set => badWordProviderRTL = value;
      }

      /// <summary>Returns all sources for the manager.</summary>
      /// <returns>List with all sources for the manager</returns>
      public System.Collections.Generic.List<Data.Source> Sources => filter?.Sources;

      /// <summary>Total number of Regex.</summary>
      /// <returns>Total number of Regex.</returns>
      public int TotalRegexCount => Sources.Sum(src => src.RegexCount);

      protected override OnContainsCompleted onContainsCompleted => OnContainsCompleted;
      protected override OnGetAllCompleted onGetAllCompleted => OnGetAllCompleted;
      protected override OnReplaceAllCompleted onReplaceAllCompleted => OnReplaceAllCompleted;

      #endregion


      #region MonoBehaviour methods

      protected override void Awake()
      {
         base.Awake();

         if (Instance == this)
            Load();
      }

      protected override void OnApplicationQuit()
      {
#if !UNITY_WSA && !UNITY_WEBGL
         if (worker?.IsAlive == true)
         {
            if (Util.Constants.DEV_DEBUG)
               Debug.Log("Kill worker!", this);

            worker.Abort();
         }
#endif

         base.OnApplicationQuit();
      }

      #endregion


      #region Public methods

      /// <summary>Resets this object.</summary>
      public static void ResetObject()
      {
         DeleteInstance();
         loggedFilterIsNull = false;
      }

      /// <summary>Loads the current filter with all settings from this object.</summary>
      public void Load()
      {
         filter = new Filter.BadWordFilter(badWordProviderLTR, badWordProviderRTL, replaceChars, replaceLeetSpeak, simpleCheck);
      }

      /// <summary>Searches for bad words in a text.</summary>
      /// <param name="text">Text to check</param>
      /// <param name="sourceNames">Relevant sources (e.g. "english", optional)</param>
      /// <returns>True if a match was found</returns>
      public bool Contains(string text, params string[] sourceNames)
      {
         bool result = false;

         if (!string.IsNullOrEmpty(text))
         {
            if (filter != null)
            {
               result = filter.Contains(text, sourceNames);
            }
            else
            {
               logFilterIsNull();
            }
         }

         return result;
      }

      /// <summary>Searches asynchronously for bad words in a text. Use the "OnContainsComplete"-callback to get the result.</summary>
      /// <param name="text">Text to check</param>
      /// <param name="sourceNames">Relevant sources (e.g. "english", optional)</param>
      public void ContainsAsync(string text, params string[] sourceNames)
      {
         StartCoroutine(containsAsync(text, sourceNames));
      }

      /// <summary>Searches for bad words in a text.</summary>
      /// <param name="text">Text to check</param>
      /// <param name="sourceNames">Relevant sources (e.g. "english", optional)</param>
      /// <returns>List with all the matches</returns>
      public System.Collections.Generic.List<string> GetAll(string text, params string[] sourceNames)
      {
         System.Collections.Generic.List<string> result = new System.Collections.Generic.List<string>();

         if (!string.IsNullOrEmpty(text))
         {
            if (filter != null)
            {
               result = filter.GetAll(text, sourceNames);
            }
            else
            {
               logFilterIsNull();
            }
         }

         return result;
      }

      /// <summary>Searches asynchronously for bad words in a text. Use the "OnGetAllComplete"-callback to get the result.</summary>
      /// <param name="text">Text to check</param>
      /// <param name="sourceNames">Relevant sources (e.g. "english", optional)</param>
      public void GetAllAsync(string text, params string[] sourceNames)
      {
         StartCoroutine(getAllAsync(text, sourceNames));
      }

      /// <summary>Searches and replaces all bad words in a text.</summary>
      /// <param name="text">Text to check</param>
      /// <param name="markOnly">Only mark the words (default: false, optional)</param>
      /// <param name="prefix">Prefix for every found bad word (optional)</param>
      /// <param name="postfix">Postfix for every found bad word (optional)</param>
      /// <param name="sourceNames">Relevant sources (e.g. "english", optional)</param>
      /// <returns>Clean text</returns>
      public string ReplaceAll(string text, bool markOnly = false, string prefix = "", string postfix = "", params string[] sourceNames)
      {
         string result = text;

         if (!string.IsNullOrEmpty(text))
         {
            if (filter != null)
            {
               result = filter.ReplaceAll(text, markOnly, prefix, postfix, sourceNames);
            }
            else
            {
               logFilterIsNull();
            }
         }

         return result;
      }

      /// <summary>Searches and replaces asynchronously all bad words in a text. Use the "OnReplaceAllComplete"-callback to get the result.</summary>
      /// <param name="text">Text to check</param>
      /// <param name="markOnly">Only mark the words (default: false, optional)</param>
      /// <param name="prefix">Prefix for every found bad word (optional)</param>
      /// <param name="postfix">Postfix for every found bad word (optional)</param>
      /// <param name="sourceNames">Relevant sources (e.g. "english", optional)</param>
      public void ReplaceAllAsync(string text, bool markOnly = false, string prefix = "", string postfix = "", params string[] sourceNames)
      {
         StartCoroutine(replaceAllAsync(text, markOnly, prefix, postfix, sourceNames));
      }

      /// <summary>Marks the text with a prefix and postfix.</summary>
      /// <param name="text">Text containing bad words</param>
      /// <param name="replace">Replace the bad words (default: false, optional)</param>
      /// <param name="prefix">Prefix for every found bad word (default: bold and red, optional)</param>
      /// <param name="postfix">Postfix for every found bad word (default: bold and red, optional)</param>
      /// <param name="sourceNames">Relevant sources (e.g. "english", optional)</param>
      /// <returns>Text with marked domains</returns>
      public string Mark(string text, bool replace = false, string prefix = "<b><color=red>", string postfix = "</color></b>", params string[] sourceNames)
      {
         string result = text;

         if (!string.IsNullOrEmpty(text))
         {
            if (filter != null)
            {
               result = filter.Mark(text, replace, prefix, postfix, sourceNames);
            }
            else
            {
               logFilterIsNull();
            }
         }

         return result;
      }

      #endregion


      #region Private methods

      private void logFilterIsNull()
      {
         if (!loggedFilterIsNull)
         {
            Debug.LogWarning($"'filter' is null!{System.Environment.NewLine}Did you add the '{GetType().Name}' to the current scene?");
            loggedFilterIsNull = true;
         }
      }

      private IEnumerator containsAsync(string text, params string[] sourceNames)
      {
#if !UNITY_WSA && !UNITY_WEBGL
         if (worker?.IsAlive != true)
         {
            bool result = true;

            worker = new System.Threading.Thread(() => result = Contains(text, sourceNames));
            worker.Start();

            do
            {
               yield return null;
            } while (worker.IsAlive);

            onContainsComplete(text, result);
         }
#else
         Debug.LogWarning("'ContainsAsync' is not supported under the current platform!", this);
         yield return null;
#endif
      }

      private IEnumerator getAllAsync(string text, params string[] sourceNames)
      {
#if !UNITY_WSA && !UNITY_WEBGL
         if (worker?.IsAlive != true)
         {
            System.Collections.Generic.List<string> result = null;

            worker = new System.Threading.Thread(() => result = GetAll(text, sourceNames));
            worker.Start();

            do
            {
               yield return null;
            } while (worker.IsAlive);

            onGetAllComplete(text, result);
         }
#else
         Debug.LogWarning("'GetAllAsync' is not supported under the current platform!", this);
         yield return null;
#endif
      }

      private IEnumerator replaceAllAsync(string text, bool markOnly = false, string prefix = "", string postfix = "", params string[] sourceNames)
      {
#if !UNITY_WSA && !UNITY_WEBGL
         if (worker?.IsAlive != true)
         {
            string result = null;

            worker = new System.Threading.Thread(() => result = ReplaceAll(text, markOnly, prefix, postfix, sourceNames));
            worker.Start();

            do
            {
               yield return null;
            } while (worker.IsAlive);

            onReplaceAllComplete(text, result);
         }
#else
         Debug.LogWarning("'ReplaceAllAsync' is not supported under the current platform!", this);
         yield return null;
#endif
      }

      #endregion
   }
}
// © 2015-2021 crosstales LLC (https://www.crosstales.com)