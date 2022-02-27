using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBall : Enemy //INHERITANCE
{
    // Start is called before the first frame update
    void Start()
    {
        GetData();
    }

    public override void RushToPlayer()
    {
        speed = 5.0f; //Polymorphism
        base.RushToPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        RushToPlayer();
    }
}
