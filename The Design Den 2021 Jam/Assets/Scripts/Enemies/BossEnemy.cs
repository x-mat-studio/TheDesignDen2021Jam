using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : BaseEnemy
{
    public float myBumpForce = 0.0f;
    public GameObject EnemySlow = null;
    public GameObject EnemyNormal = null;
    public GameObject EnemyFast = null;
    AudioSource enemySpawnSound = null;
    AudioSource bossDeath = null;
    float counterSpawn = 0.0f;
    public float spawnTimer = 100.0f; //higher = takes more time to spawn 
    public int rate100SpawnNormal = 100; //percentatge
    public int rate100SpawnSlow = 100;
    public int rate100SpawnFast = 100;

    [Range(0.0f,100.0f)]
    public float radOfSpawnMin = 15.0f;
    [Range(0.0f, 100.0f)]
    public float radOfSpawnMax = 20.0f;


    protected override void Start()
    {
        base.Start();
        myType = EnemyType.ENEMY_BOSS;
        //mySpeed = 2.0f;


        if (EnemySlow == null) { Debug.Log("Slow enemy still unasigned to boss"); }
        if (EnemyNormal == null) { Debug.Log("Normal enemy still unasigned to boss"); }
        if (EnemyFast == null) { Debug.Log("Fast enemy still unasigned to boss"); }

        GameObject audioBank = GameObject.FindWithTag("AudioMixer");
        enemySpawnSound = audioBank.transform.Find("enemySpawn").GetComponent<AudioSource>();
        bossDeath = audioBank.transform.Find("bossDeath").GetComponent<AudioSource>();

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
    protected override void Update()
    {
        base.Update();
        if (counterSpawn < spawnTimer)
        {
            counterSpawn += 1 * Time.deltaTime;
        }
        else
        {
            counterSpawn = 0;
            SpawnEnemy();
        }


    }
    private void SpawnEnemy()
    {
        float aux = Random.Range(0, 100);   //TODO: make them spawn inside the radious of the boss (random inside it), not inside the fkn boss

        GameObject auxG = null;
        Vector2 auxPos= Vector2.zero;    

        if (aux < rate100SpawnFast)
        {
            auxG = Instantiate(EnemyFast);

            auxPos = RandomSpawnpoint();
            auxG.transform.position = new Vector3(transform.position.x + auxPos.x, transform.position.y + auxPos.y, 0);

            auxG.GetComponent<BaseEnemy>().myPlayer = myPlayer;
        }

        if (aux < rate100SpawnNormal)
        {
            auxG = Instantiate(EnemyNormal);

            auxPos = RandomSpawnpoint();
            auxG.transform.position = new Vector3(transform.position.x + auxPos.x, transform.position.y + auxPos.y, 0);

            auxG.GetComponent<BaseEnemy>().myPlayer = myPlayer;
        }

        if (aux < rate100SpawnSlow)
        {
            auxG = Instantiate(EnemySlow);

            auxPos = RandomSpawnpoint();
            auxG.transform.position = new Vector3(transform.position.x + auxPos.x, transform.position.y + auxPos.y, 0);

            auxG.GetComponent<BaseEnemy>().myPlayer = myPlayer;
        }

        if (enemySpawnSound != null) { enemySpawnSound.Play(); }
        else { Debug.Log("Audio Bank isn't in the scene!!"); }

    }
    private void PushPlayer()
    {
        Debug.Log("I bumped the player");
        Vector2 myPos = new Vector2(transform.position.x, transform.position.y);
        myPlayer.AddPush(myBumpForce, myPos);

    }


    Vector2 RandomSpawnpoint()
    {
        Vector2 spawnDir = Random.insideUnitCircle.normalized;

        float scale = Random.Range(radOfSpawnMin, radOfSpawnMax);

        return spawnDir*scale;
    }

}

