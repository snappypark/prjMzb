using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;

public static partial class dUser
{
    public static string Name = "Player0123";

    public static bool NoAds = false;

    public static void Load()
    {
        Name = ObscuredPrefs.GetString("eman", string.Format("Player{0:0000}", Random.Range(0, 9999)));
        NoAds = ObscuredPrefs.GetBool("sdaon", false);
    }

    public static void SaveName(string str)
    {
        Name = str;
        ObscuredPrefs.SetString("eman", Name);
        ObscuredPrefs.Save();
    }

    public static void OnPurchase_NoAds()
    {
        NoAds = true;
        ObscuredPrefs.SetBool("sdaon", true);
        ObscuredPrefs.Save();
    }

    static void reset()
    {
        ObscuredPrefs.SetBool("sdaon", false);
        ObscuredPrefs.Save();

    }
}
