using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using  NovelGame;

public class InputUserInfo : MonoBehaviour
{
    //InputField���C���X�y�N�^�[��Őݒu
    public InputField inputField;

    public Button StatButton;
    //�\������text
    private Text txt;
    //���̃V�[���Ɏ󂯓n��user���
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
        //������
   
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