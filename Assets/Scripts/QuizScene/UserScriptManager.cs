using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
namespace QuizGame
{
    public class UserScriptManager : MonoBehaviour
    {
        [SerializeField] TextAsset _textFile;
       
        // 文章中の文（ここでは１行ごと）を入れておくためのリスト
        List<string> _sentences = new List<string>();
        List<string> _question = new List<string>();
        public static int StoryFlag;
      

        void Awake()
        {
            
            //add
            string story;
           
             story = _textFile.text;
     //StringReader reader = new StringReader(_textFile.text);
            StringReader reader = new StringReader(story);
            // テキストファイルの中身を、１行ずつリストに入れておく




            while (reader.Peek() != -1)//テキストファイルを全て読み込んだら終了
            {
                string line = reader.ReadLine();//ReadLine
                _sentences.Add(line);
            }
        }


        public int GetNext
        {
            get { return StoryFlag; }
            set { StoryFlag = value; }
        }

        // 現在の行の文を取得する
        public string GetCurrentSentence()
        {
            return _sentences[GameManager.Instance.lineNumber];
        }

        // 文が命令かどうか
        public bool IsStatement(string sentence)
        {
            if (sentence[0] == '&')
            {
                return true;
            }
            return false;
        }

        public static int getStoryFlag(int a)
        {
            StoryFlag = a;
            return StoryFlag;
        }

        // 命令を実行する
        public void ExecuteStatement(string sentence)
        {
            string[] words = sentence.Split(' ');
            //words[0] ="&命令"
            //words[1] = imageName;
            //words[2] = parentObjectName
            //words[3] = ButtonNum
            //words[4] = ButtonText1
            //words[5] = ButtonText2
            //words[6] = ButtonText3
            //words[7] = ButtonText4
            switch (words[0])
            {
                case "&img":
                    GameManager.Instance.imageManager.PutImage(words[1]);
                    break;
                case "&rmimg":
                    GameManager.Instance.imageManager.RemoveImage(words[1]);
                    break;
                case "&button":
                    GameManager.Instance.buttonManager.PutButton(words[1] ,words[2],words[3], words[4], words[5], words[6]);
                    break;
                case "&chara":
                    GameManager.Instance.characterManager.PutChara(words[1]);
                    break;
                case "&rmchara":
                    GameManager.Instance.characterManager.RemoveChara(words[1]);
                    break;
                case "&ChangeSceneStory":
                    SceneManager.LoadScene("StoryScene");
                    break;
                case "&ChangeSceneQuiz1":
                    SceneManager.LoadScene("Quiz1_WalkingDog");
                    break;
                case "&ChangeSceneQuiz2":
                    SceneManager.LoadScene("Quiz2_Purchase");
                    break;
                case "&ChangeSceneQuiz3":
                    SceneManager.LoadScene("Quiz3_Movie");
                    break;
                case "&ChangeSceneQuiz4":
                    SceneManager.LoadScene("Quiz4_Earthquake");
                    break;
                case "&EndSceneQuiz1":
                    SceneManager.LoadScene("Story3_Purchase some grocery");
                    break;
                case "&EndSceneQuiz2":
                    SceneManager.LoadScene("Story4_Cardriving");
                    break;
                case "&EndSceneQuiz3":
                    SceneManager.LoadScene("Story7_Enjoy Karaoke");
                    break;
                case "&EndSceneQuiz4":
                    SceneManager.LoadScene("FinalScene");
                    break;
            }
        }
    }
}