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

        // �e�L�X�g�t�@�C������A�������Sprite��GameObject��������悤�ɂ��邽�߂̎���
        Dictionary<string, Sprite> _textToButtoImage;
        Dictionary<string, GameObject> _ButtonPanelObject;

        // ���삵����Prefab���w��ł���悤�ɂ��邽�߂̎���
        Dictionary<int, GameObject> _ButtonPrehabObject;


        GameObject ListButton;
        GameObject NextButton;
        bool PushFlag = true;

        void Awake()
        {
            _textToButtoImage = new Dictionary<string, Sprite>();
            _textToButtoImage.Add("image", _ImageButton);
            _ButtonPanelObject = new Dictionary<string, GameObject>();

            _ButtonPanelObject.Add("ButtonPanelObject", _ButtonObject);
          
            _ButtonPrehabObject = new Dictionary<int, GameObject>();
            
        }


        
        // �{�^��(�I����)��z�u����
        public void PutButton(string num,string ButtonText1,string ButtonText2,string ButtonText3, string ButtonText4, string answer)
        {

            Sprite image = _ImageButton;//���͂��ꂽ������ŉ摜��ǂ݂���
            GameObject parentObject = _ButtonObject;//�摜��z�u����panel���Ăяo��
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
                ListButton.GetComponent<Image>().sprite = image;//�������ꂽ�I�u�W�F�N�g[item]��sprite��image��
                if (PushFlag == true)
                {
                    ListButton.GetComponent<Button>().onClick.AddListener(() => MyOnClick(n, ButtonNum, ButtonText[n], answer));
                    ListButton.GetComponent<Button>().onClick.AddListener(() => GameManager.Instance.mainTextController.OnClick());
                }
                
                _ButtonPrehabObject.Add(i, ListButton);
            }

        
        }



        // �摜���폜����
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

        public void PutNextButton()
        {

            Quaternion rotation = Quaternion.identity;
            Transform parent = _NextButtonPanel.transform;
         
            Vector2 position = new Vector2(7, -3.7f);
            NextButton = Instantiate(_NextButton, position, rotation, parent) as GameObject;
            // PushFlag = false;
            // NextButton.GetComponent<Button>().onClick.AddListener(() => RemoveButton(4));
            //  NextButton.GetComponent<Button>().onClick.AddListener(() => GameManager.Instance.mainTextController.OnClick());
            NextButton.GetComponent<Button>().onClick.AddListener(() => ClickNextButton());

        }

        public void ClickNextButton()
        {
            RemoveButton(4);
            GameManager.Instance.mainTextController.OnClick();
            Destroy(NextButton);


        }



        public void ChangeScene()//�{�^���N���b�N�Ŏ��̍s�ֈړ�
        {
            SceneManager.LoadScene("QuizScene");
        }

        


        public void MyOnClick(int index,int num,string ButtonText,string answer)
        {
           

            if (ButtonText == answer)
            {
                print("good");
                
            }
            else
            {
                print("bad");
            }
            PutNextButton();

            for (int i = 0; i < num; i++)
            {
               _ButtonPrehabObject[i].GetComponent<Button>().interactable = false;
            }

        }



    }
}