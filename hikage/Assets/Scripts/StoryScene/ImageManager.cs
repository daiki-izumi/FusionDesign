using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace QuizGame
{
    public class ImageManager : MonoBehaviour
    {
        [SerializeField] Sprite _background1;
        [SerializeField] Sprite _eventCG1;
        [SerializeField] Sprite _eventCG2;
        [SerializeField] GameObject _backgroundObject;
        [SerializeField] GameObject _eventObject;
        [SerializeField] GameObject _imagePrefab;

        // テキストファイルから、文字列でSpriteやGameObjectを扱えるようにするための辞書
        Dictionary<string, Sprite> _textToSprite;
        Dictionary<string, GameObject> _textToParentObject;

        // 操作したいPrefabを指定できるようにするための辞書
        Dictionary<string, GameObject> _textToSpriteObject;

        void Awake()
        {
            _textToSprite = new Dictionary<string, Sprite>();
            _textToSprite.Add("background1", _background1);
            _textToSprite.Add("eventCG1", _eventCG1);
            _textToSprite.Add("eventCG2", _eventCG2);

            _textToParentObject = new Dictionary<string, GameObject>();
            _textToParentObject.Add("backgroundObject", _backgroundObject);
            _textToParentObject.Add("eventObject", _eventObject);

            _textToSpriteObject = new Dictionary<string, GameObject>();
        }

        // 画像を配置する
        public void PutImage(string imageName)
        {
            Sprite image = _textToSprite[imageName];//入力された文字列で画像を読みだす
           // GameObject parentObject = _textToParentObject[parentObjectName];//画像を配置するpanelを呼び出す
            GameObject parentObject = _backgroundObject;//画像を配置するpanelを呼び出す
            Vector2 position = new Vector2(0, 0);
            Quaternion rotation = Quaternion.identity;
            Transform parent = parentObject.transform;
            GameObject item = Instantiate(_imagePrefab, position, rotation, parent);//Instantiate(original,position,rotation,parent);
            item.GetComponent<Image>().sprite = image;//生成されたオブジェクト[item]のspriteをimageに

            _textToSpriteObject.Add(imageName, item);//Prehabを操作するための辞書にitemを格納
        }

        // 画像を削除する
        public void RemoveImage(string imageName)
        {
            Destroy(_textToSpriteObject[imageName]);
        }

        

    }
}