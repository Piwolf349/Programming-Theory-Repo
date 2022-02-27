using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    private Rigidbody enemyRB;
    private GameObject player;
    private float boundary = 20.0f;
    // Start is called before the first frame update
    void Start()
    {
        //Abstracion example
        GetData();
    }

    // Update is called once per frame
    void Update()
    {
        //Other abstraction example
        RushToPlayer();
    }


    public virtual void RushToPlayer()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        enemyRB.AddForce(lookDirection * speed);


        if (transform.position.y < -boundary | transform.position.z > boundary | transform.position.z < -boundary | transform.position.x > boundary | transform.position.x < -boundary)
        {
            Destroy(gameObject);
        }
    }
    protected void GetData()
    {
        enemyRB = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }
}
