using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using SimpleJSON;

public class CSVReader : MonoBehaviour
{
    const string SHEET_ID = "1Gtyo5BhzTlDQ-BpoynnJ-5kYatWcua6PzoDu5tysla8";
    const string SHEET_NAME = "シート1";

    void Start()
    {
        StartCoroutine(Method(SHEET_NAME));
    }

    IEnumerator Method(string _SHEET_NAME)
    {
        //UnityWebRequest request = UnityWebRequest.Get("https://docs.google.com/spreadsheets/d/" + SHEET_ID + "/gviz/tq?tqx=out:csv&sheet=" + _SHEET_NAME);
        //UnityWebRequest request = UnityWebRequest.Get(https://docs.google.com/spreadsheets/d/1gM319Kf-031ZysuN3hg7oGGE8K9ZJJRiQO49wvzX6_I/gviz/tq?tqx=out:json&tq&gid=0);
        UnityWebRequest request = UnityWebRequest.Get("https://sheets.googleapis.com/v4/spreadsheets/1gM319Kf-031ZysuN3hg7oGGE8K9ZJJRiQO49wvzX6_I/values/sheet1?key=AIzaSyBRDQBQGyZwOaXENVp-xHcf0BfmVO55wC8");
        yield return request.SendWebRequest();

        if (request.isHttpError || request.isNetworkError)
        {
            Debug.Log(request.error);
        }
        else
        {
            Debug.Log(request.downloadHandler.text);
            string json = request.downloadHandler.text;
            var o = JSON.Parse(json);
            foreach(var item in o["values"]){
                var itemo = JSON.Parse(item.ToString());
                Debug.Log(itemo);
            }
            //Debug.Log(o["values"].Length);
            /*List<string[]> characterDataArrayList = ConvertToArrayListFrom(request.downloadHandler.text);
            foreach (string[] characterDataArray in characterDataArrayList)
            {
                Line linedata = new Line(characterDataArray);
                linedata.DebugParametaView();
            }*/
        }
    }

    List<string[]> ConvertToArrayListFrom(string _text)
    {
        List<string[]> characterDataArrayList = new List<string[]>();
        StringReader reader = new StringReader(_text);
        reader.ReadLine();  // 1行目はラベルなので外す
        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();        // 一行ずつ読み込み
            string[] elements = line.Split(',');    // 行のセルは,で区切られる
            for (int i = 0; i < elements.Length; i++)
            {
                if (elements[i] == "\"\"")
                {
                    continue;                       // 空白は除去
                }
                elements[i] = elements[i].TrimStart('"').TrimEnd('"');
            }
            characterDataArrayList.Add(elements);
        }
        return characterDataArrayList;
    }
}