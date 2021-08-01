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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && mainMenu.activeInHierarchy == false && audioMenu.activeInHierarchy == false && creditsMenu.activeInHierarchy == false)
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
                Application.OpenURL("https://github.com/AaronGCProg");
                break;

            case "Julia":
                Application.OpenURL("https://github.com/JuliaBlasco");
                break;

            case "Oscar":
                Application.OpenURL("https://github.com/oscarpm5");
                break;

            case "Alex":
                Application.OpenURL("https://github.com/AlexMelenchon");
                break;

            case "Adri":
                Application.OpenURL("https://github.com/adriaserrano97");
                break;

            case "Jose":
                Application.OpenURL("https://github.com/jose-tello");
                break;

            case "Ferran":
                Application.OpenURL("https://github.com/HoduRe");
                break;
        }

        canvas.GetComponent<AudioSource>().Play();

    }

    private void SetMenusFalse(GameObject trueObject)
    {

        if (mainMenu != trueObject) { mainMenu.SetActive(false); }
        else { mainMenu.SetActive(true); }

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
                break;

            case "Music":
                mixer.SetFloat("musicVolume", Mathf.Log10(gObject.GetComponent<Slider>().value) * 20);
                break;

            case "SFX":
                mixer.SetFloat("sfxVolume", Mathf.Log10(gObject.GetComponent<Slider>().value) * 20);
                break;
        }

    }

}
