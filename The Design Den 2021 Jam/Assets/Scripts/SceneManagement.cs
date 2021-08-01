using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public GameObject mainMenu = null;
    public GameObject titleCanvas = null;
    public GameObject mainMenuTitle = null;
    public GameObject audioMenu = null;
    public GameObject creditsMenu = null;
    public GameObject inGameMenu = null;
    public GameObject inGameAudioMenu = null;
    public GameObject inGameCreditsMenu = null;
    public GameObject winMenu = null;
    public GameObject canvas = null;
    public AudioMixer mixer = null;
    public GameObject globalMenu = null;
    public GameObject globalGame = null;
    public GameObject musicMenu = null;
    public GameObject musicGame = null;
    public GameObject sfxMenu = null;
    public GameObject sfxGame = null;
    public GameObject theCamera = null;
    public GameObject boss = null;
    public GameObject player = null;
    public GameObject audioBank = null;
    AudioSource bossDeathAudio = null;
    bool lockOpenMenus = true;
    float timeToWin = 0.0f;
    bool firstFrameKill = true;
    public bool bossDead = false;
    float bossDeadTimer = 0.0f;
    public float bossDeadCinematicTime = 3.0f;
    public bool enemyDead = false;
    float enemyDeadTimer = 0.0f;
    public float enemyDeaSnapshotTime = 0.5f;

    //******************* Win screen variables

    bool returnToMenu = false;

    //*******************

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0.000000001f;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("TOTAL KILLS: "+StaticGlobalVars.totalKills.ToString());

        if (!lockOpenMenus) { timeToWin += Time.deltaTime; }

        if (bossDead)
        {
            if (firstFrameKill)
            {
                firstFrameKill = false;
                StaticGlobalVars.secondsToKillBoss = timeToWin;
                Debug.Log("SECONDS TO KILL BOSS: " + StaticGlobalVars.secondsToKillBoss.ToString());
            }

            lockOpenMenus = true;
            theCamera.GetComponent<CameraFollow>().target = boss.transform;
            bossDeadTimer += Time.deltaTime;

            mixer.FindSnapshot("Victory").TransitionTo(0.1f);

            if (bossDeathAudio == null) { bossDeathAudio = audioBank.transform.Find("bossDeath").GetComponent<AudioSource>(); }
            else { if (bossDeathAudio.isPlaying == false) { bossDeathAudio.Play(); } }

            if (bossDeadCinematicTime < bossDeadTimer)
            {
                ShowWinScreen();
            }
        }

        if (Input.GetKeyUp(KeyCode.Escape) && lockOpenMenus == false)
        {
            Time.timeScale = 0.000000001f;
            inGameMenu.SetActive(true);
        }
    }

    private void ShowWinScreen()
    {

        winMenu.SetActive(true);

        if (returnToMenu) {

            bossDead = false;
            firstFrameKill = true;
            returnToMenu = false;
            theCamera.GetComponent<CameraFollow>().target = player.transform;
            bossDeadTimer = 0.0f;
            timeToWin = 0.0f;
            mixer.FindSnapshot("Snapshot").TransitionTo(0.0f);
            SetMenusFalse(mainMenu);
            Time.timeScale = 0.000000001f;

            StaticGlobalVars.ResetStaticVars();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }

    }

    public void OnClick(GameObject gObject)
    {
        switch (gObject.name)
        {
            case "Play":
                SetMenusFalse(null);
                lockOpenMenus = false;

                Time.timeScale = 1;
                break;

            case "Audio Settings":
                SetMenusFalse(audioMenu);
                break;

            case "Credits":
                SetMenusFalse(creditsMenu);
                break;

            case "InGameCredits":
                SetMenusFalse(inGameCreditsMenu);
                break;

            case "InGameAudioSettings":
                SetMenusFalse(inGameAudioMenu);
                break;

            case "Return":
            case "ReturnToMainMenu":
                SetMenusFalse(mainMenu);
                break;

            case "ReturnToInGameMenu":
                SetMenusFalse(inGameMenu);
                break;

            case "ReturnFromWinScene":
                SetMenusFalse(mainMenu);
                returnToMenu = true;
                break;

            case "Quit":
                Application.Quit();
                break;

            case "Aaron":
                Application.OpenURL("https://www.linkedin.com/in/aaron-guerrero-cruz-5a2333164/");
                break;

            case "Julia":
                Application.OpenURL("https://www.linkedin.com/in/julia-blasco-allepuz/");
                break;

            case "Oscar":
                Application.OpenURL("https://www.linkedin.com/in/oscar-perez-martin/");
                break;

            case "Alex":
                Application.OpenURL("https://www.linkedin.com/in/alex-melench%C3%B3n-maza-a46981195/");
                break;

            case "Adri":
                Application.OpenURL("https://www.linkedin.com/in/adri%C3%A0-serrano-l%C3%B3pez-7196a91a3/");
                break;

            case "Jose":
                Application.OpenURL("https://www.linkedin.com/in/joseluisredondotello/");
                break;

            case "Ferran":
                Application.OpenURL("https://www.linkedin.com/in/ferran-roger-basart-i-bosch-606b5a195/");
                break;
        }

        canvas.GetComponent<AudioSource>().Play();

    }

    private void SetMenusFalse(GameObject trueObject)
    {

        if (mainMenu != trueObject)
        {
            mainMenu.SetActive(false);
            if (mainMenuTitle != null)
                mainMenuTitle.SetActive(false);
            titleCanvas.SetActive(false);
        }
        else
        {
            mainMenu.SetActive(true);
            if (mainMenuTitle != null)
                mainMenuTitle.SetActive(true);

            titleCanvas.SetActive(true);
            lockOpenMenus = true;
        }

        if (audioMenu != trueObject) { audioMenu.SetActive(false); }
        else { audioMenu.SetActive(true); }

        if (creditsMenu != trueObject) { creditsMenu.SetActive(false); }
        else { creditsMenu.SetActive(true); }

        if (inGameMenu != trueObject) { inGameMenu.SetActive(false); }
        else { inGameMenu.SetActive(true); }

        if (inGameAudioMenu != trueObject) { inGameAudioMenu.SetActive(false); }
        else { inGameAudioMenu.SetActive(true); }

        if (inGameCreditsMenu != trueObject) { inGameCreditsMenu.SetActive(false); }
        else { inGameCreditsMenu.SetActive(true); }

        if (winMenu != trueObject) { winMenu.SetActive(false); }
        else { winMenu.SetActive(true); }

    }

    public void SetVolume(GameObject gObject)
    {
        switch (gObject.name)
        {
            case "Master":
                mixer.SetFloat("masterVolume", Mathf.Log10(gObject.GetComponent<Slider>().value) * 20);
                globalMenu.GetComponent<Slider>().value = gObject.GetComponent<Slider>().value;
                globalGame.GetComponent<Slider>().value = gObject.GetComponent<Slider>().value;
                break;

            case "Music":
                mixer.SetFloat("musicVolume", Mathf.Log10(gObject.GetComponent<Slider>().value) * 20);
                musicMenu.GetComponent<Slider>().value = gObject.GetComponent<Slider>().value;
                musicGame.GetComponent<Slider>().value = gObject.GetComponent<Slider>().value;
                break;

            case "SFX":
                mixer.SetFloat("sfxVolume", Mathf.Log10(gObject.GetComponent<Slider>().value) * 20);
                sfxMenu.GetComponent<Slider>().value = gObject.GetComponent<Slider>().value;
                sfxGame.GetComponent<Slider>().value = gObject.GetComponent<Slider>().value;
                break;
        }
    }

}
