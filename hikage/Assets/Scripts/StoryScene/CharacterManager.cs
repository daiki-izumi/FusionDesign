using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NovelGame
{
    public class CharacterManager : MonoBehaviour
    {
        [SerializeField] Sprite _NameBox;
        [SerializeField] Sprite _Character1;
        [SerializeField] Sprite _Character2;
        [SerializeField] GameObject _CharacterObject;
        [SerializeField] GameObject _CharacterPrefab;

        // テキストファイルから、文字列でSpriteやGameObjectを扱えるようにするための辞書
        Dictionary<string, Sprite> _textToCharacter;

        // 操作したいPrefabを指定できるようにするための辞書
        Dictionary<string, GameObject> _CharacterPrehabObject;

        void Awake()
        {
            _textToCharacter = new Dictionary<string, Sprite>();
            _textToCharacter.Add("background1", _NameBox);
            _textToCharacter.Add("Character1", _Character1);
            _textToCharacter.Add("Character2", _Character2);

            _CharacterPrehabObject = new Dictionary<string, GameObject>();
        }



        // 画像を配置する
        public void PutChara(string charaName)
        {
            Sprite image = _textToCharacter[charaName];//入力された文字列で画像を読みだす
            GameObject parentObject = _CharacterObject;//画像を配置するpanelを呼び出す
            Vector2 position = new Vector2(0, 0);
            Quaternion rotation = Quaternion.identity;
            Transform parent = parentObject.transform;
            GameObject item = Instantiate(_CharacterPrefab, position, rotation, parent);//Instantiate(original,position,rotation,parent);
            item.GetComponent<Image>().sprite = image;//生成されたオブジェクト[item]のspriteをimageに

            _CharacterPrehabObject.Add(charaName, item);//Prehabを操作するための辞書にitemを格納
        }

        // 画像を削除する
        public void RemoveChara(string charaName)
        {
            Destroy(_CharacterPrehabObject[charaName]);
        }




    }


}
