using UnityEngine;
using System;
using System.IO;
using System.Text;

namespace FileIO
{
    public static class Res
    {
        public static string Pasing(string path)
        {
            //Debug.Log(path);
            TextAsset textAsset = Resources.Load(path) as TextAsset;
            
            MemoryStream s = new MemoryStream(textAsset.bytes);
            BinaryReader br = new BinaryReader(s);
            string parse = br.ReadString();
            br.Close();
            s.Close();
            //Debug.Log(parse);
            return parse;
        }

        public static string PasingWave(int stageIdx, int waveIdx)
        {
            string path 
                = string.Format("waves/{0}/{1}/{2}", NameOfSubFolor(stageIdx), stageIdx, waveIdx);
            //Debug.Log(path);
            TextAsset textAsset = Resources.Load<TextAsset>(path);
            byte[] decodedBytes = Convert.FromBase64String(textAsset.text);
            return Encoding.UTF8.GetString(decodedBytes);

            /*
            using (MemoryStream s = new MemoryStream(textAsset.bytes))
                using (BinaryReader br = new BinaryReader(s ))
                    parse = br.ReadString(); */

            //Debug.Log(s.Length);
            //   byte[] bytes = br.ReadBytes((int)s.Length);
            //   string parse = System.Text.Encoding.Default.GetString(bytes);

            //Debug.Log(parse);
           // return parse;
            /**/
        }

        static string NameOfSubFolor(int idx)
        {
            int d = (int)(idx / 50);
            switch (d)
            {
                case 0: return "0-49";
                case 1: return "50-99";
                case 2: return "100-149";
                case 3: return "150-199";
                case 4: return "200-249";

                case 5: return "250-299";
                case 6: return "300-349";
                case 7: return "350-399";
                case 8: return "400-449";
                case 9: return "450-599";
            }
            return "";
        }
    }

}


