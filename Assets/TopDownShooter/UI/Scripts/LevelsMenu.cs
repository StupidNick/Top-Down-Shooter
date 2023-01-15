using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsMenu : MonoBehaviour
{
    public void OpenLevelByID(int ID)
    {
        if (SceneManager.sceneCountInBuildSettings - 1 < ID) return;
        
        SceneManager.LoadScene(ID);
    }
    
    
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
