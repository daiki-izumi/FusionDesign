using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace StartScene
{
    public class ButtonManager : MonoBehaviour
    {
        [SerializeField] GameObject _StartButton;
        [SerializeField] GameObject _NextButton;
        [SerializeField] GameObject _ButtonPanel;
        [SerializeField] GameObject _CharaButton_male;
        [SerializeField] GameObject _CharaButton_female;
        [SerializeField] Sprite Chara_male;
        [SerializeField] Sprite Chara_female;
        GameObject StartButton;
        GameObject NextButton;
        GameObject CharaButton_male;
        GameObject CharaButton_female;
        // Start is called before the first frame update




        public void PutStartButton()
        {
            GameObject parentObject = _ButtonPanel;//画像を配置するpanelを呼び出す
            Vector2 position = new Vector2(0, 0);
            Quaternion rotation = Quaternion.identity;
            Transform parent = parentObject.transform;
            StartButton = Instantiate(_StartButton, position, rotation, parent) as GameObject;
            StartButton.GetComponent<Button>().onClick.AddListener(() => ClickStart());
        }
   

        public void PutNextButton()
        {
            GameObject parentObject = _ButtonPanel;//画像を配置するpanelを呼び出す
            Vector2 position = new Vector2(6.2f, 3.5f);
            Quaternion rotation = Quaternion.identity;
            Transform parent = parentObject.transform;
            NextButton = Instantiate(_NextButton, position, rotation, parent) as GameObject;
            NextButton.GetComponent<Button>().onClick.AddListener(() => ClickNext());

        }

        public void PutCharaButton()
        {
            GameObject parentObject = _ButtonPanel;//画像を配置するpanelを呼び出す
            Vector2 position_male = new Vector2(-1.5f, 0.0f);
            Vector2 position_female = new Vector2(1.5f, 0.0f);
            Quaternion rotation = Quaternion.identity;
            Transform parent = parentObject.transform;
            CharaButton_male = Instantiate(_CharaButton_male, position_male, rotation, parent) as GameObject;
            CharaButton_male.GetComponent<Image>().sprite = Chara_male;
            //NextButton.GetComponent<Button>().onClick.AddListener(() => ClickNext());
            CharaButton_female = Instantiate(_CharaButton_female, position_female, rotation, parent) as GameObject;
            CharaButton_female.GetComponent<Image>().sprite = Chara_female;

        }


        public void ClickStart()
        {
            PutNextButton();
            Destroy(StartButton);
            StartSceneManager.Instance.SceneNumber++;
        }

        public void ClickNext()
        {
            PutCharaButton();
            StartSceneManager.Instance.startController._textpanel.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
            StartSceneManager.Instance.startController._mainTextObject.text = "  ";

        }
    }
}