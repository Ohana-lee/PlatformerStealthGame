using NUnit.Framework;
using UnityEngine;

public class TestQuestionPopup
{

    [Test]
    public void TestQuestionPopsUp()
    {
        var robot = Object.Instantiate(Resources.Load<GameObject>("robot"));
        var robotTrigger = robot.AddComponent<RobotTrigger>();
        var collider = new GameObject().AddComponent<BoxCollider2D>();
        
        robotTrigger.Construct(false, false);
        robotTrigger.OnTriggerEnter2D(collider);

        if (collider.CompareTag("mc")) 
        {
            Assert.That(true, Is.EqualTo(robotTrigger.hasShow));
        }
    }

}
