using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


namespace TextScripts
{
    public class Scripts
    {
        //セリフを取ってくる関数
        public string get_line(float now_pos)
        {
            Debug.Log("Called from Scripts Class");
            return "aiueo";
        }
        //*デバッグ用*セリフをGoogle スプレッドシートから取ってくる関数
        public string load_line()
        {
            return "kakikukeko";
        }
    }
}

