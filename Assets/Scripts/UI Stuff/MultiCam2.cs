using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiCam2 : MonoBehaviour
{
    public Camera vision;

    private void Start()
    {
        vision = GetComponent<Camera>();
        checkMode();
    }

    void checkMode()
    {
        if (GameManager.instance.players > 1)
        {
            vision.rect = new Rect(0, 0, 1, 0.5f);
        }
    }

}
