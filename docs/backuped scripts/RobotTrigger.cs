using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotTrigger : MonoBehaviour
{

    public void Construct(bool _hasShow, bool _playerInRange)
    {
        hasShow = _hasShow;
        playerInRange = _playerInRange;
    }


    public bool hasShow;
    public bool playerInRange;
    // Start is called before the first frame update
    void Start()
    {
        playerInRange = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "mc")
        {
            if (!hasShow)
            {
                playerInRange = true;
                collider.GetComponent<PlayerController>().enabled = false;
                QuestionMgr.Instance.Show();
                hasShow = true;
            }
           
        }
    }
}
