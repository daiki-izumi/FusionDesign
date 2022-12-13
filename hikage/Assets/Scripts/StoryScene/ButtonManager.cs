using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace NovelGame
{
    public class ButtonManager : MonoBehaviour
    {
        
        [SerializeField] Sprite _ImageButton;
        [SerializeField] GameObject _ButtonObject;
        [SerializeField] GameObject _ButtonPrefab;

        // �e�L�X�g�t�@�C������A�������Sprite��GameObject��������悤�ɂ��邽�߂̎���
        Dictionary<string, Sprite> _textToButtoImage;
        Dictionary<string, GameObject> _ButtonPanelObject;

        // ���삵����Prefab���w��ł���悤�ɂ��邽�߂̎���
        Dictionary<string, GameObject> _ButtonPrehabObject;


        GameObject ListButton;

        void Awake()
        {
            _textToButtoImage = new Dictionary<string, Sprite>();
            _textToButtoImage.Add("image", _ImageButton);
            _ButtonPanelObject = new Dictionary<string, GameObject>();

            _ButtonPanelObject.Add("ButtonPanelObject", _ButtonObject);
          
            _ButtonPrehabObject = new Dictionary<string, GameObject>();
        }


        
        // �{�^��(�I����)��z�u����
        public void PutButton(string num,string ButtonText1,string ButtonText2,string ButtonText3, string ButtonText4)
        {

            Sprite image = _ImageButton;//���͂��ꂽ������ŉ摜��ǂ݂���
            GameObject parentObject = _ButtonObject;//�摜��z�u����panel���Ăяo��
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
                ListButton.GetComponent<Image>().sprite = image;//�������ꂽ�I�u�W�F�N�g[item]��sprite��image��
                ListButton.GetComponent<Button>().onClick.AddListener(() => MyOnClick(n));
                
                _ButtonPrehabObject.Add(ButtonText[i], ListButton);
            }

        
        }



        // �摜���폜����
        public void RemoveButton(string num, string ButtonText1, string ButtonText2, string ButtonText3, string ButtonText4)
        {
            int ButtonNum = int.Parse(num);
            string[] ButtonText = { ButtonText1, ButtonText2, ButtonText3, ButtonText4 };
            for (int i = 0; i < 3; i++)
            {
                GameObject.Destroy(_ButtonPrehabObject[ButtonText[i]]);
            }
        }
        // button_1(Clone)

        public void ChangeScene()//�{�^���N���b�N�Ŏ��̍s�ֈړ�
        {
            SceneManager.LoadScene("QuizScene");
        }

        void MyOnClick(int index)
        {

            print(index);

            if(index == 2)
            {
               ChangeScene();
            }
        }
    }
}