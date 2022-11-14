using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TextScripts;

public class Story : MonoBehaviour
{
    //スコア
    int score;
    //時間
    float time;
    //UIのGameObject
    public GameObject UIs = null;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(UIs, new Vector3(0.0f, 2.0f, 0.0f), Quaternion.identity);
        //話者
        Text speaker = UIs.transform.GetChild(1).GetComponentInChildren<Text>();
        speaker.text = "Me";
        //セリフ
        Text lines = UIs.transform.GetChild(2).GetComponentInChildren<Text>();
        //lines.text = "hi";
        //他ファイルからの参照
        Scripts scr = new Scripts();
        lines.text = scr.get_line((float)1.0);
        //lines.text = get_line((float)1.0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //セリフを取ってくる関数
    string get_line(float now_pos)
    {
        Debug.Log("called from Story Class");
        return "aiueo";
    }
}
