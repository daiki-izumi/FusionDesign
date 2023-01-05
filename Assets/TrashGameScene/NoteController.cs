using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System.Diagnostics;

using Debug = UnityEngine.Debug;
public class NoteController : MonoBehaviour
{
    string Type;
    float Timing;

    float Distance;
    float During;

    Vector3 firstPos;
    bool isGo;
    float GoTime;

    void OnEnable()
    {
        isGo = false;
        firstPos = this.transform.position;

        this.UpdateAsObservable()
          .Where(_ => isGo)
          .Subscribe(_ => {
              this.gameObject.transform.position = new Vector3(firstPos.x - Distance * (Time.time * 1000 - GoTime) / During, high(firstPos.x - Distance * (Time.time * 1000 - GoTime) / During), firstPos.z);
          });
    }

    public void setParameter(string type, float timing)
    {
        Type = type;
        Timing = timing;
    }

    public string getType()
    {
        return Type;
    }

    public float getTiming()
    {
        return Timing;
    }

    public void go(float distance, float during)
    {
        Distance = distance;
        During = during;
        GoTime = Time.time * 1000;

        isGo = true;
    }
    
    public float high(float x)
    {
        /*
        float a,b,c;

        a = -0.0433673469f;
        b = 0.260204082f;
        c = 4.10969388f;

        if(x > 3.0f)
        {
            a = -0.027700831f;
            b = a * 6.0f;
            c = a * 9.0f + 6.5f;

        }

        return a * x * x + b * x + c;
        */

        float a, b, c;

        float middle_x, middle_y;
        float beat_x, beat_y;

        middle_x = 3.0f;
        middle_y = 4.5f;
        beat_x = -6.5f;
        beat_y = 2.0f;

         a = -0.027700831f;
       // Debug(a);
       // a = (beat_y - middle_y) / (beat_x - middle_x) * (beat_x - middle_x);
        //Debug.Log(a);
        return a * (x - 3.0f) * (x - 3.0f) + 4.5f;

    }
}