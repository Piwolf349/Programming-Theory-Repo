using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBehaviour : MonoBehaviour
{
    // public GameObject enemy;
    private float missileStrength = 50.0f;
    private float boundary = 20.0f;
    public AudioClip crashSound;
    private AudioSource missileAudio;
    public ParticleSystem explosionParticle;


    // Start is called before the first frame update
    void Start()
    {
        missileAudio = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {


        if (transform.position.x < -boundary | transform.position.x > boundary | transform.position.z > boundary | transform.position.z < -boundary)
        {
            Destroy(gameObject);
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromMissile = (collision.gameObject.transform.position - transform.position);


            enemyRigidbody.AddForce(awayFromMissile * missileStrength, ForceMode.Impulse);
            missileAudio.PlayOneShot(crashSound, 2.0f);
            explosionParticle.Play();

            Destroy(gameObject);

        }
    }
}
