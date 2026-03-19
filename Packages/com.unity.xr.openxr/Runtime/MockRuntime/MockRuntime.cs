//#define VERBOSE_LOGGING
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.XR.OpenXR.NativeTypes;
using UnityEngine.XR.OpenXR.Features.Extensions;
using UnityEngine.XR.OpenXR.Features.Extensions.PerformanceSettings;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.XR.OpenXR.Features;
using UnityEditor.Build;
#endif

[assembly: InternalsVisibleTo("Unity.XR.OpenXR.Tests")]
[assembly: InternalsVisibleTo("Unity.XR.OpenXR.Editor.Tests")]
[assembly: InternalsVisibleTo("Unity.XR.OpenXR.TestTooling")]
namespace UnityEngine.XR.OpenXR.Features.Mock
{
    /// <summary>
    /// OpenXR Mock Runtime
    /// </summary>
#if UNITY_EDITOR
    [OpenXRFeature(UiName = "Mock Runtime",
        BuildTargetGroups = new[] {BuildTargetGroup.Standalone, BuildTargetGroup.Android},
        Company = "Unity",
        Desc = "Mock runtime extension for automated testing.",
        DocumentationLink = Constants.k_DocumentationURL,
#if !OPENXR_USE_KHRONOS_LOADER
        CustomRuntimeLoaderBuildTargets = new[] { BuildTarget.StandaloneWindows64, BuildTarget.StandaloneOSX, BuildTarget.Android },
#endif
        OpenxrExtensionStrings = XR_UNITY_null_gfx + " " + XR_UNITY_android_present,
        Version = "0.0.2",
        FeatureId = featureId)]
#endif
    public class MockRuntime : OpenXRFeature
    {
        /// <summary>
        /// Script events that are possible to subscribe to.
        /// </summary>
        public enum ScriptEvent
        {
            /// <summary>
            /// Dummy script event.
            /// </summary>
            Unknown,

            /// <summary>
            /// EndFrame script event.
            /// </summary>
            EndFrame,

            /// <summary>
            /// HapticImpulse script event.
            /// </summary>
            HapticImpulse,

            /// <summary>
            /// HapticStop script event.
            /// </summary>
            HapticStop
        }

        /// <summary>
        /// Delegate invoked on ScriptEvents
        /// </summary>
        /// <param name="evt">ScriptEvent invoked.</param>
        /// <param name="param">Parameters of the script event.</param>
        public delegate void ScriptEventDelegate(ScriptEvent evt, ulong param);

        /// <summary>
        /// Delegate invoked before function calls.
        /// </summary>
        /// <param name="functionName">The OpenXR function name.</param>
        /// <returns>The XrResult of the callback.</returns>
        public delegate XrResult BeforeFunctionDelegate(string functionName);

        /// <summary>
        /// Delegate invoked after function calls.
        /// </summary>
        /// <param name="functionName">The OpenXR function name.</param>
        /// <param name="result">The XrResult of the function.</param>
        public delegate void AfterFunctionDelegate(string functionName, XrResult result);

        static Dictionary<string, AfterFunctionDelegate> s_AfterFunctionCallbacks;
        static Dictionary<string, BeforeFunctionDelegate> s_BeforeFunctionCallbacks;

        /// <summary>
        /// Subscribe delegates to ScriptEvents.
        /// </summary>
        public static event ScriptEventDelegate onScriptEvent;

        /// <summary>
        /// The feature id string. This is used to give the feature a well known id for reference.
        /// </summary>
        public const string featureId = "com.unity.openxr.feature.mockruntime";

        /// <summary>
        /// Don't fail to build if there are validation errors.
        /// </summary>
        public bool ignoreValidationErrors;

        /// <summary>
        /// Return the singleton instance of the Mock Runtime feature.
        /// </summary>
        public static MockRuntime Instance => OpenXRSettings.Instance.GetFeature<MockRuntime>();

        [AOT.MonoPInvokeCallback(typeof(ScriptEventDelegate))]
        static void ReceiveScriptEvent(ScriptEvent evt, ulong param) => onScriptEvent?.Invoke(evt, param);

        [AOT.MonoPInvokeCallback(typeof(BeforeFunctionDelegate))]
        static XrResult BeforeFunctionCallback(string function)
        {
            var callback = GetBeforeFunctionCallback(function);
            return callback?.Invoke(function) ?? XrResult.Success;
        }

        [AOT.MonoPInvokeCallback(typeof(AfterFunctionDelegate))]
        static void AfterFunctionCallback(string function, XrResult result)
        {
            var callback = GetAfterFunctionCallback(function);
            callback?.Invoke(function, result);
        }

        /// <summary>
        /// Set the callbacks to call before and after the given OpenXR function is called within the Mock Runtime
        ///
        /// Note that since some OpenXR functions are called from within the graphics thread that care should
        /// be taken to maintain thread safety from within the callbacks.
        ///
        /// Note that function callbacks can be set prior to the MockRuntime being initialized but will be
        /// reset when the mock runtime is shutdown.
        /// </summary>
        /// <param name="function">OpenXR function name</param>
        /// <param name="beforeCallback">Callback to call before the OpenXR function is called (null to clear)</param>
        /// <param name="afterCallback">Callback to call after the OpenXR function is called (null to clear)</param>
        public static void SetFunctionCallback(
            string function, BeforeFunctionDelegate beforeCallback, AfterFunctionDelegate afterCallback)
        {
            if (beforeCallback != null)
            {
                s_BeforeFunctionCallbacks ??= new Dictionary<string, BeforeFunctionDelegate>();
                s_BeforeFunctionCallbacks[function] = beforeCallback;
            }
            else if (s_BeforeFunctionCallbacks != null)
            {
                s_BeforeFunctionCallbacks.Remove(function);
                if (s_BeforeFunctionCallbacks.Count == 0)
                    s_BeforeFunctionCallbacks = null;
            }

            if (afterCallback != null)
            {
                s_AfterFunctionCallbacks ??= new Dictionary<string, AfterFunctionDelegate>();
                s_AfterFunctionCallbacks[function] = afterCallback;
            }
            else if (s_AfterFunctionCallbacks != null)
            {
                s_AfterFunctionCallbacks.Remove(function);
                if (s_AfterFunctionCallbacks.Count == 0)
                    s_AfterFunctionCallbacks = null;
            }

            MockRuntime_RegisterFunctionCallbacks(
                s_BeforeFunctionCallbacks != null ? BeforeFunctionCallback : null,
                s_AfterFunctionCallbacks != null ? AfterFunctionCallback : null);
        }

        /// <summary>
        /// Set a callback to call before the given OpenXR function is called within the Mock Runtime
        ///
        /// Note that since some OpenXR functions are called from within the graphics thread that care should
        /// be taken to maintain thread safety from within the callbacks.
        ///
        /// Note that function callbacks can be set prior to the MockRuntime being initialized but will be
        /// reset when the mock runtime is shutdown.
        /// </summary>
        /// <param name="function">OpenXR function name</param>
        /// <param name="beforeCallback">Callback to call before the OpenXR function is called (null to clear)</param>
        public static void SetFunctionCallback(string function, BeforeFunctionDelegate beforeCallback) =>
            SetFunctionCallback(function, beforeCallback, GetAfterFunctionCallback(function));

        /// <summary>
        /// Set a callback to call before the given OpenXR function is called within the Mock Runtime
        /// </summary>
        /// <param name="function">OpenXR function name</param>
        /// <param name="afterCallback">Callback to call after the OpenXR function is called (null to clear)</param>
        public static void SetFunctionCallback(string function, AfterFunctionDelegate afterCallback) =>
            SetFunctionCallback(function, GetBeforeFunctionCallback(function), afterCallback);

        /// <summary>
        /// Return the callback set to be called before the given OpenXR function is called
        /// </summary>
        /// <param name="function">OpenXR function name</param>
        /// <returns>Callback or null if no callback is set</returns>
        public static BeforeFunctionDelegate GetBeforeFunctionCallback(string function)
        {
            return s_BeforeFunctionCallbacks?.GetValueOrDefault(function);
        }

        /// <summary>
        /// Return the callback set to be called after the given OpenXR function is called
        /// </summary>
        /// <param name="function">OpenXR function name</param>
        /// <returns>Callback or null if no callback is set</returns>
        public static AfterFunctionDelegate GetAfterFunctionCallback(string function)
        {
            return s_AfterFunctionCallbacks?.GetValueOrDefault(function);
        }

        /// <summary>
        /// Remove all OpenXR function callbacks
        /// </summary>
        public static void ClearFunctionCallbacks()
        {
            s_BeforeFunctionCallbacks = null;
            s_AfterFunctionCallbacks = null;
            MockRuntime_RegisterFunctionCallbacks(null, null);
        }

        /// <summary>
        /// Reset the MockRuntime testing settings back to defaults
        /// </summary>
        public static void ResetDefaults()
        {
#if UNITY_INCLUDE_TESTS
            var instance = Instance;
            instance.TestCallback = (_, _) => true;
#endif

            onScriptEvent = null;

            ClearFunctionCallbacks();
        }

        /// <inheritdoc/>
        protected internal override void OnInstanceDestroy(ulong instance)
        {
#if UNITY_INCLUDE_TESTS
            TestCallback(MethodBase.GetCurrentMethod().Name, instance);
            XrInstance = 0ul;

            if (!KeepFunctionCallbacks)
            {
#endif
            // When the mock runtime instance shuts down we remove any callbacks that
            // were set up to ensure they do not linger around for the next usage of the mock runtime.
            ClearFunctionCallbacks();
#if UNITY_INCLUDE_TESTS
            }
#endif
        }


#if UNITY_INCLUDE_TESTS
        [NonSerialized] public Func<string, object, object> TestCallback = (_, _) => true;

        public const string XR_UNITY_mock_test = "XR_UNITY_mock_test";

        public const string XR_UNITY_null_gfx = "XR_UNITY_null_gfx";

        public const string XR_UNITY_android_present = "XR_UNITY_android_present";

        public ulong XrInstance { get; private set; } = 0ul;

        public ulong XrSession { get; private set; } = 0ul;

        static bool s_KeepFunctionCallbacks;

        internal static bool KeepFunctionCallbacks
        {
            get => s_KeepFunctionCallbacks;
            set
            {
                s_KeepFunctionCallbacks = value;
                SetKeepFunctionCallbacks(value);
            }
        }

        /// <summary>
        /// Return the current session state of the MockRuntime
        /// </summary>
        public static XrSessionState sessionState => Internal_GetSessionState();

        protected internal override IntPtr HookGetInstanceProcAddr(IntPtr func)
        {
#if VERBOSE_LOGGING
            Debug.Log("Hook Get Instance Proc Addr");
#endif
            var ret = TestCallback(MethodBase.GetCurrentMethod().Name, func);

            // Make sure existing behaviour for Mock Runtime hooks is preserved
            var nextFunc = !(ret is IntPtr) ?
                HookCreateInstance(func) :
                HookCreateInstance((IntPtr)ret);

            // Add mock function interceptors to the hook chain
            nextFunc = MockFunctionInterceptor?.Invoke(nextFunc) ?? nextFunc;

            return nextFunc;
        }

        protected internal override void OnSystemChange(ulong xrSystem)
        {
#if VERBOSE_LOGGING
            Debug.Log("On System Change");
#endif
            TestCallback(MethodBase.GetCurrentMethod().Name, xrSystem);
        }

        protected internal override bool OnInstanceCreate(ulong xrInstance)
        {
#if VERBOSE_LOGGING
            Debug.Log($"On Instance Create: {xrInstance}");
#endif
            var result = (bool)TestCallback(MethodBase.GetCurrentMethod().Name, xrInstance);
            if (result)
                XrInstance = xrInstance;

            Internal_RegisterScriptEventCallback(ReceiveScriptEvent);

            return result;
        }

        protected internal override void OnSessionCreate(ulong xrSession)
        {
#if VERBOSE_LOGGING
            Debug.Log($"On Session Create: {xrSession}");
#endif
            XrSession = xrSession;
            TestCallback(MethodBase.GetCurrentMethod().Name, xrSession);
        }

        protected internal override void OnSessionBegin(ulong xrSession)
        {
#if VERBOSE_LOGGING
            Debug.Log($"On Session Begin: {xrSession}");
#endif
            TestCallback(MethodBase.GetCurrentMethod().Name, xrSession);
        }

        protected internal override void OnAppSpaceChange(ulong xrSpace)
        {
#if VERBOSE_LOGGING
            Debug.Log($"On App Space Change: {xrSpace}");
#endif
            TestCallback(MethodBase.GetCurrentMethod().Name, xrSpace);
        }

        protected internal override void OnSessionEnd(ulong xrSession)
        {
#if VERBOSE_LOGGING
            Debug.Log($"On Session End: {xrSession}");
#endif
            TestCallback(MethodBase.GetCurrentMethod().Name, xrSession);
        }

        protected internal override void OnSessionDestroy(ulong session)
        {
#if VERBOSE_LOGGING
            Debug.Log($"On Session Destroy: {session}");
#endif
            TestCallback(MethodBase.GetCurrentMethod().Name, session);
            XrSession = 0ul;
        }

        protected internal override void OnSessionLossPending(ulong xrSession)
        {
#if VERBOSE_LOGGING
            Debug.Log($"On Session Loss Pending: {xrSession}");
#endif
            TestCallback(MethodBase.GetCurrentMethod().Name, xrSession);
        }

        protected internal override void OnInstanceLossPending(ulong xrInstance)
        {
#if VERBOSE_LOGGING
            Debug.Log($"On Instance Loss Pending: {xrInstance}");
#endif
            TestCallback(MethodBase.GetCurrentMethod().Name, xrInstance);
        }

        protected internal override void OnSubsystemCreate()
        {
#if VERBOSE_LOGGING
            Debug.Log("On Subsystem Create");
#endif
            TestCallback(MethodBase.GetCurrentMethod().Name, 0);
        }

        protected internal override void OnSubsystemStart()
        {
#if VERBOSE_LOGGING
            Debug.Log("On Subsystem Start");
#endif
            TestCallback(MethodBase.GetCurrentMethod().Name, 0);
        }

        protected internal override void OnSessionExiting(ulong xrSession)
        {
#if VERBOSE_LOGGING
            Debug.Log($"On Session Exiting: {xrSession}");
#endif
            TestCallback(MethodBase.GetCurrentMethod().Name, xrSession);
        }

        protected internal override void OnSubsystemDestroy()
        {
#if VERBOSE_LOGGING
            Debug.Log("On Subsystem Destroy");
#endif
            TestCallback(MethodBase.GetCurrentMethod().Name, 0);
        }

        protected internal override void OnSubsystemStop()
        {
#if VERBOSE_LOGGING
            Debug.Log("On Subsystem Stop");
#endif
            TestCallback(MethodBase.GetCurrentMethod().Name, 0);
        }

        protected internal override void OnFormFactorChange(int xrFormFactor)
        {
#if VERBOSE_LOGGING
            Debug.Log($"On Form Factor Change: {xrFormFactor}");
#endif
            TestCallback(MethodBase.GetCurrentMethod().Name, xrFormFactor);
        }

        protected internal override void OnEnvironmentBlendModeChange(XrEnvironmentBlendMode xrEnvironmentBlendMode)
        {
#if VERBOSE_LOGGING
            Debug.Log($"On Environment Blend Mode Change: {xrEnvironmentBlendMode}");
#endif
            TestCallback(MethodBase.GetCurrentMethod().Name, xrEnvironmentBlendMode);
        }

        protected internal override void OnViewConfigurationTypeChange(int xrViewConfigurationType)
        {
#if VERBOSE_LOGGING
            Debug.Log($"On View Configuration Type Change: {xrViewConfigurationType}");
#endif
            TestCallback(MethodBase.GetCurrentMethod().Name, xrViewConfigurationType);
        }

        internal class XrSessionStateChangedParams
        {
            public int OldState;
            public int NewState;
        }

        protected internal override void OnSessionStateChange(int oldState, int newState)
        {
#if VERBOSE_LOGGING
            Debug.Log($"On Session State Change: {oldState} -> {newState}");
#endif
            TestCallback(
                MethodBase.GetCurrentMethod().Name,
                new XrSessionStateChangedParams {OldState = oldState, NewState = newState});
        }

        public static bool TransitionToState(XrSessionState state, bool forceTransition)
        {
            var instance = Instance;
            return instance.XrSession != 0 && Internal_TransitionToState(state, forceTransition);
        }

        public static void ChooseEnvironmentBlendMode(XrEnvironmentBlendMode mode)
        {
            SetEnvironmentBlendMode(mode);
        }

        public static XrEnvironmentBlendMode GetXrEnvironmentBlendMode()
        {
            return GetEnvironmentBlendMode();
        }

#if UNITY_EDITOR
        protected internal override void GetValidationChecks(List<ValidationRule> results, BuildTargetGroup target)
        {
            foreach (var res in results)
            {
                var check = res.checkPredicate;
                res.checkPredicate = () =>
                {
                    if (enabled && ignoreValidationErrors)
                        return true;
                    return check();
                };
            }

            TestCallback(MethodBase.GetCurrentMethod().Name, results);
            base.GetValidationChecks(results, target);
        }

#endif
#endif

        const string extLib = "mock_api";

        /// <summary>
        /// Called to hook xrGetInstanceProcAddr.
        /// </summary>
        /// <param name="func">xrGetInstanceProcAddr native function pointer</param>
        /// <returns>Function pointer that Unity will use to look up XR native functions.</returns>
        [DllImport(extLib, EntryPoint = "MockRuntime_HookCreateInstance")]
        public static extern IntPtr HookCreateInstance(IntPtr func);

        /// <summary>
        /// Keep function callbacks when resetting MockRuntime.
        /// </summary>
        /// <param name="value">True to keep callbacks.</param>
        [DllImport(extLib, EntryPoint = "MockRuntime_SetKeepFunctionCallbacks")]
        public static extern void SetKeepFunctionCallbacks([MarshalAs(UnmanagedType.I1)] bool value);

        /// <summary>
        /// Set the runtime ViewPose.
        /// </summary>
        /// <param name="viewConfigurationType">The XrViewConfigurationType to use.</param>
        /// <param name="viewIndex">The indexed view being set.</param>
        /// <param name="position">Position of the view.</param>
        /// <param name="orientation">Orientation of the view.</param>
        /// <param name="fov">Field of View.</param>
        [DllImport(extLib, EntryPoint = "MockRuntime_SetView")]
        public static extern void SetViewPose(
            XrViewConfigurationType viewConfigurationType,
            int viewIndex,
            Vector3 position,
            Quaternion orientation,
            Vector4 fov);

        /// <summary>
        /// Set the runtime ViewState.
        /// </summary>
        /// <param name="viewConfigurationType">The XrViewConfigurationType to use.</param>
        /// <param name="viewStateFlags">The XrViewStateFlags to set.</param>
        [DllImport(extLib, EntryPoint = "MockRuntime_SetViewState")]
        public static extern void SetViewState(
            XrViewConfigurationType viewConfigurationType, XrViewStateFlags viewStateFlags);

        /// <summary>
        /// Set the reference space to use at Runtime.
        /// </summary>
        /// <param name="referenceSpace">The type of reference space being set.</param>
        /// <param name="position">Position of the space.</param>
        /// <param name="orientation">Orientation of the space.</param>
        /// <param name="locationFlags">XrSpaceLocationFlags for the space.</param>
        [DllImport(extLib, EntryPoint = "MockRuntime_SetReferenceSpace")]
        public static extern void SetSpace(
            XrReferenceSpaceType referenceSpace,
            Vector3 position,
            Quaternion orientation,
            XrSpaceLocationFlags locationFlags);

        /// <summary>
        /// Set the reference space to use for input actions.
        /// </summary>
        /// <param name="actionHandle">Handle to the input action.</param>
        /// <param name="position">Position of the space.</param>
        /// <param name="orientation">Orientation of the space.</param>
        /// <param name="locationFlags">XrSpaceLocationFlags for the space.</param>
        [DllImport(extLib, EntryPoint = "MockRuntime_SetActionSpace")]
        public static extern void SetSpace(
            ulong actionHandle, Vector3 position, Quaternion orientation, XrSpaceLocationFlags locationFlags);

        [DllImport(extLib, EntryPoint = "MockRuntime_RegisterScriptEventCallback")]
        static extern XrResult Internal_RegisterScriptEventCallback(ScriptEventDelegate callback);

        [DllImport(extLib, EntryPoint = "MockRuntime_TransitionToState")]
        [return: MarshalAs(UnmanagedType.U1)]
        static extern bool Internal_TransitionToState(
            XrSessionState state, [MarshalAs(UnmanagedType.I1)] bool forceTransition);

        [DllImport(extLib, EntryPoint = "MockRuntime_GetSessionState")]
        static extern XrSessionState Internal_GetSessionState();

        /// <summary>
        /// Request to exit the runtime session.
        /// </summary>
        [DllImport(extLib, EntryPoint = "MockRuntime_RequestExitSession")]
        public static extern void RequestExitSession();

        /// <summary>
        /// Force MockRuntime instance loss.
        /// </summary>
        [DllImport(extLib, EntryPoint = "MockRuntime_CauseInstanceLoss")]
        public static extern void CauseInstanceLoss();

        /// <summary>
        ///  Force user presence change.
        /// </summary>
        /// <param name="hasUserPresent">User present when true.</param>
        [DllImport(extLib, EntryPoint = "MockRuntime_CauseUserPresenceChange")]
        public static extern void CauseUserPresenceChange([MarshalAs(UnmanagedType.U1)] bool hasUserPresent);

        /// <summary>
        ///  Change the recommended image size.
        /// </summary>
        /// <param name="width">Recommended Image Width.</param>
        /// <param name="height">Recommended Image Height.</param>
        [DllImport(extLib, EntryPoint = "MockRuntime_ChangeRecommendedResolution")]
        public static extern void ChangeRecommendedImageRectExtents(uint width, uint height);

        /// <summary>
        /// Trigger a MockRuntime Recommended Resolution Changed Event.
        /// </summary>
        [DllImport(extLib, EntryPoint = "MockRuntime_CauseRecommendedResolutionChangedEvent")]
        public static extern void CauseRecommendedResolutionChangedEvent();

        [DllImport(extLib, EntryPoint = "MockRuntime_SetReferenceSpaceBounds")]
        internal static extern void SetReferenceSpaceBounds(XrReferenceSpaceType referenceSpace, Vector2 bounds);

        [DllImport(extLib, EntryPoint = "MockRuntime_GetEndFrameStats")]
        internal static extern void GetEndFrameStats(out int primaryLayerCount, out int secondaryLayerCount);

        [DllImport(extLib, EntryPoint = "MockRuntime_ActivateSecondaryView")]
        internal static extern void ActivateSecondaryView(
            XrViewConfigurationType viewConfigurationType, [MarshalAs(UnmanagedType.I1)] bool activate);

        [DllImport(extLib, EntryPoint = "MockRuntime_RegisterFunctionCallbacks")]
        static extern void MockRuntime_RegisterFunctionCallbacks(
            BeforeFunctionDelegate hookBefore, AfterFunctionDelegate hookAfter);

        [DllImport(extLib, EntryPoint = "MockRuntime_MetaPerformanceMetrics_SeedCounterOnce_Float")]
        internal static extern void MetaPerformanceMetrics_SeedCounterOnce_Float(
            string xrPathString, float value, uint unit);

        [DllImport(extLib, EntryPoint = "MockRuntime_PerformanceSettings_CauseNotification")]
        internal static extern void PerformanceSettings_CauseNotification(
            PerformanceDomain domain, PerformanceSubDomain subDomain, PerformanceNotificationLevel level);

        [DllImport(extLib, EntryPoint = "MockRuntime_PerformanceSettings_GetPerformanceLevelHint")]
        internal static extern PerformanceLevelHint PerformanceSettings_GetPerformanceLevelHint(PerformanceDomain domain);

#if UNITY_EDITOR
        static void UseGenericLoaderAndroid()
        {
            var defines = PlayerSettings.GetScriptingDefineSymbols(NamedBuildTarget.Android);
            defines += ";OPENXR_USE_KHRONOS_LOADER";
            PlayerSettings.SetScriptingDefineSymbols(NamedBuildTarget.Android, defines);

#if UNITY_2023_1_OR_NEWER
            // Use GameActivity if possible so we have test coverage there.
            // No JNI on main thread when GameActivity is selected.
            PlayerSettings.Android.applicationEntry = AndroidApplicationEntry.GameActivity;
            PlayerSettings.Android.targetSdkVersion = AndroidSdkVersions.AndroidApiLevel33;
#endif
        }

#endif

#if UNITY_ANDROID
        [DllImport(extLib, EntryPoint = "MockRuntime_IsAndroidThreadTypeRegistered")]
        [return: MarshalAs(UnmanagedType.U1)]
        private static extern bool Internal_IsAndroidThreadTypeRegistered(uint threadType);

        [DllImport(extLib, EntryPoint = "MockRuntime_GetRegisteredAndroidThreadsCount")]
        private static extern ulong Internal_GetRegisteredAndroidThreadsCount();
#endif

        internal static bool IsAndroidThreadTypeRegistered(uint threadType)
        {
#if UNITY_ANDROID
            return Internal_IsAndroidThreadTypeRegistered(threadType);
#else
            return false;
#endif
        }

        internal static ulong GetRegisteredAndroidThreadsCount()
        {
#if UNITY_ANDROID
            return Internal_GetRegisteredAndroidThreadsCount();
#else
            return 0;
#endif
        }

        internal Func<IntPtr, IntPtr> MockFunctionInterceptor;

        internal void AddTestHookGetInstanceProcAddr(Func<IntPtr, IntPtr> nativeFunctionHook)
        {
            MockFunctionInterceptor = nativeFunctionHook;
        }

        internal void ClearTestHookGetInstanceProcAddr()
        {
            MockFunctionInterceptor = null;
        }
    }
}
