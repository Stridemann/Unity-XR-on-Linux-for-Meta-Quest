using System;
using UnityEngine.XR.OpenXR;
using UnityEngine.XR.OpenXR.Features;

#if UNITY_EDITOR || PACKAGE_DOCS_GENERATION
namespace UnityEditor.XR.OpenXR.Features
{
    public class FeatureCategory
    {
        public const string Default = "";
        public const string Feature = "Feature";
        public const string Interaction = "Interaction";
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class OpenXRFeatureAttribute : Attribute
    {
        internal class CopyFieldAttribute : Attribute
        {
            public string FieldName;
            public CopyFieldAttribute(string fieldName)
            {
                FieldName = fieldName;
            }
        }

        /// <summary>
        /// Feature name to show in the feature configuration UI.
        /// </summary>
        [CopyField(nameof(OpenXRFeature.nameUi))] public string UiName;

        /// <summary>
        /// Hide this feature from the UI.
        /// </summary>
        public bool Hidden;

        /// <summary>
        /// Feature description to show in the UI.
        /// </summary>
        public string Desc;

        /// <summary>
        /// OpenXR runtime extension strings that need to be enabled to use this extension.
        /// If these extensions can't be enabled, a message will be logged, but execution will continue.
        /// Can contain multiple extensions separated by spaces.
        /// </summary>
        [CopyField(nameof(OpenXRFeature.openxrExtensionStrings))] public string OpenxrExtensionStrings;

        /// <summary>
        /// Company that created the feature, shown in the feature configuration UI.
        /// </summary>
        [CopyField(nameof(OpenXRFeature.company))] public string Company;

        /// <summary>
        /// Link to the feature documentation. The help button in the UI opens this link in a web browser.
        /// </summary>
        public string DocumentationLink;

        /// <summary>
        /// Feature version.
        /// </summary>
        [CopyField(nameof(OpenXRFeature.version))] public string Version;

        /// <summary>
        /// BuildTargets in this list use a custom runtime loader (that is, openxr_loader.dll).
        /// Only one feature per platform can have a custom runtime loader.
        /// Unity will skip copying the default loader to the build and use this feature's loader instead on these platforms.
        /// Loader must be placed alongside the OpenXRFeature script or in a subfolder of it.
        ///
        /// If this field is populated, it signifies that this OpenXRFeature provides its own OpenXR Loader
        /// </summary>
        public BuildTarget[] CustomRuntimeLoaderBuildTargets;

        /// <summary>
        /// OpenXR API version that the <see cref="OpenXRFeature"/>'s custom runtime loader (that is, openxr_loader.dll) supports, as a string.
        /// Refer to <see href="https://registry.khronos.org/OpenXR/specs/1.1/html/xrspec.html#fundamentals-api-version-numbers-and-semantics">OpenXR: API Version Numbers and Semantics</see>
        /// for specific details on the OpenXR versioning format.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Unity uses the custom runtime loader with the highest OpenXR loader version supported
        /// and the highest <see cref="Priority"/> set.
        /// </para>
        /// <para>
        /// If no <see cref="CustomRuntimeLoaderVersion"/> is provided, Unity overrides all other custom loaders with the
        /// one provided in this feature. Only one feature per platform can override all other custom runtime loaders.
        /// </para>
        /// <para>
        /// If more than one feature attempts to override other custom loaders on the same platform,
        /// Unity emits an error when an application developer attempts to enter Play mode or build
        /// their application. To successfully build a project, the application developer must
        /// disable all but one of the conflicting features. To avoid this scenario, you should only
        /// override other custom loaders when absolutely necessary.
        /// </para>
        /// <para>
        /// If no <see cref="CustomRuntimeLoaderBuildTargets"/> is specified for the OpenXRFeature, this field is ignored. CustomRuntimeLoaderBuildTargets must be populated to
        /// signal that this OpenXRFeature provides its own OpenXR Loader
        /// </para>
         /// <para>
        /// This field does not control the OpenXR API version that is used when launching the application. Use <see cref="TargetOpenXRApiVersion"/> to indicate that the application should
        /// target a specific version of the OpenXR API.
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// [OpenXRFeature(
        ///     CustomRuntimeLoaderBuildTargets = new []{ BuildTarget.StandaloneWindows64 },
        ///     CustomRuntimeLoaderVersion = "1.1.45")]
        /// public class MyCustomLoaderFeature : OpenXRFeature
        /// {
        ///     // Implementation code for the custom loader feature
        /// }
        /// </code>
        ///
        /// Examples of supported version strings:
        /// - "1.1.45"
        /// - "1.0.40"
        /// - "1.0.0"
        ///
        /// Example of unsupported version strings:
        /// - "1.0" (requires 3 version numbers major, minor, and patch)
        /// - "1.1.45.0" (extra version numbers)
        /// - "1.4.0f" (version numbers can't include a letter)
        /// </example>
        public string CustomRuntimeLoaderVersion;

        /// <summary>
        /// Name of the custom loader library to be used for this feature. The name must be the same for all custom loaders provided by this feature.
        /// </summary>
        /// <remarks>
        /// The customized name should not specify an extension, otherwise features that override loaders for multiple platforms may have some of
        /// their libraries removed from the built project.
        /// <para>
        /// If no <see cref="CustomRuntimeLoaderName"> is defined, the loader name will be assumed to be "openxr_loader".
        ///</para>
        ///<para>
        /// If no <see cref="CustomRuntimeLoaderBuildTargets"/> is specified for the OpenXRFeature, this loader name is ignored.
        /// </para>
        /// </remarks>
        [CopyField(nameof(OpenXRFeature.customRuntimeLoaderName))] public string CustomRuntimeLoaderName;

        /// <summary>
        /// BuildTargetsGroups that this feature supports. The feature will only be shown or included on these platforms.
        /// </summary>
        public BuildTargetGroup[] BuildTargetGroups;

        /// <summary>
        /// Feature category.
        /// </summary>
        public string Category = "";

        /// <summary>
        /// True if this feature is required, false otherwise.
        /// Required features will cause the loader to fail to initialize if they fail to initialize or start.
        /// </summary>
        [CopyField(nameof(OpenXRFeature.required))] public bool Required = false;

        /// <summary>
        /// Determines the order in which the feature will be called in both the GetInstanceProcAddr hook list and
        /// when events such as OnInstanceCreate are called. Higher priority features will hook after lower priority features and
        /// be called first in the event list.
        /// </summary>
        [CopyField(nameof(OpenXRFeature.priority))] public int Priority = 0;

        /// <summary>
        /// The OpenXR API version required by the <see cref="OpenXRFeature"/> for full functionality
        /// Refer to <see href="https://registry.khronos.org/OpenXR/specs/1.1/html/xrspec.html#fundamentals-api-version-numbers-and-semantics">OpenXR: API Version Numbers and Semantics</see>
        /// for specific details on the OpenXR versioning format.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Unity applications will use the highest OpenXR API version requested among all OpenXR Features and the default loader.
        /// If the targetted OpenXR API Version is lower than the version specified by the default loader, the default version is used instead.
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// [OpenXRFeature(
        ///     TargetOpenXRApiVersion = "1.1.45")]
        /// public class MyCustomLoaderFeature : OpenXRFeature
        /// {
        /// }
        /// </code>
        ///
        /// Examples of supported version strings:
        /// - "1.1.45"
        /// - "1.0.40"
        /// - "1.0.0"
        ///
        /// Example of unsupported version strings:
        /// - "1.0" (requires 3 version numbers major, minor, and patch)
        /// - "1.1.45.0" (extra version numbers)
        /// - "1.4.0f" (version numbers can't include a letter)
        /// </example>
        [CopyField(nameof(OpenXRFeature.targetOpenXRApiVersion))] public string TargetOpenXRApiVersion;

        /// <summary>
        /// A well known string id for this feature. It is recommended that that id be in reverse DNS naming format (com.foo.bar.feature).
        /// </summary>
        [CopyField(nameof(OpenXRFeature.featureIdInternal))] public string FeatureId = "";


        internal static readonly System.Text.RegularExpressions.Regex k_PackageVersionRegex = new System.Text.RegularExpressions.Regex(@"(\d*\.\d*)\..*");

        /// <summary>
        /// This method returns the OpenXR internal documentation link.  This is necessary because the documentation link was made public in the
        /// Costants class which prevents it from being alterned in anything but a major revision.  This method will patch up the documentation links
        /// as needed as long as they are internal openxr documentation links.
        /// </summary>
        internal string InternalDocumentationLink
        {
            get
            {
                if (string.IsNullOrEmpty(DocumentationLink))
                    return DocumentationLink;

                // Update the version if needed
                if (DocumentationLink.StartsWith(Constants.k_DocumentationManualURL))
                {
                    var version = PackageManager.PackageInfo.FindForAssembly(typeof(OpenXRFeatureAttribute).Assembly)?.version;
                    var majorminor = k_PackageVersionRegex.Match(version).Groups[1].Value;
                    DocumentationLink = DocumentationLink.Replace("1.0", majorminor);
                }

                return DocumentationLink;
            }
        }
    }
}
#endif
