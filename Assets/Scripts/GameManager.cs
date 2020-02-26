using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("There can be only one GameManager.");
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
}