using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayModeTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void PlayModeTestsSimplePasses()
    {
        // Use the Assert class to test conditions
        Assert.AreNotEqual(0, 1);
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator PlayModeTestsWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        Assert.AreNotEqual(0, 1);
        // Use yield to skip a frame.
        yield return null;
    }
}
