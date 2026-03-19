using System;
using UnityEditor.Build.Reporting;
using UnityEngine.XR.OpenXR;
using UnityEngine.XR.OpenXR.Features;

namespace UnityEditor.XR.OpenXR.Features
{
    /// <summary>
    /// This class implements callback functions that will hook into the build process.
    /// They will be called when a project with the SpaceWarp feature enabled is being built.
    /// </summary>
    internal class SpaceWarpFeatureBuildHooks : OpenXRFeatureBuildHooks
    {
        /// <summary>
        /// The feature ID string. This is used to give the feature a well known ID for reference.
        /// </summary>
        const string k_SpaceWarpFeatureId = "com.unity.openxr.feature.spacewarp";

        /// <inheritdoc/>
        public override int callbackOrder => 2;

        /// <inheritdoc/>
        protected internal override bool bootConfigEntryRequired => true;

        /// <summary>
        /// Returns the System.Type of the SpaceWarpFeature class.
        /// </summary>
        public override Type featureType => typeof(SpaceWarpFeature);

        /// <inheritdoc/>
        /// <remarks>
        /// This function in the base class is abstract, so there is no default implementation.
        /// </remarks>
        protected override void OnPreprocessBuildExt(BuildReport report) { }

        /// <inheritdoc/>
        /// <remarks>
        /// This function in the base class is abstract, so there is no default implementation.
        /// </remarks>
        protected override void OnPostGenerateGradleAndroidProjectExt(string path) { }

        /// <inheritdoc/>
        /// <remarks>
        /// This function in the base class is abstract, so there is no default implementation.
        /// </remarks>
        protected override void OnPostprocessBuildExt(BuildReport report) { }

        /// <summary>
        /// Called during the build process when SpaceWarp extension is enabled. Adds Boot Config
        /// Settings to allow access to features and tools tailored for Meta's extended reality (XR) platform.
        /// </summary>
        /// <param name="report">BuildReport that contains information about the build, such as the target platform and
        /// output path.</param>
        /// <param name="builder">This is the Boot Config interface that can be used to write boot configs</param>
        protected override void OnProcessBootConfigExt(BuildReport report, BootConfigBuilder builder)
        {
            var spaceWarpFeature = OpenXRSettings.ActiveBuildTargetInstance.GetFeature<SpaceWarpFeature>();
            builder.SetBootConfigBoolean("xr-meta-enabled", spaceWarpFeature != null && spaceWarpFeature.enabled);
        }
    }
}
