using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowEnemy : BaseEnemy
{
    protected override void Start()
    {
        base.Start();
        myType = EnemyType.ENEMY_SLOW;
        //mySpeed = 2.0f;
    }
}
