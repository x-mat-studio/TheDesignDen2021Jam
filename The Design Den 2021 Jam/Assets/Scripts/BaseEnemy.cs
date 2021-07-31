using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    float mySpeed = 0;
    int myLife = 1;
    Vector2 myDirection = Vector2.zero;
    public Color myColor = Color.black;
    public Color myOutlineColor = Color.red;
    public SpriteRenderer mySprite = null;
    public SpriteRenderer myOutlineSprite = null;
    public PlayerController myPlayer = null;
    bool thereIsPlayer = true;
   

    public enum EnemyType 
    {
        ENEMY_SLOW,
        ENEMY_NORMAL,
        ENEMY_FAST,
        ENEMY_BOSS,
    }


    // Start is called before the first frame update
    void Start()
    {
         if (myPlayer == null)
        {
            thereIsPlayer = false;
            Debug.LogError("NO PLAYER ASSIGNED TO THIS ENEMY");
        }
    }

    // Update is called once per frame
    void Update()
    {
      Move();  
    }

    void SetSpriteColor(Color inside, Color outline)
    {
        this.myColor = inside;
        this.myOutlineColor = outline;

        if (mySprite != null)
        {
        mySprite.color = this.myColor;
        }

        if (myOutlineSprite != null)
        {
        myOutlineSprite.color = this.outline;
        }
    }

    void Die()
    {
        //Bloodsplat
        //Sound
        Debug.Log("I got killed");
        Destroy(this); //keep this as last line of the code   
    }

    void Move()
    {
        if (!thereIsPlayer)
        {return;}
     
        this.myDirection = Vector2(this.position, myPlayer.transform.position);
        this.myDirection.Normalize();
        this.myDirection *= this.mySpeed;
        this.position += this.myDirection;
    }
    
   virtual void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject == myPlayer.GameObject)
        {
           TakeDamage();
        }
    }

    void TakeDamage()
    {
        this.myLife -= 1;
        if(this.myLife == 0)
        {
           Die();
        }
    }
}