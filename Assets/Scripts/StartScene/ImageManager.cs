using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace StartScene
{
    public class ImageManager : MonoBehaviour
    {

        [SerializeField] Sprite _JPLIFE;
        [SerializeField] GameObject _ImagePanel;
        [SerializeField] GameObject _ImagePrehab;

        GameObject Title;
        // Start is called before the first frame update
        void Start()
        {
            GameObject parentObject = _ImagePanel;//‰æ‘œ‚ğ”z’u‚·‚épanel‚ğŒÄ‚Ño‚·
            Vector2 position = new Vector2(0, 2);
            Quaternion rotation = Quaternion.identity;
            Transform parent = parentObject.transform;


            Title = Instantiate(_ImagePrehab, position, rotation, parent) as GameObject;
            Title.GetComponent<Image>().sprite = _JPLIFE;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}