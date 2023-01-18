using UnityEngine;
using System.Collections;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
public class TouchBW : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
   // [SerializeField] EffectManager effectManager;

    void Updata()
    {
        gameManager.touch_BW = false;
    }
    public void onClickAct()
    {
      // gameManager. beat("1", Time.time * 1000 - PlayTime);
       
        //effectManager.setboolBW();
        gameManager.touch_BW = true;

    }



}