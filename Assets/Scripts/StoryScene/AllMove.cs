using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllMove : MonoBehaviour
{
    private bool buttonRDownflag = false;
    private bool buttonLDownflag = false;
    //Light spped
    private float speed = 0.0f;
    private Transform bg;
    private Transform bg1;
    private Text tx;

    // Start is called before the first frame update
    void Start()
    {
        //Light
        GameObject light = GameObject.Find("L");
        Debug.Log(light.name);
        //Right
        GameObject right = GameObject.Find("R");
        //background
        bg = GameObject.Find("bg").GetComponent<Transform>();
        bg1 = GameObject.Find("bg1").GetComponent<Transform>();
        //Text
        tx = GameObject.Find("Speed").GetComponent<Text>();
        tx.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        tx.text = speed.ToString();
        if (buttonLDownflag)
        {
            if (speed > -0.6f)
            {
                speed -= 0.0001f;
            }
            else
            {
                speed -= 0.001f;
            }
            
            //Debug.Log($"speed is {speed}");
        }
        if (buttonRDownflag)
        {
            speed += 0.005f;
            if (speed > 0.0f)
            {
                speed = 0.0f;
            }
            //Debug.Log($"speed is {speed}");

        }
        bg.transform.Translate(0, speed, 0);
        float y_pos = 1737.0f;
        if (bg.transform.position.y <= -1741)
        {
            bg.transform.position = new Vector3(550.0f, y_pos, 0);
        }


    }
    public void PushRUI()
    {
        Debug.Log("R Pushed");
        buttonRDownflag = true;
    }
    public void ReleaseRUI()
    {
        Debug.Log("R Released");
        buttonRDownflag = false;
    }
    public void PushLUI()
    {
        Debug.Log("L Pushed");
        buttonLDownflag = true;

    }
    public void ReleaseLUI()
    {
        Debug.Log("L Released");
        buttonLDownflag = false;
    }
}
