using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCreation : MonoBehaviour
{
    public GameObject gridPrefab = null;

    public static bool updatingGrid = false;
    public bool needUpdate = false;
    public bool wasUpdated = false;

    private void Update()
    {
        if (wasUpdated == true)
        {
            wasUpdated = false;
            needUpdate = false;
            updatingGrid = false;
        }

        else if (needUpdate == true && updatingGrid == false)
        {
            GenerateGrids();

            needUpdate = false;
            updatingGrid = true;
            wasUpdated = true;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag != "Grid")
        {
            if (updatingGrid == false)
            {
                GenerateGrids();

                updatingGrid = true;
                wasUpdated = true;
            }

            else
                needUpdate = true;
        }
    }

    private void GenerateGrids()
    {
        //UP
        Vector2 nord = new Vector2(0.0f, 1.0f);

        RaycastHit2D hit = Physics2D.Raycast(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 10), nord);
        
        if (hit.collider == null)
        {
            GameObject grid = Instantiate(gridPrefab);
            grid.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 10, 0.0f);
        }

        else if (hit.distance < 9.5f)
        {
            GameObject grid = Instantiate(gridPrefab);
            grid.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 10, 0.0f);
        }

        Vector2 nordEast = new Vector2(1.0f, 1.0f);

        hit = Physics2D.Raycast(new Vector2(gameObject.transform.position.x + 10, gameObject.transform.position.y + 10), nordEast);

        if (hit.collider == null)
        {
            GameObject grid = Instantiate(gridPrefab);
            grid.transform.position = new Vector3(gameObject.transform.position.x + 10, gameObject.transform.position.y + 10, 0.0f);
        }

        else if (hit.distance < 9.5f)
        {
            GameObject grid = Instantiate(gridPrefab);
            grid.transform.position = new Vector3(gameObject.transform.position.x + 10, gameObject.transform.position.y + 10, 0.0f);
        }

        Vector2 nordWest = new Vector2(-1.0f, 1.0f);

        hit = Physics2D.Raycast(new Vector2(gameObject.transform.position.x - 10, gameObject.transform.position.y + 10), nordWest);

        if (hit.collider == null)
        {
            GameObject grid = Instantiate(gridPrefab);
            grid.transform.position = new Vector3(gameObject.transform.position.x - 10, gameObject.transform.position.y + 10, 0.0f);
        }

        else if (hit.distance < 9.5f)
        {
            GameObject grid = Instantiate(gridPrefab);
            grid.transform.position = new Vector3(gameObject.transform.position.x - 10, gameObject.transform.position.y + 10, 0.0f);
        }

        //LATERALS
        Vector2 east = new Vector2(1.0f, 0.0f);

        hit = Physics2D.Raycast(new Vector2(gameObject.transform.position.x + 10, gameObject.transform.position.y), east);

        if (hit.collider == null)
        {
            GameObject grid = Instantiate(gridPrefab);
            grid.transform.position = new Vector3(gameObject.transform.position.x + 10, gameObject.transform.position.y, 0.0f);
        }

        else if (hit.distance < 9.5f)
        {
            GameObject grid = Instantiate(gridPrefab);
            grid.transform.position = new Vector3(gameObject.transform.position.x + 10, gameObject.transform.position.y, 0.0f);
        }

        Vector2 west = new Vector2(-1.0f, 0.0f);

        hit = Physics2D.Raycast(new Vector2(gameObject.transform.position.x - 10, gameObject.transform.position.y), nordWest);

        if (hit.collider == null)
        {
            GameObject grid = Instantiate(gridPrefab);
            grid.transform.position = new Vector3(gameObject.transform.position.x - 10, gameObject.transform.position.y, 0.0f);
        }

        else if (hit.distance < 9.5f)
        {
            GameObject grid = Instantiate(gridPrefab);
            grid.transform.position = new Vector3(gameObject.transform.position.x - 10, gameObject.transform.position.y, 0.0f);
        }

        //DOWN

        Vector2 sud = new Vector2(0.0f, -1.0f);

        hit = Physics2D.Raycast(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 10), sud);

        if (hit.collider == null)
        {
            GameObject grid = Instantiate(gridPrefab);
            grid.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 10, 0.0f);
        }

        else if (hit.distance < 9.5f)
        {
            GameObject grid = Instantiate(gridPrefab);
            grid.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 10, 0.0f);
        }

        Vector2 sudEast = new Vector2(1.0f, -1.0f);

        hit = Physics2D.Raycast(new Vector2(gameObject.transform.position.x + 10, gameObject.transform.position.y - 10), sudEast);

        if (hit.collider == null)
        {
            GameObject grid = Instantiate(gridPrefab);
            grid.transform.position = new Vector3(gameObject.transform.position.x + 10, gameObject.transform.position.y - 10, 0.0f);
        }

        else if (hit.distance < 9.5f)
        {
            GameObject grid = Instantiate(gridPrefab);
            grid.transform.position = new Vector3(gameObject.transform.position.x + 10, gameObject.transform.position.y - 10, 0.0f);
        }

        Vector2 sudWest = new Vector2(-1.0f, -1.0f);

        hit = Physics2D.Raycast(new Vector2(gameObject.transform.position.x - 10, gameObject.transform.position.y - 10), sudWest);

        if (hit.collider == null)
        {
            GameObject grid = Instantiate(gridPrefab);
            grid.transform.position = new Vector3(gameObject.transform.position.x - 10, gameObject.transform.position.y - 10, 0.0f);
        }
        else if (hit.distance < 9.5f)
        {
            GameObject grid = Instantiate(gridPrefab);
            grid.transform.position = new Vector3(gameObject.transform.position.x - 10, gameObject.transform.position.y - 10, 0.0f);
        }
    }
}
