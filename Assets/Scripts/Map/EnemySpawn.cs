using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        GameManager.instance.enemySpawnPoints.Add(this.gameObject);
    }

    private void OnDestroy()
    {
        GameManager.instance.enemySpawnPoints.Remove(this.gameObject);
    }
}
