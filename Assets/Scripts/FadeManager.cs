using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class FadeManager : MonoBehaviour
{

    float fadeSpeed = 0.001f;        //�����x���ς��X�s�[�h���Ǘ�
    float red_img, green_img, blue_img, alfa_img;   //�p�l���̐F�A�s�����x���Ǘ�
    float red_te, green_te, blue_te,alfa_te;

    public bool isFadeOut = false;  //�t�F�[�h�A�E�g�����̊J�n�A�������Ǘ�����t���O
    public bool isFadeIn = false;   //�t�F�[�h�C�������̊J�n�A�������Ǘ�����t���O
    public bool FadeOutflag = false;  
    public bool FadeInflag = false;

    Image fadeImage;                //�����x��ύX����p�l���̃C���[�W
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
        fadeImage.enabled = true;  // a)�p�l���̕\�����I���ɂ���
        alfa_img += fadeSpeed;         // b)�s�����x�����X�ɂ�����
        SetAlpha_img();
        if (alfa_img >= 1)
        {             // d)���S�ɕs�����ɂȂ����珈���𔲂���
            isFadeIn = false;
            FadeInflag = false;
        }
        
    }

    void StartFadeOut_img()
    {
        alfa_img -= fadeSpeed;                //a)�s�����x�����X�ɉ�����
        SetAlpha_img();                      //b)�ύX�����s�����x�p�l���ɔ��f����
        if (alfa_img <= 0)
        {                    //c)���S�ɓ����ɂȂ����珈���𔲂���
            isFadeOut = false;
            FadeOutflag = false;
            fadeImage.enabled = false;    //d)�p�l���̕\�����I�t�ɂ���
        }
    }

    void SetAlpha_img()
    {
        fadeImage.color = new Color(red_img, green_img, blue_img, alfa_img);
    }



    //Text
    void StartFadeIn_te()
    {
        fadeText.enabled = true;  // a)�p�l���̕\�����I���ɂ���
        alfa_te += fadeSpeed;         // b)�s�����x�����X�ɂ�����
        SetAlpha_te();
       
        if (alfa_img >= 1)
        {             // d)���S�ɕs�����ɂȂ����珈���𔲂���
            isFadeIn = false;
            FadeInflag = false;
        }

    }

    void StartFadeOut_te()
    {
        alfa_te -= fadeSpeed;                //a)�s�����x�����X�ɉ�����
        SetAlpha_te();                      //b)�ύX�����s�����x�p�l���ɔ��f����
        if (alfa_te <= 0)
        {                    //c)���S�ɓ����ɂȂ����珈���𔲂���
            isFadeOut = false;
            FadeOutflag = false;
            fadeText.enabled = false;    //d)�p�l���̕\�����I�t�ɂ���
            Destroy(this);
        }
    }

    void SetAlpha_te()
    {
        fadeText.color = new Color(red_te, green_te, blue_te, alfa_te);
    }





}