using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//���Z�[�u���[�h���g�����߂̂��܂��Ȃ�
using fileSL;

public class testFileSL : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //���N���X�̐錾
        //fileSL.fileSaveLoad �ϐ��� = new fileSL.fileSaveLoad();
        fileSL.fileSaveLoad ld = new fileSL.fileSaveLoad();
        //�Z�[�u
        //�����ɕۑ��������摜�t�@�C����
        ld.Save("Chara_male.png");
        //���[�h
        //�����ɂ͉�������Ȃ��B�Ԃ�l�ɂ͉摜�t�@�C�������Ԃ��Ă���B
        //�Z�[�u�����f�[�^���Ȃ���ԂŃ��[�h��������f�t�H���g��"Chara_female.png"���Ԃ��Ă���B
        string l = ld.Load();
        Debug.Log($"Load Result is {l}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
