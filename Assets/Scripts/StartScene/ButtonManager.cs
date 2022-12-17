using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] GameObject _StartButton;
    [SerializeField] GameObject _NextButton;
    [SerializeField] GameObject _ButtonPanel;
    GameObject  StartButton;
    // Start is called before the first frame update
    void Start()
    {
        GameObject parentObject = _ButtonPanel;//‰æ‘œ‚ğ”z’u‚·‚épanel‚ğŒÄ‚Ño‚·
       

       

        Vector2 position = new Vector2(0, 0);

        Quaternion rotation = Quaternion.identity;
        Transform parent = parentObject.transform;
         StartButton = Instantiate(_StartButton, position, rotation, parent) as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
