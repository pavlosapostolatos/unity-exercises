/*
 * Copyright (c) 2014 Martin Preisler
 * Licensed under http://opensource.org/licenses/MIT
 */
 
using UnityEngine;
using NUnit.Framework;
using System;
 
public class TestUtils
{
    #region Vector2
    public static void AreEqual(Vector2 expected, Vector2 actual, float delta, string message)
    {
        if (delta <= 0)
            delta = 0.001f;
 
        float distance = Vector2.Distance(expected, actual);
 
        if (string.IsNullOrEmpty(message))
            message = String.Format("Expected: Vector2({0}, {1})\nBut was:  Vector2({2}, {3})\nDistance: {4} is greated than allowed delta {5}",
                                    expected.x, expected.y, actual.x, actual.y,
                                    distance, delta);
 
        Assert.That(distance, Is.LessThanOrEqualTo(delta), message);
 
        /*
        if (float.IsNaN(expected.x) || float.IsInfinity(expected.x))
            Assert.That(actual.x, Is.EqualTo(expected.x), message);
        else
            Assert.That(actual.x, Is.EqualTo(expected.x).Within(delta), message);
 
        if (float.IsNaN(expected.y) || float.IsInfinity(expected.y))
            Assert.That(actual.y, Is.EqualTo(expected.y), message);
        else
            Assert.That(actual.y, Is.EqualTo(expected.y).Within(delta), message);
        */
    }
 
    public static void AreEqual(Vector2 expected, Vector2 actual, float delta)
    {
        AreEqual(expected, actual, delta, null);
    }
 
    public static void AreEqual(Vector2 expected, Vector2 actual, string message)
    {
        AreEqual(expected, actual, 0, message);
    }
 
    public static void AreEqual(Vector2 expected, Vector2 actual)
    {
        AreEqual(expected, actual, 0, null);
    }
    #endregion
 
    #region Vector3
    public static void AreEqual(Vector3 expected, Vector3 actual, float delta, string message)
    {
        if (delta <= 0)
            delta = 0.001f;
 
        float distance = Vector3.Distance(expected, actual);
 
        if (string.IsNullOrEmpty(message))
            message = String.Format("Expected: Vector3({0}, {1}, {2})\nBut was:  Vector3({3}, {4}, {5})\nDistance: {6} is greated than allowed delta {7}",
                                    expected.x, expected.y, expected.z,
                                    actual.x, actual.y, actual.z,
                                    distance, delta);
 
        Assert.That(distance, Is.LessThanOrEqualTo(delta), message);
 
        /*
        if (float.IsNaN(expected.x) || float.IsInfinity(expected.x))
            Assert.That(actual.x, Is.EqualTo(expected.x), message);
        else
            Assert.That(actual.x, Is.EqualTo(expected.x).Within(delta), message);
 
        if (float.IsNaN(expected.y) || float.IsInfinity(expected.y))
            Assert.That(actual.y, Is.EqualTo(expected.y), message);
        else
            Assert.That(actual.y, Is.EqualTo(expected.y).Within(delta), message);
 
        if (float.IsNaN(expected.z) || float.IsInfinity(expected.z))
            Assert.That(actual.z, Is.EqualTo(expected.z), message);
        else
            Assert.That(actual.z, Is.EqualTo(expected.z).Within(delta), message);
        */
    }
 
    public static void AreEqual(Vector3 expected, Vector3 actual, float delta)
    {
        AreEqual(expected, actual, delta, null);
    }
 
    public static void AreEqual(Vector3 expected, Vector3 actual, string message)
    {
        AreEqual(expected, actual, 0, message);
    }
 
    public static void AreEqual(Vector3 expected, Vector3 actual)
    {
        AreEqual(expected, actual, 0, null);
    }
    #endregion
}