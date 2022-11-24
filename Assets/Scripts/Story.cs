using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using TextScripts;
using UnityEngine.Networking;
using SimpleJSON;


public class Story : MonoBehaviour
{
    //スコア
    int score;
    //時間
    float time;
    //UIのGameObject
    public GameObject UIs = null;

    public string now_speaker;
    public string now_line;

    private Text speaker;
    private Text lines;

    // Start is called before the first frame update
    void Start()
    {        
        Instantiate(UIs, new Vector3(0.0f, 2.0f, 0.0f), Quaternion.identity);
        //話者
        speaker = UIs.transform.GetChild(1).GetComponentInChildren<Text>();
        //セリフ
        lines = UIs.transform.GetChild(2).GetComponentInChildren<Text>();
        //デバッグ用
        int index_number = 1;
        StartCoroutine(load_line(index_number,(numnum) =>
        {
            now_line = numnum;
            Debug.Log($"called [{numnum}]");
            lines.text = split_line(now_line);
        }));
        //lines.text = "hi";
        //他ファイルからの参照
        //Scripts scr = new Scripts();
        //lines.text = get_line((float)1.0);
        //string line = "こんにちは、赤ちゃん。";
        //lines.text = scr.split_line(line);
        //Debug.Log(o.GetType());
        //話者
        //speaker.text = scr.load_line();
        //テキスト内容
        //Debug.Log(scr.now_line.GetType());
    }

    // Update is called once per frame
    void Update()
    {
        
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
    public string split_line(string lines)
    {
        //セリフ１行の長さ
        //int line_horizontal = 15;
        //セリフ１列の長さ
        //int line_vertical = 2;
        //セリフの「、」の数
        int search_n_num = lines.IndexOf("、") + 1;
        lines = lines.Insert(search_n_num, "\n");
        //セリフの「。」の数
        int search_end_num = lines.IndexOf("。");

        
        Debug.Log("search_end_num is " + search_end_num);
        return lines;
    }
}
