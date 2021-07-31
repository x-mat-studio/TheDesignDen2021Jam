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
        NONE
    }

    public EnemyType myType { get; protected set; } = EnemyType.NONE;

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
        myColor = inside;
        myOutlineColor = outline;

        if (mySprite != null)
        {
            mySprite.color = myColor;
        }

        if (myOutlineSprite != null)
        {
            myOutlineSprite.color = outline;
        }
    }

    void Die()
    {
        //Bloodsplat
        //Sound
        Debug.Log("I got killed");
        Destroy(gameObject); //keep this as last line of the code   
    }

    void Move()
    {
        if (!thereIsPlayer)
            return;

        myDirection = new Vector2(transform.position.x- myPlayer.transform.position.x, transform.position.y - myPlayer.transform.position.y);
        myDirection.Normalize();
        myDirection *= mySpeed;
        transform.position += new Vector3(myDirection.x,myDirection.y,0.0f);
    }

    protected virtual void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject == myPlayer.gameObject)
        {
            TakeDamage();
        }
    }

    void TakeDamage()
    {
        myLife -= 1;
        if (myLife == 0)
        {
            Die();
        }
    }
}