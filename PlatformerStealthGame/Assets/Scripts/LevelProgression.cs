using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelProgression : MonoBehaviour
{

    public bool playerInRange;
    public GameObject Dlgres;
    public bool haveCard;
    public bool doorlocked = false;
    public PlayerController playerController;
    public Timer timer;

    // private GameObject Chest;
    // private bool ChestnotOpened = true;

    public void Construct(bool _playerInRange)
     {
         playerInRange = _playerInRange;
     }

    public void Awake()
    {
      playerInRange = false;
    }

    public void Update()
    {
        //Debug.Log("truth get? " + truthGet);
        //Debug.Log("in " + SceneManager.GetActiveScene().name);
        if (playerInRange)
        {
            if (SceneManager.GetActiveScene().name == "secret room")
            {
                /*if (truthGet)
                {
                    SceneManager.LoadScene("true end");
                }
                else
                {
                    PCfirst.SetActive(true);
                }*/
            }
            else if (SceneManager.GetActiveScene().name == "level 7 - Zhuhan")
            {
                Dlgres.SetActive(true);
            }
            /*else if (SceneManager.GetActiveScene().name == "secret room")
            {
                if (truthGet)
                {
                    SceneManager.LoadScene("true end");
                }
                else
                {
                    PCfirst.SetActive(true);
                }
            }*/
            else if (SceneManager.GetActiveScene().name == "level-generation")
            {
                float time = timer.GetTime();
                Debug.Log(time);
                SceneManager.LoadScene(SceneManager.GetSceneByName("level-generation").buildIndex);
                Debug.Log(time);
                timer.PassTime(time);
            }
            else
            {
                SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex) + 1);
            }
        //Chest = GetComponentsInChildren<bool>(); 
            if (playerController.haveCard && doorlocked) { doorlocked = false; }
       // if (gameObject.name == "door") {
            if ((playerInRange) && (!doorlocked) && SceneManager.GetActiveScene().name != "level-generation")
            {
                SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex) + 1);
            }
            else
            {
                //Debug.Log("Card needed to unlock door");
            }
   
       }
    }

     public void OnTriggerEnter2D(Collider2D collider)
    {
      if (collider.gameObject.tag == "mc")
      {
        playerInRange = true;
      }
    }
}
