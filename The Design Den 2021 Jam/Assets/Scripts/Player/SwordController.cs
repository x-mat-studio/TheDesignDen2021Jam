using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    public GameObject sword = null;

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

    public float timeBeforeStopping = 1.0f;
    public float timeReduceSpeed = 0.5f;
    private float beforeStopTimer = 0.0f;


    float maxRPM = 0.0f; //maxRPM
    public float rpm = 0.0f;
    [Range(0.01f,0.99f)]
    public float rpmLerpSpeed = 0.5f;//between 0.01 and 1

    // Update is called once per frame
    void Update()
    {
        CalculateCurrentQuadrant();
        UpdateSpinState();

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

        if (rpm < maxRPM-0.1f)
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
        }
        else if (quadrantChanges == -4)
        {
            loops--;
            quadrantChanges = 0;
            RecalculateRPM();

            beforeStopTimer = timeBeforeStopping;
        }
    }
    void RecalculateRPM()
    {
        maxRPM = loops * 2; //Change this as designers want
    }

    float RPMtoDegreesPerFrame()
    {
        Debug.Log("CURRENT RPM: ==" + rpm.ToString() + "==");
        return rpm*6*Time.deltaTime;
    }
}
