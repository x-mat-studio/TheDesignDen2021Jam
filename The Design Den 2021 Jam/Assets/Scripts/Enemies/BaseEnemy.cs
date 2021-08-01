using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public float mySpeed = 0;

    public int myLife = 1;
    Vector2 myDirection = Vector2.zero;
    Color myColor = Color.black;
    Color myOutlineColor = Color.red;
    public SpriteRenderer mySprite = null;
    public SpriteRenderer myOutlineSprite = null;
    public PlayerController myPlayer = null;
    AudioSource entityHit = null;
    bool thereIsPlayer = true;
    public ParticleSystem deathParticle = null;

    [Range(0.0f, 100.0f)]
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
        SetColorFromSprites();

        if (myPlayer == null)
        {
            thereIsPlayer = false;
            Debug.LogError("NO PLAYER ASSIGNED TO THIS ENEMY");
        }

        GameObject audioBank = GameObject.FindWithTag("AudioMixer");

        if (audioBank != null)
            entityHit = audioBank.transform.Find("entityHit").GetComponent<AudioSource>();

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Rotate();
        lastFramePos = new Vector2(transform.position.x, transform.position.y);
        Move();

        if (myPlayer != null)
        {
            if (Mathf.Abs(Vector3.Distance(transform.position, myPlayer.transform.position)) > 100)
            {
                Suicide();
            }
        }
    }

    private void Suicide() 
    {
        Destroy(gameObject); //pops out of existence offscreen
    }

    void SetColorFromSprites()
    {
        if (mySprite != null)
            myColor = mySprite.color;
        if (myOutlineSprite != null)
            myOutlineColor = myOutlineSprite.color;
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

        Vector3 direction = gameObject.transform.position - myPlayer.transform.position;
        direction.z = 0.0f;

        direction.Normalize();

        float angle = Vector3.Angle(direction, new Vector3(1.0f, 0.0f, 0.0f));

        float upperAngle = Vector3.Angle(direction, new Vector3(0.0f, 1.0f, 0.0f));
        float downAngle = Vector3.Angle(direction, new Vector3(0.0f, -1.0f, 0.0f));

        if (downAngle < upperAngle)
            angle = -angle;

        BloodSplat.bloodSplatHolder.GetComponent<BloodSplat>().CreateSplat(gameObject.transform.position, angle,
                                            new Vector3(myColor.r, myColor.g, myColor.b));
        //DeathParticle

        if (deathParticle != null)
        {
            deathParticle.Play();
        }

        //Enemy dies means spin goes brbrbr
        
        myPlayer.GetComponent<SwordController>().ChangeRotationSudden(Mathf.FloorToInt(mySpeed / 2));

        //kill this mofo
        Debug.Log("I got killed");
        StaticGlobalVars.totalKills++;
        

        
        Destroy(gameObject); //keep this as last line of the code.cant destroy it directly or particle system stops doing particles


    }


    void Rotate()
    {
        float speed = new Vector2(transform.position.x - lastFramePos.x, transform.position.y - lastFramePos.y).magnitude / Time.deltaTime;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z - speed * rotSpeedMultiplier * Time.deltaTime);
    }

    void Move()
    {
        if (!thereIsPlayer)
            return;

        myDirection = new Vector2(transform.position.x - myPlayer.transform.position.x, transform.position.y - myPlayer.transform.position.y);
        myDirection.Normalize();
        myDirection *= (mySpeed * Time.deltaTime);
        transform.position += new Vector3(myDirection.x, myDirection.y, 0.0f);
    }



    public virtual void TakeDamage()
    {
        if (entityHit != null)
            entityHit.Play();

        else
            Debug.Log("Audio Bank isn't in the scene!!");

        myLife -= 1;
        if (myLife == 0)
        {
            GameObject manager = GameObject.Find("SceneManager");

            if (gameObject.tag != "Boss")
            {

                if (manager != null) { manager.GetComponent<SceneManagement>().enemyDead = true; }
                else { Debug.Log("There is no Scene Manager in your scene. Manage it."); }
                Die();

            }

            else
            {
                if (manager != null) { manager.GetComponent<SceneManagement>().bossDead = true; }
                else { Debug.Log("There is no Scene Manager in your scene. Manage it."); }
            }

        }
    }
}