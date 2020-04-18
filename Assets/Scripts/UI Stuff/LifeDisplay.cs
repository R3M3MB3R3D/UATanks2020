using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeDisplay : MonoBehaviour
{
    public TankData data;
    public Text text;

    public void Start()
    {
        data = GetComponentInParent<TankData>();
        text = GetComponent<Text>();
    }

    public void Update()
    {
        text.text = "Lives " + data.lives.ToString() + ".";
    }
}