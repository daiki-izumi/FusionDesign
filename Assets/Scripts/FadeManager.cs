using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class FadeManager : MonoBehaviour
{

    float fadeSpeed = 0.001f;        //透明度が変わるスピードを管理
    float red_img, green_img, blue_img, alfa_img;   //パネルの色、不透明度を管理
    float red_te, green_te, blue_te,alfa_te;

    public bool isFadeOut = false;  //フェードアウト処理の開始、完了を管理するフラグ
    public bool isFadeIn = false;   //フェードイン処理の開始、完了を管理するフラグ
    public bool FadeOutflag = false;  
    public bool FadeInflag = false;

    Image fadeImage;                //透明度を変更するパネルのイメージ
    Text fadeText;
    void Start()
    {
        fadeImage = GetComponent<Image>();
        
        red_img = fadeImage.color.r;
        green_img = fadeImage.color.g;
        blue_img = fadeImage.color.b;
        alfa_img = fadeImage.color.a;

        if (GetComponentInChildren<Text>() != null)
        {
            fadeText = GetComponentInChildren<Text>();
            red_te = fadeText.color.r;
            green_te = fadeText.color.g;
            blue_te = fadeText.color.b;
            alfa_te = fadeText.color.a;
        }

        if (FadeInflag)
        {
            alfa_img = 0.0f;
            if (GetComponentInChildren<Text>() != null)
            {
                alfa_te = 0.0f;
            }
        }
        if (FadeOutflag)
        {
            alfa_img = 1.0f;
            if (GetComponentInChildren<Text>() != null)
            {
                alfa_te = 1.0f;
            }
        }
    }

    void Update()
    {
        if (isFadeIn)
        {
            StartFadeIn_img();
            if (GetComponentInChildren<Text>() != null)
            {
                StartFadeIn_te();
            }
        }

        if (isFadeOut)
        {        
            StartFadeOut_img();
            if (GetComponentInChildren<Text>() != null)
            {
                StartFadeOut_te();
            }
        }





    }


    //Image
    void StartFadeIn_img()
    {
        fadeImage.enabled = true;  // a)パネルの表示をオンにする
        alfa_img += fadeSpeed;         // b)不透明度を徐々にあげる
        SetAlpha_img();
        if (alfa_img >= 1)
        {             // d)完全に不透明になったら処理を抜ける
            isFadeIn = false;
            FadeInflag = false;
        }
        
    }

    void StartFadeOut_img()
    {
        alfa_img -= fadeSpeed;                //a)不透明度を徐々に下げる
        SetAlpha_img();                      //b)変更した不透明度パネルに反映する
        if (alfa_img <= 0)
        {                    //c)完全に透明になったら処理を抜ける
            isFadeOut = false;
            FadeOutflag = false;
            fadeImage.enabled = false;    //d)パネルの表示をオフにする
        }
    }

    void SetAlpha_img()
    {
        fadeImage.color = new Color(red_img, green_img, blue_img, alfa_img);
    }



    //Text
    void StartFadeIn_te()
    {
        fadeText.enabled = true;  // a)パネルの表示をオンにする
        alfa_te += fadeSpeed;         // b)不透明度を徐々にあげる
        SetAlpha_te();
       
        if (alfa_img >= 1)
        {             // d)完全に不透明になったら処理を抜ける
            isFadeIn = false;
            FadeInflag = false;
        }

    }

    void StartFadeOut_te()
    {
        alfa_te -= fadeSpeed;                //a)不透明度を徐々に下げる
        SetAlpha_te();                      //b)変更した不透明度パネルに反映する
        if (alfa_te <= 0)
        {                    //c)完全に透明になったら処理を抜ける
            isFadeOut = false;
            FadeOutflag = false;
            fadeText.enabled = false;    //d)パネルの表示をオフにする
            Destroy(this);
        }
    }

    void SetAlpha_te()
    {
        fadeText.color = new Color(red_te, green_te, blue_te, alfa_te);
    }





}