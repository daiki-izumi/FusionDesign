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
using fileSL;



public class Story : MonoBehaviour
{
    //スコア
    int score;
    //時間
    float time;
    //メインカメラ
    public Camera targetCamera;
    //テキスト
    private string[,] data_all;
    private string now_speaker;
    private string [] now_line;
    //UI
    private Text speaker;
    private Text lines;
    private int count;
    private GameObject bgObject;
    //選択肢のPrefab
    public GameObject choice;
    //ロードが終わったかどうかの判定
    private bool load_flag = false;
    //セリフの表示が終わったかどうかの判定
    private bool show_flag = false;
    //呼び出す番号
    private int index_number = 2;
    //画像のパス
    private string ipath = "Assets/Images/";// character/Chara_male.png"

    //非同期処理、Update内で一度だけ実行
    private bool isCalledOnce = false;

    // Start is called before the first frame update
    void Start()
    {
        //キャンバスの設定
        Canvas canvas = this.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = targetCamera;
        //@@子オブジェクト
        bgObject = this.transform.GetChild(0).gameObject;
        //Image charaimage = bgObject.GetComponent<Transform>().transform.GetChild(0).GetComponentInChildren<Image>();
        //Debug.Log($"Child is {bgObject.name}");
        //@@孫オブジェクト
        //@セリフ背景画像
        GameObject gcObject = bgObject.transform.GetChild(1).gameObject;
        //@キャラクター画像
        GameObject crObject = bgObject.transform.GetChild(0).gameObject;
        //@話者
        GameObject spObject = bgObject.transform.GetChild(2).gameObject;
        speaker = spObject.GetComponent<Text>();
        speaker.text = "Loading...";
        //@セリフ
        GameObject lnObject = bgObject.transform.GetChild(3).gameObject;
        lines = lnObject.GetComponent<Text>();
        lines.text = "Loading...";
        Debug.Log($"GrandChild is {gcObject.name}");

        //キャラクター画像
        Image charaimage = crObject.GetComponent<Image>();
        Texture2D chara_texture = (Texture2D)AssetDatabase.LoadAssetAtPath<Texture>(ipath + "character/Chara_male.png");
        charaimage.sprite = Sprite.Create(chara_texture, new Rect(0, 0, chara_texture.width, chara_texture.height), Vector2.zero);
        Transform charatransform = crObject.GetComponent<Transform>();
        charatransform.localScale = new Vector3((float)0.8, (float)0.8, (float)1.0);

        //背景画像
        Image bgimage = bgObject.GetComponent<Image>();
        Texture2D bg_texture = (Texture2D)AssetDatabase.LoadAssetAtPath<Texture>(ipath + "Backgrounds/scene1.jpg");
        bgimage.sprite = Sprite.Create(bg_texture, new Rect(0, 0, bg_texture.width, bg_texture.height), Vector2.zero);

        //セリフ背景画像
        Image lnimage = gcObject.GetComponent<Image>();
        Texture2D ln_texture = (Texture2D)AssetDatabase.LoadAssetAtPath<Texture>(ipath + "UI/CharaWindow.png");
        lnimage.sprite = Sprite.Create(ln_texture, new Rect(0, 0, ln_texture.width, ln_texture.height), Vector2.zero);
        
        //カウント
        count = 0;
        //int index_number = 2;
        /*StartCoroutine(load_line(index_number, (numnum) =>
        {
            //Debug.Log($"called [{numnum}]");
            split_line(numnum[index_number, 3], out now_line);
            now_speaker = numnum[index_number, 1];
            load_flag = true;
            Debug.Log($"load is done");
        }));*/
        //speaker.text = "Oi";
        StartCoroutine(load_line(OnFinished));
        //セーブロードテスト
        //fileSL.fileSave sv = new fileSL.fileSave();
        //string s = sv.Save("a", "b", "c", "d");
        //Debug.Log($"File path is {s}");
        fileSL.fileSaveLoad ld = new fileSL.fileSaveLoad();
        string[] l = ld.Load();
        Debug.Log($"Load Result is {l}");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log($"Flag is {load_flag}");
        if (load_flag)
        {
            //speaker.text = "Hi";
            Debug.Log($"Load is done");
            /*if (Input.GetMouseButtonDown(0))
            {
                while (count <= now_line.Length - 1)
                {
                    Debug.Log($"Count is {count}, {now_line.Length - 1}");
                    //lines.text = now_line[count];
                    Debug.Log($"Clicked {now_line[count]}");
                    count += 1;
                }*/
                /*do
                {
                    Debug.Log($"Count is {count}, {now_line.Length - 1}");
                    //lines.text = now_line[count];
                    //Debug.Log($"Clicked {now_line[count]}");
                    count += 1; 
                } while (count < now_line.Length - 1);*/

            //}
            /*else if (count == 0)
            {
                Debug.Log($"Not Clicked {now_line[0]}");
                lines.text = now_line[0];
            }*/
        }
        else
        {
            Debug.Log($"Load is not done");
            //speaker.text = "None";
            //lines.text = "None";
        }
    }
    //*デバッグ用*セリフをGoogle スプレッドシートから取ってくる関数
    public IEnumerator load_line(UnityEngine.Events.UnityAction<string[,]> callback)//
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
            int s_count = 0;
            //Debug.Log(request.downloadHandler.text);
            string json = request.downloadHandler.text;
            var o = JSON.Parse(json);
            //Debug.Log(o.GetType());
            //JSONの中身の長さ
            foreach (var item in o["values"])
            {
                s_count += 1;
            }
            Debug.Log(s_count);
            numnum = new string[s_count, 5];
            int length_array = o["values"][0].Count - 1;
            //Debug.Log(o["values"][1][length_array]);
            //セリフ取得
            //numnum[0,0] = o["values"][index_number][2];
            Debug.Log($"Value is {o["values"][index_number]},{o["values"][index_number][0]}");
            for (int i = 0; i < s_count; i++)
            {
                for (int j = 0; j <= 4; j++)
                {
                    numnum[i, j] = o["values"][i][j];
                    //Debug.Log($"SoFuck {i},{j},{o["values"][i][j]}");
                    //Debug.Log($"Fuck {numnum[i,j]}");
                }
            }

        }

        callback(numnum);
    }
    //セリフのロードが完了したら
    public void OnFinished(string[,] numnum)
    {
        //int index_number = 2;
        load_flag = true;
        Debug.Log($"Load is done,{numnum[index_number, 1]}");
        //配列のコピー
        data_all = new string[numnum.GetLength(0), numnum.GetLength(1)];
        for (int i = 0; i < numnum.GetLength(0); i++){
            for (int j = 0; j < numnum.GetLength(1); j++){
                data_all[i, j] = numnum[i, j];
            }
        }
        speaker.text = data_all[index_number, 1];
        lines.text = data_all[index_number, 3];
        //セリフの表示の開始
        if (!show_flag)
        {
            StartCoroutine(show_line(OnFinishedShow));

        }
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
    //セリフを一文字ずつ表示させるコルーチン
    public IEnumerator show_line(UnityEngine.Events.UnityAction<bool> callback)
    {
        //Debug.Log($"{data_all[index_number, 3]}, type is");
        //今見ているテキスト
        string now_string = data_all[index_number, 3];
        //空のテキスト
        string now_buff = "";
        Debug.Log($"now_string is {now_string},{now_string.Length}");
        lines.text = "";
        for (int i = 0; i < now_string.Length; i++)
        {
            //Debug.Log($"i is {i},{now_string[i]}");
            now_buff += now_string[i];
            lines.text = now_buff;
            yield return new WaitForSeconds(0.02f);
        }
        yield return new WaitForSeconds(1.0f);
        callback(true);
        //yield return;
    }
    //セリフの表示が完了したら
    public void OnFinishedShow(bool nu)
    {
        show_flag = true;
        //選択肢
        GameObject choice_fabs = Instantiate(choice, new Vector3(0.0f, 2.0f, 0.0f), Quaternion.identity);
        choice_fabs.transform.SetParent(bgObject.transform, false);
        //ボタン背景画像
        Image btn_img = choice_fabs.GetComponent<Image>();
        Texture2D btn_texture = (Texture2D)AssetDatabase.LoadAssetAtPath<Texture>(ipath + "UI/button2.png");
        btn_img.sprite = Sprite.Create(btn_texture, new Rect(0, 0, btn_texture.width, btn_texture.height), Vector2.zero);
        Transform choicetransform = choice_fabs.GetComponent<Transform>();
        choicetransform.localScale = new Vector3((float)0.8, (float)1.2, (float)1.0);
        //ボタンのOnclick
        Button btn_btn = choice_fabs.GetComponent<Button>();
        btn_btn.onClick.AddListener(BtnClick);
        Debug.Log($"{btn_btn.name},{choice_fabs.name}");
        //choice_fabs.onClick.AddListener(BtnClick);
    }
    public void BtnClick()
    {
        Debug.Log("Pressed");
        MoveScene();
    }
    //シーン移動
    public void MoveScene()
    {
        /*double root_point1 = Math.Floor(root * 10) / 10;
        int root_path = (int)(root - root_point1);
        //小数第一位によって分岐
        if (root_path == 0)
            SceneManager.LoadScene("QuizScene");*/
        SceneManager.LoadScene("QuizScene");
    }
}
