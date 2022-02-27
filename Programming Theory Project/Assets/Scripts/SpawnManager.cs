using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject[] powerupPrefabs;
    public GameObject boss;
    private float spawnRange = 9;
    private int enemyCount;
    private int waveNumber = 1;
    private bool gameOver;
    private int bossWave = 5;
    private int bossCount;

    //Inheritance is managed with the enemy class managing both GreenBall and RedBall scripts
    //Polymorphism is done with GreenBall and RedBall who give a different speed.
    //Encapsulation (protecting data): TBD, ptet en rajoutant un menu où le joueur ajoute son nom ? Et on le get?
    //Abstraction is done in the Enemy script whitin the start and update functions
    
    // Start is called before the first frame update
    void Start()
    {
        gameOver = GameObject.Find("Player").GetComponent<PlayerController>().gameOver;
    }

    // Update is called once per frame
    void Update()
    {
        DefineWaveTypeAndSpawn();
    }

    void DefineWaveTypeAndSpawn()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        bossCount = FindObjectsOfType<BossScript>().Length;

        if (enemyCount == 0 && waveNumber != bossWave && !gameOver && bossCount == 0)
        {
            SpawnEnemyWave(waveNumber);
            waveNumber++;
        }
        else if (enemyCount == 0 && waveNumber == bossWave && !gameOver)
        {
            SpawnBossWave();
            waveNumber++;
        }
    }

    void SpawnBossWave()
    {
        Instantiate(boss, new Vector3(0, 1.37f, 0), boss.transform.rotation);
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            int index = Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[index], GenerateSpawnPosition(), enemyPrefabs[index].transform.rotation);
        }

        int indexPowerup = Random.Range(0, powerupPrefabs.Length);
        Instantiate(powerupPrefabs[indexPowerup], GenerateSpawnPosition(), powerupPrefabs[indexPowerup].transform.rotation);

    }


    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);

        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);

        return randomPos;
    }       

}
