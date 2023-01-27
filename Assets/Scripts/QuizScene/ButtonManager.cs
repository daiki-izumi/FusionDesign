using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace QuizGame { 
    public class ButtonManager : MonoBehaviour
    {
        
        [SerializeField] Sprite _ImageButton;
        
        [SerializeField] GameObject _GoodPrefab;
        [SerializeField] GameObject _BadPrefab;
        [SerializeField] GameObject _ButtonObject;
        [SerializeField] GameObject _ButtonPrefab;
        [SerializeField] GameObject _NextButton;
        [SerializeField] GameObject _NextButtonPanel;

        // テキストファイルから、文字列でSpriteやGameObjectを扱えるようにするための辞書
        Dictionary<string, Sprite> _textToButtoImage;
        Dictionary<string, GameObject> _ButtonPanelObject;

        // 操作したいPrefabを指定できるようにするための辞書
        Dictionary<int, GameObject> _ButtonPrehabObject;
        Dictionary<string, GameObject> _GoodPrehabObject;
        Dictionary<string, GameObject> _BadPrehabObject;

        GameObject ListButton;
        GameObject NextButton;
        GameObject TextButton;
        GameObject Good;
        GameObject Bad;

        void Awake()
        {
            _textToButtoImage = new Dictionary<string, Sprite>();
            _textToButtoImage.Add("image", _ImageButton);
            _ButtonPanelObject = new Dictionary<string, GameObject>();

            _ButtonPanelObject.Add("ButtonPanelObject", _ButtonObject);
          
            _ButtonPrehabObject = new Dictionary<int, GameObject>();

            _GoodPrehabObject = new Dictionary<string, GameObject>();
            _BadPrehabObject = new Dictionary<string, GameObject>();

        }


        
        // ボタン(選択肢)を配置する
        public void PutButton(string num,string ButtonText1,string ButtonText2,string ButtonText3, string ButtonText4, string answer)
        {

            Sprite image = _ImageButton;//入力された文字列で画像を読みだす
            GameObject parentObject = _ButtonObject;//画像を配置するpanelを呼び出す
            int ButtonNum = int.Parse(num);

            string[] ButtonText = {ButtonText1,ButtonText2,ButtonText3,ButtonText4};
           
            Vector2 position_2 = new Vector2(-3, 0);
            Vector2 position_3 = new Vector2(-4, 0);
            Vector2 position_4 = new Vector2(-4, 2);
            Quaternion rotation = Quaternion.identity;
            Transform parent = parentObject.transform;
           
            Destroy(NextButton);
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

                    //
                    Good = Instantiate(_GoodPrefab, position_4, rotation, parent) as GameObject;
                    Good.GetComponent<Transform>().localPosition = new Vector3(Good.GetComponent<Transform>().localPosition.x + 220.0f , Good.GetComponent<Transform>().localPosition.y - 45.0f, Good.GetComponent<Transform>().localPosition.z);
                    Bad = Instantiate(_BadPrefab, position_4, rotation, parent) as GameObject;
                    Bad.GetComponent<Transform>().localPosition = new Vector3(Bad.GetComponent<Transform>().localPosition.x + 220.0f,Bad.GetComponent<Transform>().localPosition.y - 25.0f, Bad.GetComponent<Transform>().localPosition.z);

                    Good.gameObject.SetActive(false);
                    Bad.gameObject.SetActive(false);
                    //
                    //

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
               
                    ListButton.GetComponent<Button>().onClick.AddListener(() => MyOnClick(n, ButtonNum, ButtonText[n], answer));
                    ListButton.GetComponent<Button>().onClick.AddListener(() => GameManager.Instance.mainTextController.OnClick());
               
                
                _ButtonPrehabObject.Add(i, ListButton);
                _GoodPrehabObject.Add(ButtonText[n], Good);
                _BadPrehabObject.Add(ButtonText[n], Bad);
            }

        
        }



        // 画像を削除する
        public void RemoveButton(int num)
        {
           // _ButtonPrehabObject = new Dictionary<int, GameObject>();

            for (int i = 0; i <num; i++)
            {
                
                Destroy(_ButtonPrehabObject[i]);
                _ButtonPrehabObject.Remove(i);
               
            }
        }
        // button_1(Clone)


        public void PutTextButton()
        {

            Quaternion rotation = Quaternion.identity;
            Transform parent = _NextButtonPanel.transform;

            Vector2 position = new Vector2(6, 3.7f);
            TextButton = Instantiate(_NextButton, position, rotation, parent) as GameObject;

            TextButton.GetComponent<Button>().onClick.AddListener(() => ClickTextButton());


        }

        public void ClickTextButton()
        {

            GameManager.Instance.mainTextController.OnClick();


           

        }
        public void DestroyTextButton()
        {

            Destroy(TextButton);
        }

        public void PutNextButton(string answer,string ButtonText)
        {

            Quaternion rotation = Quaternion.identity;
            Transform parent = _NextButtonPanel.transform;
         
            Vector2 position = new Vector2(6, 3.7f);
            NextButton = Instantiate(_NextButton, position, rotation, parent) as GameObject;
            
            NextButton.GetComponent<Button>().onClick.AddListener(() => ClickNextButton(answer,ButtonText));

        }

        public void ClickNextButton(string answer,string ButtonText)
        {
            RemoveButton(4);
            GameManager.Instance.mainTextController.OnClick();


            _GoodPrehabObject[answer].gameObject.SetActive(false);
            _BadPrehabObject[ButtonText].gameObject.SetActive(false);

            Destroy(NextButton);


        }



        public void ChangeScene()
        {
            SceneManager.LoadScene("QuizScene");
        }


        


        public void MyOnClick(int index,int num,string ButtonText,string answer)
        {
            _GoodPrehabObject[answer].gameObject.SetActive(true); 


            if (ButtonText == answer)
            {
                print("good");
                _GoodPrehabObject[answer].gameObject.SetActive(true);
            }
            else
            {
                print("bad");
                _BadPrehabObject[ButtonText].gameObject.SetActive(true);
            }
            
            PutNextButton(answer,ButtonText);

            for (int i = 0; i < num; i++)
            {
               _ButtonPrehabObject[i].GetComponent<Button>().interactable = false;
            }

        }



    }
}