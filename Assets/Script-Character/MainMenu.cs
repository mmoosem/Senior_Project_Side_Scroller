using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public bool loading = false;
    public string NewGameStart;

    public void NewGame()
    {
        LoadStatic.LoadChar = false;
        SceneManager.LoadScene(0);
    }
    public void LoadGame()
    {
        LoadStatic.LoadChar = true;
        SceneManager.LoadScene(0);
        
    }
    public void Options()
    {

    }
    public void QuitGame()
    {
        Application.Quit();
    }

}