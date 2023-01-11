using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SlideManager : MonoBehaviour
{


    [SerializeField] Sprite _0;
    [SerializeField] Sprite _1;
    [SerializeField] Sprite _2;
    [SerializeField] Sprite _3;
    [SerializeField] Sprite _4;

    [SerializeField] GameObject _backgroundObject;
    [SerializeField] GameObject panel;

    List<GameObject> Slide;

    // Start is called before the first frame update
    void Start()
    {

       Slide = new List<GameObject>();
        GameObject slide;

        slide = Instantiate(panel, new Vector3(-1.0f, 0.0f, 0.0f), Quaternion.identity, _backgroundObject.transform);
     //   slide.GetComponent<Image>().sprite =  _0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
