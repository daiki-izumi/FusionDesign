using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using System.Diagnostics;

using Debug = UnityEngine.Debug;
using Random = System.Random;
public class NoteController : MonoBehaviour
{
    string Type;
    float Timing;

    float Distance;
    float During;

    Vector3 firstPos;
    bool isGo;
    float GoTime;
    float StartTime;
    bool one = true;

    void OnEnable()
    {
        isGo = false;
        firstPos = this.transform.position;

        Random myObject = new Random();
        float ranNum = (float)myObject.NextDouble();
        Quaternion rot = Quaternion.Euler(0, 0, ranNum * 0.6f);
        // Debug.Log(ranNum);
        

        this.UpdateAsObservable()
          .Where(_ => isGo)
          .Subscribe(_ => {
              this.gameObject.transform.position = new Vector3(firstPos.x - Distance * (Time.time * 1000 - GoTime) / During, high(firstPos.x - Distance * (Time.time * 1000 - GoTime) / During), firstPos.z);

          
              Quaternion q = this.transform.rotation;
              this.gameObject.transform.rotation = q * rot;
               //Debug.Log(StartTime );
              if (Time.time - StartTime > 38.0 )
              {
                 // Debug.Log("end");//ƒQ[ƒ€I—¹
              }
            
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
        if (one)
        {
            StartTime = Time.time;
        }
        one= false;
        isGo = true;
    }
    
    public float high(float x)
    {
      

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