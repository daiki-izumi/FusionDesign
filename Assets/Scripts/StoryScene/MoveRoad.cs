using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class MoveRoad : MonoBehaviour
{
    private Transform roadTransform;
    private string spath;
    // Start is called before the first frame update
    void Start()
    {
        roadTransform = transform.GetComponent<Transform>();
        //ÉVÅ[ÉìÇÃì«Ç›çûÇ›
        string nowscene = SceneManager.GetActiveScene().name;
        Debug.Log($"now scene is {nowscene}");
        string c = "Story";
        string Chap = nowscene.Substring(0, 3);
        string n = nowscene.Substring(4, 1);
        int ni = int.Parse(n)+1;
        spath = Chap + "_" + ni.ToString() + "_1_" + "Game";
        spath = Chap + "_" + ni.ToString() + "_" + "Story";
        Debug.Log(spath);
    }

    // Update is called once per frame
    void Update()
    {
        if (roadTransform.position.y < -230.0f)
        {
            Debug.Log("End");
            // SceneManager.LoadScene(spath);
            SceneManager.LoadScene("Mid_6_Story");
        }
        else
        {
            roadTransform.position -= new Vector3(0, 0.01f, 0);
            Debug.Log($"Y pos {roadTransform.position.y}");
        }
    }
}
