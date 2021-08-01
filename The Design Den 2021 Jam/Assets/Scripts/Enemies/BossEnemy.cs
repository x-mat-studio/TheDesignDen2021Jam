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
    float counterSpawn = 0.0f;
    public float spawnTimer = 100.0f; //higher = takes more time to spawn 
    public int rate100SpawnNormal = 100; //percentatge
    public int rate100SpawnSlow = 100;
    public int rate100SpawnFast = 100;
    public int minEnemiesSpawned = 0;
    public int maxEnemiesSpawned = 0;
    public float distanceOfAction = 10;

    public GameObject HitParticle = null;

    public GameObject[] lifes;

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
        if (audioBank != null)
            enemySpawnSound = audioBank.transform.Find("enemySpawn").GetComponent<AudioSource>();

    }

    protected override void Update()
    {
        
        if (Mathf.Abs(Vector3.Distance(transform.position, myPlayer.transform.position)) < distanceOfAction)
        {
            base.Update();
        }

        if (counterSpawn < spawnTimer)
        {
            counterSpawn += 1 * Time.deltaTime;
        }
        else
        {
            counterSpawn = 0;
            SpawnEnemy();
        }

        if (myLife == 2)
        {
            lifes[2].SetActive(false);
        }
        else if(myLife == 1)
        {
            lifes[1].SetActive(false);
        }
        else if( myLife == 0)
        {
            lifes[0].SetActive(false);
        }

    }
    private void SpawnEnemy()
    {
      if(myLife > 0)
      {
        float aux = Random.Range(0, 100);   //TODO: make them spawn inside the radious of the boss (random inside it), not inside the fkn boss

        GameObject auxG = null;
        Vector2 auxPos= Vector2.zero;

        int randSpawn = Random.Range(minEnemiesSpawned, maxEnemiesSpawned);


        for (int i=0; i< Mathf.Max(randSpawn,1); i++) 
        {
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
        }
        if (enemySpawnSound != null) { enemySpawnSound.Play(); }
        else { Debug.Log("Audio Bank isn't in the scene!!"); }
      }
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

    public override void TakeDamage()
    {
        PushPlayer();

        base.TakeDamage();

        if (myLife == 0 && base.deathParticle != null)
        {
            GameObject aux = Instantiate(base.deathParticle, gameObject.transform);          
            aux.transform.position = transform.position;
            base.mySpeed = 0;
            base.rotSpeedMultiplier = 0;
        }
        else if (HitParticle != null) 
        {
            GameObject aux = Instantiate(HitParticle, gameObject.transform);
            aux.transform.position = transform.position;
        }
    }
}

