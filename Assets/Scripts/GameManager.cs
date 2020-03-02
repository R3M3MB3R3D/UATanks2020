using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject instantiatedPlayerTank;
    public GameObject playerTankPrefab;

    public GameObject[] instantiatedEnemyTanks;
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

    public void Update()
    {
        if (instantiatedPlayerTank == null)
        {
            SpawnPlayer(RandomSpawnPoint(playerSpawnPoints));
        }
    }

    private GameObject RandomSpawnPoint(List<GameObject> SpawnPoints)
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
        //create an enemy tank and label it in the game manager.
    }
}