using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public GameObject gameOverText;
    public GameObject restartButton;

    public GameObject[] enemyPrefabs;
    public GameObject[] powerupPrefabs;
    public GameObject boss;
    private float spawnRange = 9;
    private int enemyCount;
    private int waveNumber = 1;

    private Transform playerTransform;
    private float lowerBound = -10.0f;

    private int bossWave = 5;
    private int bossCount;

    
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        DefineWaveTypeAndSpawn();
        if (playerTransform.position.y < lowerBound)
        {
            GameOver();
        }
    }



    void DefineWaveTypeAndSpawn()
    {
        UpdateScore();
        enemyCount = FindObjectsOfType<Enemy>().Length;
        bossCount = FindObjectsOfType<BossScript>().Length;

        if (enemyCount == 0 && waveNumber != bossWave && bossCount == 0)
        {
            SpawnEnemyWave(waveNumber);
            waveNumber++;
        }
        else if (enemyCount == 0 && waveNumber == bossWave)
        {
            SpawnBossWave();
            waveNumber++;
        }
    }
    void UpdateScore()
    {
        scoreText.text = MenuManager.playerName + "'s score : " + (waveNumber - 1);
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

    public void GameOver()
    {
        gameOverText.SetActive(true);
        restartButton.SetActive(true);
        Time.timeScale = 0;
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

}
