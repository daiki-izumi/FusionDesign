using UnityEngine;
using System.Collections;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
public class TouchGCP : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    //[SerializeField] EffectManager effectManager;

    void Updata()
    {
        gameManager.touch_GCP = false;
    }
    public void onClickGCP()
    {
        // gameManager. beat("1", Time.time * 1000 - PlayTime);

        //effectManager.setboolBW();
        gameManager.touch_GCP = true;

    }



}