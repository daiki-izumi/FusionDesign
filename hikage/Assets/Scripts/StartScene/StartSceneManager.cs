using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StartScene
{
    public class StartSceneManager : MonoBehaviour
    {
        public static StartSceneManager Instance { get; private set; }


        void Awake()
        {
           
            // これで、別のクラスからGameManagerの変数などを使えるようになる。
            Instance = this;
        }
    }
}