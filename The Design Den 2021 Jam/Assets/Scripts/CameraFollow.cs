using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Camera currentCam;
    public Transform target;
    
    public bool followActive = true;

    [Range(0.0f,1.0f)]
    public float smoothSpeed = 0.125f; //between 0 & 1
    public float smoothSizeSpeed = 0.125f; //the size of the camera will be determined by the z component of the target scale
    public float cameraWantedSize = 5;
    public Vector2 offset;

    // Start is called before the first frame update
    void Start()
    {
        if (target == null)
        {
            followActive = false;
            Debug.LogError("NO TARGET ASSIGNED FOR THE CAMERA TO FOLLOW");
        }
        currentCam = gameObject.GetComponent<Camera>();
        if (currentCam == null)
        {
            followActive = false;
            Debug.LogError("CAMERA FOLLOW SCRIPT NOT ASSIGNED TO A CAMERA");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(followActive==false)
            return;
        
        Vector2 desiredPos = new Vector2(target.position.x,target.position.y) + offset;
        Vector2 smoothedPos = Vector2.Lerp(transform.position, desiredPos, smoothSpeed);
       
        float smoothedSize = fLerp(currentCam.orthographicSize, cameraWantedSize, smoothSizeSpeed*Time.deltaTime);
        transform.position = new Vector3(smoothedPos.x,smoothedPos.y,transform.position.z);
        currentCam.orthographicSize = smoothedSize;
    }

    public void NewTarget(Transform t)
    {
        target = t;
        if (target = null)
        {
            followActive = false;
            Debug.LogError("NO TARGET ASSIGNED FOR THE CAMERA TO FOLLOW");
        }
    }
   
    private float fLerp(float origin, float destination, float t)
    {
        float f;
        f = ((1 - t) * origin) + (t * destination);

        return f;
    }
}
