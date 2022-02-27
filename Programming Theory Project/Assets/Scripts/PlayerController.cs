using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 2.0f;
    private Rigidbody playerRB;
    private GameObject focalPoint;

    public bool hasPowerup = false;
    private float powerupStrength = 15.0f;
    public GameObject powerupIndicator;

    public GameObject missile;
    public bool hasPowerupMissile = false;
    public Rigidbody missileRB;
    public float missileSpeed = 30.0f;
    public int enemyCount;
    public GameObject enemy;

    public bool hasPowerupShockwave = false;
    private float shockwaveStrength = 500.0f;
    private float jumpStrength = 10.0f;
    private bool isOnGround = true;
    public ParticleSystem explosionParticle;


    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("FocalPoint");
    }

    // Update is called once per frame
    void Update()
    {
        // Move player when pressing arrow keys
        float forwardInput = Input.GetAxis("Vertical");
        playerRB.AddForce(focalPoint.transform.forward * forwardInput * speed);
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);


        // for PowerupMissile
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] missiles = GameObject.FindGameObjectsWithTag("Missile");
        //missiles homing + destroy redundant missiles
        for (int i = 0; i < missiles.Length; i++)
        {
            if (i > enemies.Length)
            {
                Destroy(missiles[i].gameObject);
            }
            missileRB = missiles[i].GetComponent<Rigidbody>();
            missileRB.AddForce((enemies[i].transform.position - missiles[i].transform.position).normalized * missileSpeed * Time.deltaTime);
            Destroy(missiles[i], 4.0f);
        }
        //missiles creation
        if (Input.GetKeyDown(KeyCode.Space) && hasPowerupMissile && missiles.Length < enemies.Length) 
        {
            for(int i = 0; i < (enemies.Length - missiles.Length); i++)
            {
                Instantiate(missile, ((enemies[i].transform.position + transform.position)/2 + transform.position)/2, missile.transform.rotation); // 

            }
        }

        
        //for powerupShockwave
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && hasPowerupShockwave)
        {
            playerRB.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);

            isOnGround = false;
            StartCoroutine(ShockwaveFallCountdown());
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            powerupIndicator.gameObject.SetActive(true);            
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
        }
        
        if (other.CompareTag("PowerupMissile"))
        {
            hasPowerupMissile = true;
            powerupIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
        }
        if (other.CompareTag("PowerupShockwave"))
        {
            hasPowerupShockwave = true;
            powerupIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
        }
    }

    //Set timer for powerup countdown
    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        hasPowerupMissile = false;
        hasPowerupShockwave = false;
        powerupIndicator.gameObject.SetActive(false);
    }

    IEnumerator ShockwaveFallCountdown()
    {
        yield return new WaitForSeconds(1);
        playerRB.AddForce(Vector3.down * jumpStrength * 4, ForceMode.Impulse);

    }



    private void OnCollisionEnter(Collision collision)
    {
        //Bounce ennemies off when touching them
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);

            enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
        }

        //Trigger shockwave when touching the ground
        if(collision.gameObject.CompareTag("Ground") && hasPowerupShockwave)
        {
            explosionParticle.Play();

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            isOnGround = true;
            for(int i = 0; i< enemies.Length; i++)
            {
                Vector3 awayFromPlayer = (enemies[i].transform.position - transform.position).normalized;
                Rigidbody enemyRigidbody = enemies[i].gameObject.GetComponent<Rigidbody>();
                float distanceEnemyPlayer = Mathf.Sqrt(Mathf.Pow((enemies[i].transform.position.x - transform.position.x),2) + Mathf.Pow((enemies[i].transform.position.z - transform.position.z), 2));
                enemyRigidbody.AddForce((awayFromPlayer * shockwaveStrength)/distanceEnemyPlayer, ForceMode.Impulse);
            }
        }
    }
}
