using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueTrigger : MonoBehaviour
{
    public bool playerInRange;
    public bool playerEarnedTrust;
    public GameObject questionUI;

    // so if the mc answer's the given npc's Q correctly, it will earn its trust and not be bugged everytime it is in range of the npc.

   // public GameObject diamondPrefab;s

    public void Construct(bool _playerInRange)
     {
         playerInRange = _playerInRange;
     }

    public void Awake()
    {
      playerInRange = false;
      playerEarnedTrust = false;
    }

    public void Update()
    {
      if (playerInRange && !playerEarnedTrust)
      {
            questionUI.SetActive(true);
            GameObject.Find("mc_sprite_walk_1").GetComponent<PlayerController>().enabled = false;

            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<EnemyBehavior>().enabled = false;

        }
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
      if (collider.gameObject.tag == "mc")
      {
        playerInRange = true;
      }
    }

    public void OnTriggerExit2D(Collider2D collider)
    {
      if (collider.gameObject.tag == "mc")
      {
        playerInRange = false;
      }
    }

}
