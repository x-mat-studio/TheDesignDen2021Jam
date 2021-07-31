using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemy : BaseEnemy
{
    protected override void Start()
    {
        base.Start();
        myType = EnemyType.ENEMY_NORMAL;
        //mySpeed = 2.0f;
    }
}
