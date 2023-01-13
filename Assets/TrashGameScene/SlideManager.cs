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

    [SerializeField] DestroyObject destroyObject;

    List<GameObject> Slide;

    GameObject Next;

    GameObject Back;
    GameObject slide, background;

    GameObject START;
    float x;
    float move_value = 0.0f;

    bool Next_flag, Back_flag;

    int list_number;
    void Awake()
    {

        Next_flag = false;
        Back_flag = false;

        Slide = new List<GameObject>();
      //  GameObject slide, background;
        Transform parent = _backgroundObject.transform;
        Quaternion q = Quaternion.identity;

        background = Instantiate(backpanel, new Vector3(0.0f, 0.0f, 2.0f), q, parent) as GameObject;
       











        panel.gameObject.transform.localScale = new Vector3(0.9f, 0.9f, 1.0f);

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

        }//!=null
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
           // START.GetComponent<Transform>().localPosition = new Vector3(START.GetComponent<Transform>().localPosition.x + x, START.GetComponent<Transform>().localPosition.y, START.GetComponent<Transform>().localPosition.z);
        }
    }


    void OnNext()
    {
        Next_flag = true;
/*
        Destroy(Back);
        Destroy(Next);
        Destroy(_background);
        for (int i = 0; i < Slide.Count; i++)
            Destroy(Slide[i]);
*/

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
        Next = Instantiate(_Next, new Vector3(7.0f, -3.5f, 2.0f), Quaternion.identity, _backgroundObject.transform);
    }

    void PutBack()
    {
        
        Back = Instantiate(_Back, new Vector3(-7.0f, -3.5f, 2.0f), Quaternion.identity, _backgroundObject.transform);
    }

    void ClickSTART()
    {

        Destroy(Back);
        Destroy(Next);
        Destroy(START);
        // Destroy(background);
        destroyObject.isFadeOut = true;
        gameManager.play();

    }













}
