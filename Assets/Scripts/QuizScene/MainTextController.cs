using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace QuizGame
{
    public class MainTextController : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _mainTextObject;
        int _displayedSentenceLength;
        int _sentenceLength;
        float _time;
        float _feedTime;
      

        // Start is called before the first frame update
        void Start()
        {
            _time = 0f;
            _feedTime = 0.05f;

            // 最初の行のテキストを表示、または命令を実行
            string statement = GameManager.Instance.userScriptManager.GetCurrentSentence();

            if(statement == null)
            {
                SceneManager.LoadScene("SampleScene");
            }
            if (GameManager.Instance.userScriptManager.IsStatement(statement))
            {
                GameManager.Instance.userScriptManager.ExecuteStatement(statement);
                GoToTheNextLine();
            }
            DisplayText();
        }

        // Update is called once per frame
        void Update()
        {
            // 文章を１文字ずつ表示する
            _time += Time.deltaTime;
            if (_time >= _feedTime)
            {
                _time -= _feedTime;
                if (!CanGoToTheNextLine())
                {
                    _displayedSentenceLength++;
                    _mainTextObject.maxVisibleCharacters = _displayedSentenceLength;
                }
            }

        }

        public bool CanGoToTheNextLine()
        {
            string sentence = GameManager.Instance.userScriptManager.GetCurrentSentence();
             return (_displayedSentenceLength > sentence.Length);
            //return false;
        }

        // 次の行へ移動
        public void GoToTheNextLine()
        {
            _displayedSentenceLength = 0;
            _time = 0f;
            _mainTextObject.maxVisibleCharacters = 0;
            GameManager.Instance.lineNumber++;
            string sentence = GameManager.Instance.userScriptManager.GetCurrentSentence();
            if (GameManager.Instance.userScriptManager.IsStatement(sentence))
            {
                GameManager.Instance.userScriptManager.ExecuteStatement(sentence);
                GoToTheNextLine();
            }
        }

        // テキストを表示
        public void DisplayText()
        {
            string sentence = GameManager.Instance.userScriptManager.GetCurrentSentence();//表示するテキスト読み込み
            _mainTextObject.text = sentence;//表示
        }

        public void OnClick()//ボタンクリックで次の行へ移動
        {
                GoToTheNextLine();
                DisplayText();
          
        }

    }
}