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
        if (Input.GetKeyDown(KeyCode.Space) == true)
        {
            CreateSplat(new Vector2(Random.Range(-6.0f, 6.0f), Random.Range(-6.0f, 6.0f)), Random.Range(0.0f, 360.0f), new Vector3(1.0f, 0.0f, 0.3f));
        }
    }

    public void CreateSplat(Vector2 position, float rotation, Vector3 color)
    {
        if (bloodSplatPrefab != null)
        {
            GameObject splat = Instantiate(bloodSplatPrefab);
            splat.transform.SetParent(this.gameObject.transform, true);

            bloodSplatPrefab.transform.position = new Vector3(position.x, position.y, 1.0f);
            bloodSplatPrefab.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotation);

            int randomIndex = Random.Range(0, sprites.Length);
            bloodSplatPrefab.GetComponent<SpriteRenderer>().sprite = sprites[randomIndex];

            bloodSplatPrefab.GetComponent<SpriteRenderer>().color = new Color(color.x, color.y, color.z);
        }
        else
            Debug.LogError("Need to add blood splat prefab asshole :D");
    }
}
