using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] GameObject _StartButton;
    [SerializeField] GameObject _NextButton;
    [SerializeField] GameObject _ButtonPanel;
    [SerializeField] GameObject _CharaButton_male;
    [SerializeField] GameObject _CharaButton_female;
    [SerializeField] GameObject _MorningButton;
    [SerializeField] GameObject _MiddayButton;
    [SerializeField] GameObject _EveningButton;
    [SerializeField] Sprite Chara_male;
    [SerializeField] Sprite Chara_female;
    [SerializeField] UserInformation userInformation;

    GameObject StartButton;
    GameObject NextButton;
    GameObject CharaButton_male;
    GameObject CharaButton_female;
    GameObject MorningButton;
    GameObject MiddayButton;
    GameObject EveningButton;

    [SerializeField] AudioClip push;



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
        Vector2 position_male = new Vector2(-2.5f, 0.5f);
        Vector2 position_female = new Vector2(2.5f, 0.5f);
        Quaternion rotation = Quaternion.identity;
        Transform parent = parentObject.transform;
        CharaButton_male = Instantiate(_CharaButton_male, position_male, rotation, parent) as GameObject;
        CharaButton_male.GetComponent<Image>().sprite = Chara_male;
        CharaButton_female = Instantiate(_CharaButton_female, position_female, rotation, parent) as GameObject;
        CharaButton_female.GetComponent<Image>().sprite = Chara_female;

        CharaButton_male.GetComponent<Button>().onClick.AddListener(() => ClickMale());
        CharaButton_female.GetComponent<Button>().onClick.AddListener(() => ClickFemale());

    }

    public void PutSelectButton()
    {
        GameObject parentObject = _ButtonPanel;//画像を配置するpanelを呼び出す
        Vector2 position_morning = new Vector2(-2.7f, -0.3f);
        Vector2 position_midday = new Vector2(0.0f, 1.5f);
        Vector2 position_evening = new Vector2(2.7f, -0.3f);
        Quaternion rotation = Quaternion.identity;
        Transform parent = parentObject.transform;
        MorningButton = Instantiate(_MorningButton, position_morning, rotation, parent) as GameObject;
        MiddayButton = Instantiate(_MiddayButton, position_midday, rotation, parent) as GameObject;
        EveningButton = Instantiate(_EveningButton, position_evening, rotation, parent) as GameObject;

        MorningButton.GetComponent<Button>().onClick.AddListener(() => ClickMorning());
        MiddayButton.GetComponent<Button>().onClick.AddListener(() => ClickMidday());
        EveningButton.GetComponent<Button>().onClick.AddListener(() => ClickEvening());

    }

    public void ClickMale()
    {

        Destroy(CharaButton_female);
        Destroy(CharaButton_male);
        Destroy(StartSceneManager.Instance.imageManager.CharaWindow);
        userInformation.gender = true;
        PutSelectButton();

    }

    public void ClickFemale()
    {

        Destroy(CharaButton_female);
        Destroy(CharaButton_male);
        Destroy(StartSceneManager.Instance.imageManager.CharaWindow);
        userInformation.gender = false;
        PutSelectButton();
    }

    public void ClickStart()
    {

        PutNextButton();
        Destroy(StartButton);
        StartSceneManager.Instance.SceneNumber++;

    }

    public void ClickNext()
    {
        StartSceneManager.Instance.SceneNumber++;

        PutCharaButton();
        StartSceneManager.Instance.startController._textpanel.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        StartSceneManager.Instance.startController._mainTextObject.text = "  ";
        StartSceneManager.Instance.imageManager.PutCharaWindow();
        Destroy(NextButton);

    }

    public void ClickMorning()
    {
        SceneManager.LoadScene("Mor_1_Story");
    }
    public void ClickMidday()
    {
        SceneManager.LoadScene("Mid_5_Story");
    }
    public void ClickEvening()
    {
        SceneManager.LoadScene("Eve_7_Story");
    }



}
