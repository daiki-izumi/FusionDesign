using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

using Debug = UnityEngine.Debug;

public class DestroyObject : MonoBehaviour
{

    float fadeSpeed = 0.001f;        //�����x���ς��X�s�[�h���Ǘ�
    float red_img, green_img, blue_img, alfa_img;   //�p�l���̐F�A�s�����x���Ǘ�
    float red_te, green_te, blue_te, alfa_te;
   

   public  bool isFadeOut;  //�t�F�[�h�A�E�g�����̊J�n�A�������Ǘ�����t���O

    

    Image fadeImage;                //�����x��ύX����p�l���̃C���[�W
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
       

         //�t�F�[�h�A�E�g�����̊J�n�A�������Ǘ�����t��
     
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
        alfa_img -= fadeSpeed;                //a)�s�����x�����X�ɉ�����
        SetAlpha_img();
        Debug.Log("ffffff");//b)�ύX�����s�����x�p�l���ɔ��f����
        if (alfa_img <= 0)
        {                    //c)���S�ɓ����ɂȂ����珈���𔲂���

            Debug.Log("ffffff");
            isFadeOut = false;
           
            fadeImage.enabled = false;    //d)�p�l���̕\�����I�t�ɂ���
        }
    }

    void SetAlpha_img()
    {
        fadeImage.color = new Color(red_img, green_img, blue_img, alfa_img);
    }







}