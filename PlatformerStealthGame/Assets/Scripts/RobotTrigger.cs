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
    void Start()
    {
        playerInRange = false;
    }
    
    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("mc") && !hasShow)
        {
            collider.GetComponent<PlayerController>().enabled = false;
            QuestionMgr.Instance.Show(); //to call the popup questions
            hasShow = true;
            // if (collider.GetComponent<EnemyBehavior>().enabled)
            // {
            //     collider.GetComponent<EnemyBehavior>().enabled = false;
            // } else if (collider.GetComponent<CCTVBehavior>().enabled)
            // {
            //     collider.GetComponent<CCTVBehavior>().enabled = false;
            // }
        }
    }
}
