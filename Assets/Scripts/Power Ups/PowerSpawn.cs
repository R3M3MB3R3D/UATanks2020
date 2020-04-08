using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSpawn : MonoBehaviour
{
    public float spawnTimeCurrent;
    public float spawnTimeMax;
    public GameObject powerUp;

    void Start()
    {
        spawnTimeMax = 15.0f;
        spawnTimeCurrent = spawnTimeMax;
    }

    void Update()
    {
        spawnTimeCurrent -= Time.deltaTime;
        if (spawnTimeCurrent <= 0)
        {
            GameObject powerup = Instantiate(powerUp, transform.position, transform.rotation) as GameObject;
            resetSpawnTime();
        }
    }


    public void resetSpawnTime()
    {
        spawnTimeCurrent = spawnTimeMax;
    }
}