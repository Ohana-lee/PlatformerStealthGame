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
    GameObject robot = Object.Instantiate(Resources.Load<GameObject>("robot"));
    GameObject cctv = Object.Instantiate(Resources.Load<GameObject>("cctv"));

    // robot can move from left-right but not up-down; cctv vice versa.
    [Test]
    public void TestNPCMovementSimplePasses()
    {
        // instantiate robot prefab, add req component, construct, record Y pos
        var robotBehavior = robot.AddComponent<EnemyBehavior>();
        robotBehavior.Construct(3, 5, 0, 5);
        var robotInitYPos = robotBehavior.transform.position.y;
        
        // instantiate cctv prefab, add req component, construct, record X pos
        var cctvBehavior = cctv.AddComponent<CCTVBehavior>();
        cctvBehavior.Construct(3, 5);
        var cctvInitXPos = cctvBehavior.transform.position.x;
        
        System.Threading.Thread.Sleep(3000);

        Assert.That(cctvInitXPos, Is.EqualTo(cctvBehavior.transform.position.x));
        Assert.That(robotInitYPos, Is.EqualTo(robotBehavior.transform.position.y));
    }

    // if the mc is in range of an npc the bool val should be changed
    [Test]
    public void TestNPCCollidesPasses()
    {
        var robotTrigger = robot.AddComponent<RobotTrigger>();
        var cctvTrigger = cctv.AddComponent<RobotTrigger>();
        
        var collider1 = new GameObject().AddComponent<BoxCollider2D>();
        var collider2 = new GameObject().AddComponent<BoxCollider2D>();

        robotTrigger.Construct(false, false);
        robotTrigger.OnTriggerEnter2D(collider1);
        cctvTrigger.OnTriggerEnter2D(collider2);

        /*
         if collider has a tag of "mc", the test checks for inequality, 
         otherwise, the test checks for equality
         */
        
        Assert.That(false,
            collider1.CompareTag("mc")
                ? Is.Not.EqualTo(robotTrigger.playerInRange)
                : Is.EqualTo(robotTrigger.playerInRange));
        
        Assert.That(false,
            collider2.CompareTag("mc")
                ? Is.Not.EqualTo(cctvTrigger.playerInRange)
                : Is.EqualTo(cctvTrigger.playerInRange));
        
        GameObject mc = Object.Instantiate(Resources.Load<GameObject>("mc"));
        mc.AddComponent<BoxCollider2D>();
        
        Assert.That(true,
            mc.CompareTag("mc")
                ? Is.Not.EqualTo(robotTrigger.playerInRange)
                : Is.EqualTo(robotTrigger.playerInRange));
        
        Assert.That(true,
            mc.CompareTag("mc")
                ? Is.Not.EqualTo(cctvTrigger.playerInRange)
                : Is.EqualTo(cctvTrigger.playerInRange));
    }
}
