using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

using Debug = UnityEngine.Debug;

public class EffectManager : MonoBehaviour
{



    [SerializeField] GameObject bagBW;
    [SerializeField] GameObject bagNBW;
    [SerializeField] GameObject bagGCP;
    [SerializeField] GameObject bagOW;


    Transform transform_BW, transform_NBW, transform_GCP, transform_OW;

    float x_BW, z_BW;
    float x_NBW, z_NBW;
    float x_GCP, z_GCP;
    float x_OW, z_OW;
    float y = -3.0f;
    float move_BW = -1.0f;
    float move_NBW = -1.0f;
    float move_GCP = -1.0f;
    float move_OW = -1.0f;

    bool isEffect_BW, isEffect_NBW, isEffect_GCP, isEffect_OW;  //true‚ÅeffectON


    void Awake()
    {
        transform_BW = bagBW.GetComponent<Transform>();
        x_BW = transform_BW.localPosition.x;
        z_BW = transform_BW.localPosition.z;
        
        transform_NBW = bagNBW.GetComponent<Transform>();
        x_NBW = transform_NBW.localPosition.x;
        z_NBW = transform_NBW.localPosition.z;

        transform_GCP = bagGCP.GetComponent<Transform>();
        x_GCP = transform_GCP.localPosition.x;
        z_GCP = transform_GCP.localPosition.z;

        transform_OW = bagOW.GetComponent<Transform>();
        x_OW = transform_OW.localPosition.x;
        z_OW = transform_OW.localPosition.z;

    }

    // Update is called once per frame
    void Update()
    {
        
        if (isEffect_BW)
        {
            move_BW += 0.01f;
            Effect();
            if(move_BW > 1.0f)
            {
                isEffect_BW = false;
                 move_BW = -1.0f;
            }
        }

        if (isEffect_NBW)
        {
            move_NBW += 0.01f;
            Effect();
            if (move_NBW > 1.0f)
            {
                isEffect_NBW = false;
                move_NBW = -1.0f;
            }
        }

        if (isEffect_GCP)
        {
            move_GCP += 0.01f;
            Effect();
            if (move_GCP > 1.0f)
            {
                isEffect_GCP = false;
                move_GCP = -1.0f;
            }
        }

        if (isEffect_OW)
        {
            move_OW += 0.01f;
            Effect();
            if (move_OW > 1.0f)
            {
                isEffect_OW = false;
                move_OW = -1.0f;
            }
        }
    }

    public void Effect()
    {
        transform_BW.localPosition = new Vector3(x_BW, y -(move_BW * move_BW) + 1.0f, z_BW);
        transform_NBW.localPosition = new Vector3(x_NBW, y - (move_NBW * move_NBW) + 1.0f, z_NBW);
        transform_GCP.localPosition = new Vector3(x_GCP, y - (move_GCP * move_GCP) + 1.0f, z_GCP);
        transform_OW.localPosition = new Vector3(x_OW, y - (move_OW * move_OW) + 1.0f, z_OW);
    }


    public void setboolBW()
    {
        
        isEffect_BW = true;
    }
    public void setboolNBW()
    {
       
        isEffect_NBW = true;
    }
    public void setboolGCP()
    {
     
        isEffect_GCP = true;
    }
    public void setboolOW()
    {
       
        isEffect_OW = true;
    }
}
