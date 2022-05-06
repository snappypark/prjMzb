using UnityEngine;

namespace Crosstales.BWF.Data
{
   /// <summary>Data definition of a source.</summary>
   [HelpURL("https://www.crosstales.com/media/data/assets/badwordfilter/api/class_crosstales_1_1_b_w_f_1_1_data_1_1_source.html")]
   [CreateAssetMenu(fileName = "New Source", menuName = Util.Constants.ASSET_NAME + "/Source", order = 1000)]
   public class Source : ScriptableObject
   {
      #region Variables

      /// <summary>Name of the source.</summary>
      [Header("Information"), Tooltip("Name of the source.")] public string Name = string.Empty;

      /// <summary>Culture of the source (ISO 639-1).</summary>
      [Tooltip("Culture of the source (ISO 639-1).")] public string Culture = string.Empty;

      /// <summary>Description for the source (optional).</summary>
      [Tooltip("Description for the source (optional).")] public string Description = string.Empty;

      /// <summary>Icon to represent the source (e.g. country flag, optional)</summary>
      [Tooltip("Icon to represent the source (e.g. country flag, optional)")] public Sprite Icon;


      /// <summary>URL of a text file containing all regular expressions for this source. Add also the protocol-type ('http://', 'file://' etc.).</summary>
      [Header("Settings"),
       Tooltip("URL of a text file containing all regular expressions for this source. Add also the protocol-type ('http://', 'file://' etc.).")]
      public string URL = string.Empty;

      /// <summary>Text file containing all regular expressions for this source.</summary>
      [Tooltip("Text file containing all regular expressions for this source.")] public TextAsset Resource;

      public int RegexCount { get; set; }

      #endregion

      /*
      public void OnEnable()
      {
          name = Name;
      }
      */

      #region Overridden methods

      public override string ToString()
      {
         System.Text.StringBuilder result = new System.Text.StringBuilder();

         result.Append(GetType().Name);
         result.Append(Util.Constants.TEXT_TOSTRING_START);

         result.Append("Name='");
         result.Append(Name);
         result.Append(Util.Constants.TEXT_TOSTRING_DELIMITER);

         result.Append("Culture='");
         result.Append(Culture);
         result.Append(Util.Constants.TEXT_TOSTRING_DELIMITER);

         result.Append("Description='");
         result.Append(Description);
         result.Append(Util.Constants.TEXT_TOSTRING_DELIMITER);

         result.Append("Icon='");
         result.Append(Icon);
         result.Append(Util.Constants.TEXT_TOSTRING_DELIMITER_END);

         result.Append(Util.Constants.TEXT_TOSTRING_END);

         return result.ToString();
      }

      public override bool Equals(object obj)
      {
         if (obj == null || GetType() != obj.GetType())
            return false;

         Source o = (Source)obj;

         return Name == o.Name &&
                Culture == o.Culture &&
                Description == o.Description &&
                URL == o.URL &&
                Resource == o.Resource;
      }

      public override int GetHashCode()
      {
         int hash = 0;

         if (Name != null)
            hash += Name.GetHashCode();
         if (Culture != null)
            hash += Culture.GetHashCode();
         if (Description != null)
            hash += Description.GetHashCode();
         if (URL != null)
            hash += URL.GetHashCode();
         if (Resource != null)
            hash += Resource.GetHashCode();

         return hash;
      }

      #endregion
   }
}
// © 2018-2021 crosstales LLC (https://www.crosstales.com)