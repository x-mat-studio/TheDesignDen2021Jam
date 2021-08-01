using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Camera currentCam;
    public Transform target;
   
    public float deadZoneDistance = 1.0f;

    [Range(0.0f,1.0f)]
    public float smoothSpeed = 0.125f; //between 0 & 1

    public bool followActive = true;
    public bool scaleActive = true;

    public float speedToMaxScale = 2.0f; //speed needed for the camera to go to its max scale
    public float minCamScale;//scale of the camera when it is stationary
    public float maxCamScale;//scale of the camera when it travels 


    PlayerController myPlayer = null;

    Vector2 lastCamPos = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        if (target == null)
        {
            followActive = false;
            Debug.LogError("NO TARGET ASSIGNED FOR THE CAMERA TO FOLLOW");
        }
        else
        {
            myPlayer = target.GetComponent<PlayerController>();
            if (myPlayer == null)
            {
                scaleActive = false;
                Debug.LogError("THE TARGET IS NOT A PLAYER");
            }
        }
        currentCam = gameObject.GetComponent<Camera>();
        if (currentCam == null)
        {
            followActive = false;
            Debug.LogError("CAMERA FOLLOW SCRIPT NOT ASSIGNED TO A CAMERA");
        }

        lastCamPos = new Vector2(transform.position.x, transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        TryMove();
        TryScale();
        lastCamPos = new Vector2(transform.position.x, transform.position.y);
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
   
    private float fLerp(float origin, float destination, float t) //TODO NOT USED, DELETE?
    {
        float f;
        f = ((1 - t) * origin) + (t * destination);

        return f;
    }

    void TryMove()
    {
        if (IsInDeadZone()||followActive==false)
            return;

        //Move

        Vector2 desiredPos = new Vector2(target.position.x, target.position.y);
        Vector2 currentPos = new Vector2(transform.position.x, transform.position.y);

        Vector2 camMovementDir = (desiredPos - currentPos).normalized;
        Vector2 desiredDeadzonePos = desiredPos - camMovementDir * deadZoneDistance;
        Vector2 smoothedPos = Vector2.Lerp(currentPos, desiredDeadzonePos, smoothSpeed*Time.deltaTime*10);
        transform.position = new Vector3(smoothedPos.x, smoothedPos.y, transform.position.z);
    }

    void TryScale()
    {
        if (scaleActive == false)
            return;

        Vector2 newPos = new Vector2(transform.position.x, transform.position.y);

        float newScale01 = Mathf.Clamp01(((newPos-lastCamPos).magnitude/Time.deltaTime) / speedToMaxScale);
        currentCam.orthographicSize = Mathf.Lerp(minCamScale, maxCamScale, newScale01*newScale01);
    }


    bool IsInDeadZone()
    {
        return (new Vector2(transform.position.x - target.position.x, transform.position.y - target.position.y).magnitude < deadZoneDistance);//TODO use sqrMagnitude if we need to optimize
    }
}
