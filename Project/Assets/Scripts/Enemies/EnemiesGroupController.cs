using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesGroupController : MonoBehaviour
{
    public List<GameObject> destinations;
    public List<GameObject> spawnPositions;
    public float enemyWaveDelay;
    private float backupWaveDelay;
    public float checkingDelay;
    private float backupCheckingDelay;
    public float singleEnemySpawnDelay;
    private float backupSingleEnemySpawnDelay;
    public HashSet<GameObject> ActiveEnemies;
    public int waveEnemyNumbers;
    public GameObject enemyPrefab;
    int destinationIndex;
    int sourceIndex;
    // Start is called before the first frame update
    void Start()
    {
        backupCheckingDelay = checkingDelay;
        backupWaveDelay = enemyWaveDelay;
    }

    // Update is called once per frame
    void Update()
    {
        checkingDelay -= Time.deltaTime;
        if (checkingDelay <= 0)
        {
            checkingDelay = -1.0f;
            if (checkIFAllEnemyDead())
            {
                enemyWaveDelay -= Time.deltaTime;
                if (enemyWaveDelay <= 0)
                {
                    checkingDelay = backupCheckingDelay;
                    enemyWaveDelay = backupWaveDelay;
                    spawnEnemies();
                }

            }
        }
    }

    private void spawnEnemies() {
        destinationIndex = Random.Range(0, destinations.Count);
        sourceIndex = Random.Range(0, spawnPositions.Count);
        Invoke("invokeSpawn",singleEnemySpawnDelay);
    }

    private void invokeSpawn() {
        ActiveEnemies.Add(Instantiate<GameObject>(enemyPrefab, spawnPositions[sourceIndex].transform));
        
        if (ActiveEnemies.Count < waveEnemyNumbers)
        {
            Invoke("invokeSpawn", singleEnemySpawnDelay);
        }
        else {
            return;
        }
    }
        
    private bool checkIFAllEnemyDead() {
        if (ActiveEnemies.Count == 0)
        {
            return true;
        }
        return false;
    }
}
