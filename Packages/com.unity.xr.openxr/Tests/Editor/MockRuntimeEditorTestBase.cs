using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.XR.OpenXR.TestTooling;

namespace UnityEditor.XR.OpenXR.Tests
{
    abstract class MockRuntimeEditorTestBase
    {
        protected MockOpenXREnvironment m_Environment;

        [OneTimeSetUp]
        public virtual void OneTimeSetUp()
        {
            m_Environment = MockOpenXREnvironment.CreateEnvironment();
        }

        [TearDown]
        public virtual void TearDown() => m_Environment.Stop();

        [OneTimeTearDown]
        public virtual void OneTimeTearDown()
        {
            m_Environment.Dispose();
        }
    }
}
