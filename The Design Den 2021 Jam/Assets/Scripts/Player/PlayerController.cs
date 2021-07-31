using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Vector2 currentDir;

    [Range(0.01f, 50.0f)]
    public float speed = 10.0f;

    [Range(0.0f, 1.0f)] //1 drift, cannot change direction - 0 drift changes direction right away
    public float drift = 0.5f; //percentatge of slerp between current dir and new input dir


    Vector2 vel;
    Vector2 pos;


    // Start is called before the first frame update
    void Start()
    {
        currentDir = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        HandleDirectionInputs();
    }


    void HandleDirectionInputs()
    {
        Vector2 newDir = Vector2.zero; //direction at which the player wants to move
        newDir.x = Input.GetAxisRaw("Horizontal");
        newDir.y = Input.GetAxisRaw("Vertical");
        newDir.Normalize();

        if (newDir != Vector2.zero)
        {
            currentDir = Vector2.Lerp(currentDir, newDir, (1.0f - drift) * Time.deltaTime);
        }


        if (currentDir.magnitude > 1.0f)
        {
            currentDir.Normalize();
        }

        //currentDir -= currentDir * drag*Time.deltaTime;
        gameObject.transform.position = gameObject.transform.position + new Vector3(currentDir.x, currentDir.y, 0) * speed * Time.deltaTime;

    }



    public void AddPush(float pushForce, Vector2 origin)
    {
        vel += new Vector2(transform.position.x-origin.x,transform.position.y-origin.y).normalized * pushForce * Time.deltaTime;
    }
}
