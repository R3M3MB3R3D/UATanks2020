using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropDown : MonoBehaviour
{
    public DropDown dropDown;

    void Start()
    {

    }

    public void ChooseMap(int val)
    {
        if (val == 0)
        {
            GameManager.instance.mapType = 0;
            PlayerPrefs.SetInt("Map", GameManager.instance.mapType);
        }
        else if (val == 1)
        {
            GameManager.instance.mapType = 1;
            PlayerPrefs.SetInt("Map", GameManager.instance.mapType);
        }
        else
        {
            GameManager.instance.mapType = 2;
            PlayerPrefs.SetInt("Map", GameManager.instance.mapType);
        }
    }
}
