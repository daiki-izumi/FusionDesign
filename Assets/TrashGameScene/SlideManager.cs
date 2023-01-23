using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;


using Debug = UnityEngine.Debug;

public class SlideManager : MonoBehaviour
{

    [SerializeField] GameManager gameManager;



    [SerializeField] Sprite _0;
    [SerializeField] Sprite _1;
    [SerializeField] Sprite _2;
    [SerializeField] Sprite _3;
    [SerializeField] Sprite _4;


    [SerializeField] GameObject _Next;
    [SerializeField] GameObject _Back;


    [SerializeField] GameObject _backgroundObject;
    [SerializeField] GameObject backpanel;
    [SerializeField] GameObject panel;

    [SerializeField] GameObject _START;

   //

    List<GameObject> Slide;

    GameObject Next;

    GameObject Back;
    GameObject slide, background;

    GameObject START;
    float x;
    float move_value = 0.0f;

    bool Next_flag, Back_flag;

    int list_number;


    float fadeSpeed = 0.001f;        //透明度が変わるスピードを管理
    float red_img, green_img, blue_img, alfa_img;   //パネルの色、不透明度を管理

    bool isFadeOut;  //フェードアウト処理の開始、完了を管理するフラグ
    Image fadeImage;

    void Awake()
    {

        Next_flag = false;
        Back_flag = false;
        
        Slide = new List<GameObject>();
      
        Transform parent = _backgroundObject.transform;
        Quaternion q = Quaternion.identity;

        background = Instantiate(backpanel, new Vector3(0.0f, 0.0f, 2.0f), q, parent) as GameObject;
       



        panel.gameObject.transform.localScale = new Vector3(0.8f, 0.8f, 1.0f);

        slide = Instantiate(panel, new Vector3(0.0f, 0.0f, 5.0f), q, parent);
        slide.GetComponent<Image>().sprite = _0;
        Slide.Add(slide);

        slide = Instantiate(panel, new Vector3(20.0f, 0.0f, 7.0f), q, parent);
        slide.GetComponent<Image>().sprite = _1;
        Slide.Add(slide);

        slide = Instantiate(panel, new Vector3(40.0f, 0.0f, 9.0f), q, parent);
        slide.GetComponent<Image>().sprite = _2;
        Slide.Add(slide);

      

        
        slide = Instantiate(panel, new Vector3(60.0f, 0.0f, 11.0f), Quaternion.identity, _backgroundObject.transform);
        slide.GetComponent<Image>().sprite = _3;
        Slide.Add(slide);

       // START.GetComponent<Transform>().localPosition = new Vector3(START.GetComponent<Transform>().localPosition.x , START.GetComponent<Transform>().localPosition.y, START.GetComponent<Transform>().localPosition.z);
      // START.GetComponent<Transform>().localPosition = new Vector3(60.0f, 0.0f, 11.0f);
        /*
       slide = Instantiate(panel, new Vector3(0.0f, 0.0f, 13.0f), Quaternion.identity, _backgroundObject.transform);
        slide.GetComponent<Image>().sprite = _4;
        Slide.Add(slide);
        */
        PutNext();
        PutBack();

        START = Instantiate(_START, new Vector3(0.0f, 0.0f, 0.0f), q, parent);



        if ((background.GetComponent<Image>() != null) && (Back.GetComponent<Image>() != null) && (Next.GetComponent<Image>() != null) && (START.GetComponent<Image>() != null))
        {
            fadeImage = background.GetComponent<Image>();
            alfa_img = fadeImage.color.a;

            isFadeOut = false;
        }


    }

    // Update is called once per frame
    void Update()
    {

        if ((Next != null) && (Back != null) && (START != null))
        {
            Next.GetComponent<Button>().onClick.AddListener(() => OnNext());
            Back.GetComponent<Button>().onClick.AddListener(() => OnBack());
            START.GetComponent<Button>().onClick.AddListener(() => ClickSTART());


            if (Next_flag)
            {
                x = 3.1f;
                set_Nextposition();
                move_value += x;


                if (move_value > 2160.0f)
                {
                    OffNext();
                    move_value = 0.0f;
                }

            }

            if (Back_flag)
            {
                x = 3.1f;
                set_Backposition();
                move_value -= x;



                if (move_value < -2160.0f)
                {
                    OffBack();
                    move_value = 0.0f;
                }


            }
            if (Slide[0].GetComponent<Transform>().localPosition.x > -5.0f)
            {
                Back.GetComponent<Button>().interactable = false;
            }
            else
            {
                Back.GetComponent<Button>().interactable = true;
            }

            if (Slide[0].GetComponent<Transform>().localPosition.x < -(2160.0f * Slide.Count) + 20.0f)
            {
                Next.GetComponent<Button>().interactable = false;
            }
            else
            {
                Next.GetComponent<Button>().interactable = true;
            }

            /////






            /////


            if (background.GetComponent<Image>() != null)
            {
                //Debug.Log(fadeImage);
             //   Debug.Log(isFadeOut);
                if (isFadeOut)
                {
                    StartFadeOut_img();

                }

            }

           




        }//!=null


        if (background.GetComponent<Image>() != null)
        {
            //Debug.Log(fadeImage);
          //  Debug.Log(isFadeOut);
            if (isFadeOut)
            {
                StartFadeOut_img();

            }

        }
    }
      
    

     void set_Nextposition()
    {
        for (int i = 0; i < Slide.Count; i++)
            {
            Slide[i].GetComponent<Transform>().localPosition = new Vector3(Slide[i].GetComponent<Transform>().localPosition.x - x, Slide[i].GetComponent<Transform>().localPosition.y, Slide[i].GetComponent<Transform>().localPosition.z);
            START.GetComponent<Transform>().localPosition = new Vector3(Slide[i].GetComponent<Transform>().localPosition.x - x + 2160.0f, Slide[i].GetComponent<Transform>().localPosition.y, Back.GetComponent<Transform>().localPosition.z);
        }


    }

    void set_Backposition()
    {
        for (int i = 0; i < Slide.Count; i++)

        {
            Slide[i].GetComponent<Transform>().localPosition = new Vector3(Slide[i].GetComponent<Transform>().localPosition.x + x, Slide[i].GetComponent<Transform>().localPosition.y, Slide[i].GetComponent<Transform>().localPosition.z);
            START.GetComponent<Transform>().localPosition = new Vector3(START.GetComponent<Transform>().localPosition.x + x, START.GetComponent<Transform>().localPosition.y, START.GetComponent<Transform>().localPosition.z);
        }
    }


    void OnNext()
    {
        Next_flag = true;


    }

    void OnBack()
    {
        Back_flag = true;
    }

    void OffNext()
    {
        Next_flag = false;
    }

    void OffBack()
    {
        Back_flag = false;
    }

    void PutNext()
    {
        Next = Instantiate(_Next, new Vector3(7.5f, -3.8f, 2.0f), Quaternion.identity, _backgroundObject.transform);
    }

    void PutBack()
    {
        
        Back = Instantiate(_Back, new Vector3(-7.5f, -3.8f, 2.0f), Quaternion.identity, _backgroundObject.transform);
    }

    void ClickSTART()
    {

       
       isFadeOut = true;
        gameManager.play();

    }



    void StartFadeOut_img()
    {
        alfa_img -= fadeSpeed;                //a)不透明度を徐々に下げる
        SetAlpha_img();
       // Debug.Log("ffffff");//b)変更した不透明度パネルに反映する
        if (alfa_img <= 0)
        {                    //c)完全に透明になったら処理を抜ける

            // Debug.Log("ffffff");
              isFadeOut = false;
            Destroy(Back);
            Destroy(Next);
            Destroy(START);
            fadeImage.enabled = false;    //d)パネルの表示をオフにする
        }
    }

    void SetAlpha_img()
    {
        fadeImage.color = new Color(background.GetComponent<Image>().color.r, background.GetComponent<Image>().color.g, background.GetComponent<Image>().color.b, alfa_img);
        Back.GetComponent<Image>().color = new Color(background.GetComponent<Image>().color.r, background.GetComponent<Image>().color.g, background.GetComponent<Image>().color.b, alfa_img);
        Next.GetComponent<Image>().color = new Color(background.GetComponent<Image>().color.r, background.GetComponent<Image>().color.g, background.GetComponent<Image>().color.b, alfa_img);
        START.GetComponent<Image>().color = new Color(background.GetComponent<Image>().color.r, background.GetComponent<Image>().color.g, background.GetComponent<Image>().color.b, alfa_img);


    }











}
