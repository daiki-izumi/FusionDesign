using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using TextScripts;
using UnityEngine.Networking;
using SimpleJSON;
using UnityEngine.SceneManagement;
using System;


public class Story : MonoBehaviour
{
    //スコア
    int score;
    //時間
    float time;
    //UIのGameObject
    public GameObject UIs = null;

    private string now_speaker;
    private string [] now_line;

    private Text speaker;
    private Text lines;

    private int count;

    private bool load_flag = false;

    // Start is called before the first frame update
    void Start()
    {        
        Instantiate(UIs, new Vector3(0.0f, 2.0f, 0.0f), Quaternion.identity);
        //話者
        speaker = UIs.transform.GetChild(1).GetComponentInChildren<Text>();
        //セリフ
        lines = UIs.transform.GetChild(2).GetComponentInChildren<Text>();
        //カウント
        count = 0;
        //デバッグ用
        int index_number = 1;
        StartCoroutine(load_line(index_number,(numnum) =>
        {
            Debug.Log($"called [{numnum}]");
            split_line(numnum, out now_line);
            Debug.Log($"Now line is {now_line.Length}");
            load_flag = true;
            Debug.Log($"load is done");
        }));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && load_flag)
        {
            while (count <= now_line.Length -1)
            {
                Debug.Log($"Count is {count}, {now_line.Length - 1}");
                lines.text = now_line[count];
                Debug.Log($"Clicked {now_line[count]}");
                count += 1;
            }
            /*do
            {
                Debug.Log($"Count is {count}, {now_line.Length - 1}");
                //lines.text = now_line[count];
                //Debug.Log($"Clicked {now_line[count]}");
                count += 1; 
            } while (count < now_line.Length - 1);*/

        }
        else if (count == 0 && load_flag)
        {
            Debug.Log($"Not Clicked {now_line[0]}");
            lines.text = now_line[0];
        }
    }
    //*デバッグ用*セリフをGoogle スプレッドシートから取ってくる関数
    public IEnumerator load_line(int index_number, UnityEngine.Events.UnityAction<string> callback)//
    {
        UnityWebRequest request = UnityWebRequest.Get("https://sheets.googleapis.com/v4/spreadsheets/1gM319Kf-031ZysuN3hg7oGGE8K9ZJJRiQO49wvzX6_I/values/sheet1?key=AIzaSyBRDQBQGyZwOaXENVp-xHcf0BfmVO55wC8");
        yield return request.SendWebRequest();
        string numnum ="";
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
                //Debug.Log(itemo);
                //Debug.Log(JSON.Count(itemo[0]));
                //Debug.Log(itemo[0].GetType());
            }
            int length_array = o["values"][0].Count - 1;
            //Debug.Log(o["values"][1][length_array]);

            //speaker.text = (string)o["values"][index_number][1];
            //lines.text = split_line((string)o["values"][index_number][length_array]);
            numnum = o["values"][index_number][length_array];

        }

        callback(numnum);
    }

    //セリフを分割する関数
    public void split_line(string lines, out string [] line_split)
    {
        //切り分ける文字列
        //string lines = "こんにちは";//。こんばんは。ごきげんよう。";
        //文字列内の「。」の個数
        int num = lines.Length - lines.Replace("。", "").Length;
        //出力する文字列
        //string[] line_split;
        //
        int hoge;
        if (num != 0)
        {
            //配列
            line_split = new string[num];
            hoge = 0;
            for (int i = 0; i < num; i++)
            {
                int search_end_num = lines.IndexOf("。") + 1;
                line_split[i] = lines.Substring(0, search_end_num);
                lines = lines.Substring(line_split[i].Length);
            }
        }
        else
        {
            //「。」が文字列にない場合
            line_split = new string[1];
            hoge = 1;
            line_split[0] = lines;

        }
        //デバッグ用
        /*for (int i = 0; i < num + hoge; i++)
        {
            Console.WriteLine($"{line_split[i]}");
        }
        Console.WriteLine($"{line_split.Length}");*/
        //return line_split;
    }
    //シーン移動
    public void MoveScene(double root)
    {
        double root_point1 = Math.Floor(root * 10) / 10;
        int root_path = (int)(root - root_point1);
        //小数第一位によって分岐
        if (root_path == 0)
            SceneManager.LoadScene("MainScene");
    }
}
