using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using SimpleJSON;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using UnityEditor;
using fileSL;



public class Story : MonoBehaviour
{
    //スコア
    int score;
    //時間
    float time;
    //メインカメラ
    //public Camera targetCamera;
    //テキスト
    private string[,] data_all;
    private string now_speaker;
    private string[] now_line;
    //UI
    private Text speaker;
    private Text lines;
    private Transform linestransform;
    private int count;
    private GameObject bgObject;
    //セリフ背景画像
    private GameObject gcObject;
    private Image lnimage;
    private Texture2D ln_texture;
    private Transform lnimagetransform;
    //スタートボタン
    private GameObject stObject;
    private GameObject stbgObject;
    private bool start_flag = false;
    //絵文字
    private GameObject emjObject;
    private Image emjimage;
    private Texture2D emj_texture;
    //選択肢のPrefab
    public GameObject choice;
    //ロードが終わったかどうかの判定
    private bool load_flag = false;
    //セリフの表示が終わったかどうかの判定
    private bool show_flag = false;
    private int nextcount = 0;
    private bool next_flag = false;
    //呼び出す番号
    private int index_number = 1;
    //画像のパス
    private string ipath = "Assets/Images/";// character/Chara_male.png"
    //非同期処理、Update内で一度だけ実行
    private bool isCalledOnce = false;
    //シーンの番号
    private int scene_index = 0;
    private string spath;
    private string[] emj_paths;
    //中断するボタン
    private bool stop_flag = false;

    // Start is called before the first frame update
    void Start()
    {
        //シーンの読み込み
        string nowscene = SceneManager.GetActiveScene().name;
        Debug.Log($"now scene is {nowscene}");
        string c = "Story";
        string Chap = nowscene.Substring(0, 3);
        string n = nowscene.Substring(4, 1);
        spath = Chap + "_" + n + "_" + "Game";
        Debug.Log($"Chap is {Chap}, n is {n}, combine is {spath}");
        scene_index = int.Parse(n);

        string[] bg_paths = new string[6] { "bus stop.jpg", "scene1.jpg", "Park.jpg", "grocery store.jpg", "Shopping.jpg", "Earthquake.jpg" };
        emj_paths = new string[12] { "13.png" , "14.png" , "15.png", "11.png", "12.png", "18.png", "17.png", "19.png", "24.png", "17.png", "22.png", "25.png", };
        //@@子オブジェクト
        bgObject = GameObject.Find("Background_image"); //this.transform.GetChild(0).gameObject;
        //@@孫オブジェクト
        //@キャラクター画像
        GameObject crObject = GameObject.Find("CharaImage"); //bgObject.transform.GetChild(0).gameObject;
        //@話者
        GameObject spObject = GameObject.Find("Speaker"); //bgObject.transform.GetChild(2).gameObject;
        speaker = spObject.GetComponent<Text>();
        speaker.text = " ";
        //@セリフ
        GameObject lnObject = GameObject.Find("Line"); //bgObject.transform.GetChild(3).gameObject;
        lines = lnObject.GetComponent<Text>();
        lines.text = " ";
        linestransform = lnObject.GetComponent<Transform>();
        linestransform.position = new Vector3(0, -0.3f, 0);
        //スタートボタン
        stObject = GameObject.Find("start"); //bgObject.transform.GetChild(3).gameObject;
        stbgObject = GameObject.Find("Backgroud_start");
        //絵文字
        emjObject = GameObject.Find("emoji");
        emjimage = emjObject.GetComponent<Image>();
        emj_texture = (Texture2D)AssetDatabase.LoadAssetAtPath<Texture>(ipath + "Emoji/" + emj_paths[scene_index]);
        emjimage.sprite = Sprite.Create(emj_texture, new Rect(0, 0, emj_texture.width, emj_texture.height), Vector2.zero);
        Transform emjtransform = emjObject.GetComponent<Transform>();
        emjtransform.localScale = new Vector3((float)0.02, (float)0.02, (float)1.0);
        //キャラクター画像
        Image charaimage = crObject.GetComponent<Image>();
        string chara = "";
        fileSL.fileSaveLoad ld = new fileSL.fileSaveLoad();
        chara = ld.Load();
        chara = "Woman/Woman - Park scene 1.png";
        Texture2D chara_texture = (Texture2D)AssetDatabase.LoadAssetAtPath<Texture>(ipath + "character/" + chara);
        charaimage.sprite = Sprite.Create(chara_texture, new Rect(0, 0, chara_texture.width, chara_texture.height), Vector2.zero);
        Transform charatransform = crObject.GetComponent<Transform>();
        charatransform.localScale = new Vector3((float)0.2, (float)0.2, (float)1.0);
        Debug.Log($"chara image {chara_texture}");

        //背景画像
        Image bgimage = bgObject.GetComponent<Image>();
        Texture2D bg_texture = (Texture2D)AssetDatabase.LoadAssetAtPath<Texture>(ipath + "Backgrounds/"+ bg_paths[scene_index]);
        bgimage.sprite = Sprite.Create(bg_texture, new Rect(0, 0, bg_texture.width, bg_texture.height), Vector2.zero);

        //セリフ背景画像のロード
        lnimage = stbgObject.GetComponent<Image>();
        ln_texture = (Texture2D)AssetDatabase.LoadAssetAtPath<Texture>(ipath + "UI/Rectangle89.png");
        ln_texture = (Texture2D)AssetDatabase.LoadAssetAtPath(ipath + "UI/Rectangle89.png", typeof(Texture2D));
        lnimage.sprite = Sprite.Create(ln_texture, new Rect(0, 0, ln_texture.width, ln_texture.height), Vector2.zero);
        //セリフ背景画像の位置配置
        lnimagetransform = stbgObject.GetComponent<Transform>();
        lnimagetransform.position = new Vector3(0, -0.05f, 0);
        lnimagetransform.localScale = new Vector3((float)0.35, (float)0.35, (float)1.0);

        //セリフの読み込み開始
        StartCoroutine(load_line(OnFinished));
    }

    // Update is called once per frame
    void Update()
    {
    }
    //*デバッグ用*セリフをGoogle スプレッドシートから取ってくる関数
    public IEnumerator load_line(UnityEngine.Events.UnityAction<string[,]> callback)//
    {
        UnityWebRequest request = UnityWebRequest.Get("https://sheets.googleapis.com/v4/spreadsheets/19z89ky0ljd-cHRcqTj5LZRsJNq67AO53PV7eFEewZZQ/values/sheet1?key=AIzaSyCuqQfgKMHUOhtICFZ7m4Zq8A88EklPXi4");
        yield return request.SendWebRequest();
        string[,] numnum = new string[1, 4];
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
    //スタートボタンが押されたら
    public IEnumerator startStory()
    {
        yield return new WaitForSeconds(0.5f);
        //Debug.Log("hello");
        //スタート画面の消去
        stObject.SetActive(false);
        //stbgObject.SetActive(false);
        ln_texture = (Texture2D)AssetDatabase.LoadAssetAtPath<Texture>(ipath + "UI/Rectangle 888.png");
        lnimage.sprite = Sprite.Create(ln_texture, new Rect(0, 0, ln_texture.width, ln_texture.height), Vector2.zero);
        lnimagetransform.position = new Vector3(0, -0.8f, 0); ;
        lnimagetransform.localScale = new Vector3((float)0.35, (float)0.35, (float)1.0);
        linestransform.position = new Vector3(0, -0.5f, 0);
        //絵文字の消去
        emjObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        //StartCoroutine(load_line(OnFinished));
    }
    public void OnStart()
    {
        if (load_flag && !next_flag)
        {
            start_flag = true;
            nextcount += 1;
            lines.text = now_line[nextcount];
            //StartCoroutine(show_line(OnFinishedShow));
            Debug.Log($"next count is {nextcount}");
            StartCoroutine(startStory());
            StartCoroutine(show_line(OnFinishedShow));
        }
        else if (load_flag && next_flag)
        {
            MoveScene();
        }
    }
    //ネクストボタンが押されたら
    public void OnNext()
    {
        if (load_flag && start_flag)
        {
            Debug.Log($"length is {now_line.Length}, next is {now_line.Length - 1}");
            nextcount += 1;
            if (nextcount < now_line.Length)
            {
                StartCoroutine(show_line(OnFinishedShow));
                Debug.Log($"next count is {nextcount}, now line is {now_line[nextcount]}");
            }
            //次ボタンが終わったら
            else //if (nextcount == now_line.Length-1)
            {
                next_flag = true;
                //絵文字の変更
                emj_texture = (Texture2D)AssetDatabase.LoadAssetAtPath<Texture>(ipath + "Emoji/" + emj_paths[scene_index+1]);
                emjimage.sprite = Sprite.Create(emj_texture, new Rect(0, 0, emj_texture.width, emj_texture.height), Vector2.zero);
                emjObject.SetActive(true);
                stObject.SetActive(true);
                ln_texture = (Texture2D)AssetDatabase.LoadAssetAtPath<Texture>(ipath + "UI/Rectangle 88.png");
                lnimage.sprite = Sprite.Create(ln_texture, new Rect(0, 0, ln_texture.width, ln_texture.height), Vector2.zero);
                //セリフ背景画像の位置配置
                lnimagetransform.position = new Vector3(0, -0.4f, 0);
                lnimagetransform.localScale = new Vector3((float)0.35, (float)0.35, (float)1.0);
                //セリフの位置配置
                linestransform.position = new Vector3(0, -1.4f, 0);

            }
            
            //Debug.Log($"Next Clicked {nextcount}");
        }
    }
    //バックボタンが押されたら
    public void OnBack()
    {
        if (load_flag)
        {
            Debug.Log($"length is {now_line.Length}, next is {now_line.Length - 1}");
            nextcount -= 1;
            Debug.Log($"next count is {nextcount}");
            if (nextcount > 0 )
            {
                lines.text = now_line[nextcount];
            }
            //次ボタンが終わったら
            else //if (nextcount == now_line.Length-1)
            {
                //シーンに戻る
                next_flag = true;
                //絵文字の変更
                emj_texture = (Texture2D)AssetDatabase.LoadAssetAtPath<Texture>(ipath + "Emoji/14.png");
                emjimage.sprite = Sprite.Create(emj_texture, new Rect(0, 0, emj_texture.width, emj_texture.height), Vector2.zero);
                emjObject.SetActive(true);
                stObject.SetActive(true);
                ln_texture = (Texture2D)AssetDatabase.LoadAssetAtPath<Texture>(ipath + "UI/Rectangle 88.png");
                lnimage.sprite = Sprite.Create(ln_texture, new Rect(0, 0, ln_texture.width, ln_texture.height), Vector2.zero);
                //セリフ背景画像の位置配置
                lnimagetransform.position = new Vector3(0, -0.4f, 0);
                lnimagetransform.localScale = new Vector3((float)0.35, (float)0.35, (float)1.0);
                //セリフの位置配置
                linestransform.position = new Vector3(0, -1.4f, 0);

            }
            Debug.Log($"Next Clicked {nextcount}");
        }
    }
    //中断ボタンが押されたら
    public void OnStop()
    {
        if (!stop_flag)
        {
            stop_flag = true;
        }
        else
        {
            stop_flag = false;
        }
        
    }
    //セリフのロードが完了したら
    public void OnFinished(string[,] numnum)
    {
        //int index_number = 2;
        load_flag = true;
        //Debug.Log($"Load is done,{numnum[index_number, 1]}");
        //配列のコピー
        data_all = new string[numnum.GetLength(0), numnum.GetLength(1)];
        for (int i = 0; i < numnum.GetLength(0); i++)
        {
            for (int j = 0; j < numnum.GetLength(1); j++)
            {
                data_all[i, j] = numnum[i, j];
            }
        }
        //配列のコピー
        string[] buff = split_line2(data_all[scene_index, 3]);
        now_line = new string[buff.Length];
        //改行コード
        string newLine = Environment.NewLine;
        for (int i = 0; i < buff.Length; i++)
        {
            now_line[i] = buff[i].Replace(".", "." + newLine);
            now_line[i] = now_line[i].Replace("?", "?" + newLine);
            now_line[i] = now_line[i].Replace("!", "!" + newLine);
            now_line[i] = now_line[i].Replace(",", "," + newLine);
            //Debug.Log($"now line is {now_line[i]}, buff is {buff[i]}");
        }
        lines.text = now_line[0];
        //Debug.Log($"line buff is {buff}, Length is {buff.Length}, Index is {buff[0]}");
        //セリフの表示の開始
        /*if (!show_flag)
        {
            StartCoroutine(show_line(OnFinishedShow));

        }*/
    }
    //セリフを分割する関数
    public void split_line(string lines, out string[] line_split)
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
    //セリフを分割する関数2
    public string[] split_line2(string lines)
    {
        //切り分ける文字列
        //string lines = "こんにちは";//。こんばんは。ごきげんよう。";
        //文字列内の「。」の個数
        string search = "/";
        int num = lines.Length - lines.Replace(search, "").Length;
        //lines = lines.Replace(search, "");
        //Debug.Log($"lines is {lines}, num is {num}");
        //出力する文字列
        string[] line_splited;
        //
        int hoge;
        if (num != 0)
        {
            //配列
            line_splited = new string[num];
            hoge = 0;
            for (int i = 0; i < num; i++)
            {
                int search_end_num = lines.IndexOf(search) + 1;
                line_splited[i] = lines.Substring(0, search_end_num);
                string bf = lines.Substring(0, line_splited[i].Length - 1);
                line_splited[i] = bf;
                lines = lines.Substring(line_splited[i].Length + 1);
            }
        }
        else
        {
            //「。」が文字列にない場合
            line_splited = new string[1];
            hoge = 1;
            line_splited[0] = lines;

        }
        for (int i = 0; i < line_splited.Length; i++)
        {
            Debug.Log($"Line is {line_splited[i]}");
        }
        return line_splited;
    }
    //セリフを一文字ずつ表示させるコルーチン
    public IEnumerator show_line(UnityEngine.Events.UnityAction<bool> callback)
    {
        //Debug.Log($"{data_all[index_number, 3]}, type is");
        //今見ているテキスト
        string now_string = now_line[nextcount];//data_all[index_number, 3];
        //空のテキスト
        string now_buff = "";
        Debug.Log($"now_string is {now_string},{now_string.Length}");
        lines.text = "";
        for (int i = 0; i < now_string.Length; i++)
        {
            while (stop_flag)
            {
                yield return null;
            }
            //Debug.Log($"i is {i},{now_string[i]}");
            now_buff += now_string[i];
            lines.text = now_buff;
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(1.0f);
        callback(true);
        //yield return;
    }
    //セリフの表示が完了したら
    public void OnFinishedShow(bool nu)
    {
        show_flag = true;
    }
    //シーン移動
    public void MoveScene()
    {
        SceneManager.LoadScene(spath);
    }

}
