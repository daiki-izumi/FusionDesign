using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace QuizGame { 
    public class ButtonManager : MonoBehaviour
    {
        
        [SerializeField] Sprite _ImageButton;
        [SerializeField] GameObject _ButtonObject;
        [SerializeField] GameObject _ButtonPrefab;
        [SerializeField] GameObject _NextButton;
        [SerializeField] GameObject _NextButtonPanel;

        // テキストファイルから、文字列でSpriteやGameObjectを扱えるようにするための辞書
        Dictionary<string, Sprite> _textToButtoImage;
        Dictionary<string, GameObject> _ButtonPanelObject;

        // 操作したいPrefabを指定できるようにするための辞書
        Dictionary<int, GameObject> _ButtonPrehabObject;


        GameObject ListButton;
        GameObject NextButton;
        void Awake()
        {
            _textToButtoImage = new Dictionary<string, Sprite>();
            _textToButtoImage.Add("image", _ImageButton);
            _ButtonPanelObject = new Dictionary<string, GameObject>();

            _ButtonPanelObject.Add("ButtonPanelObject", _ButtonObject);
          
            _ButtonPrehabObject = new Dictionary<int, GameObject>();
            
        }


        
        // ボタン(選択肢)を配置する
        public void PutButton(string num,string ButtonText1,string ButtonText2,string ButtonText3, string ButtonText4, string answer)
        {

            Sprite image = _ImageButton;//入力された文字列で画像を読みだす
            GameObject parentObject = _ButtonObject;//画像を配置するpanelを呼び出す
            int ButtonNum = int.Parse(num);

            string[] ButtonText = {ButtonText1,ButtonText2,ButtonText3,ButtonText4};
            string link;
            Vector2 position_2 = new Vector2(-3, 0);
            Vector2 position_3 = new Vector2(-4, 0);
            Vector2 position_4 = new Vector2(-4, 2);
            Quaternion rotation = Quaternion.identity;
            Transform parent = parentObject.transform;


           

            for (int i = 0; i < ButtonNum; i++)
            {
                if (ButtonNum == 3)
                {
                    ListButton = Instantiate(_ButtonPrefab, position_3, rotation, parent) as GameObject;
                }else if(ButtonNum == 2)
                {
                    ListButton = Instantiate(_ButtonPrefab, position_2, rotation, parent) as GameObject;
                }else if( ButtonNum == 4) 
                {
                    ListButton = Instantiate(_ButtonPrefab, position_4, rotation, parent) as GameObject;
                }
                var listbuttontext = ListButton.GetComponentInChildren<Text>();
                listbuttontext.text = ButtonText[i];
               
                position_2.x += 6.0f;
                position_3.x += 4.0f;
                position_3.y += 2.0f;
                position_4.x *= -1.0f;
                if (i == 1)
                {
                    position_4.y = -0.5f;
                }
                int n = i;
                ListButton.GetComponent<Image>().sprite = image;//生成されたオブジェクト[item]のspriteをimageに
                ListButton.GetComponent<Button>().onClick.AddListener(() => MyOnClick(n, ButtonNum, ButtonText[n],answer));
                
                _ButtonPrehabObject.Add(i, ListButton);
            }

        
        }



        // 画像を削除する
        public void RemoveButton(int num)
        {
            _ButtonPrehabObject = new Dictionary<int, GameObject>();

            for (int i = 0; i <num; i++)
            {
                _ButtonPrehabObject.Remove(i);
                //GameObject.Destroy(_ButtonPrehabObject[i]);
            }
        }
        // button_1(Clone)

        public void PutNextButton()
        {

            Quaternion rotation = Quaternion.identity;
              Transform parent = _NextButtonPanel.transform;
         
            Vector2 position = new Vector2(7, -3.7f);
            NextButton = Instantiate(_NextButton, position, rotation, parent) as GameObject;
           
            NextButton.GetComponent<Button>().onClick.AddListener(() => RemoveButton(4));
            NextButton.GetComponent<Button>().onClick.AddListener(() => GameManager.Instance.mainTextController.OnClick());

        }

      

        public void ChangeScene()//ボタンクリックで次の行へ移動
        {
            SceneManager.LoadScene("QuizScene");
        }

        


        public void MyOnClick(int index,int num,string ButtonText,string answer)
        {
           
            
            if(ButtonText == answer)
            {
                print("good");
                PutNextButton();
            }
            else
            {
                print("bad");
            }


            if(index == 2)
            {
             //  ChangeScene();
            }
        }



    }
}