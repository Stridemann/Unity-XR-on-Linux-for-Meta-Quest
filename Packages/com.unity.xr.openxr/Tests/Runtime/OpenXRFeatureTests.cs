using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine.TestTools;
using UnityEngine.XR.OpenXR.Features;
using UnityEngine.XR.OpenXR.Features.Mock;

namespace UnityEngine.XR.OpenXR.Tests
{
    class OpenXRFeatureTests : OpenXRLoaderSetup
    {
        class FakeFeature : OpenXRFeature
        { }

        [Test]
        public void HighPriority()
        {
            MockRuntime.Instance.priority = Int32.MaxValue;

            InitializeAndStart();

            Assert.IsTrue(OpenXRSettings.Instance.features[0] == MockRuntime.Instance);
        }

        [Test]
        public void LowPriority()
        {
            MockRuntime.Instance.priority = Int32.MinValue;

            InitializeAndStart();

            Assert.IsTrue(OpenXRSettings.Instance.features[^1] == MockRuntime.Instance);
        }

        [Test]
        public void ChangeEnabledAtRuntime()
        {
            InitializeAndStart();

            MockRuntime.Instance.enabled = false;
            LogAssert.Expect(LogType.Error, "OpenXRFeature.enabled cannot be changed while OpenXR is running");
        }

        [Test]
        public void FeatureFailedInitialization()
        {
            //Force OnInstanceCreate returning false so that failedInitialization is true.
            MockRuntime.Instance.TestCallback = (methodName, _) => methodName != nameof(OpenXRFeature.OnInstanceCreate);
            InitializeAndStart();
            var enableStatus = MockRuntime.Instance.enabled;
            MockRuntime.Instance.enabled = enableStatus;
            Assert.IsTrue(MockRuntime.Instance.enabled == enableStatus);
        }

        [Test]
        public void GetFeatureCount()
        {
            Assert.IsTrue(OpenXRSettings.Instance.featureCount == OpenXRSettings.Instance.GetFeatures(typeof(OpenXRFeature)).Length);
        }

        [Test]
        public void GetFeatureByTypeBadType()
        {
            Assert.IsNull(OpenXRSettings.Instance.GetFeature(typeof(OpenXRLoader)));
        }

        [Test]
        public void GetFeatureByTypeNotFound()
        {
            Assert.IsNull(OpenXRSettings.Instance.GetFeature<FakeFeature>());
        }

        [Test]
        public void GetFeatureByType()
        {
            var feature = OpenXRSettings.Instance.GetFeature<MockRuntime>();
            Assert.IsNotNull(feature);
            Assert.IsNotNull(OpenXRSettings.Instance.GetFeature(typeof(MockRuntime)) as MockRuntime);
        }

        [Test]
        public void GetFeaturesByTypeArray()
        {
            var features = OpenXRSettings.Instance.GetFeatures<MockRuntime>();
            Assert.IsNotNull(features);
            Assert.IsTrue(features.Length == 1);
            Assert.IsTrue(features[0] is MockRuntime);

            features = OpenXRSettings.Instance.GetFeatures(typeof(MockRuntime));
            Assert.IsNotNull(features);
            Assert.IsTrue(features.Length == 1);
            Assert.IsTrue(features[0] is MockRuntime);
        }

        [Test]
        public void GetFeaturesByGenericList()
        {
            var features = new List<MockRuntime>();
            Assert.IsTrue(OpenXRSettings.Instance.GetFeatures(features) == 1);
            Assert.IsNotNull(features[0]);
        }

        [Test]
        public void GetFeaturesByTypeList()
        {
            var features = new List<OpenXRFeature>();
            Assert.IsTrue(OpenXRSettings.Instance.GetFeatures(typeof(MockRuntime), features) == 1);
            Assert.IsNotNull(features[0]);
            Assert.IsTrue(features[0] is MockRuntime);
        }

        [Test]
        public void GetFeaturesArray()
        {
            var features = OpenXRSettings.Instance.GetFeatures();
            Assert.IsNotNull(features);
            Assert.IsTrue(features.Length == OpenXRSettings.Instance.featureCount);
        }

        [Test]
        public void GetFeaturesList()
        {
            var features = new List<OpenXRFeature>();
            Assert.IsTrue(OpenXRSettings.Instance.GetFeatures(features) == OpenXRSettings.Instance.featureCount);
        }
    }
}
