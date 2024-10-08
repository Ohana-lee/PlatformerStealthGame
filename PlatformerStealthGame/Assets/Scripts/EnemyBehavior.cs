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
    DrawRobotCollider drawRobotCollider;

    public void Construct(float _startWaitTime, float _y, float _minX, float _maxX)
    {
        startWaitTime = _startWaitTime;
        y = _y;
        minX = _minX;
        maxX = _maxX;
    }

    // Start is called before the 1st frame update.
    void Start()
    {
        spi = GetComponent<SpriteRenderer>();
        waitTime = startWaitTime;
        moveSpot.position = new Vector3(Random.Range(minX, maxX), y, 10);
        drawRobotCollider = GetComponent<DrawRobotCollider>();
    }

    // Update is called once per frame.
    void Update()
    {
        Vector3 oldPosition = transform.position;
        transform.position = Vector3.MoveTowards(oldPosition, moveSpot.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, moveSpot.position) < 0.2f)
        {
            if (waitTime <= 0)
            {
                moveSpot.position = new Vector3(Random.Range(minX, maxX), y, 10);
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
        
        if (oldPosition.x < transform.position.x)
        {
            spi.flipX = true;
            drawRobotCollider.SetColliderRight(true);
        }
        
        else if (transform.position.x < oldPosition.x)
        {
            spi.flipX = false;
            drawRobotCollider.SetColliderRight(false);
        }
    }
}