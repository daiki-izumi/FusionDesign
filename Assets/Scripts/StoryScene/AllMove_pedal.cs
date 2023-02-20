using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllMove_pedal : MonoBehaviour
{
    private bool buttonRDownflag = false;
    private bool buttonLDownflag = false;
    private bool buttonPDownflag = false;
    private bool buttonBDownflag = false;
    //Light spped
    private float speed = 0.0f;
    private Transform bg;
    private Transform carTransform;
    private Transform meterTransform;
    private Text tx;
    private Vector3 localAngle;
    //é‘
    public GameObject car;
    private float meterval = 200.0f;
    private float rotval = 60.0f;
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
        //é‘
        //GameObject car = GameObject.Find("car");
        carTransform = car.transform.GetComponent<Transform>();
        //ÉÅÅ[É^Å[
        GameObject meter = GameObject.Find("meter");
        meterTransform = meter.transform.GetComponent<RectTransform>();
        meterTransform.rotation = Quaternion.Euler(0, 0, meterval);//new Vector3(0, 0, meterval);
        carTransform.rotation = Quaternion.Euler(0, 0, rotval);
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

            rotval += 0.05f;
            carTransform.rotation = Quaternion.Euler(0, 0, rotval);
            //Debug.Log($"speed is {speed}");
        }
        if (buttonRDownflag)
        {
            rotval -= 0.05f;
            carTransform.rotation = Quaternion.Euler(0, 0, rotval);
            //Debug.Log($"speed is {speed}");

        }
        if (buttonPDownflag)
        {
            carTransform.position += new Vector3(0, -speed, 0);
            //Debug.Log($"speed is {speed}");

        }
        if (buttonBDownflag)
        {
            carTransform.position += new Vector3(0, speed, 0);
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
    public void PushPUI()
    {
        Debug.Log("Pedal Pushed");
        buttonPDownflag = true;

    }
    public void ReleasePUI()
    {
        Debug.Log("Pedal Released");
        buttonPDownflag = false;
    }
    public void PushBUI()
    {
        Debug.Log("Break Pushed");
        buttonBDownflag = true;

    }
    public void ReleaseBUI()
    {
        Debug.Log("Break Released");
        buttonBDownflag = false;
    }
}
