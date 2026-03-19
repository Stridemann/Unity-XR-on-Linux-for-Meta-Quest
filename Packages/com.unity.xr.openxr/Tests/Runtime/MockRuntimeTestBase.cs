using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine.XR.OpenXR.Features;
using UnityEngine.XR.OpenXR.Features.Mock;
using UnityEngine.XR.OpenXR.TestTooling;

namespace UnityEngine.XR.OpenXR.Tests
{
    abstract class MockRuntimeTestBase
    {
        protected MockOpenXREnvironment m_Environment;
        protected Dictionary<Type, OpenXRFeature> m_EnabledFeaturesInProject = new();

        [OneTimeSetUp]
        protected void OneTimeSetup()
        {
            foreach (var feature in OpenXRSettings.Instance.features)
            {
                if (feature.enabled)
                    m_EnabledFeaturesInProject.Add(feature.GetType(), feature);

                feature.enabled = false;
            }

            m_Environment = MockOpenXREnvironment.CreateEnvironment();
            m_Environment.Settings.EnableFeature<MockRuntime>(true);

            CustomOneTimeSetup();
        }

        [OneTimeTearDown]
        protected void OneTimeTearDown()
        {
            m_Environment.Settings.EnableFeature<MockRuntime>(false);

            foreach (var feature in m_EnabledFeaturesInProject.Values)
            {
                feature.enabled = true;
            }

            m_EnabledFeaturesInProject.Clear();
            m_Environment.Dispose();

            CustomOneTimeTearDown();
        }

        [SetUp]
        protected void Setup()
        {
            CustomSetup();
        }

        [TearDown]
        protected void TearDown()
        {
            m_Environment.Stop();

            CustomTearDown();
        }

        protected virtual void CustomOneTimeSetup() { }
        protected virtual void CustomOneTimeTearDown() { }
        protected virtual void CustomSetup() { }
        protected virtual void CustomTearDown() { }
    }
}
