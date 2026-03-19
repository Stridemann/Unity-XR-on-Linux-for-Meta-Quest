using System;
using NUnit.Framework;
using System.Collections;
using UnityEngine.TestTools;
using UnityEngine.XR.OpenXR.Features;
using UnityEngine.XR.OpenXR.Features.Interactions;

namespace UnityEngine.XR.OpenXR.Tests
{
    internal class TargetOpenXRApiVersionTest : OpenXRLoaderSetup
    {
        private OpenXRFeature EnableFeatureWithApiVersion(Type featureType, string ApiVersion, bool enable)
        {
            var feature = GetFeature(featureType);
            Assert.IsNotNull(feature);
            feature.targetOpenXRApiVersion = ApiVersion;
            feature.enabled = enable;
            return feature;
        }

        private T EnableFeatureWithApiVersion<T>(string ApiVersion, bool enable) where T : OpenXRFeature => EnableFeatureWithApiVersion(typeof(T), ApiVersion, enable) as T;

        private string ToString(OpenXRApiVersion apiVersion)
        {
            return $"{apiVersion.Major}.{apiVersion.Minor}.{apiVersion.Patch}";
        }

#if !OPENXR_USE_KHRONOS_LOADER
        // Only enabled features version being considered
        [UnityTest]
        public IEnumerator SelectEnabledFeature()
        {
            string enabledVersion = "1.100.1000"; // enabled feature with lower api version
            string disabledVersion = "1.100.2000"; // disabled feature with higher api version

            EnableFeatureWithApiVersion<OculusTouchControllerProfile>(enabledVersion, true);
            EnableFeatureWithApiVersion<MetaQuestTouchProControllerProfile>(disabledVersion, false);

            base.InitializeAndStart();
            yield return null;
            Assert.AreEqual(OpenXRRuntime.apiVersion, enabledVersion);
        }

        // Only valid version being added
        [UnityTest]
        public IEnumerator SelectValidApiVersion()
        {
            string validVersion = "1.100.1000";
            string invalidVersion1 = "1.100";
            string invalidVersion2 = "1.100a.2000b";

            EnableFeatureWithApiVersion<OculusTouchControllerProfile>(validVersion, true);
            EnableFeatureWithApiVersion<MetaQuestTouchProControllerProfile>(invalidVersion1, true);
            EnableFeatureWithApiVersion<MetaQuestTouchPlusControllerProfile>(invalidVersion2, true);

            base.InitializeAndStart();
            yield return null;
            Assert.AreEqual(OpenXRRuntime.apiVersion, validVersion);
        }

        // Ignore versions lower than default
        [UnityTest]
        public IEnumerator IgnoreLowerApiVersion()
        {
            string invalidVersion1 = "1.1.42";
            string invalidVersion2 = "1.0.43";
            string invalidVersion3 = "1.0.100";

            EnableFeatureWithApiVersion<MetaQuestTouchProControllerProfile>(invalidVersion1, true);
            EnableFeatureWithApiVersion<MetaQuestTouchPlusControllerProfile>(invalidVersion2, true);
            EnableFeatureWithApiVersion<OculusTouchControllerProfile>(invalidVersion3, true);

            base.InitializeAndStart();
            yield return null;
            Assert.AreEqual(OpenXRRuntime.apiVersion, ToString(OpenXRApiVersion.Current));
        }

        // Ignore major version > 1 cases
        [UnityTest]
        public IEnumerator IgnoreMajorApiVersion()
        {
            string invalidVersion1 = "2.0.100";
            string invalidVersion2 = "2.1.1";
            string validVersion = "1.2.100";

            EnableFeatureWithApiVersion<MetaQuestTouchProControllerProfile>(invalidVersion1, true);
            EnableFeatureWithApiVersion<MetaQuestTouchPlusControllerProfile>(invalidVersion2, true);
            EnableFeatureWithApiVersion<OculusTouchControllerProfile>(validVersion, true);

            base.InitializeAndStart();
            yield return null;
            Assert.AreEqual(OpenXRRuntime.apiVersion, validVersion);
        }

        // Ignore versions not following patch version incremental policy
        [UnityTest]
        public IEnumerator IgnorePatchApiVersionNotIncremental()
        {
            string invalidVersion1 = "1.2.5";
            string invalidVersion2 = "1.3.20";

            EnableFeatureWithApiVersion<MetaQuestTouchProControllerProfile>(invalidVersion1, true);
            EnableFeatureWithApiVersion<MetaQuestTouchPlusControllerProfile>(invalidVersion2, true);

            base.InitializeAndStart();
            yield return null;
            Assert.AreEqual(OpenXRRuntime.apiVersion, ToString(OpenXRApiVersion.Current));
        }

        // Select the highest version
        [UnityTest]
        public IEnumerator SelectHighestVersion()
        {
            string version1 = "1.2.100";
            string version2 = "1.1.200";
            string version3 = "1.0.300";

            EnableFeatureWithApiVersion<OculusTouchControllerProfile>(version1, true);
            EnableFeatureWithApiVersion<MetaQuestTouchPlusControllerProfile>(version2, true);
            EnableFeatureWithApiVersion<MetaQuestTouchProControllerProfile>(version3, true);

            base.InitializeAndStart();
            yield return null;
            Assert.AreEqual(OpenXRRuntime.apiVersion, version1);
        }

        // Make sure default version is set as expected
        [UnityTest]
        public IEnumerator TestVersionFallback()
        {
            string version1 = "";
            string version2 = "";
            string version3 = "";

            EnableFeatureWithApiVersion<OculusTouchControllerProfile>(version1, false);
            EnableFeatureWithApiVersion<MetaQuestTouchPlusControllerProfile>(version2, false);
            EnableFeatureWithApiVersion<MetaQuestTouchProControllerProfile>(version3, false);

            base.InitializeAndStart();
            yield return null;
            Assert.AreEqual(OpenXRRuntime.apiVersion, ToString(OpenXRApiVersion.Current));
        }
#endif
    }
}
