using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;



public class StartController : MonoBehaviour
    {
        // Start is called before the first frame update
        public TextMeshProUGUI _mainTextObject;
        public GameObject _textpanel;
        [SerializeField] TextAsset _textFile;
        [SerializeField] private SoundManager soundManager;
        [SerializeField]  AudioClip clip;
        List<string> _sentences = new List<string>();
        string sentence;
        float _time;
        float _feedTime;
        bool flag = true;


        void Start()
        {
            _time = 0f;
            _feedTime = 0.05f;
            StringReader reader = new StringReader(_textFile.text);

            while (reader.Peek() != -1)//テキストファイルを全て読み込んだら終了
            {
                string line = reader.ReadLine();//ReadLine
                _sentences.Add(line);
            }

            soundManager.PlayBgm(clip);

            _mainTextObject.text = GetCurrentSentences();

            StartSceneManager.Instance.buttonManager.PutStartButton();

         
        }

        // Update is called once per frame
        void Update()
        {
            if(GetCurrentSceneNumber() == 1 && flag)
            {
                _mainTextObject.text = _sentences[GetCurrentSceneNumber()];
                flag = false;
            }

        }

        public string GetCurrentSentences()
        {
            return _sentences[GetCurrentSceneNumber()];
        }

        public int GetCurrentSceneNumber()
        {
            return StartSceneManager.Instance.SceneNumber;

        }
    }
