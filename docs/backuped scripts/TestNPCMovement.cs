using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestNPCMovement
{

    Transform moveSpot;
    float minX;
    float maxX;
    float y;


    // npc can move from left-right but not up-down
    [Test]
    public void TestNPCMovementSimplePasses()
    {
        var enemyBehavior = new GameObject().AddComponent<EnemyBehavior>();
        enemyBehavior.Construct(3, 5);
        var ogYPosition = enemyBehavior.transform.position.y;

        System.Threading.Thread.Sleep(3000);

        Assert.That(ogYPosition, Is.EqualTo(enemyBehavior.transform.position.y));
    }

    // if the mc is in range of an npc the bool val should be changed
    [Test]
    public void TestNPCCollidesPasses()
    {
        var robotTrigger = new GameObject().AddComponent<RobotTrigger>();
        var collider = new GameObject().AddComponent<BoxCollider2D>();

        robotTrigger.Construct(false, false);
        robotTrigger.OnTriggerEnter2D(collider);
        
        if (collider.tag == "mc") 
        {
            Assert.That(false, Is.Not.EqualTo(robotTrigger.playerInRange));
        } else {
            Assert.That(false, Is.EqualTo(robotTrigger.playerInRange));
        }
    }
}
