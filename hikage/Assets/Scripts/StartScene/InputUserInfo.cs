using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using  NovelGame;

public class InputUserInfo : MonoBehaviour
{
    //InputFieldをインスペクター上で設置
    public InputField inputField;

    public Button StatButton;
    //表示するtext
    private Text txt;
    //次のシーンに受け渡すuser情報
    public string user_name;

   

    // Start is called before the first frame update
    void Start()
    {
       
        UserScriptManager user = new UserScriptManager();
        user.GetNext = 1;
        txt = GetComponent<Text>();
       
      
    }

    public void GetText()
    {
        //初期化
   
        txt.text = "";

        txt.text = inputField.text;

        user_name = txt.text;
    }

    public void ClickButton()
    {

      //  NextStory = 1;
        SceneManager.LoadScene("StoryScene");
    }

}