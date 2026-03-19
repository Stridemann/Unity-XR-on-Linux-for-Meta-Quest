using System;
using NUnit.Framework;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEditor.XR.OpenXR.Features;
using UnityEngine.XR.OpenXR.Features.Interactions;
using UnityEngine.XR.OpenXR;

namespace UnityEditor.XR.OpenXR.Tests
{
    internal class OpenXREditorTests
    {
        [Test]
        public void DocumentationVersion()
        {
            var version = PackageManager.PackageInfo.FindForAssembly(typeof(OpenXREditorTests).Assembly)?.version;
            var majorminor = "@" + OpenXRFeatureAttribute.k_PackageVersionRegex.Match(version).Groups[1].Value + "/";
            UnityEngine.Debug.Log(typeof(KHRSimpleControllerProfile).GetCustomAttribute<OpenXRFeatureAttribute>().InternalDocumentationLink);
            Assert.IsTrue(typeof(KHRSimpleControllerProfile).GetCustomAttribute<OpenXRFeatureAttribute>().InternalDocumentationLink.Contains(majorminor));
        }
    }
}
