using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using TextScripts;
using UnityEngine.Networking;
using SimpleJSON;
using UnityEngine.SceneManagement;
using System;
using UnityEditor;

public class gc : MonoBehaviour
{
    public GameObject UIs = null;
    private string ipath = "Assets/Images/";
    private Image charaimage;
    // Start is called before the first frame update
    void Start()
    {
        //子オブジェクト
        GameObject bgObject = this.transform.GetChild(0).gameObject;
        //Image charaimage = bgObject.GetComponent<Transform>().transform.GetChild(0).GetComponentInChildren<Image>();
        Debug.Log($"Child is {bgObject.name}");
        //孫オブジェクト
        GameObject gcObject = bgObject.transform.GetChild(1).gameObject;
        GameObject crObject = bgObject.transform.GetChild(0).gameObject;
        Debug.Log($"GrandChild is {gcObject.name}");

        //キャラクター画像
        Image charaimage = crObject.GetComponent<Image>();
        Texture2D chara_texture = (Texture2D)AssetDatabase.LoadAssetAtPath<Texture>(ipath + "character/Chara_male.png");
        charaimage.sprite = Sprite.Create(chara_texture, new Rect(0, 0, chara_texture.width, chara_texture.height), Vector2.zero);

        //背景
        Image bgimage = bgObject.GetComponent<Image>();
        Texture2D bg_texture = (Texture2D)AssetDatabase.LoadAssetAtPath<Texture>(ipath + "Backgrounds/scene1.jpg");
        bgimage.sprite = Sprite.Create(bg_texture, new Rect(0, 0, bg_texture.width, bg_texture.height), Vector2.zero);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
