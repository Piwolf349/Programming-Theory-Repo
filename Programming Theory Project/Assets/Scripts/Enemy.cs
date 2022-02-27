using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float m_speed = 3.0f;
    public float speed //ENCAPSULATION
    {
        get { return m_speed; }
        set
        {
            if (value < 0.0f)
            {
                Debug.LogError("A positive value is required");
            }
            else
            {
                m_speed = 3.0f;
            }
        }
    }
    private Rigidbody enemyRB;
    private GameObject player;
    private float boundary = 20.0f;

    void Start()
    {
        //ABSTRACTION 
        GetData();
    }

    void Update()
    {
        //ABSTRACTION
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
