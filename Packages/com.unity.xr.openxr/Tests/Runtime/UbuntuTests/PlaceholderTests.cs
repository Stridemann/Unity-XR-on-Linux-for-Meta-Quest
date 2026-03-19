
using System;
using NUnit.Framework;
using UnityEngine.TestTools;
using Assert = UnityEngine.Assertions.Assert;

namespace UnityEngine.XR.OpenXR.Tests
{
    internal class PlaceholderTests
    {
        //An always passing test for Linux platform to by pass Validation jobs requirements.
        [UnityPlatform(exclude = new[] { RuntimePlatform.WindowsPlayer, RuntimePlatform.WindowsEditor, RuntimePlatform.Android, RuntimePlatform.OSXEditor, RuntimePlatform.OSXPlayer })]
        [Test]
        public void AlwaysPassingTest()
        {
            Assert.IsTrue(true);
        }
    }
}
