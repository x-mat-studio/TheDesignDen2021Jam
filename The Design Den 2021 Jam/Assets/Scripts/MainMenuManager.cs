using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{

    public GameObject mainMenu = null;
    public GameObject audioMenu = null;
    public GameObject creditsMenu = null;
    public AudioMixer mixer = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick(GameObject gObject)
    {
        switch (gObject.name)
        {
            case "Play":
                mainMenu.SetActive(false);
                audioMenu.SetActive(false);
                creditsMenu.SetActive(false);
                break;

            case "Audio Settings":
                mainMenu.SetActive(false);
                audioMenu.SetActive(true);
                creditsMenu.SetActive(false);
                break;

            case "Credits":
                mainMenu.SetActive(false);
                audioMenu.SetActive(false);
                creditsMenu.SetActive(true);
                break;

            case "Return":
                mainMenu.SetActive(true);
                audioMenu.SetActive(false);
                creditsMenu.SetActive(false);
                break;

            case "Quit":
                Application.Quit();
                break;

            case "Aaron":
                Application.OpenURL("https://github.com/AaronGCProg");
                break;

            case "Julia":
                Application.OpenURL("");
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
