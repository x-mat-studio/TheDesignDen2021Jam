using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

public class BumperSpawner : MonoBehaviour
{
    [Range(0, 100)]
    public int positiveToNegativeBumperRatio = 50;
    public int numOfBumpersToSpawnAtOnce = 2;
    public float secondsBetweenWaves = 60.0f;
    public float spawnRadiusFromPlayer = 10.0f;

    public int maxBumpersAllowed = 10;

    float secondsSinceLastWave = 0.0f;


    List<GameObject> bumpers;

    public GameObject bumperPlus;
    public GameObject bumperMinus;

    // Start is called before the first frame update
    void Start()
    {
        bumpers = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        secondsSinceLastWave += Time.deltaTime;
        if (secondsSinceLastWave >= secondsBetweenWaves)
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
            inst = InstantiateWithPercentage();
            Vector2 newPosOffset = Random.insideUnitCircle.normalized * spawnRadiusFromPlayer;
            inst.transform.position = transform.position + new Vector3(newPosOffset.x, newPosOffset.y, 0.0f);
            bumpers.Add(inst);
        }
    }


    GameObject InstantiateWithPercentage()
    {
        int number = Mathf.FloorToInt(Random.Range(0.0f, 100.0f));
        if (number <= positiveToNegativeBumperRatio)
            return Instantiate(bumperPlus);
        else
            return Instantiate(bumperMinus);
    }
    void DeleteBumpers()
    {
        if (bumpers.Count + numOfBumpersToSpawnAtOnce >= maxBumpersAllowed)
        {

            int toDelete = bumpers.Count + numOfBumpersToSpawnAtOnce - maxBumpersAllowed;
            SortBumpersByDist();


            GameObject objToDelete;
            for (int i = 0; i < toDelete; ++i)
            {
                objToDelete = bumpers[0];
                bumpers.RemoveAt(0);
                Destroy(objToDelete);
            }
        }
    }

    void SortBumpersByDist()
    {
        bumpers.OrderBy(x => Vector2.Distance(this.transform.position, x.transform.position)).ToList(); //TODO check if we can avoid using dist and use SqrDist instead
    }



}
