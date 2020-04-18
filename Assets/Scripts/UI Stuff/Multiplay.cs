using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Multiplay : MonoBehaviour
{
    public Toggle toggle;

    public void isMultiplayer()
    {
        if (toggle.isOn == true)
        {
            //Debug.Log("multiplayer on");
            GameManager.instance.isMultiplayer = true;

        }
        else
        {
            //Debug.Log("Multiplayer off");
            GameManager.instance.isMultiplayer = false;
        }
    }
}
