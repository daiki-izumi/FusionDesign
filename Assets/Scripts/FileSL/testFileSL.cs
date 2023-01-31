using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//↓セーブロードを使うためのおまじない
using fileSL;

public class testFileSL : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //↓クラスの宣言
        //fileSL.fileSaveLoad 変数名 = new fileSL.fileSaveLoad();
        fileSL.fileSaveLoad ld = new fileSL.fileSaveLoad();
        //セーブ
        //引数に保存したい画像ファイル名
        ld.Save("Chara_male.png");
        //ロード
        //引数には何も入れない。返り値には画像ファイル名が返ってくる。
        //セーブしたデータがない状態でロードをしたらデフォルトの"Chara_female.png"が返ってくる。
        string l = ld.Load();
        Debug.Log($"Load Result is {l}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
