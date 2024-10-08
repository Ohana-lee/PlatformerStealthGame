using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelProgression : MonoBehaviour
{

    public bool playerInRange;

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
      if (playerInRange)
      {
            SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex)+1);
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