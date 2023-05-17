using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
public class WorldLocalEditTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void WorldLocalSimpleTest()
    {
        // Use the Assert class to test conditions
        Assert.AreEqual(1, 1);
        // Gizmos.
        WorldLocal worldLocal = new GameObject().AddComponent<WorldLocal>();
        //set treansform of worldLocal
        worldLocal.transform.position = new Vector3(1, 1, 1);
        // worldLocal.transform.rotation = new Quaternion(1, 1, 1, 1);
        Assert.AreEqual(new Vector3(1, 1, 1), worldLocal.transform.position);

        //make me a random Vector2
        Vector2 randomVector = new Vector2(Random.Range(-100, 100), Random.Range(-100, 100));
        Assert.AreEqual((Vector2)worldLocal.transform.InverseTransformPoint(randomVector),
            worldLocal.WorldToLocal(randomVector));
        Assert.AreEqual((Vector2)worldLocal.transform.TransformPoint(randomVector),
            worldLocal.LocalToWorld(randomVector));
    }
    
    [Test]
    public void WorldLocalTestWithRotation()
    {
        // Use the Assert class to test conditions
        Assert.AreEqual(1, 1);
        // Gizmos.
        WorldLocal worldLocal = new GameObject().AddComponent<WorldLocal>();
        //set treansform of worldLocal
        worldLocal.transform.position = new Vector3(1, 1, 1);
        worldLocal.transform.rotation = new Quaternion(1, 1, 0, 0);
        Assert.AreEqual(new Vector3(1, 1, 1), worldLocal.transform.position);

        //make me a random Vector2
        Vector2 randomVector = new Vector2(Random.Range(-100, 100), Random.Range(-100, 100));
        TestUtils.AreEqual((Vector2)worldLocal.transform.InverseTransformPoint(randomVector),
            worldLocal.WorldToLocal(randomVector),0.0001f);
        TestUtils.AreEqual((Vector2)worldLocal.transform.TransformPoint(randomVector),
            worldLocal.LocalToWorld(randomVector),0.0001f);
    }
}