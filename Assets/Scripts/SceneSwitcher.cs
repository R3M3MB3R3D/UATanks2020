using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(1);    
    }

    public void LoadOver()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadOptions()
    {
        SceneManager.LoadScene(3);
    }

    public void Loadany(int sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public string getCurrentScene()
    {
        return SceneManager.GetActiveScene().name;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
