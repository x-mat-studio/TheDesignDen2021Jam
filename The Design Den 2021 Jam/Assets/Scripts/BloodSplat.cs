using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSplat : MonoBehaviour
{
    public GameObject bloodSplatPrefab = null;
    public GameObject audioBank = null;
    private AudioSource audioSplat1 = null;
    private AudioSource audioSplat2 = null;
    public Sprite[] sprites;

    public static BloodSplat bloodSplatHolder = null;

    // Start is called before the first frame update
    void Start()
    {
        if (bloodSplatHolder != null)
            GameObject.Destroy(bloodSplatHolder);

        bloodSplatHolder = this;

        if (audioBank != null)
        {
            audioSplat1 = audioBank.transform.Find("bloodSplat1").gameObject.GetComponent<AudioSource>();
            audioSplat2 = audioBank.transform.Find("bloodSplat2").gameObject.GetComponent<AudioSource>();
        }
        else { Debug.Log("There is no Audio Bank :0"); }

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void CreateSplat(Vector3 position, float rotation, Vector3 color)
    {
        if (audioSplat1 != null && audioSplat2 != null)
        {
            if (Random.Range(1.0f, 2.0f) > 1.5) { audioSplat1.Play(); }
            else { audioSplat2.Play(); }
        }

        if (bloodSplatPrefab != null)
        {
            GameObject splat = Instantiate(bloodSplatPrefab);

            splat.transform.position = position;
            splat.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotation);

            int randomIndex = Random.Range(0, sprites.Length);
            bloodSplatPrefab.GetComponent<SpriteRenderer>().sprite = sprites[randomIndex];

            bloodSplatPrefab.GetComponent<SpriteRenderer>().color = new Color(color.x, color.y, color.z);
        }
        else
            Debug.LogError("Need to add blood splat prefab asshole :D");
    }
}
