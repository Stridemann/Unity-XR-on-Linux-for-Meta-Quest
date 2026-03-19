using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using NUnit.Framework;
using UnityEditor.XR.OpenXR.Features;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.XR.OpenXR;
using UnityEngine.XR.OpenXR.Features;
using static UnityEditor.XR.OpenXR.Features.FeatureHelpersInternal;

namespace UnityEditor.XR.OpenXR.Tests
{
    internal class CustomLoaderTests
    {
        private static readonly OpenXRApiVersion s_upgradedVersion = new(
            (ushort)(OpenXRApiVersion.Current.Major + 1),
            (ushort)(OpenXRApiVersion.Current.Minor + 1),
            OpenXRApiVersion.Current.Patch + 1);
        private static readonly OpenXRApiVersion s_downgradedVersion = new(
            (ushort)(OpenXRApiVersion.Current.Major - 1),
            (ushort)(OpenXRApiVersion.Current.Minor - 1),
            OpenXRApiVersion.Current.Patch - 1);

        internal class MockFeature : OpenXRFeature { }

        private static readonly FeatureInfo s_OverrideLoader = new()
        {
            Attribute = new OpenXRFeatureAttribute
            {
                FeatureId = "OverrideLoader",
                UiName = "OverrideLoader",
                Priority = 0,
            },
            Feature = CreateMockFeature(true),
            PluginPath = "/test/path/lib",
            LoaderVersion = null,
            HasLoaderForBuildTarget = true,
        };

        private static readonly FeatureInfo s_LoaderWithHighVersionHighPriority = new()
        {
            Attribute = new OpenXRFeatureAttribute
            {
                FeatureId = "UpdatedLoaderHighPriority",
                UiName = "OverrideLoader",
                Priority = 100,
            },
            Feature = CreateMockFeature(true),
            PluginPath = "/test/path/lib",
            LoaderVersion = s_upgradedVersion,
            HasLoaderForBuildTarget = true
        };

        private static readonly FeatureInfo s_LoaderWithHighVersionLowPriority = new()
        {
            Attribute = new OpenXRFeatureAttribute
            {
                FeatureId = "UpdatedLoaderLowPriority",
                UiName = "UpdatedLoaderLowPriority",
                Priority = -100,
            },
            Feature = CreateMockFeature(true),
            PluginPath = "/test/path/lib",
            LoaderVersion = s_upgradedVersion,
            HasLoaderForBuildTarget = true
        };

        private static readonly FeatureInfo s_DowngradedLoader = new()
        {
            Attribute = new OpenXRFeatureAttribute
            {
                FeatureId = "DowngradedLoader",
                UiName = "DowngradedLoader",
                Priority = 0,
            },
            Feature = CreateMockFeature(true),
            PluginPath = "/test/path/lib",
            LoaderVersion = s_downgradedVersion,
            HasLoaderForBuildTarget = true
        };

        private static readonly FeatureInfo s_UpgradedButInactiveLoader = new()
        {
            Attribute = new OpenXRFeatureAttribute
            {
                FeatureId = "InactiveLoader",
                UiName = "InactiveLoader",
                Priority = 100,
            },
            Feature = CreateMockFeature(false),
            PluginPath = "/test/path/lib",
            LoaderVersion = s_upgradedVersion,
            HasLoaderForBuildTarget = true
        };

        private static OpenXRFeature CreateMockFeature(bool enabled)
        {
            var feature = (MockFeature)ScriptableObject.CreateInstance(typeof(MockFeature));
            feature.enabled = enabled;
            return feature;
        }


        [Test]
        public void UseDefaultCustomLoader()
        {
            var customLoaderFeatures = new List<FeatureInfo>();

            // Use the Assert class to test conditions
            var customLoaderFound = TryFindCustomLoaderWithHighestPriority(customLoaderFeatures, out var _);

            Assert.IsFalse(customLoaderFound, "A custom loader was selected when no custom loader exists.");
        }

        [Test]
        public void UseOverrideLoader()
        {
            var customLoaderFeatures = new List<FeatureInfo>()
            {
                s_OverrideLoader,
                s_LoaderWithHighVersionHighPriority,
                s_LoaderWithHighVersionLowPriority
            };

            // Use the Assert class to test conditions
            var customLoaderFound = FeatureHelpersInternal.TryFindCustomLoaderWithHighestPriority(customLoaderFeatures, out var selectedCustomLoader);

            Assert.IsTrue(customLoaderFound, "A custom loader should've been found.");
            Assert.AreEqual(s_OverrideLoader, selectedCustomLoader, "Wrong custom loader chosen.");
        }

        [Test]
        public void UseLoaderWithHighestVersionAndHighPriority()
        {
            var customLoaderFeatures = new List<FeatureInfo>()
            {
                s_LoaderWithHighVersionHighPriority,
                s_LoaderWithHighVersionLowPriority
            };

            // Use the Assert class to test conditions
            var customLoaderFound = TryFindCustomLoaderWithHighestPriority(customLoaderFeatures, out var selectedCustomLoader);

            Assert.IsTrue(customLoaderFound, "A custom loader should've been found.");
            Assert.AreEqual(s_LoaderWithHighVersionHighPriority, selectedCustomLoader, "Wrong custom loader chosen.");
        }

        [Test]
        public void UseDefaultLoaderWhileDowngradedLoaderIsActive()
        {
            var customLoaderFeatures = new List<FeatureInfo>()
            {
                s_DowngradedLoader
            };

            // Use the Assert class to test conditions
            var customLoaderFound = TryFindCustomLoaderWithHighestPriority(customLoaderFeatures, out var _);

            Assert.IsFalse(customLoaderFound, "A downgraded custom loader was selected when the default loader has a higher version.");
        }

        [Test]
        public void UseDefaultLoaderWhileUpgradedLoaderIsInactive()
        {
            var customLoaderFeatures = new List<FeatureInfo>()
            {
                s_UpgradedButInactiveLoader
            };

            // Use the Assert class to test conditions
            var customLoaderFound = TryFindCustomLoaderWithHighestPriority(customLoaderFeatures, out var _);

            Assert.IsFalse(customLoaderFound, "An inactive custom loader was selected.");
        }

        [Test]
        public void LogErrorWhenManyOverrideLoadersAreActive()
        {
            var customLoaderFeatures = new List<FeatureInfo>()
            {
                s_OverrideLoader,
                s_OverrideLoader
            };

            var errorRegex = new Regex(
                @".+" + string.Join(",", customLoaderFeatures.Select(feature => feature.Attribute.UiName)));
            LogAssert.Expect(LogType.Error, errorRegex);

            TryFindCustomLoaderWithHighestPriority(customLoaderFeatures, out var _);
        }
    }
}
