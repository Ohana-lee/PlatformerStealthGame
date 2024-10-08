using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.TestTools;

public class TestEnemyColliders
{
    // A Test behaves as an ordinary method
    [Test]
    public void TestColliderToggles()
    {
        // instantiate prefabs from "Resources" folder (formerly "Prefabs") w required components
        var robot = Object.Instantiate(Resources.Load<GameObject>("robot"));
        var cctv = Object.Instantiate(Resources.Load<GameObject>("cctv"));

        robot.AddComponent<PolygonCollider2D>();
        robot.AddComponent<DrawRobotCollider>();
        cctv.AddComponent<PolygonCollider2D>();
        cctv.AddComponent<DrawCCTVCollider>();

        // both enemies should show their colliders at the start
        var active = robot.GetComponent<DrawRobotCollider>().active &&
                         cctv.GetComponent<DrawCCTVCollider>().active;
        Assert.That(active, Is.EqualTo(true));
        
        // once ToggleCollider method is triggered, no longer should show colliders
        robot.GetComponent<DrawRobotCollider>().ToggleCollider();
        cctv.GetComponent<DrawCCTVCollider>().ToggleCollider();
        
        active = robot.GetComponent<DrawRobotCollider>().active &&
                 cctv.GetComponent<DrawCCTVCollider>().active;
        Assert.That(active, Is.EqualTo(false));
    }
}
