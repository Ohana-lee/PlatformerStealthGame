using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerOfTruth : MonoBehaviour
{
    public bool playerInRange = false;
    public GameObject truth;
    public AudioSource audioSource;
    public AudioClip secretAudio;
    //public GameObject door;
    //public SecretRmDoor srd;

    private bool truthTriggered = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((playerInRange)&&(!truthTriggered))
        {
            truth.SetActive(true);
            //srd.truthGet = true;
            //door.GetComponent<SecretRmDoor>().truthGet = true;
        }
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "mc")
        {
            playerInRange = true;
            audioSource.clip = secretAudio;
            audioSource.Play();
        }
    }
}
