using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestQuestionPopup
{

    [Test]
    public void TestQuestionPopsUp()
    {
        var robotTrigger = new GameObject().AddComponent<RobotTrigger>();
        var collider = new GameObject().AddComponent<BoxCollider2D>();
        
        robotTrigger.Construct(false, false);
        robotTrigger.OnTriggerEnter2D(collider);

        if (collider.tag == "mc") 
        {
            Assert.That(true, Is.EqualTo(robotTrigger.hasShow));
        }
    }

}
