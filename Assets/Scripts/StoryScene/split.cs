using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections;

public class split : MonoBehaviour
{
    public bool start = false;
    public bool stop = false;
    // Start is called before the first frame update
    void Start()
    {

        //�؂蕪���镶����
        string lines = "����ɂ���.����΂��./��������悤./";
        //��������́u�B�v�̌�
        string search = "/";
        
        int num = lines.Length - lines.Replace(search, "").Length;
        //lines = lines.Replace(search, "");
        Debug.Log($"lines is {lines}, num is {num}");
        //�o�͂��镶����
        string[] line_splited;
        //
        int hoge;
        if (num != 0)
        {
            //�z��
            line_splited = new string[num];
            hoge = 0;
            for (int i = 0; i < num; i++)
            {
                int search_end_num = lines.IndexOf(search) + 1;
                //string bf = lines.Substring(0, search_end_num);
                line_splited[i] = lines.Substring(0, search_end_num);
                string bf = lines.Substring(0,line_splited[i].Length-1);
                line_splited[i] = bf;
                lines = lines.Substring(line_splited[i].Length+1);
                //Debug.Log($"splited {line_splited[i]}, rest line is {lines}, bf is {bf}");
            }
        }
        else
        {
            //�u�B�v��������ɂȂ��ꍇ
            line_splited = new string[1];
            hoge = 1;
            line_splited[0] = lines;

        }
        for (int i = 0; i < line_splited.Length; i++)
        {
            Debug.Log($"Line is {line_splited[i]}");
        }
        //string line_enterd = new string[num];
        string newLine = Environment.NewLine;
        for (int i = 0; i < line_splited.Length; i++)
        {
            line_splited[i] = line_splited[i].Replace(".", "." + newLine);

        }

    }
    public void OnStart()
    {
        if (!start)
        {
            Debug.Log("Start");
            stop = true;
            StartCoroutine(DelayLog());
        }
    }
    public void OnStop()
    {
        if (stop)
        {
            stop = false;
        }
        else
        {
            stop = true;
        }
        
        Debug.Log("Stop");
        //return stop;
    }
    IEnumerator DelayLog()
    {
        Debug.Log("5�b��Ƀ��O���\������܂�");
        //yield return new WaitUntil(() => stop);
        while (stop)
        {
            Debug.Log("Waiting...");
            yield return null;
        }
        yield return new WaitForSeconds(5.0f);
        Debug.Log("Here!");
    }


}
