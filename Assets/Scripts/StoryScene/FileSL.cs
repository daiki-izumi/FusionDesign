using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using System.IO;
using System.Text;

namespace fileSL
{
    
    public class Save
    {
        public Save()
        {
            string path = "./player_data.json";
            StreamWriter streamWriter = new StreamWriter(path);
            JSONObject json = new JSONObject();
            int key = 10;
            json.Add("Key", key);
            streamWriter.Write(json.ToString(), false, Encoding.UTF8);
        }

    }

    public class Load
    {
        public Load()
        {
            string path = "./player_data.json";
            StreamReader streamReader = new StreamReader(path, Encoding.UTF8);
            string json = streamReader.ReadToEnd();
            var o = JSON.Parse(json);
        }
    }
}

