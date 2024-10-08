using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float moveSpeed = 1f;
    private float waitTime;
    public float startWaitTime;

    public Transform moveSpot;
    public float minX;
    public float maxX;
    public float y;
    SpriteRenderer spi;
    RobotTrigger rbt;

    public void Construct(float _startWaitTime, float _y)
    {
        startWaitTime = _startWaitTime;
        y = _y;
    }

    // Start is called before the 1st frame update.
    void Start()
    {
        spi = GetComponent<SpriteRenderer>();
        waitTime = startWaitTime;
        moveSpot.position = new Vector3(Random.Range(minX, maxX), y, 10);
    }

    // Update is called once per frame.
    void Update()
    {
        // rbt = gameObject.GetComponent<RobotTrigger>();
        // bool hs = rbt;

        // if (hs == true) {
        //     gameObject.GetComponent<EnemyBehavior>().enabled = false;
        // }


        Vector3 oldPosition = transform.position;
            transform.position = Vector3.MoveTowards(transform.position, moveSpot.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, moveSpot.position) < 0.2f)
        {
            if (waitTime <= 0)
            {
                moveSpot.position = new Vector3(Random.Range(minX, maxX), y, 10);
                waitTime = startWaitTime;
            } else
            {
                waitTime -= Time.deltaTime;
            }
        }

        float dist = Vector3.Distance(transform.position, oldPosition);
        if (dist > 0)
        {
            spi.flipX = true;
        }
        else
        {
            spi.flipX = false;
        }
    }
}
