using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using TextScripts;
using UnityEngine.Networking;
using SimpleJSON;
using UnityEngine.SceneManagement;
using System;
using UnityEditor;


public class Story : MonoBehaviour
{
    //スコア
    int score;
    //時間
    float time;
    //メインカメラ
    public Camera targetCamera;
    //UIのGameObject
    public GameObject UIs = null;
    //テキスト
    private string now_speaker;
    private string [] now_line;
    //UI
    private Canvas canvas;
    private Text speaker;
    private Text lines;
    private Image bgimage;
    private Image charaimage;
    private Image bglines;
    private int count;

    private bool load_flag = false;

    //画像のパス
    private string ipath = "Assets/Images/";// character/Chara_male.png"

    //非同期処理、Update内で一度だけ実行
    private bool isCalledOnce = false;

    // Start is called before the first frame update
    void Start()
    {
        //キャンバスの設定
        canvas = UIs.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = targetCamera;
        //キャラクター画像
        GameObject bgObject = UIs.transform.GetChild(0).gameObject;
        Image charaimage = bgObject.GetComponent<Transform>().transform.GetChild(0).GetComponentInChildren<Image>();
        Texture2D chara_texture = (Texture2D)AssetDatabase.LoadAssetAtPath<Texture>(ipath+"character/Chara_male.png");
        charaimage.sprite = Sprite.Create(chara_texture, new Rect(0, 0, chara_texture.width, chara_texture.height), Vector2.zero);
        //話者
        speaker = bgObject.GetComponent<Transform>().transform.GetChild(0).GetComponentInChildren<Text>();
        //セリフ
        lines = UIs.transform.GetChild(3).GetComponentInChildren<Text>();
        //セリフ背景
        bglines = UIs.transform.GetChild(1).GetComponentInChildren<Image>();
        Texture2D line_texture = (Texture2D)AssetDatabase.LoadAssetAtPath<Texture>(ipath + "UI/messagewindow.png");
        bglines.sprite = Sprite.Create(line_texture, new Rect(0, 0, line_texture.width, line_texture.height), Vector2.zero);
        //背景
        bgimage = UIs.transform.GetChild(4).GetComponentInChildren<Image>();
        //背景テクスチャの読み出し
        //Texture2D texture = Images.Load("Backgrounds/bg1") as Texture2D;
        Texture2D texture = (Texture2D)AssetDatabase.LoadAssetAtPath<Texture>(ipath+"Backgrounds/scene1.jpg");
        bgimage.sprite = Sprite.Create(texture,new Rect(0, 0, texture.width, texture.height),Vector2.zero);
        //カウント
        count = 0;
        int index_number = 2;
        /*StartCoroutine(load_line(index_number, (numnum) =>
        {
            //Debug.Log($"called [{numnum}]");
            split_line(numnum[index_number, 3], out now_line);
            now_speaker = numnum[index_number, 1];
            load_flag = true;
            Debug.Log($"load is done");
        }));*/
        speaker.text = "Oi";
        StartCoroutine(load_line(index_number, OnFinished));
        Instantiate(UIs, new Vector3(0.0f, 2.0f, 0.0f), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        //デバッグ用
        /*if (!isCalledOnce)
        {
            int index_number = 2;
            StartCoroutine(load_line(index_number, (numnum) =>
            {
                //Debug.Log($"called [{numnum}]");
                split_line(numnum[index_number, 3], out now_line);
                now_speaker = numnum[index_number, 1];
                load_flag = true;
                Debug.Log($"load is done");
            }));
        }*/
        Debug.Log($"Flag is {load_flag}");
        if (load_flag)
        {
            speaker.text = "Hi";
            if (Input.GetMouseButtonDown(0))
            {
                while (count <= now_line.Length - 1)
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
            /*else if (count == 0)
            {
                Debug.Log($"Not Clicked {now_line[0]}");
                lines.text = now_line[0];
            }*/
        }
        else
        {
            speaker.text = "None";
            lines.text = "None";
        }
    }
    //*デバッグ用*セリフをGoogle スプレッドシートから取ってくる関数
    public IEnumerator load_line(int index_number, UnityEngine.Events.UnityAction<string[,]> callback)//
    {
        UnityWebRequest request = UnityWebRequest.Get("https://sheets.googleapis.com/v4/spreadsheets/1gM319Kf-031ZysuN3hg7oGGE8K9ZJJRiQO49wvzX6_I/values/sheet1?key=AIzaSyBRDQBQGyZwOaXENVp-xHcf0BfmVO55wC8");
        yield return request.SendWebRequest();
        string[,] numnum= new string[1, 4];
        if (request.isHttpError || request.isNetworkError)
        {
            Debug.Log(request.error);
        }
        else
        {
            int count = 0;
            //Debug.Log(request.downloadHandler.text);
            string json = request.downloadHandler.text;
            var o = JSON.Parse(json);
            //Debug.Log(o.GetType());
            //JSONの中身の長さ
            foreach (var item in o["values"])
            {
                count += 1;
            }
            Debug.Log(count);
            numnum = new string[count,5];
            int length_array = o["values"][0].Count - 1;
            //Debug.Log(o["values"][1][length_array]);
            //セリフ取得
            //numnum[0,0] = o["values"][index_number][2];
            Debug.Log($"Value is {o["values"][index_number]},{o["values"][index_number][0]}");
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j <= 4; j++)
                {
                    numnum[i, j] = o["values"][i][j];
                    //Debug.Log($"SoFuck {i},{j},{o["values"][i][j]}");
                    Debug.Log($"Fuck {numnum[i,j]}");
                }
            }

        }

        callback(numnum);
    }
    //ロードが完了したら
    public void OnFinished(string[,] numnum)
    {
        int num = 1;
        load_flag = true;
        Debug.Log($"Load is done,{numnum[num, 1]}");
        //now_speaker = numnum[num, 1];
        speaker.text = "Hi";
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
