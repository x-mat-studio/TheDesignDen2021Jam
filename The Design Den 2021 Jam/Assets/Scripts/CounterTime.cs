using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CounterTime : MonoBehaviour
{
    private Text txt = null;

    // Start is called before the first frame update
    void Start()
    {
        txt = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (txt != null)
            txt.text = StaticGlobalVars.secondsToKillBoss.ToString();
    }
}
