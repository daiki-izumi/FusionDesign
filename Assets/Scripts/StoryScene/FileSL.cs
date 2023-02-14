using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using System.IO;
using System.Text;

namespace fileSL
{
    
    public class fileSaveLoad
    {
        private string path = Application.persistentDataPath + "/player_data.json";
        public string Save(string index, string chara, string line,string loc)
        {
            StreamWriter streamWriter = new StreamWriter(path);
            JSONObject json = new JSONObject();
            //番号
            json.Add("Number", index);
            //キャラクター
            json.Add("Character", chara);
            //セリフ
            json.Add("Line", line);
            Debug.Log($"format is{json.ToString()},{json.ToString().GetType()}");
            streamWriter.Write(json.ToString());
            streamWriter.Close();
            return path;
        }
        //セーブデータをロードしてstringの配列を返す
        //第1引数が番号,第2引数がキャラクター画像,第3引数がセリフ
        public string[] Load()
        {
            StreamReader streamReader = new StreamReader(path, Encoding.UTF8);
            string json = streamReader.ReadToEnd();
            var o = JSON.Parse(json);
            //Debug.Log(o.GetType());
            string[] dic = new string[]{ "Number", "Character", "Line" };
            int i_count = 0;
            string[] output = new string[dic.Length];
            for (int i = 0; i < dic.Length; i++)
            {
                output[i] = o[dic[i]];
                //Debug.Log(o[dic[i]]);
                //Debug.Log(dic[i]);
            }
            return output;
        }
    }
}

