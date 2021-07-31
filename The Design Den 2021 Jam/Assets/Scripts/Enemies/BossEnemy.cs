using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : BaseEnemy
{
    public float myBumpForce = 0.0f;
    protected override void Start()
    {
        base.Start();
        myType = EnemyType.ENEMY_BOSS;
        //mySpeed = 2.0f;
    }

    protected override void OnCollisionEnter2D(Collision2D col)
    {
        if (myPlayer == null)
        {
            Debug.LogError("ERROR: BOSS HAS NO PLAYER ASSIGNED");
            return;
        }

        PushPlayer();
        base.OnCollisionEnter2D(col); //keep this line as last line on function. Boss entity can get destroyed here.
    }

    private void PushPlayer()
    {
        Debug.Log("I bumped the player");
        Vector2 myPos = new Vector2(transform.position.x, transform.position.y);
        myPlayer.AddPush(myBumpForce, myPos);
       
    }    
}

