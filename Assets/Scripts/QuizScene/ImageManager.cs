using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace QuizGame
{
    public class ImageManager : MonoBehaviour
    {
        [SerializeField] Sprite _Scene1;
        [SerializeField] Sprite _Scene2;
        [SerializeField] Sprite _Scene3;
        [SerializeField] Sprite _Scene4;
        [SerializeField] Sprite _Scene5;
        [SerializeField] Sprite _Scene6;
        [SerializeField] Sprite _Scene7;
        [SerializeField] Sprite _Scene8;
       
        [SerializeField] GameObject _backgroundObject;
        [SerializeField] GameObject _imagePrefab;

        // �e�L�X�g�t�@�C������A�������Sprite��GameObject��������悤�ɂ��邽�߂̎���
        Dictionary<string, Sprite> _textToSprite;

        // ���삵����Prefab���w��ł���悤�ɂ��邽�߂̎���
        Dictionary<string, GameObject> _textToSpriteObject;

        void Awake()
        {
            _textToSprite = new Dictionary<string, Sprite>();
            _textToSprite.Add("Scene1", _Scene1);
            _textToSprite.Add("Scene2", _Scene2);
            _textToSprite.Add("Scene3", _Scene3);
            _textToSprite.Add("Scene4", _Scene4);
            _textToSprite.Add("Scene5", _Scene5);
            _textToSprite.Add("Scene6", _Scene6);
            _textToSprite.Add("Scene7", _Scene7);
            _textToSprite.Add("Scene8", _Scene8);
          

            _textToSpriteObject = new Dictionary<string, GameObject>();
        }

        // �摜��z�u����
        public void PutImage(string imageName)
        {
            Sprite image = _textToSprite[imageName];//���͂��ꂽ������ŉ摜��ǂ݂���
            GameObject parentObject = _backgroundObject;//�摜��z�u����panel���Ăяo��
            Vector2 position = new Vector2(0, 0);
            Quaternion rotation = Quaternion.identity;
            Transform parent = parentObject.transform;
            GameObject item = Instantiate(_imagePrefab, position, rotation, parent);//Instantiate(original,position,rotation,parent);
            item.GetComponent<Image>().sprite = image;//�������ꂽ�I�u�W�F�N�g[item]��sprite��image��

            _textToSpriteObject.Add(imageName, item);//Prehab�𑀍삷�邽�߂̎�����item���i�[
            GameManager.Instance.characterManager.PutChara("Man");
        }

        // �摜���폜����
        public void RemoveImage(string imageName)
        {
            Destroy(_textToSpriteObject[imageName]);
        }

        

    }
}