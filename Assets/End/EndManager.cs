using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using fileSL;



public class EndManager : MonoBehaviour
{

    [SerializeField] GameObject _background;
    [SerializeField] GameObject _backgroundObject;
    [SerializeField] GameObject _Button;


    [SerializeField] Sprite _Character1;
    [SerializeField] Sprite _Character2;
    [SerializeField] GameObject _CharacterPrefab;

    GameObject Chara;

    GameObject background;

    float x_1, y_1;
    void Start()
    {

        Transform parent = _backgroundObject.transform;
        Quaternion q = Quaternion.identity;



        background = Instantiate(_background, new Vector3(0.0f, 0.0f, 90.0f), q, parent) as GameObject;

        x_1 = Screen.width / _backgroundObject.GetComponent<RectTransform>().sizeDelta.x;
        y_1 = Screen.height / _backgroundObject.GetComponent<RectTransform>().sizeDelta.y;

       // background.gameObject.transform.localScale = new Vector3(1.0f, 1.0f, x_1);

        Chara = Instantiate(_CharacterPrefab, new Vector3(2.5f, 0.0f, 90.0f), q, parent) as GameObject;
        
        if (UIManager.ld.Load() == "Chara_male.png")
        {
            Chara.GetComponent<Image>().sprite = _Character1;
        }
        else
        {
            Chara.GetComponent<Image>().sprite = _Character2;
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
