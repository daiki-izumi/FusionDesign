using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public float speed = 0.01f;  //“§–¾‰»‚Ì‘¬‚³
    float alfa;    //A’l‚ğ‘€ì‚·‚é‚½‚ß‚Ì•Ï”
    float red, green, blue;    //RGB‚ğ‘€ì‚·‚é‚½‚ß‚Ì•Ï”

    // Start is called before the first frame update
    void Start()
    {
        red = GetComponent<Image>().color.r;
        green = GetComponent<Image>().color.g;
        blue = GetComponent<Image>().color.b;

    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Image>().color = new Color(red, green, blue, alfa);
        alfa += speed;
    }
}
