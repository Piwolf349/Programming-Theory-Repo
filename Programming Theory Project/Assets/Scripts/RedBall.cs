using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBall : Enemy //INHERITANCE
{
    void Start()
    {
        GetData();
    }

    public override void RushToPlayer()
    {
        speed = 2.0f; //POLYMORPHISM
        base.RushToPlayer();
    }

    void Update()
    {
        RushToPlayer();
    }
}

