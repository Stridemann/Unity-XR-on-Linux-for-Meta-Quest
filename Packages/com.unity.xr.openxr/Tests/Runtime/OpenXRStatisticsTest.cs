using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.TestTools;
using UnityEngine.XR.OpenXR.Features;

namespace UnityEngine.XR.OpenXR.Tests
{
    class OpenXRStatisticsTests : OpenXRLoaderSetup
    {
        [UnityTest]
        public IEnumerator RegisterAndSetTestStatistic_UsingStatFlagStatOptionNone()
        {
            var feature = ScriptableObject.CreateInstance<SingleStatTestFeature>();
            feature.ClearStatOnUpdate = false;

            InitializeAndStart();
            yield return null;

            feature.CreateStat();
            yield return null;

            feature.SetStatValue(1.0f);
            yield return null;
            var stat1Success =
                Provider.XRStats.TryGetStat(GetFirstDisplaySubsystem(), SingleStatTestFeature.StatName, out float value1);

            StopAndShutdown();
            Object.DestroyImmediate(feature);

            Assert.IsTrue(stat1Success);
            Assert.AreEqual(1.0f, value1);
        }

        [UnityTest]
        public IEnumerator RegisterAndSetTestStatistic_UsingStatFlagClearOnUpdate()
        {
            var feature = ScriptableObject.CreateInstance<SingleStatTestFeature>();
            feature.ClearStatOnUpdate = true;

            InitializeAndStart();
            yield return null;

            feature.CreateStat();
            yield return null;

            feature.SetStatValue(1.0f);
            yield return null;
            var beforeUpdateSuccess =
                Provider.XRStats.TryGetStat(GetFirstDisplaySubsystem(), SingleStatTestFeature.StatName, out float beforeUpdateValue);

            yield return null;
            var afterUpdateSuccess =
                Provider.XRStats.TryGetStat(GetFirstDisplaySubsystem(), SingleStatTestFeature.StatName, out float afterUpdateValue);

            StopAndShutdown();
            Object.DestroyImmediate(feature);

            Assert.IsTrue(beforeUpdateSuccess);
            Assert.AreEqual(1.0f, beforeUpdateValue);

            Assert.IsTrue(afterUpdateSuccess);
            Assert.AreEqual(0.0f, afterUpdateValue);
        }

        static IntegratedSubsystem GetFirstDisplaySubsystem()
        {
            List<XRDisplaySubsystem> displays = new();
            SubsystemManager.GetSubsystems(displays);
            if (displays.Count == 0)
            {
                Debug.Log("No display subsystem found.");
                return null;
            }
            return displays[0];
        }
    }

    class SingleStatTestFeature : OpenXRFeature
    {
        public const string StatName = "TestStat";

        [NonSerialized]
        ulong m_statId;

        public bool ClearStatOnUpdate { get; set; } = false;

        public void CreateStat()
        {
            StatFlags flags = StatFlags.StatOptionNone;

            if (ClearStatOnUpdate)
            {
                flags |= StatFlags.ClearOnUpdate;
            }

            m_statId = RegisterStatsDescriptor(StatName, flags);
        }

        public void SetStatValue(float value)
        {
            SetStatAsFloat(m_statId, value);
        }
    }
}
