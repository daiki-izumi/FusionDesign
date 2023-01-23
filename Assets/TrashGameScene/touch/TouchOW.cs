using UnityEngine;
using System.Collections;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
public class TouchOW : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
   // [SerializeField] EffectManager effectManager;

    void Updata()
    {
        gameManager.touch_OW = false;
    }
    public void onClickOW()
    {
        // gameManager. beat("1", Time.time * 1000 - PlayTime);

        //effectManager.setboolBW();
        gameManager.touch_OW = true;

    }



}