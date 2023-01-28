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
        //セーブデータをロードして画像ファイル名を返す
        //第1引数がキャラクター画像のファイル名
        public string Save(string chara)
        {
            StreamWriter streamWriter = new StreamWriter(path);
            JSONObject json = new JSONObject();
            //キャラクター
            json.Add("Character", chara);
            Debug.Log($"format is{json.ToString()},{json.ToString().GetType()}");
            streamWriter.Write(json.ToString());
            streamWriter.Close();
            return path;
        }
        //セーブデータをロードして画像ファイル名を返す
        //第1引数がキャラクター画像
        public string Load()
        {
            string output = "";
            if (File.Exists(path))
            {
                StreamReader streamReader = new StreamReader(path, Encoding.UTF8);
                string json = streamReader.ReadToEnd();
                var o = JSON.Parse(json);
                //Debug.Log(o.GetType());
                string dic = "Character";
                int i_count = 0;
                output = o[dic];
                Debug.Log(path);
            }
            else
            {
                output = "Chara_female.png";
            }
            Debug.Log(output);
            return output;
        }
    }
}

