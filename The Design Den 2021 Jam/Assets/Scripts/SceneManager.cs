using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    public GameObject mainMenu = null;
    public GameObject audioMenu = null;
    public GameObject creditsMenu = null;
    public GameObject inGameMenu = null;
    public GameObject inGameAudioMenu = null;
    public GameObject inGameCreditsMenu = null;
    public GameObject canvas = null;
    public AudioMixer mixer = null;
    public GameObject globalMenu = null;
    public GameObject globalGame = null;
    public GameObject musicMenu = null;
    public GameObject musicGame = null;
    public GameObject sfxMenu = null;
    public GameObject sfxGame = null;
    public GameObject camera = null;
    public GameObject boss = null;
    public GameObject player = null;
    bool lockOpenMenus = true;
    public bool bossDead = false;
    float bossDeadTimer = 0.0f;
    public float bossDeadCinematicTime = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (bossDead)
        {
            lockOpenMenus = true;
            camera.GetComponent<CameraFollow>().target = boss.transform;
            bossDeadTimer += Time.deltaTime;

            if (bossDeadCinematicTime < bossDeadTimer) {
                bossDead = false;
                camera.GetComponent<CameraFollow>().target = player.transform;
                bossDeadTimer = 0.0f;
                SetMenusFalse(mainMenu);
            }
        }

        if (Input.GetKeyUp(KeyCode.Escape) && lockOpenMenus == false)
        {
            inGameMenu.SetActive(true);
        }
    }

    public void OnClick(GameObject gObject)
    {
        switch (gObject.name)
        {
            case "Play":
                SetMenusFalse(null);
                lockOpenMenus = false;
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

        if (mainMenu != trueObject) { mainMenu.SetActive(false); }
        else {
            mainMenu.SetActive(true);
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
