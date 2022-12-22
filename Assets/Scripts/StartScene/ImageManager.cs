using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


    public class ImageManager : MonoBehaviour
    {

        [SerializeField] Sprite _JPLIFE;
        [SerializeField] Sprite _CharaWindow;
        [SerializeField] GameObject _ImagePanel;
        [SerializeField] GameObject _WindowPrefab;
        [SerializeField] GameObject _ImagePrefab;

        GameObject Title;
        public GameObject CharaWindow;
        // Start is called before the first frame update
        void Start()
        {
            GameObject parentObject = _ImagePanel;//画像を配置するpanelを呼び出す
            Vector2 position = new Vector2(0, 2);
            Quaternion rotation = Quaternion.identity;
            Transform parent = parentObject.transform;


            Title = Instantiate(_ImagePrefab, position, rotation, parent) as GameObject;
            Title.GetComponent<Image>().sprite = _JPLIFE;
        }

        public void PutCharaWindow()
        {
            //var windowtext;
            GameObject parentObject = _ImagePanel;//画像を配置するpanelを呼び出す
            Vector2 position = new Vector2(0.0f, -3.0f);
            Quaternion rotation = Quaternion.identity;
            Transform parent = parentObject.transform;


            Debug.Log(StartSceneManager.Instance.startController.GetCurrentSceneNumber());

            CharaWindow = Instantiate(_WindowPrefab, position, rotation, parent) as GameObject;
            CharaWindow.GetComponent<Image>().sprite = _CharaWindow;
            CharaWindow.GetComponent<Image>().sprite = _CharaWindow;
            var windowtext = CharaWindow.GetComponentInChildren<Text>();
            windowtext.text = StartSceneManager.Instance.startController.GetCurrentSentences(); ;
            windowtext.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            //CharaWindow.GetComponentInChildren<Text>().text = StartSceneManager.Instance.startController.GetCurrentSentences();
        }



    }
