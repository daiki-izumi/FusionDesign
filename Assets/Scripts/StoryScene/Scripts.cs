using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;


namespace TextScripts
{
    public class Scripts : MonoBehaviour
    {
        //現在のテキスト
        public string now_line;
        //現在の話者
        public string now_speaker;
        //セリフを取ってくる関数
        public string get_line(float now_pos)
        {
            Debug.Log("Called from Scripts Class");
            return "aiueo";
        }
        //*デバッグ用*セリフをGoogle スプレッドシートから取ってくる関数
        public IEnumerator load_line()
        {
            UnityWebRequest request = UnityWebRequest.Get("https://sheets.googleapis.com/v4/spreadsheets/1gM319Kf-031ZysuN3hg7oGGE8K9ZJJRiQO49wvzX6_I/values/sheet1?key=AIzaSyBRDQBQGyZwOaXENVp-xHcf0BfmVO55wC8");
            yield return request.SendWebRequest();
            if (request.isHttpError || request.isNetworkError)
            {
                Debug.Log(request.error);
            }
            else
            {
                //Debug.Log(request.downloadHandler.text);
                string json = request.downloadHandler.text;
                var o = JSON.Parse(json);
                foreach (var item in o["values"])
                {
                    var itemo = JSON.Parse(item.ToString());
                    Debug.Log(itemo);
                    //Debug.Log(JSON.Count(itemo[0]));
                    //Debug.Log(itemo[0].GetType());
                }
                int length_array = o["values"][0].Count - 1;
                //Debug.Log(o["values"][1][length_array]);
                this.now_speaker = (string)o["values"][2][1];
                this.now_line = (string)o["values"][2][length_array];
                //Debug.Log(now_line.GetType());

            }
        }
        public string load_line2()
        {
            UnityWebRequest request = UnityWebRequest.Get("https://sheets.googleapis.com/v4/spreadsheets/1gM319Kf-031ZysuN3hg7oGGE8K9ZJJRiQO49wvzX6_I/values/sheet1?key=AIzaSyBRDQBQGyZwOaXENVp-xHcf0BfmVO55wC8");
            if (request.isHttpError || request.isNetworkError)
            {
                Debug.Log(request.error);
                return "error";
            }
            else
            {
                Debug.Log(request.downloadHandler.text);
                string json = request.downloadHandler.text;
                var o = JSON.Parse(json);
                Debug.Log(o["values"].GetType());
                foreach (var item in o["values"])
                {
                    var itemo = JSON.Parse(item.ToString());
                    Debug.Log(itemo);
                    //Debug.Log(JSON.Count(itemo[0]));
                    //Debug.Log(itemo[0].GetType());
                }
                int length_array = o["values"][0].Count - 1;
                //Debug.Log(o["values"][1][length_array]);
                return o["values"][2][length_array];
            }
        }
        //セリフを分割する関数
        public string split_line(string lines)
        {
            //セリフ１行の長さ
            int line_horizontal = 15;
            //セリフ１列の長さ
            int line_vertical = 2;
            int search_num = lines.IndexOf("、") + 1;
            lines = lines.Insert(search_num,"\n");
            Debug.Log("search_num is "+ search_num);
            string split = "aa";//{"a","b" };
            return lines;
        }
    }
}

