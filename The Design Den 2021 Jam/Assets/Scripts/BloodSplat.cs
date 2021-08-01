using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSplat : MonoBehaviour
{
    public GameObject bloodSplatPrefab = null;
    public Sprite[] sprites;

    public static BloodSplat bloodSplatHolder = null;

    // Start is called before the first frame update
    void Start()
    {
        if (bloodSplatHolder != null)
            GameObject.Destroy(bloodSplatHolder);

        bloodSplatHolder = this;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void CreateSplat(Vector3 position, float rotation, Vector3 color)
    {
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
