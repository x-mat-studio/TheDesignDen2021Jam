using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public float mySpeed = 0;
  
    public int myLife = 1;
    Vector2 myDirection = Vector2.zero;
    public Color myColor = Color.black;
    public Color myOutlineColor = Color.red;
    public SpriteRenderer mySprite = null;
    public SpriteRenderer myOutlineSprite = null;
    public PlayerController myPlayer = null;
    bool thereIsPlayer = true;

    [Range(0.0f,100.0f)]
    public float rotSpeedMultiplier = 1.0f;

    Vector2 lastFramePos = Vector2.zero;
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
    protected virtual void Start()
    {
        if (myPlayer == null)
        {
            thereIsPlayer = false;
            Debug.LogError("NO PLAYER ASSIGNED TO THIS ENEMY");
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Rotate();
        lastFramePos = new Vector2(transform.position.x, transform.position.y);
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


    void Rotate()
    {
        float speed = new Vector2(transform.position.x - lastFramePos.x, transform.position.y - lastFramePos.y).magnitude/Time.deltaTime;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z - speed * rotSpeedMultiplier * Time.deltaTime);
    }

    void Move()
    {
        if (!thereIsPlayer)
            return;

        myDirection = new Vector2(transform.position.x- myPlayer.transform.position.x, transform.position.y - myPlayer.transform.position.y);
        myDirection.Normalize();
        myDirection *= (mySpeed * Time.deltaTime);
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