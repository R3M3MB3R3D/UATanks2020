using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Since all the 'data' concerning the gameObject comes from
//TankData and all 'functions' for that 'data' come from
//other scripts, we make the functions require the data.
[RequireComponent(typeof(TankData))]

public class TankLife : MonoBehaviour
{
    //creating variables to attach scripts and objects.
    public TankData data;

    void Awake()
    {
        //attaching scripts and objects.
        data = GetComponent<TankData>();
    }

    void Update()
    {
        //This is so that we never have more health than we are
        //allowed to have, this can happen with the health pickup.
        if (data.tankCurrentLife > data.tankMaxLife)
        {
            data.tankCurrentLife = data.tankMaxLife;
        }

        //This is so that when the tank falls below 1 health, it is
        //destroyed and the score is updated for the destroyer.
        if (data.tankCurrentLife <= 0)
        {
            if (data.lives > 0)
            {
                //instead of destroying the players we will reset them elsewhere
                //as long as they have lives to spare.
                if (data.playerNumber == 0 || data.playerNumber == 1)
                {
                    data.lives -= 1;
                    data.tankCurrentLife = data.tankMaxLife;
                    transform.position = GameManager.instance.RandomSpawnPoint(GameManager.instance.playerSpawnPoints).transform.position;
                }
                else
                {
                    Destroy(this.gameObject);
                }
            }
            else
            {
                if (data.playerNumber == 0)
                {
                    GameManager.instance.p1Score = data.score;
                }
                if (data.playerNumber == 1)
                {
                    GameManager.instance.p2Score = data.score;
                }
                if (data.score > GameManager.instance.hiScore)
                {
                    GameManager.instance.hiScore = data.score;
                    PlayerPrefs.SetInt("HighScore", data.score);
                }
                GameManager.instance.instantiatedPlayerTanks.Remove(this.gameObject);
                Destroy(this.gameObject);
            }
        }
    }
}