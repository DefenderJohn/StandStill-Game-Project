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

    private void spawnEnemies()
    {
        destinationIndex = Random.Range(0, destinations.Count);
        sourceIndex = Random.Range(0, spawnPositions.Count);
        Invoke("invokeSpawn", singleEnemySpawnDelay);
    }

    private void invokeSpawn()
    {
        GameObject newEnemy = Instantiate<GameObject>(enemyPrefab, spawnPositions[sourceIndex].transform);
        newEnemy.gameObject.GetComponent<EnemyController>().movePos = destinations[destinationIndex].transform.position;

        if (ActiveEnemies.Count < waveEnemyNumbers)
        {
            Invoke("invokeSpawn", singleEnemySpawnDelay);
        }
        else
        {
            return;
        }
    }

    private bool checkIFAllEnemyDead()
    {
        if (ActiveEnemies.Count == 0)
        {
            return true;
        }
        return false;
    }
}

/*
 * I think the premise is gripping. I have always been a fan of castle defense, so I went into the experience
 * understanding the basic premise. I think a simple controls page are necessary, although I commend the controls
 * on being easy to pick up once I was told their purpose. I'm impressed by the tank pathfinding, although more HUD
 * or something similar is needed to help the player manage their military. When engaged in combat, I found myself
 * confused as to how far away I could be and still fire at the enemry. The game is ambitious, but after taking a
 * quick peek at your source code and understanding your approach to programming the game, I think it's still doable.
 * As it stands, to me, I got the understanding this is a vertical slice of the game, where core mechanics are implemented.
 * I'll anticipate its completion ! An alert system or similar to the player if their base is being attacked would allow the 
 * player to have better board management without having to refer a minimap. 
 */