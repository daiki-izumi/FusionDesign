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

        // �e�L�X�g�t�@�C������A�������Sprite��GameObject��������悤�ɂ��邽�߂̎���
        Dictionary<string, Sprite> _textToCharacter;

        // ���삵����Prefab���w��ł���悤�ɂ��邽�߂̎���
        Dictionary<string, GameObject> _CharacterPrehabObject;

        void Awake()
        {
            _textToCharacter = new Dictionary<string, Sprite>();
            _textToCharacter.Add("background1", _NameBox);
            _textToCharacter.Add("Character1", _Character1);
            _textToCharacter.Add("Character2", _Character2);

            _CharacterPrehabObject = new Dictionary<string, GameObject>();
        }



        // �摜��z�u����
        public void PutChara(string charaName)
        {
            Sprite image = _textToCharacter[charaName];//���͂��ꂽ������ŉ摜��ǂ݂���
            GameObject parentObject = _CharacterObject;//�摜��z�u����panel���Ăяo��
            Vector2 position = new Vector2(0, 0);
            Quaternion rotation = Quaternion.identity;
            Transform parent = parentObject.transform;
            GameObject item = Instantiate(_CharacterPrefab, position, rotation, parent);//Instantiate(original,position,rotation,parent);
            item.GetComponent<Image>().sprite = image;//�������ꂽ�I�u�W�F�N�g[item]��sprite��image��

            _CharacterPrehabObject.Add(charaName, item);//Prehab�𑀍삷�邽�߂̎�����item���i�[
        }

        // �摜���폜����
        public void RemoveChara(string charaName)
        {
            Destroy(_CharacterPrehabObject[charaName]);
        }




    }


}
