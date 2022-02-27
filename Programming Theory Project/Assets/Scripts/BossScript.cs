using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public bool bossAlive = true;
    private float boundary = 20.0f;
    private Rigidbody bossRB;
    private GameObject player;
    private float speed = 100.0f;


    public GameObject miniBoss;
    private float spawnRange = 9;
    private float spawnInterval = 2.0f;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnMiniBosses", 0, spawnInterval);
        bossRB = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");

    }

    // Update is called once per frame
    void Update()
    {

        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        bossRB.AddForce(lookDirection * speed);

        if (transform.position.y < -boundary | transform.position.z > boundary | transform.position.z < -boundary | transform.position.x > boundary | transform.position.x < -boundary)
        {
            bossAlive = false;
            Destroy(gameObject);
        }

    }

    void SpawnMiniBosses ()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        Instantiate(miniBoss, randomPos, miniBoss.transform.rotation);
    }
}
