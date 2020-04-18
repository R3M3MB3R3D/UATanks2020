using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Create a GameManager to handle most aspects of the game
    public static GameManager instance;
    public GameObject levelGameObject;
    public InputControl control;
    public SceneSwitcher scene;

    public bool isMultiplayer;
    public int mapType;
    public int players;
    public int enemies;

    //Create a variable for AI to recognize player.
    public GameObject[] playerTankPrefab;
    public List<GameObject> instantiatedPlayerTanks;

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
    public int p1Score;
    public int p2Score;
    public int hiScore;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);

        scene = GetComponent<SceneSwitcher>();
        hiScore = PlayerPrefs.GetInt("HighScore");
        mapType = PlayerPrefs.GetInt("Map");
    }

    public void Update()
    {
        if (isMultiplayer == true)
        {
            players = 2;
        }
        else
        {
            players = 1;
        }

        if (instantiatedPlayerTanks.Count == 0 && scene.getCurrentScene() == "Game")
        {
            scene.LoadOver();
        }
    }

    public GameObject RandomSpawnPoint(List<GameObject> SpawnPoints)
    {
        // get a random spawn point from inside our list of spawn points.
        int spawnToGet = UnityEngine.Random.Range(0, SpawnPoints.Count - 1);
        return SpawnPoints[spawnToGet];
    }

    public void StartGame()
    {
        for (int i = 0; i < players; ++i)
        {
            SpawnPlayer(RandomSpawnPoint(playerSpawnPoints), i);
        }
        for (int e = 0; e < enemies; ++e)
        {
            SpawnEnemy(RandomSpawnPoint(enemySpawnPoints));
        }
    }

    public void SpawnPlayer(GameObject spawnPoint, int player)
    {
        //create a player tank and label it in the game manager.
        instantiatedPlayerTanks.Add(Instantiate(playerTankPrefab[player], spawnPoint.transform.position, Quaternion.identity));
    }

    public void SpawnEnemy(GameObject spawnPoint)
    {
        int index = Random.Range(0, enemyTankPrefabs.Length);
        GameObject instantiatedEnemyTank = Instantiate(enemyTankPrefabs[index], RandomSpawnPoint(enemySpawnPoints).transform.position, Quaternion.identity);
        instantiatedEnemyTanks.Add(instantiatedEnemyTank);
    }
}