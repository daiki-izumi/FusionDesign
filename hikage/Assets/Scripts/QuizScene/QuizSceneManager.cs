using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using NovelGame;
public class QuizSceneManager : MonoBehaviour
{

    
   
    public void OnClickStartButton()
    {
        UserScriptManager user = new UserScriptManager();
        user.GetNext = 1;
        SceneManager.LoadScene("StoryScene");
    }
}
