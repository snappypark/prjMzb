﻿using System.Linq;
using UnityEngine;

namespace Crosstales.BWF.Provider
{
   /// <summary>Base class for domain providers.</summary>
   public abstract class DomainProvider : BaseProvider
   {
      #region Variables

      protected readonly System.Collections.Generic.List<Model.Domains> domains = new System.Collections.Generic.List<Model.Domains>();

      private const string domainRegexStart = @"\b{0,1}((ht|f)tp(s?)\:\/\/)?[\w\-\.\@]*[\.]";

      //private const string domainRegexEnd = @"(:\d{1,5})?(\/|\b)([\a-zA-Z0-9\-\.\?\!\,\=\'\/\&\%#_]*)?\b";
      private const string domainRegexEnd = @"(:\d{1,5})?(\/|\b)";

      private System.Collections.Generic.Dictionary<string, System.Text.RegularExpressions.Regex> domainsRegex = new System.Collections.Generic.Dictionary<string, System.Text.RegularExpressions.Regex>();
      private System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<System.Text.RegularExpressions.Regex>> debugDomainsRegex = new System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<System.Text.RegularExpressions.Regex>>();

      #endregion


      #region Properties

      /// <summary>RegEx for domains.</summary>
      public System.Collections.Generic.Dictionary<string, System.Text.RegularExpressions.Regex> DomainsRegex
      {
         get => domainsRegex;
         protected set => domainsRegex = value;
      }

      /// <summary>Debug-version of "RegEx for domains".</summary>
      public System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<System.Text.RegularExpressions.Regex>> DebugDomainsRegex
      {
         get => debugDomainsRegex;
         protected set => debugDomainsRegex = value;
      }

      #endregion


      #region MonoBehaviour methods

      private void Start()
      {
         //do nothing, just allow to enable/disable the script
      }

      #endregion


      #region Implemented methods

      public override void Load()
      {
         if (ClearOnLoad)
         {
            domains.Clear();
         }
      }

      protected override void init()
      {
         DomainsRegex.Clear();

         if (Util.Config.DEBUG_DOMAINS)
            Debug.Log("++ DomainProvider started in debug-mode ++", this);

         foreach (Model.Domains domain in domains)
         {
            if (Util.Config.DEBUG_DOMAINS)
            {
               try
               {
                  System.Collections.Generic.List<System.Text.RegularExpressions.Regex> domainRegexes = new System.Collections.Generic.List<System.Text.RegularExpressions.Regex>(domain.DomainList.Count);
                  domainRegexes.AddRange(domain.DomainList.Select(line => new System.Text.RegularExpressions.Regex(domainRegexStart + line + domainRegexEnd, RegexOption1 | RegexOption2 | RegexOption3 | RegexOption4 | RegexOption5)));

                  if (!DebugDomainsRegex.ContainsKey(domain.Source.Name))
                  {
                     DebugDomainsRegex.Add(domain.Source.Name, domainRegexes);
                  }
               }
               catch (System.Exception ex)
               {
                  Debug.LogError($"Could not generate debug regex for source '{domain.Source.Name}': {ex}", this);

                  if (Util.Constants.DEV_DEBUG)
                     Debug.Log(domain.DomainList.CTDump(), this);
               }
            }
            else
            {
               try
               {
                  if (!DomainsRegex.ContainsKey(domain.Source.Name))
                  {
                     DomainsRegex.Add(domain.Source.Name, new System.Text.RegularExpressions.Regex($"{domainRegexStart}({string.Join("|", domain.DomainList.ToArray())}){domainRegexEnd}", RegexOption1 | RegexOption2 | RegexOption3 | RegexOption4 | RegexOption5));
                  }
               }
               catch (System.Exception ex)
               {
                  Debug.LogError($"Could not generate exact regex for source '{domain.Source.Name}': {ex}", this);

                  if (Util.Constants.DEV_DEBUG)
                     Debug.Log(domain.DomainList.CTDump(), this);
               }
            }

            if (Util.Config.DEBUG_DOMAINS)
               Debug.Log($"Domain resource '{domain.Source}' loaded and {domain.DomainList.Count} entries found.", this);
         }

         isReady = true;
         //raiseOnProviderReady();
      }

      #endregion
   }
}
// © 2015-2021 crosstales LLC (https://www.crosstales.com)