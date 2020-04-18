using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneConnect : MonoBehaviour
{
    public SceneSwitcher scene;

    void Start()
    {
        scene = GameManager.instance.scene;
    }

    public void SceneSelect(int sceneToLoad)
    {
        GameManager.instance.scene.Loadany(sceneToLoad);
    }

    public void ExitGame()
    {
        GameManager.instance.scene.ExitGame();
    }
}
