using UnityEngine;
using System.Collections;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
public class TouchNBW : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
   // [SerializeField] EffectManager effectManager;

    void Updata()
    {
        gameManager.touch_NBW = false;
    }
    public void onClickNBW()
    {
        // gameManager. beat("1", Time.time * 1000 - PlayTime);

        //effectManager.setboolBW();
        gameManager.touch_NBW = true;

    }



}