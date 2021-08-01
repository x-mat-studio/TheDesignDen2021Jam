using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{

    public Text killCountText;
    public Text maxRPMText;
    public Text timeText;
    public Text highScoreText;

    public SwordController playerSwordController = null;

    public float animationDuration = 5.0f;
    float animationCurrent = 0.0f;

    float killCountFinal = 0.0f;
    float maxRPMFinal = 0.0f;
    float timeFinal = 0.0f;
    float highScoreFinal = 0.0f;

    float killCountCurrent = 0.0f;
    float maxRPMCurrent = 0.0f;
    float timeCurrent = 0.0f;
    float highScoreCurrent = 0.0f;



    // Start is called before the first frame update
    void Start()
    {
        killCountFinal = StaticGlobalVars.totalKills;
        //maxRPMFinal //TODO
        timeFinal = StaticGlobalVars.secondsToKillBoss;
        highScoreFinal = killCountFinal + maxRPMFinal + timeFinal;

        animationCurrent = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        animationCurrent += Time.deltaTime;

        if (animationCurrent < animationDuration)
        {
            float percentage = animationCurrent / animationDuration;

            killCountCurrent = Mathf.Lerp(0, killCountFinal, percentage);
            maxRPMCurrent = Mathf.Lerp(0, maxRPMFinal, percentage);
            timeCurrent = Mathf.Lerp(0, timeFinal, percentage);
            highScoreCurrent = Mathf.Lerp(0, highScoreFinal, percentage);

        }
        else
        {
            killCountCurrent = killCountFinal;
            maxRPMCurrent = maxRPMFinal;
            timeCurrent = timeFinal;
            highScoreCurrent = highScoreFinal;
        }


        killCountText.text = "KILL COUNT: " + killCountCurrent.ToString();
        maxRPMText.text = "MAX RPM: " + maxRPMCurrent.ToString();
        timeText.text = "TIME: " + timeCurrent.ToString();
        highScoreText.text = "HIGHSCORE: " + highScoreCurrent.ToString();

    }
}
