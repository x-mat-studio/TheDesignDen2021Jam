using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperScript : MonoBehaviour
{
    private bool managingBonk = false;
    public float pushForce = 650.0f;
    public float sizeExpand = 0.3f;
    public float sizeExpandSpeed = 5.0f;
    private float actualSize = 0.0f;
    public int rotationBonus = 1;
    public ParticleSystem particleBurst = null;
    private GameObject myPlayer;
    public GameObject audioBank;
    AudioSource positiveBumperSound = null;
    AudioSource negativeBumperSound = null;

    // Start is called before the first frame update
    void Start()
    {
        myPlayer = GameObject.FindGameObjectWithTag("Player");
        if (myPlayer == null) Debug.LogError("Bumper did not find player");

        if (audioBank != null)
        {
            positiveBumperSound = audioBank.transform.Find("bumperPositive").gameObject.GetComponent<AudioSource>();
            negativeBumperSound = audioBank.transform.Find("bumperNegative").gameObject.GetComponent<AudioSource>();           
        }
        else { Debug.Log("There is no Audio Bank :0"); }
    }

    // Update is called once per frame
    void Update()
    {
        if(managingBonk)
        ManageBonk();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == myPlayer) 
        {
            BumperBonk();
            if (rotationBonus > 0 && positiveBumperSound != null) { positiveBumperSound.Play(); }
            else if (rotationBonus < 0 && negativeBumperSound != null) { negativeBumperSound.Play(); }
        }
    }

    void BumperBonk() 
    { 

        if(particleBurst != null) particleBurst.Play();

        if (myPlayer != null) 
        {
            myPlayer.GetComponent<PlayerController>().AddPush(pushForce, new Vector2(transform.position.x, transform.position.y));
            myPlayer.GetComponent<SwordController>().ChangeRotationSudden(rotationBonus);
        }

        managingBonk = true;
    }

    void ManageBonk() 
    {
        if (actualSize < sizeExpand) 
        {
            actualSize += Time.deltaTime * sizeExpandSpeed;
            transform.localScale += new Vector3(actualSize, actualSize,0);
        }
        else 
        {
            managingBonk = false;
            actualSize = 0.0f;
            transform.localScale = Vector3.one;
        }
    }
}
