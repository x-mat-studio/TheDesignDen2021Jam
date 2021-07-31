using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    [Range(0.01f, 50.0f)]
    public float speed = 10.0f;

    [Range(0.0f, 1.0f)] //1 drift, cannot change direction - 0 drift changes direction right away
    public float drift = 0.5f; //percentatge of slerp between current dir and new input dir
    [Range(0.0f, 10.0f)]
    public float drag = 1.0f;
    Vector2 vel = Vector2.zero;
    Vector2 pos = Vector2.zero;



    // Start is called before the first frame update
    void Start()
    {
        pos = new Vector2(transform.position.x, transform.position.y);
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
            vel += newDir * speed * Time.deltaTime*(1.0f-drift);
        }

        vel -= vel * drag * Time.deltaTime;//Drag?
        pos += vel * Time.deltaTime;
        gameObject.transform.position = new Vector3(pos.x, pos.y, gameObject.transform.position.z);

    }



    public void AddPush(float pushForce, Vector2 origin)
    {
        vel += new Vector2(transform.position.x - origin.x, transform.position.y - origin.y).normalized * pushForce * Time.deltaTime;
    }
}
