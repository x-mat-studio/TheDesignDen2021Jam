using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Vector2 direction;
    [Range(0.01f,50.0f)]
    public float speed=10.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        direction = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        HandleDirectionInputs();
        gameObject.transform.position = gameObject.transform.position + new Vector3(direction.x,direction.y,0)*speed*Time.deltaTime;
    }


    void HandleDirectionInputs()
    {
        direction = Vector2.zero;
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");

        direction.Normalize();
    }
}
