using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillTextBehaviour : MonoBehaviour
{

    public float raiseSpeed = 0.1f;
    public float timeUntilDead = 2.0f;
    [HideInInspector]
    public float currTime = 0.0f;
    [HideInInspector]
    Color myColor = Color.black;

    TextMesh tm = null;
    // Start is called before the first frame update
    void Start()
    {
        currTime = 0.0f;
        tm = GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        if (tm == null)
        {
            Debug.LogError("No TextMeshComponent found");
            return;
        }

        currTime += Time.deltaTime;
        if(currTime>=timeUntilDead)
        {
            Destroy(gameObject);
        }

        transform.position = transform.position + new Vector3(0, 0, Time.deltaTime * raiseSpeed);

        myColor.a =1.0f- (currTime/ timeUntilDead);
        
        
        
        
        tm.color = myColor;
    }
}
