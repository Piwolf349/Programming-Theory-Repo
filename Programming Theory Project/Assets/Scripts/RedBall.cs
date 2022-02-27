using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBall : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        GetData();
    }

    public override void RushToPlayer()
    {
        speed = 2.0f;
        base.RushToPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        RushToPlayer();
    }
}

