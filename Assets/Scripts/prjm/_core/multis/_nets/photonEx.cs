using UnityEngine;

namespace Photon.Custom
{
    public static class photonEx /*Ex*/
    {
        public static T Value<T>(this ExitGames.Client.Photon.Hashtable hash, string key) where T : struct
        {
            object value;
            if (hash.TryGetValue(key, out value))
                return (T)value;
            return (T)value;
        }

        public static float Float(this ExitGames.Client.Photon.Hashtable hash, string key, float defaultValue = 0.0f)
        {
            object value;
            if (hash.TryGetValue(key, out value))
                return (float)value;
            return defaultValue;
        }

        public static int Int(this ExitGames.Client.Photon.Hashtable hash, string key, int defaultValue = 0)
        {
            object value;
            if (hash.TryGetValue(key, out value))
                return (int)value;
            return defaultValue;
        }

        public static byte Byte(this ExitGames.Client.Photon.Hashtable hash, string key, byte defaultValue = 0)
        {
            object value;
            if (hash.TryGetValue(key, out value))
                return (byte)value;
            return defaultValue;
        }

        public static bool Bool(this ExitGames.Client.Photon.Hashtable hash, string key, bool defaultValue = false)
        {
            object value;
            if (hash.TryGetValue(key, out value))
                return (bool)value;
            return defaultValue;
        }
    }

}