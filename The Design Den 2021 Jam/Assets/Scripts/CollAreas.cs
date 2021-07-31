using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollAreas : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {       
        if(col.gameObject.tag == "Player")
        {
            gameObject.transform.parent.GetComponent<MapLoop>().GenerateNeighbour(gameObject.tag);
        }
    }
}
