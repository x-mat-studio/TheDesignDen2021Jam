using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLoop : MonoBehaviour
{
    public GameObject chunk;

    [Header("Neighbours Points")]
    public GameObject neighbour_N;
    public GameObject neighbour_E;
    public GameObject neighbour_S;
    public GameObject neighbour_W;

    public bool n_N_Generated = false;
    public bool n_E_Generated = false;
    public bool n_S_Generated = false;
    public bool n_W_Generated = false;

    void Update()
    {
        
    }

    public void GenerateNeighbour(string text)
    {
        if(text == "NW")
        {
            if (!n_N_Generated)
            {
                GameObject newChunk = Instantiate(chunk, neighbour_N.transform);
                newChunk.GetComponent<MapLoop>().n_S_Generated = true;
                n_N_Generated = true;
            }

            if (!n_W_Generated)
            {
                GameObject newChunk = Instantiate(chunk, neighbour_W.transform);
                newChunk.GetComponent<MapLoop>().n_E_Generated = true;
                n_W_Generated = true;
            }        
        }

        if (text == "NE")
        {
            if (!n_N_Generated)
            {
                GameObject newChunk = Instantiate(chunk, neighbour_N.transform);
                newChunk.GetComponent<MapLoop>().n_S_Generated = true;
                n_N_Generated = true;
            }

            if (!n_E_Generated)
            {
                GameObject newChunk = Instantiate(chunk, neighbour_E.transform);
                newChunk.GetComponent<MapLoop>().n_W_Generated = true;
                n_E_Generated = true;
            }
        }

        if (text == "SW")
        {
            if (!n_S_Generated)
            {
                GameObject newChunk = Instantiate(chunk, neighbour_S.transform);
                newChunk.GetComponent<MapLoop>().n_N_Generated = true;
                n_S_Generated = true;
            }

            if (!n_W_Generated)
            {
                GameObject newChunk = Instantiate(chunk, neighbour_W.transform);
                newChunk.GetComponent<MapLoop>().n_E_Generated = true;
                n_W_Generated = true;
            }
        }

        if (text == "SE")
        {
            if (!n_S_Generated)
            {
                GameObject newChunk = Instantiate(chunk, neighbour_S.transform);
                newChunk.GetComponent<MapLoop>().n_N_Generated = true;
                n_S_Generated = true;
            }

            if (!n_E_Generated)
            {
                GameObject newChunk = Instantiate(chunk, neighbour_E.transform);
                newChunk.GetComponent<MapLoop>().n_W_Generated = true;
                n_E_Generated = true;
            }
        }
    }

}
