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
           
            // ����ŁA�ʂ̃N���X����GameManager�̕ϐ��Ȃǂ��g����悤�ɂȂ�B
            Instance = this;
        }
    }
}