using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCTVBehavior : MonoBehaviour
{
    public float moveSpeed = 1f;
    private float waitTime;
    public float startWaitTime;

    public Transform moveSpot;
    public float x;
    public float minY;
    public float maxY;
    SpriteRenderer spi;
    RobotTrigger rbt;

    public void Construct(float _startWaitTime, float _x)
    {
        startWaitTime = _startWaitTime;
        x = _x;
    }

    void Start()
    {
        spi = GetComponent<SpriteRenderer>();
        waitTime = startWaitTime;
        moveSpot.position = new Vector3(x, Random.Range(minY, maxY), 10);
    }

    void Update()
    {
        // rbt = gameObject.GetComponent<RobotTrigger>();
        // bool hs = rbt;

        // if (hs == false) {
        //     gameObject.GetComponent<CCTVBehavior>().enabled = false;
        // }

        Vector3 oldPosition = transform.position;
        transform.position = Vector3.MoveTowards(transform.position, moveSpot.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, moveSpot.position) < 0.2f)
        {
            if (waitTime <= 0)
            {
                moveSpot.position = new Vector3(x, Random.Range(minY, maxY), 10);
                waitTime = startWaitTime;
            } else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }
}
