using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StartScene
{
    public class StartSceneManager : MonoBehaviour
    {

        public static StartSceneManager Instance { get; private set; }

        public ButtonManager buttonManager;
        public ImageManager imageManager;
        public StartController startController;

        [System.NonSerialized] public int SceneNumber;

        void Awake()
        {
            Instance = this;

            SceneNumber = 0;

        }

    }
}
