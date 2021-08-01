using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCollision : MonoBehaviour
{
    public GameObject audioBank = null;
    private AudioSource audioHit1 = null;
    private AudioSource audioHit2 = null;
    private AudioSource audioHit3 = null;

    private void Start()
    {
        if(audioBank != null)
        {
            audioHit1 = audioBank.transform.Find("weaponHit1").gameObject.GetComponent<AudioSource>();
            audioHit2 = audioBank.transform.Find("weaponHit2").gameObject.GetComponent<AudioSource>();
            audioHit3 = audioBank.transform.Find("weaponHit3").gameObject.GetComponent<AudioSource>();
        }
        else { Debug.Log("There is no Audio Bank :0"); }
    }

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" || collision.tag == "Boss")
        {
            collision.gameObject.GetComponent<BaseEnemy>().TakeDamage();

            if (audioHit1.isPlaying == false && audioHit2.isPlaying == false && audioHit3.isPlaying == false) {
                float random = Random.Range(0.0f, 2.0f);
                if (random < 1.0f) { audioHit1.Play(); }
                else if (random >= 1.0f && random < 2.0f) { audioHit2.Play(); }
                else if (random >= 2.0f) { audioHit3.Play(); }
            }
        }
    }
}
