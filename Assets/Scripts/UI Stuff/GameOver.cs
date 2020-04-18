using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Text p1Score;
    public Text p2Score;
    public Text hiScore;

    void Start()
    {
        p1Score.text = "Player One - " + GameManager.instance.p1Score.ToString() + ".";
        p2Score.text = "Player Two - " + GameManager.instance.p2Score.ToString() + ".";
        hiScore.text = "High Score - " + GameManager.instance.hiScore.ToString() + ".";
    }
}
