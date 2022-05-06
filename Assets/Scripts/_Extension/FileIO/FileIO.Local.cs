using UnityEngine;
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

public static class XOR
{ 
    public static string Cipher(string data, string key = "rpo-l")
    {
        int dataLen = data.Length;
        int keyLen = key.Length;
        char[] output = new char[dataLen];

        for (int i = 0; i < dataLen; ++i)
        {
            output[i] = (char)(data[i] ^ key[i % keyLen]);
        }

        return new string(output);
    }
}

namespace FileIO
{
    public static class Local
    {
        static string GetFolderPath() { return string.Format("{0}/js", Application.persistentDataPath); }
        static string GetFilePath(string fileName, string extension) { return string.Format("{0}/{1}.{2}", GetFolderPath(), fileName, extension); }

        public static FileInfo[] GetSaveFiles()
        {
            DirectoryInfo dir = new DirectoryInfo(GetFolderPath());
            if (dir.Exists == false)
                return null;
            return dir.GetFiles("*.txt", SearchOption.AllDirectories);
        }

        public static List<string> GetSaveFileNames()
        {
            FileInfo[] files = GetSaveFiles();
            if (null == files)
                return null;

            List<string> lstName = new List<string>();
            for (int i = 0; i < files.Length; ++i)
                lstName.Add(files[i].Name.Substring(0, files[i].Name.LastIndexOf('.')));
            return lstName;
        }

        public static string Read(string fileName, string extension)
        {
            //Debug.Log(GetFilePath(fileName, extension));
            FileInfo file = new FileInfo(GetFilePath(fileName, extension));
            if (!file.Exists)
                return null;

            ///*
            byte[] byteArray = File.ReadAllBytes(GetFilePath(fileName, extension));
            string result = Encoding.UTF8.GetString(byteArray);
            byte[] decodedBytes = Convert.FromBase64String(result);
            result = Encoding.UTF8.GetString(decodedBytes);
            //Debug.Log("[editor]" + result);
            return result;
            //*/

            /*
            Debug.Log("[editor]" + result);
            string xor = result;//XOR.Cipher(result);
            byte[] decodedBytes = Convert.FromBase64String(xor);
            string decodedText = Encoding.UTF8.GetString(decodedBytes);
            return decodedText;
            //*/
            /*
            FileStream fs = new FileStream(GetFilePath(fileName, extension), FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            string parse = br.ReadString();
            br.Close();
            fs.Close();
            //Debug.Log("[editor]"+parse);
            return parse;
            //*/

            ///*
            //         string fileContents;
            ////         using (StreamReader reader = new StreamReader(fs))
            //             fileContents = reader.ReadToEnd();
        }

        public static void Write(string fileName, string context, string extension)
        {
            DirectoryInfo di = new DirectoryInfo(GetFolderPath());
            if (di.Exists == false)
                di.Create();

            //Debug.Log(GetFolderPath());
            string fileurl = GetFilePath(fileName, extension);
            if (File.Exists(fileurl))
            {
                File.Delete(fileurl);
            }

            byte[] info = new UTF8Encoding(true).GetBytes(context);
            string endcode = Convert.ToBase64String(info);
            info = new UTF8Encoding(true).GetBytes(endcode);
            // byte[] info2 = new UTF8Encoding(true).GetBytes(xor);
            FileStream fs = new FileStream(GetFilePath(fileName, extension), FileMode.OpenOrCreate, FileAccess.Write);
   
            fs.Write(info, 0, info.Length);
            /*
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(context);
            bw.Close();
            //*/

            /*
            StreamWriter sw = new StreamWriter(fs);
            sw.Flush();
            sw.WriteLine(context);
            sw.Close();*/
            fs.Close();
        }

        public static void Delete(string fileName)
        {
            FileInfo[] files = GetSaveFiles();
            if (null == files)
                return;

            for (int i = 0; i < files.Length; ++i)
                if (files[i].Name.Equals(string.Format("{0}.txt", fileName)))
                {
                    files[i].Delete();
                    return;
                }
        }




    }

}
