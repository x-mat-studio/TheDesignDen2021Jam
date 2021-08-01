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

    public float animationDuration = 5.0f;

    float killCountFinal = 0.0f;
    float maxRPMFinal = 0.0f;
    float timeFinal = 0.0f;
    float highScoreFinal = 0.0f;



    // Start is called before the first frame update
    void Start()
    {
        killCountFinal = StaticGlobalVars.totalKills;
        //maxRPMFinal //TODO
        timeFinal = StaticGlobalVars.secondsToKillBoss;
        highScoreFinal = killCountFinal + maxRPMFinal + timeFinal;
    }

    // Update is called once per frame
    void Update()
    {
        killCountText.text = "KILL COUNT: " + killCountFinal.ToString();
        maxRPMText.text = "MAX RPM: " + maxRPMFinal.ToString();
        timeText.text = "TIME: " + timeFinal.ToString();
        highScoreText.text = "HIGHSCORE: " + highScoreFinal.ToString();

    }
}
