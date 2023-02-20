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
    private Transform carTransform;
    private Transform meterTransform;
    private Text tx;
    private Vector3 localAngle;
    //車
    public GameObject car;
    private float meterval = 200.0f;
    // Start is called before the first frame update
    void Start()
    {
        //Light
        GameObject light = GameObject.Find("L");
        Debug.Log(light.name);
        //Right
        GameObject right = GameObject.Find("R");
        //background
        //bg = GameObject.Find("bg").GetComponent<Transform>();
        //bg1 = GameObject.Find("bg1").GetComponent<Transform>();
        //Text
        tx = GameObject.Find("Speed").GetComponent<Text>();
        tx.text = "";
        //車
        //GameObject car = GameObject.Find("car");
        carTransform = car.transform.GetComponent<Transform>();
        //メーター
        GameObject meter = GameObject.Find("meter");
        meterTransform = meter.transform.GetComponent<RectTransform>();
        meterTransform.rotation = Quaternion.Euler(0, 0, meterval);//new Vector3(0, 0, meterval);
        //localAngle = meterTransform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        speed = 0.01f;
        meterval -= 0.05f;
        meterTransform.rotation = Quaternion.Euler(0, 0, meterval);
        if (meterval < 70)
        {
            meterTransform.rotation = Quaternion.Euler(0, 0, 70);
        }
        
        tx.text = speed.ToString();
        if (buttonLDownflag)
        {
            carTransform.position += new Vector3(-speed, 0, 0);
            //Debug.Log($"speed is {speed}");
        }
        if (buttonRDownflag)
        {
            carTransform.position += new Vector3(speed, 0, 0);
            //Debug.Log($"speed is {speed}");

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
