using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

public class BumperSpawner : MonoBehaviour
{
    [Range(0.0f, 100.0f)]
    public float positiveToNegativeBumperRatio = 50.0f;
    public int numOfBumpersToSpawnAtOnce = 2;
    public float secondsBetweenWaves = 60.0f;
    public float spawnRadiusFromPlayer = 10.0f;

    float secondsSinceLastWave = 0.0f;


    List<GameObject> bumpers;

    public GameObject bumperPlus;
    public GameObject bumperMinus;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(secondsSinceLastWave>=secondsBetweenWaves)
        {
            secondsSinceLastWave = 0.0f;
            DeleteBumpers();
            SpawnBumpers();
        }
    }

    void SpawnBumpers()
    {
        GameObject inst;
        for (int i = 0; i < numOfBumpersToSpawnAtOnce; ++i)
        {
            inst = Instantiate(bumperPlus); //TODO change this for a percentage
            Vector2 newPosOffset = Random.insideUnitCircle.normalized * spawnRadiusFromPlayer;
            inst.transform.position = transform.position + new Vector3(newPosOffset.x,newPosOffset.y,0.0f);
        }
    }
    void DeleteBumpers()
    {
        SortBumpersByDist();

        for (int i = 0; i < numOfBumpersToSpawnAtOnce; ++i)
        {
            bumpers.RemoveAt(0);
        }
    }

    void SortBumpersByDist()
    {
        bumpers.OrderBy(x => Vector2.Distance(this.transform.position, x.transform.position)).ToList(); //TODO check if we can avoid using dist and use SqrDist instead
    }



}
