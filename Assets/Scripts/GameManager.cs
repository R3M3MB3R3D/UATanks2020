using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Create a GameManager to handle most aspects of the game
    public static GameManager instance;
    public GameObject levelGameObject;

    //Create a variable for AI to recognize player.
    public GameObject instantiatedPlayerTank;
    public GameObject playerTankPrefab;

    //Create a list of enemies.
    public GameObject[] enemyTankPrefabs;
    public List<GameObject> instantiatedEnemyTanks;

    //Create a list of player spawn points.
    public List<GameObject> playerSpawnPoints;
    //Create a list of enemy spawn points.
    public List<GameObject> enemySpawnPoints;
    //Create a list of waypoints.
    public List<GameObject> waypoints;
    //Create a list of powerups.
    public List<GameObject> upgradeSpawns;


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

    public GameObject RandomSpawnPoint(List<GameObject> SpawnPoints)
    {
        // get a random spawn point from inside our list of spawn points.
        int spawnToGet = UnityEngine.Random.Range(0, SpawnPoints.Count - 1);
        return SpawnPoints[spawnToGet];
    }

    public void SpawnPlayer(GameObject spawnPoint)
    {
        //create a player tank and label it in the game manager.
        instantiatedPlayerTank = Instantiate(playerTankPrefab, spawnPoint.transform.position, Quaternion.identity);
    }

    public void SpawnEnemy(GameObject spawnPoint)
    {
        //create as many enemy tanks as we have identities for them
        //Create enemy tanks and label them in the game manager.
        for (int i = 0; i < enemyTankPrefabs.Length; ++i)
        {
            GameObject instantiatedEnemyTank = Instantiate(enemyTankPrefabs[i], RandomSpawnPoint(enemySpawnPoints).transform.position, Quaternion.identity);
            instantiatedEnemyTanks.Add(instantiatedEnemyTank);
        }
    }
}