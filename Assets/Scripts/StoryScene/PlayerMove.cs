using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private GameObject shock;
    private Transform transformShock;
    //public 

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        shock = GameObject.Find("Shock");
        transformShock = shock.transform.GetComponent<Transform>();
        transformShock.localScale = new Vector3(0, 0, 0);
        Debug.Log($"Shock {shock}");
        //shock.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Car"))
        {
            other.gameObject.SetActive(false);
            StartCoroutine(show_shock());
        }
    }    
    //衝突した画像を表示するコルーチン
    public IEnumerator show_shock()
    {
        //shock.SetActive(true);
        transformShock.localScale = new Vector3(1, 1, 1);
        yield return new WaitForSeconds(1.0f);
        //shock.SetActive(false);
        transformShock.localScale = new Vector3(0, 0, 0);
    }

}
