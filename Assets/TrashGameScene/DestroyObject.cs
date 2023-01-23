using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

using Debug = UnityEngine.Debug;

public class DestroyObject : MonoBehaviour
{

    float fadeSpeed = 0.001f;        //透明度が変わるスピードを管理
    float red_img, green_img, blue_img, alfa_img;   //パネルの色、不透明度を管理
    float red_te, green_te, blue_te, alfa_te;
   

   public  bool isFadeOut;  //フェードアウト処理の開始、完了を管理するフラグ

    

    Image fadeImage;                //透明度を変更するパネルのイメージ
    Text fadeText;
    void Awake()
    {
        if (GetComponent<Image>() != null)
        {
            fadeImage = GetComponent<Image>();
            red_img = fadeImage.color.r;
            green_img = fadeImage.color.g;
            blue_img = fadeImage.color.b;
            alfa_img = fadeImage.color.a;
        }
       

         //フェードアウト処理の開始、完了を管理するフラ
     
    }

    void Update()
    {

     
        if (GetComponent<Image>() != null)
        {
            Debug.Log(fadeImage);
            Debug.Log(alfa_img);
            if (isFadeOut)
            {
                StartFadeOut_img();

            }

        }
    }


    //Image
   

    void StartFadeOut_img()
    {
        alfa_img -= fadeSpeed;                //a)不透明度を徐々に下げる
        SetAlpha_img();
        Debug.Log("ffffff");//b)変更した不透明度パネルに反映する
        if (alfa_img <= 0)
        {                    //c)完全に透明になったら処理を抜ける

            Debug.Log("ffffff");
            isFadeOut = false;
           
            fadeImage.enabled = false;    //d)パネルの表示をオフにする
        }
    }

    void SetAlpha_img()
    {
        fadeImage.color = new Color(red_img, green_img, blue_img, alfa_img);
    }







}