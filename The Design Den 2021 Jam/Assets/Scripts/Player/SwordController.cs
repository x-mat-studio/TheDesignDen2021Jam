using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    public GameObject sword = null;
    public GameObject audioBank = null;
    private AudioSource audioBuzz1 = null;
    private AudioSource audioBuzz2 = null;

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

    public float timeBeforeDead = 10.0f;
    public float deadTimer = 0.0f;

    private float sfxTimer = 0.0f;

    //RPM
    float maxRPM = 0.0f; //maxRPM
    public float rpm = 0.0f;
    [Range(0.01f, 0.99f)]
    public float rpmLerpSpeed = 0.5f;//between 0.01 and 1

    private void Start()
    {
        if (audioBank != null)
        {
            audioBuzz1 = audioBank.transform.Find("weaponBuzz1").gameObject.GetComponent<AudioSource>();
            audioBuzz2 = audioBank.transform.Find("weaponBuzz2").gameObject.GetComponent<AudioSource>();            
        }
        else { Debug.Log("There is no Audio Bank :0"); }
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

            if (deadTimer <= 0.0f)
            {
                Debug.Log("You dead");
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

        sfxTimer++;
        if (sfxTimer > (Random.Range(1.0f, 3.0f) - (rpmLerpSpeed * 1.5))) {
            sfxTimer = 0.0f;
            if (Random.Range(1.0f, 2.0f) > 1.5f) { audioBuzz1.Play(); }
            else { audioBuzz2.Play(); }
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
