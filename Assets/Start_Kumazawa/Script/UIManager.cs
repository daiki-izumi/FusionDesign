using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//add
using fileSL;
using Debug = UnityEngine.Debug;
//
public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject StartPanel;
    [SerializeField] GameObject DescriptionPanel;
    [SerializeField] GameObject SelectCharacterPanel0;
    [SerializeField] GameObject SelectCharacterPanel1;
    [SerializeField] GameObject SelectScenePanel;

    //add
    public static fileSL.fileSaveLoad ld;

    // Start is called before the first frame update
    void Awake()
    {
        ld = new fileSL.fileSaveLoad();

        BackToMenu();
    }

    public void SelectStartButton()
    {
        StartPanel.SetActive(false);
        DescriptionPanel.SetActive(true);
    }

    public void SelectNextButton()
    {
        DescriptionPanel.SetActive(false);
        SelectCharacterPanel0.SetActive(true);
    }

    public void SelectGoToCharacterButton()
    {
        SelectCharacterPanel0.SetActive(false);
        SelectCharacterPanel1.SetActive(true);
    }

    public void SelectBoyButton()
    {
        SelectCharacterPanel1.SetActive(false);
        SelectScenePanel.SetActive(true);
        //add
        ld.Save("Chara_male.png");
        Debug.Log(ld.Load());

    }

    public void SelectGirlButton()
    {
        SelectCharacterPanel1.SetActive(false);
        SelectScenePanel.SetActive(true);
        //add
        ld.Save("Chara_female.png");
        Debug.Log(ld.Load());
    }

    public void BackToMenu()
    {
        StartPanel.SetActive(true);
        DescriptionPanel.SetActive(false);
        SelectCharacterPanel0.SetActive(false);
        SelectCharacterPanel1.SetActive(false);
        SelectScenePanel.SetActive(false);
    }

    //add
    public void ClickMorning()
    {
        SceneManager.LoadScene("Mor_1_Story");
    }
    public void ClickMidday()
    {
        SceneManager.LoadScene("Mid_4_Story");
    }
    public void ClickEvening()
    {
        SceneManager.LoadScene("Eve_8_Story");
    }
    //add



}
