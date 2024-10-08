using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SecretRmDoor : MonoBehaviour
{
    //public bool truthGet = false;
    //public GameObject PCfirst;
    public bool playerInRange = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if (truthGet)
        //{
            SceneManager.LoadScene("true end");
        //}
        //else
        //{
          //  PCfirst.SetActive(true);
        //}
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "mc")
        {
            playerInRange = true;
        }
    }
}
