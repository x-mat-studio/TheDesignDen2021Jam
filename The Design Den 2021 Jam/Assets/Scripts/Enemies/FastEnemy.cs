using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastEnemy : BaseEnemy
{
    protected override void Start()
    {
        base.Start();
        myType = EnemyType.ENEMY_FAST;
        //mySpeed = 2.0f;
    }
}

