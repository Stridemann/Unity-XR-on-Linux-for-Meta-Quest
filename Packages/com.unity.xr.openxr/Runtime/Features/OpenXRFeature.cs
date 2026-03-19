using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Serialization;
using UnityEngine.InputSystem;
using UnityEngine.XR.OpenXR.Input;
using UnityEngine.XR.OpenXR.NativeTypes;
#if UNITY_EDITOR
using UnityEditor;
using System.Linq;
#endif

[assembly: InternalsVisibleTo("Unity.XR.OpenXR.Editor")]
[assembly: InternalsVisibleTo("Unity.XR.OpenXR.Features.MetaQuestSupport.Editor")]
[assembly: InternalsVisibleTo("UnityEditor.XR.OpenXR.Tests")]
[assembly: InternalsVisibleTo("Unity.XR.OpenXR.Testing")]
[assembly: InternalsVisibleTo("Unity.XR.OpenXR.TestTooling.Tests")]
namespace UnityEngine.XR.OpenXR.Features
{
    /// <summary>
    /// A Unity OpenXR Feature.
    /// This class can be inherited from to add feature specific data and logic.
    /// Feature-specific settings are serialized for access at runtime.
    /// </summary>
    [Serializable]
    public abstract partial class OpenXRFeature : ScriptableObject
    {
#if UNITY_EDITOR
        internal static Func<string, bool> canSetFeatureDisabled;
#endif
        /// <summary>
        /// Feature will be enabled when OpenXR is initialized.
        /// </summary>
        [FormerlySerializedAs("enabled")]
        [HideInInspector]
        [SerializeField]
        bool m_enabled;

        internal bool failedInitialization { get; private set; }

        /// <summary>
        /// True if a required feature failed initialization, false if all features initialized successfully.
        /// </summary>
        internal static bool requiredFeatureFailed { get; private set; }

        /// <summary>
        /// Feature is enabled and will be started when the OpenXR loader is initialized.
        ///
        /// Note that the enabled state of a feature cannot be modified once OpenXR is initialized and
        /// can be used at runtime to determine if a feature successfully initialized.
        /// </summary>
        public bool enabled
        {
            get => m_enabled && (OpenXRLoaderBase.Instance == null || !failedInitialization);
            set
            {
                if (enabled == value)
                    return;

#if UNITY_EDITOR
                if (canSetFeatureDisabled != null && !value && !canSetFeatureDisabled.Invoke(featureIdInternal))
                    return;
#endif //UNITY_EDITOR

                if (OpenXRLoaderBase.Instance != null)
                {
                    Debug.LogError("OpenXRFeature.enabled cannot be changed while OpenXR is running");
                    return;
                }

                m_enabled = value;

#if UNITY_EDITOR
                EditorUtility.SetDirty(this);
#endif //UNITY_EDITOR

                OnEnabledChange();
            }
        }

        /// <summary>
        /// Automatically filled out by the build process from OpenXRFeatureAttribute.
        /// Name of the feature.
        /// </summary>
        [HideInInspector] [SerializeField] internal string nameUi;

        /// <summary>
        /// Automatically filled out by the build process from OpenXRFeatureAttribute.
        /// Version of the feature.
        /// </summary>
        [HideInInspector] [SerializeField] internal string version;

        /// <summary>
        /// Feature id.
        /// </summary>
        [HideInInspector] [SerializeField] internal string featureIdInternal;

        /// <summary>
        /// Automatically filled out by the build process from OpenXRFeatureAttribute.
        /// OpenXR runtime extension strings that need to be enabled to use this extension.
        /// May contain multiple extensions separated by spaces.
        /// </summary>
        [HideInInspector] [SerializeField] internal string openxrExtensionStrings;

        /// <summary>
        /// Automatically filled out by the build process from OpenXRFeatureAttribute.
        /// Company name of the author of the feature.
        /// </summary>
        [HideInInspector] [SerializeField] internal string company;

        /// <summary>
        /// Automatically filled out by the build process from OpenXRFeatureAttribute.
        /// Priority of the feature.
        /// </summary>
        [HideInInspector] [SerializeField] internal int priority;

        /// <summary>
        /// Automatically filled out by the build process from OpenXRFeatureAttribute.
        /// OpenXR API version that the feature requests to be used when creating the XR Instance.
        /// </summary>
        [HideInInspector] [SerializeField] internal string targetOpenXRApiVersion;

        /// <summary>
        /// Automatically filled out by the build process from OpenXRFeatureAttribute.
        /// True if the feature is required, false otherwise.
        /// </summary>
        [HideInInspector] [SerializeField] internal bool required;

        /// <summary>
        /// Automatically filled out by the build process from OpenXRFeatureAttribute.
        /// Name of the custom loader, if defined.
        /// </summary>
        [HideInInspector] [SerializeField] internal string customRuntimeLoaderName = null;

        /// <summary>
        /// Set to true if the internal fields have been updated in the current domain
        /// </summary>
        [NonSerialized]
        internal bool internalFieldsUpdated = false;

        /// <summary>
        /// Accessor for xrGetInstanceProcAddr function pointer.
        /// </summary>
        protected static IntPtr xrGetInstanceProcAddr => Internal_GetProcAddressPtr(false);

        /// <summary>
        /// Called to hook xrGetInstanceProcAddr.
        /// Returning a different function pointer allows intercepting any OpenXR method.
        /// </summary>
        /// <param name="func">xrGetInstanceProcAddr native function pointer</param>
        /// <returns>Function pointer that Unity will use to look up OpenXR native functions.</returns>
        protected internal virtual IntPtr HookGetInstanceProcAddr(IntPtr func) => func;

        /// <summary>
        /// Called after the OpenXR Loader is initialized and has created its subsystems.
        /// </summary>
        protected internal virtual void OnSubsystemCreate() { }

        /// <summary>
        /// Called after the OpenXR loader has started its subsystems.
        /// </summary>
        protected internal virtual void OnSubsystemStart() { }

        /// <summary>
        /// Called before the OpenXR loader stops its subsystems.
        /// </summary>
        protected internal virtual void OnSubsystemStop() { }

        /// <summary>
        /// Called before the OpenXR loader destroys its subsystems.
        /// </summary>
        protected internal virtual void OnSubsystemDestroy() { }

        /// <summary>
        /// Called after `xrCreateInstance`. Override this method to validate that any necessary OpenXR extensions were
        /// successfully enabled (<see cref="OpenXRRuntime.IsExtensionEnabled">OpenXRRuntime.IsExtensionEnabled</see>)
        /// and that any required system properties are supported. If this method returns <see langword="false"/>,
        /// the feature's <see cref="OpenXRFeature.enabled"/> property is set to <see langword="false"/>.
        /// </summary>
        /// <param name="xrInstance">Handle of the native `xrInstance`.</param>
        /// <returns><see langword="true"/> if this feature successfully initialized. Otherwise, <see langword="false"/>.</returns>
        /// <remarks>
        /// If this feature is a required feature of an enabled feature set, returning <see langword="false"/> here
        /// causes the `OpenXRLoader` to fail, and XR Plug-in Management will fall back to another loader if enabled.
        /// </remarks>
        /// <seealso href="xref:openxr-features#enabling-openxr-spec-extension-strings">Enabling OpenXR spec extension strings</seealso>
        protected internal virtual bool OnInstanceCreate(ulong xrInstance) => true;

        /// <summary>
        /// Called after xrGetSystem.
        /// </summary>
        /// <param name="xrSystem">Handle of the xrSystemId</param>
        protected internal virtual void OnSystemChange(ulong xrSystem) { }

        /// <summary>
        /// Called after xrCreateSession.
        /// </summary>
        /// <param name="xrSession">Handle of the xrSession</param>
        protected internal virtual void OnSessionCreate(ulong xrSession) { }

        /// <summary>
        /// Called when the reference xrSpace for the app changes.
        /// </summary>
        /// <param name="xrSpace">Handle of the xrSpace</param>
        protected internal virtual void OnAppSpaceChange(ulong xrSpace) { }

        /// <summary>
        /// Called when the OpenXR loader receives the XR_TYPE_EVENT_DATA_SESSION_STATE_CHANGED event
        /// from the runtime signaling that the XrSessionState has changed.
        /// </summary>
        /// <param name="oldState">Previous state</param>
        /// <param name="newState">New state</param>
        protected internal virtual void OnSessionStateChange(int oldState, int newState) { }

        /// <summary>
        /// Called after xrSessionBegin.
        /// </summary>
        /// <param name="xrSession">Handle of the xrSession</param>
        protected internal virtual void OnSessionBegin(ulong xrSession) { }

        /// <summary>
        /// Called before xrEndSession.
        /// </summary>
        /// <param name="xrSession">Handle of the xrSession</param>
        protected internal virtual void OnSessionEnd(ulong xrSession) { }

        /// <summary>
        /// Called when the runtime transitions to the XR_SESSION_STATE_EXITING state.
        /// </summary>
        /// <param name="xrSession">Handle of the xrSession</param>
        protected internal virtual void OnSessionExiting(ulong xrSession) { }

        /// <summary>
        /// Called before xrDestroySession.
        /// </summary>
        /// <param name="xrSession">Handle of the xrSession</param>
        protected internal virtual void OnSessionDestroy(ulong xrSession) { }

        /// <summary>
        /// Called before xrDestroyInstance
        /// </summary>
        /// <param name="xrInstance">Handle of the xrInstance</param>
        protected internal virtual void OnInstanceDestroy(ulong xrInstance) { }

        /// <summary>
        /// Called when the runtime transitions to the XR_SESSION_STATE_LOSS_PENDING
        /// state. This is a notification to the feature implementer that the session is
        /// about to be lost. This feature should do what it needs to do to
        /// prepare for potential session recreation.
        /// </summary>
        /// <param name="xrSession">The session that is going to be lost</param>
        protected internal virtual void OnSessionLossPending(ulong xrSession) { }

        /// <summary>
        /// Called when the OpenXR loader receives the XR_TYPE_EVENT_DATA_INSTANCE_LOSS_PENDING event
        /// from the runtime.  This is a notification to the feature implementer that the instance is
        /// about to be lost. This feature should do what it needs to do to
        /// clean up in preparation for termination.
        /// </summary>
        /// <param name="xrInstance">The instance that is going to be lost</param>
        protected internal virtual void OnInstanceLossPending(ulong xrInstance) { }

        /// <summary>
        /// Notification to the feature implementer that the form factor has changed.
        /// </summary>
        /// <param name="xrFormFactor">New form factor value</param>
        protected internal virtual void OnFormFactorChange(int xrFormFactor) { }

        /// <summary>
        /// Notification to the feature implementer that the view configuration type has changed.
        /// </summary>
        /// <param name="xrViewConfigurationType">New view configuration type</param>
        protected internal virtual void OnViewConfigurationTypeChange(int xrViewConfigurationType) { }

        /// <summary>
        /// Notification to the feature implementer that the environment blend mode has changed.
        /// </summary>
        /// <param name="xrEnvironmentBlendMode">New environment blend mode value</param>
        protected internal virtual void OnEnvironmentBlendModeChange(XrEnvironmentBlendMode xrEnvironmentBlendMode) { }

        /// <summary>
        /// Called when the enabled state of a feature changes
        /// </summary>
        protected internal virtual void OnEnabledChange()
        {
        }

        /// <summary>
        /// Converts an XrPath to a string.
        /// </summary>
        /// <param name="path">Path to convert</param>
        /// <returns>String that represents the path, or null if the path is invalid.</returns>
        protected static string PathToString(ulong path) =>
            Internal_PathToStringPtr(path, out var stringPtr) ? Marshal.PtrToStringAnsi(stringPtr) : null;

        /// <summary>
        /// Converts a string to an XrPath.
        /// </summary>
        /// <param name="str">String to convert</param>
        /// <returns>Path of converted string, or XrPath.none if string could not be converted.</returns>
        protected static ulong StringToPath(string str) =>
            Internal_StringToPath(str, out var id) ? id : 0ul;

        /// <summary>
        /// Returns the path of the current interaction profile for the given user path.
        /// </summary>
        /// <param name="userPath">OpenXR User Path (eg: /user/hand/left)</param>
        /// <returns>A path to the interaction profile, or XrPath.none if the path could not be retrieved.</returns>
        protected static ulong GetCurrentInteractionProfile(ulong userPath) =>
            Internal_GetCurrentInteractionProfile(userPath, out ulong profileId) ? profileId : 0ul;

        /// <summary>
        /// Returns the path of the current interaction profile for the given user path.
        /// </summary>
        /// <param name="userPath">User path</param>
        /// <returns>A path to the interaction profile, or XrPath.none if the path could not be retrieved.</returns>
        protected static ulong GetCurrentInteractionProfile(string userPath) =>
            GetCurrentInteractionProfile(StringToPath(userPath));

        /// <summary>
        /// Returns the current app space.
        /// </summary>
        /// <returns>Current app space</returns>
        protected static ulong GetCurrentAppSpace() =>
            Internal_GetAppSpace(out ulong appSpaceId) ? appSpaceId : 0ul;

        /// <summary>
        /// Returns viewConfigurationType for the given renderPass index.
        /// </summary>
        /// <param name="renderPassIndex">RenderPass index</param>
        /// <returns>viewConfigurationType for certain renderPass. Return 0 if invalid renderPass.</returns>
        protected static int GetViewConfigurationTypeForRenderPass(int renderPassIndex) =>
            Internal_GetViewTypeFromRenderIndex(renderPassIndex);

        /// <summary>
        /// Set the current XR Environment Blend Mode if it is supported by the active runtime. If not supported, fall back to the runtime preference.
        /// </summary>
        /// <param name="xrEnvironmentBlendMode">Environment Blend Mode (e.g.: Opaque = 1, Additive = 2, AlphaBlend = 3)</param>
        protected static void SetEnvironmentBlendMode(XrEnvironmentBlendMode xrEnvironmentBlendMode) =>
            Internal_SetEnvironmentBlendMode(xrEnvironmentBlendMode);

        /// <summary>
        /// Returns the current XR Environment Blend Mode.
        /// </summary>
        /// <returns>Current XR Environment Blend Mode</returns>
        protected static XrEnvironmentBlendMode GetEnvironmentBlendMode() =>
            Internal_GetEnvironmentBlendMode();

#if UNITY_EDITOR
        /// <summary>
        /// A Build-time validation rule.
        /// </summary>
        public class ValidationRule
        {
            /// <summary>
            /// Creates a validation rule for an OpenXRFeature.
            /// </summary>
            /// <param name="feature">Feature to create validation rule for</param>
            public ValidationRule(OpenXRFeature feature)
            {
                if (feature == null)
                    throw new Exception("Invalid feature");
                this.feature = feature;
            }

            internal ValidationRule()
            {}

            /// <summary>
            /// Message describing the rule that will be showed to the developer if it fails.
            /// </summary>
            public string message;

            /// <summary>
            /// Lambda function that returns true if validation passes, false if validation fails.
            /// </summary>
            public Func<bool> checkPredicate;

            /// <summary>
            /// Lambda function that fixes the issue, if possible.
            /// </summary>
            public Action fixIt;

            /// <summary>
            /// Text describing how the issue is fixed, shown in a tooltip.
            /// </summary>
            public string fixItMessage;

            /// <summary>
            /// True if the fixIt Lambda function performs a function that is automatic and does not require user input.  If your fixIt
            /// function requires user input, set fixitAutomatic to false to prevent the fixIt method from being executed during fixAll
            /// </summary>
            public bool fixItAutomatic = true;

            /// <summary>
            /// If true, failing the rule is treated as an error and stops the build.
            /// If false, failing the rule is treated as a warning and it doesn't stop the build. The developer has the option to correct the problem, but is not required to.
            /// </summary>
            public bool error;

            /// <summary>
            /// If true, will deny the project from entering playmode in editor.
            /// If false, can still enter playmode in editor if this issue isn't fixed.
            /// </summary>
            public bool errorEnteringPlaymode;

            /// <summary>
            /// Optional text to display in a help icon with the issue in the validator.
            /// </summary>
            public string helpText;

            /// <summary>
            /// Optional link that will be opened if the help icon is clicked.
            /// </summary>
            public string helpLink;

            /// <summary>
            /// Optional struct HighlighterFocusData used to create HighlighterFocus functionality.
            /// WindowTitle contains the name of the window tab to highlight in.
            /// SearchPhrase contains the text to be searched and highlighted.
            /// </summary>
            public HighlighterFocusData highlighterFocus { get; set; }
            public struct HighlighterFocusData
            {
                public string windowTitle { get; set; }
                public string searchText { get; set; }
            }

            internal OpenXRFeature feature;

            internal BuildTargetGroup buildTargetGroup = BuildTargetGroup.Unknown;
        }

        /// <summary>
        /// Allows a feature to add to a list of validation rules which your feature will evaluate at build time.
        /// Details of the validation results can be found in OpenXRProjectValidation.
        /// </summary>
        /// <param name="rules">Your feature will check the rules in this list at build time. Add rules that you want your feature to check, and remove rules that you want your feature to ignore.</param>
        /// <param name="targetGroup">Build target group these validation rules will be evaluated for.</param>
        protected internal virtual void GetValidationChecks(List<ValidationRule> rules, BuildTargetGroup targetGroup)
        {
            // Check if this feature has an OpenXR API Version that it targets. If it does, validate that it targets an API Version
            // that is higher than the API version that our package is currently built against.
            OpenXRApiVersion apiVersion = OpenXRApiVersion.TryParse(targetOpenXRApiVersion, out var version) ? version : null;
            if (apiVersion != null)
            {
                if(apiVersion.Major != OpenXRApiVersion.Current.Major)
                {
                    rules.Add(new ValidationRule(this)
                    {
                        message = $"The targeted OpenXR API version uses an unsupported major version and will be ignored. Please check the latest supported versions at: https://github.com/KhronosGroup/OpenXR-SDK-Source/releases",
                        checkPredicate = () =>
                        {
                            return apiVersion.Major == OpenXRApiVersion.Current.Major;
                        },
                        fixItMessage = "Update the OpenXR Features to target the appropriate API version, or disable it"
                    });

                }
                else
                {
                    rules.Add(new ValidationRule(this)
                    {
                        message = $"This OpenXR Feature targets an API version with a patch version lower than {OpenXRApiVersion.Current}, which is the recommended version for this package. The requested API version will be ignored.",
                        checkPredicate = () =>
                        {
                            return apiVersion.Patch >= OpenXRApiVersion.Current.Patch;
                        },
                        fixItMessage = "Update the OpenXR Features to target the appropriate API version, or disable it"
                    });
                }
            }
        }

        internal static void GetValidationList(List<ValidationRule> rules, BuildTargetGroup targetGroup)
        {
            var openXrSettings = OpenXRSettings.GetSettingsForBuildTargetGroup(targetGroup);
            if (openXrSettings == null)
            {
                return;
            }

            var features = openXrSettings.features.Where(f => f != null)
                .OrderByDescending(f => f.priority)
                .ThenBy(f => f.nameUi);
            foreach (var feature in features)
            {
                if (feature != null && feature.enabled)
                    feature.GetValidationChecks(rules, targetGroup);
            }
        }

#endif

        /// <summary>
        /// Creates a subsystem based on a given a list of descriptors and a specific subsystem id.
        /// Promoted to public for extensions.
        /// </summary>
        ///
        /// <typeparam name="TDescriptor">The descriptor type being passed in</typeparam>
        /// <typeparam name="TSubsystem">The subsystem type being requested</typeparam>
        /// <param name="descriptors">List of TDescriptor instances to use for subsystem matching</param>
        /// <param name="id">The identifier key of the particular subsystem implementation being requested</param>
        protected void CreateSubsystem<TDescriptor, TSubsystem>(List<TDescriptor> descriptors, string id)
            where TDescriptor : ISubsystemDescriptor
            where TSubsystem : ISubsystem
        {
            if (OpenXRLoaderBase.Instance == null)
            {
                Debug.LogError("CreateSubsystem called before loader was initialized");
                return;
            }

            OpenXRLoaderBase.Instance.CreateSubsystem<TDescriptor, TSubsystem>(descriptors, id);
        }

        /// <summary>
        /// Start a subsystem instance of a given type. Subsystem is assumed to already be loaded from
        /// a previous call to CreateSubsystem.
        /// Promoted to public for extensions.
        /// </summary>
        ///
        /// <typeparam name="T">A subclass of <see cref="ISubsystem"/></typeparam>
        protected void StartSubsystem<T>() where T : class, ISubsystem
        {
            if (OpenXRLoaderBase.Instance == null)
            {
                Debug.LogError("StartSubsystem called before loader was initialized");
                return;
            }

            OpenXRLoaderBase.Instance.StartSubsystem<T>();
        }

        /// <summary>
        /// Stops a subsystem instance of a given type. Subsystem is assumed to already be loaded from
        /// a previous call to CreateSubsystem.
        /// Promoted to public for extensions.
        /// </summary>
        ///
        /// <typeparam name="T">A subclass of <see cref="ISubsystem"/></typeparam>
        protected void StopSubsystem<T>() where T : class, ISubsystem
        {
            if (OpenXRLoaderBase.Instance == null)
            {
                Debug.LogError("StopSubsystem called before loader was initialized");
                return;
            }

            OpenXRLoaderBase.Instance.StopSubsystem<T>();
        }

        /// <summary>
        /// Destroys a subsystem instance of a given type. Subsystem is assumed to already be loaded from
        /// a previous call to CreateSubsystem.
        /// Promoted to public for extensions.
        /// </summary>
        ///
        /// <typeparam name="T">A subclass of <see cref="ISubsystem"/></typeparam>
        protected void DestroySubsystem<T>() where T : class, ISubsystem
        {
            if (OpenXRLoaderBase.Instance == null)
            {
                Debug.LogError("DestroySubsystem called before loader was initialized");
                return;
            }

            OpenXRLoaderBase.Instance.DestroySubsystem<T>();
        }

        /// <summary>Called when the object is loaded.</summary>
        /// <remarks>
        /// Additional information:
        /// <a href="https://docs.unity3d.com/ScriptReference/ScriptableObject.OnEnable.html">ScriptableObject.OnEnable</a>
        /// </remarks>
        protected virtual void OnEnable()
        {
        }

        /// <summary>Called when the object is loaded.</summary>
        /// <remarks>
        /// Additional information:
        /// <a href="https://docs.unity3d.com/ScriptReference/ScriptableObject.OnDisable.html">ScriptableObject.OnDisable</a>
        /// </remarks>
        protected virtual void OnDisable()
        {
            // Virtual for future expansion and to match OnEnable
        }

        /// <summary>Called when the object is loaded.</summary>
        /// <remarks>
        /// Additional information:
        /// <a href="https://docs.unity3d.com/ScriptReference/ScriptableObject.Awake.html">ScriptableObject.Awake</a>
        /// </remarks>
        protected virtual void Awake()
        {
        }

        internal enum LoaderEvent
        {
            SubsystemCreate,
            SubsystemDestroy,
            SubsystemStart,
            SubsystemStop,
        }

        internal static bool ReceiveLoaderEvent(OpenXRLoaderBase loader, LoaderEvent e)
        {
            var instance = OpenXRSettings.Instance;
            if (instance == null)
                return true;

            foreach (var feature in instance.features)
            {
                if (feature == null || !feature.enabled)
                    continue;

                switch (e)
                {
                    case LoaderEvent.SubsystemCreate:
                        feature.OnSubsystemCreate();
                        break;
                    case LoaderEvent.SubsystemDestroy:
                        feature.OnSubsystemDestroy();
                        break;
                    case LoaderEvent.SubsystemStart:
                        feature.OnSubsystemStart();
                        break;
                    case LoaderEvent.SubsystemStop:
                        feature.OnSubsystemStop();
                        break;

                    default:
                        throw new ArgumentOutOfRangeException(nameof(e), e, null);
                }
            }

            return true;
        }

        // Must be kept in sync with unity_type.h ScriptEvents
        internal enum NativeEvent
        {
            // Setup
            XrSetupConfigValues,
            XrSystemIdChanged,
            XrInstanceChanged,
            XrSessionChanged,
            XrBeginSession,

            // Runtime
            XrSessionStateChanged,
            XrChangedSpaceApp,

            // Shutdown
            XrEndSession,
            XrDestroySession,
            XrDestroyInstance,

            // General Session Events
            XrIdle,
            XrReady,
            XrSynchronized,
            XrVisible,
            XrFocused,
            XrStopping,
            XrExiting,
            XrLossPending,
            XrInstanceLossPending,
            XrRestartRequested,
            XrRequestRestartLoop,
            XrRequestGetSystemLoop,
        };

        internal static void ReceiveNativeEvent(NativeEvent e, ulong payload)
        {
            if (null == OpenXRSettings.Instance)
                return;

            foreach (var feature in OpenXRSettings.Instance.features)
            {
                if (feature == null || !feature.enabled)
                    continue;

                switch (e)
                {
                    case NativeEvent.XrSetupConfigValues:
                        feature.OnFormFactorChange(Internal_GetFormFactor());
                        feature.OnEnvironmentBlendModeChange(Internal_GetEnvironmentBlendMode());
                        feature.OnViewConfigurationTypeChange(Internal_GetViewConfigurationType());
                        break;

                    case NativeEvent.XrSystemIdChanged:
                        feature.OnSystemChange(payload);
                        break;
                    case NativeEvent.XrInstanceChanged:
                        feature.failedInitialization = !feature.OnInstanceCreate(payload);
                        requiredFeatureFailed |= (feature.required && feature.failedInitialization);
                        break;
                    case NativeEvent.XrSessionChanged:
                        feature.OnSessionCreate(payload);
                        break;
                    case NativeEvent.XrBeginSession:
                        feature.OnSessionBegin(payload);
                        break;
                    case NativeEvent.XrChangedSpaceApp:
                        feature.OnAppSpaceChange(payload);
                        break;
                    case NativeEvent.XrSessionStateChanged:
                        Internal_GetSessionState(out var oldState, out var newState);
                        feature.OnSessionStateChange(oldState, newState);
                        break;
                    case NativeEvent.XrEndSession:
                        feature.OnSessionEnd(payload);
                        break;
                    case NativeEvent.XrExiting:
                        feature.OnSessionExiting(payload);
                        break;
                    case NativeEvent.XrDestroySession:
                        feature.OnSessionDestroy(payload);
                        break;
                    case NativeEvent.XrDestroyInstance:
                        feature.OnInstanceDestroy(payload);
                        break;
                    case NativeEvent.XrLossPending:
                        feature.OnSessionLossPending(payload);
                        break;
                    case NativeEvent.XrInstanceLossPending:
                        feature.OnInstanceLossPending(payload);
                        break;
                }
            }
        }

        internal static void Initialize()
        {
            requiredFeatureFailed = false;

            var instance = OpenXRSettings.Instance;
            if (instance == null || instance.features == null)
                return;

            foreach (var feature in instance.features)
                if (feature != null)
                    feature.failedInitialization = false;
        }

        internal static void HookGetInstanceProcAddr()
        {
            var procAddr = Internal_GetProcAddressPtr(true);

            var instance = OpenXRSettings.Instance;
            if (instance != null && instance.features != null)
            {
                // Hook the features in reverse priority order to ensure the highest priority feature is
                // hooked last.  This will ensure the highest priority feature is called first in the chain.
                for (var featureIndex = instance.features.Length - 1; featureIndex >= 0; featureIndex--)
                {
                    var feature = instance.features[featureIndex];
                    if (feature == null || !feature.enabled)
                        continue;

                    procAddr = feature.HookGetInstanceProcAddr(procAddr);
                }
            }

            Internal_SetProcAddressPtrAndLoadStage1(procAddr);
        }

        /// <summary>
        /// Returns XrAction handle bound to the given <see cref="UnityEngine.InputSystem.InputAction"/>.
        /// </summary>
        /// <param name="inputAction">Action to retrieve XrAction handles for</param>
        /// <returns>XrAction handle bound to the given <see cref="UnityEngine.InputSystem.InputAction"/> or 0 if there is no bound XrAction</returns>
        protected ulong GetAction(InputAction inputAction) => OpenXRInput.GetActionHandle(inputAction);

        /// <summary>
        /// Returns XrAction handle bound to the given device and usage.
        /// </summary>
        /// <param name="device">Device to retrieve XrAction handles for</param>
        /// <param name="usage">Usage to retrieve XrAction handles for</param>
        /// <returns>XrAction handle bound to the given device and usage, or 0 if there is no bound XrAction</returns>
        protected ulong GetAction(InputDevice device, InputFeatureUsage usage) => OpenXRInput.GetActionHandle(device, usage);

        /// <summary>
        /// Returns XrAction handle bound to the given device and usage.
        /// </summary>
        /// <param name="device">Device to retrieve XrAction handles for</param>
        /// <param name="usageName">Usage name to retrieve XrAction handles for</param>
        /// <returns>XrAction handle bound to the given device and usage, or 0 if there is no bound XrAction</returns>
        protected ulong GetAction(InputDevice device, string usageName) => OpenXRInput.GetActionHandle(device, usageName);

        /// <summary>
        /// Flags that control various options and behaviors on registered stats.
        /// </summary>
        [Flags]
        protected internal enum StatFlags
        {
            /// <summary>
            /// Stat will have no special options or behaviors
            /// </summary>
            StatOptionNone = 0,
            /// <summary>
            /// Stat will clear to 0.0f at the beginning of every frame
            /// </summary>
            ClearOnUpdate = 1 << 0,
            /// <summary>
            /// Stat will have all special options and behaviors
            /// </summary>
            All = (1 << 1) - 1
        }

        /// <summary>
        /// Registers an OpenXR statistic with the given name and flags.
        /// This method is not thread safe, so it should only be called at OnInstanceCreate.
        /// </summary>
        /// <param name="statName">String identifier for the statistic.</param>
        /// <param name="statFlags">Properties to be applied to the statistic.</param>
        /// <returns>Stat Id</returns>
        protected internal static ulong RegisterStatsDescriptor(string statName, StatFlags statFlags)
        {
            return runtime_RegisterStatsDescriptor(statName, statFlags);
        }

        /// <summary>
        /// Assigns a float value to a registered statistic. Its thread safe.
        /// </summary>
        /// <param name="statId">Identifier of the previously registered statistic.</param>
        /// <param name="value">Float value to be assigned to the stat.</param>
        protected internal static void SetStatAsFloat(ulong statId, float value)
        {
            runtime_SetStatAsFloat(statId, value);
        }

        /// <summary>
        /// Assigns an unsigned integer value to a registered statistic. It is thread safe.
        /// </summary>
        /// <remarks>
        /// IMPORTANT: Due to limitations in native code, values over 16777216 (1&lt;&lt;24) might not be reflected accurately.
        /// </remarks>
        /// <param name="statId">Identifier of the previously registered statistic.</param>
        /// <param name="value">Unsigned integer value to be assigned to the stat.</param>
        protected internal static void SetStatAsUInt(ulong statId, uint value)
        {
            runtime_SetStatAsUInt(statId, value);
        }
    }
}
