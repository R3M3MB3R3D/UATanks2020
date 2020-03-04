using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject levelGameObject;

    public GameObject instantiatedPlayerTank;
    public GameObject playerTankPrefab;

    public List<GameObject> instantiatedEnemyTanks;
    public GameObject[] enemyTankPrefabs;

    public List<GameObject> playerSpawnPoints;
    public List<GameObject> enemySpawnPoints;


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