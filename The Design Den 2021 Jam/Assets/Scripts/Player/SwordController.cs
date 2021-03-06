using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwordController : MonoBehaviour
{
    public GameObject sword = null;
    public GameObject audioBank = null;
    public CameraFollow cam = null;
    private AudioSource audioDeath = null;

    private int loops = 0;  //Negative means counter clock wise movement
    private int quadrantChanges = 0;

    enum QUADRANT : int
    {
        NONE = -1,

        TOP_LEFT,
        TOP_RIGHT,
        BOTTOM_LEFT,
        BOTTOM_RIGHT
    }

    private QUADRANT previousQuadrant = QUADRANT.NONE;
    private QUADRANT currentQuadrant = QUADRANT.NONE;

    //Timers
    public float timeBeforeStopping = 1.0f;
    public float timeReduceSpeed = 0.5f;
    private float beforeStopTimer = 0.0f;

    public float timeBeforeDead = 5.0f;
    public float deadTimer = 0.0f;

    public float timeBeforeBlurr = 2.0f;
    public float blurTimer = 0.0f;

    //RPM
    float maxRPM = 0.0f; //maxRPM
    public float rpm = 0.0f;
    [Range(0.01f, 0.99f)]
    public float rpmLerpSpeed = 0.5f;//between 0.01 and 1

    [HideInInspector]
    public float maxRPMReached = 0.0f;

    private void Start()
    {
        if (audioBank != null)
        {
            audioDeath = audioBank.transform.Find("playerDeath").gameObject.GetComponent<AudioSource>();
        }
        else { Debug.Log("There is no Audio Bank :0"); }

        deadTimer = timeBeforeDead;
        blurTimer = timeBeforeBlurr;
    }

    // Update is called once per frame
    void Update()
    {

        CalculateCurrentQuadrant();
        UpdateSpinState();

        UpdateTimers();


        if (rpm < maxRPM - 0.1f)
        {
            rpm = Mathf.Lerp(rpm, maxRPM, rpmLerpSpeed);
        }
        else
        {
            rpm = maxRPM;
        }

        sword.transform.eulerAngles = new Vector3(sword.transform.eulerAngles.x, sword.transform.eulerAngles.y, sword.transform.eulerAngles.z - RPMtoDegreesPerFrame());

        if (rpm > maxRPMReached)
        {
            maxRPMReached = rpm;
            StaticGlobalVars.maxRPMReached = Mathf.Abs(maxRPMReached);
        }

        StaticGlobalVars.revolutionsPerMinuteDisplay = Mathf.Abs(rpm);
    }

    private void CalculateCurrentQuadrant()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 swordPos = sword.transform.position;

        previousQuadrant = currentQuadrant;

        if (mousePos.x < swordPos.x)
        {
            if (mousePos.y > swordPos.y)
                currentQuadrant = QUADRANT.TOP_LEFT;

            else
                currentQuadrant = QUADRANT.BOTTOM_LEFT;
        }

        else if (mousePos.x > swordPos.x)
        {
            if (mousePos.y > swordPos.y)
                currentQuadrant = QUADRANT.TOP_RIGHT;

            else
                currentQuadrant = QUADRANT.BOTTOM_RIGHT;
        }
    }


    private void UpdateSpinState()
    {
        switch (currentQuadrant)
        {
            case QUADRANT.TOP_LEFT:

                if (previousQuadrant == QUADRANT.BOTTOM_LEFT)
                    quadrantChanges++;

                else if (previousQuadrant == QUADRANT.TOP_RIGHT)
                    quadrantChanges--;

                break;

            case QUADRANT.TOP_RIGHT:
                if (previousQuadrant == QUADRANT.TOP_LEFT)
                    quadrantChanges++;

                else if (previousQuadrant == QUADRANT.BOTTOM_RIGHT)
                    quadrantChanges--;
                break;

            case QUADRANT.BOTTOM_LEFT:
                if (previousQuadrant == QUADRANT.BOTTOM_RIGHT)
                    quadrantChanges++;

                else if (previousQuadrant == QUADRANT.TOP_LEFT)
                    quadrantChanges--;
                break;

            case QUADRANT.BOTTOM_RIGHT:
                if (previousQuadrant == QUADRANT.TOP_RIGHT)
                    quadrantChanges++;

                else if (previousQuadrant == QUADRANT.BOTTOM_LEFT)
                    quadrantChanges--;
                break;
        }

        if (quadrantChanges == 4)
        {
            loops++;
            quadrantChanges = 0;
            RecalculateRPM();

            beforeStopTimer = timeBeforeStopping;
            deadTimer = timeBeforeDead;
        }
        else if (quadrantChanges == -4)
        {
            loops--;
            quadrantChanges = 0;
            RecalculateRPM();

            beforeStopTimer = timeBeforeStopping;
            deadTimer = timeBeforeDead;
        }
    }
    void RecalculateRPM()
    {
        maxRPM = loops * 2; //Change this as designers want
    }

    float RPMtoDegreesPerFrame()
    {
        Debug.Log("CURRENT RPM: ==" + rpm.ToString() + "==");
        return rpm * 6 * Time.deltaTime;
    }

    private void UpdateTimers()
    {
        if (loops == 0)
        {
            deadTimer -= Time.deltaTime;

            if (deadTimer <= timeBeforeDead * 0.5f)
            {
                blurTimer -= Time.deltaTime;

                if (blurTimer <= 0.0f)
                {
                    
                    blurTimer = timeBeforeBlurr;
                    if (cam != null)
                        cam.StartCameraShake();
                    //SCREEN SHACKE HERE
                }
            }

            if (deadTimer <= 0.0f)
            {
                if (audioDeath != null && audioDeath.isPlaying == false) 
                {
                    audioDeath.Play();
                    StaticGlobalVars.ResetStaticVars();
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
                
            }
        }

        else
        {
            if (beforeStopTimer > 0.0f)
                beforeStopTimer -= Time.deltaTime;

            else if (beforeStopTimer <= 0.0f)
            {
                if (loops > 0)
                    loops--;
                else if (loops < 0)
                    loops++;

                RecalculateRPM();
                beforeStopTimer = timeReduceSpeed;
            }
        }

    }

    public void ChangeRotationSudden(int addedLoops)
    {
        if (loops >= 0)
        {
            loops += addedLoops;
            loops = Mathf.Max(0, loops);
        }
        else if (loops < 0)
        {
            loops -= addedLoops;
            loops = Mathf.Min(0, loops);
        }
        RecalculateRPM();
    }
}
