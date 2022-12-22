using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInformation : MonoBehaviour
{
    public int score;
    public bool gender;//male true;female false;

    void Start()
    {
        DontDestroyOnLoad(this);
    }
  


    
}
