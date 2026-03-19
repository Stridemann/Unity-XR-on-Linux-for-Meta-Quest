using System;
using System.Runtime.InteropServices;
using UnityEngine.Assertions;
using UnityEngine.XR.Management;
using UnityEngine.XR.OpenXR.Features.Mock;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.XR.OpenXR;
using UnityEditor.XR.OpenXR.Features;
#endif

namespace UnityEngine.XR.OpenXR.TestTooling
{
    /// <summary>
    /// Sets up and manages a mock XR environment used for testing,
    /// using the OpenXR Mock Runtime feature.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Use this class for testing low-level OpenXR features, such as OpenXR-based loaders,
    /// performing calls to the Mock Runtime, or testing lifecycle events.
    /// </para>
    /// <para>
    /// Note that only one MockOpenXREnvironment instance should be created and started at a time, otherwise settings and resources may conflict.
    /// </para>
    /// </remarks>
    public class MockOpenXREnvironment : IDisposable
    {
        bool m_MockRuntimeIsRunning = false;

#if UNITY_EDITOR && OPENXR_USE_KHRONOS_LOADER
        private const string k_RuntimeJsonEnvKey = "XR_RUNTIME_JSON";
        private string m_BackupXRRuntimeJsonPath = null;
#endif

        /// <summary>
        /// Resources and settings used by this environment to run the Mock Runtime.
        /// </summary>
        /// <remarks>
        /// Use this object to add or remove features and extensions to be used by the environment.
        /// </remarks>
        public readonly MockOpenXREnvironmentSettings Settings;

        static bool AnyLoaderIsRunning => OpenXRLoaderBase.Instance != null;

        MockOpenXREnvironment(MockOpenXREnvironmentSettings settings)
        {
            Settings = settings;
        }

        /// <summary>
        /// Builds a new MockOpenXREnvironment instance and sets it up for testing.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method stops any running loader instances and initializes the Mock Runtime, to make a clean and stable environment for running.
        /// </para>
        /// <para>
        /// The method runs automatically if the environment is built with a <see langword="using"/> statement or declaration.
        /// </para>
        /// <para>
        /// When calling this method while running the tests in the Editor, any open Project Settings window will be closed
        /// to avoid changes in the settings, which may result in the settings files receiving unitnended changes
        /// and tests failing.
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// Example of creating a new environment for testing with a <see langword="using"/> statement:
        /// </para>
        /// <code lang="cs">
        /// <![CDATA[
        /// using (var env = MockOpenXREnvironment.CreateEnvironment())
        /// {
        ///     env.Start();
        ///     // Run tests
        ///     env.Stop();
        ///     // The environment will be disposed automatically after the using block
        /// }
        /// ]]>
        /// </code>
        /// </example>
        /// <returns>A new <see cref="MockOpenXREnvironment"/> instance.</returns>
        public static MockOpenXREnvironment CreateEnvironment()
        {
            if (AnyLoaderIsRunning)
            {
                StopSubsystems();
                DeinitializeLoader();
            }

            // Prevents settings being edited while the tests are running in Editor.
            if (!Application.isBatchMode)
            {
                CloseProjectSettingsWindow();
            }

            var settings = new MockOpenXREnvironmentSettings();

            var newMockRuntimeEnvironment = new MockOpenXREnvironment(settings);
            MockRuntime.Instance.AddTestHookGetInstanceProcAddr(InstallMockRuntimeFunctionInterceptor);
            return newMockRuntimeEnvironment;
        }

        /// <summary>
        /// Tears down the environment and its resources.
        /// </summary>
        /// <remarks>
        /// This method stops the Mock Runtime and disposes of the environment settings.
        /// After calling this method, the environment object is in a disposed state and can't be used anymore.
        /// </remarks>
        public void Dispose()
        {
            if (m_MockRuntimeIsRunning)
            {
                Stop();
            }

            MockRuntime.Instance.ClearTestHookGetInstanceProcAddr();

            Settings.Dispose();

            NativeApi.Cleanup();
        }

        /// <summary>
        /// Starts the Mock Runtime and initializes the XR subsystems for testing.
        /// </summary>
        /// <remarks>
        /// The method does nothing if the environment has been disposed.
        ///
        /// If the Mock Runtime is already running, the method calls <see cref="Stop()"/>
        /// before restarting and reinitializing it.
        /// </remarks>
        public void Start()
        {
            if (m_MockRuntimeIsRunning)
            {
                Stop();
            }

            PrepareBeforeTest();
            InitializeLoader();
            StartSubsystems();
            m_MockRuntimeIsRunning = true;
        }

        /// <summary>
        /// Stops the Mock Runtime and deinitializes the XR subsystems.
        /// </summary>
        /// <remarks>
        /// The method does nothing if the environment has been disposed.
        /// </remarks>
        public void Stop()
        {
            if (m_MockRuntimeIsRunning)
            {
                StopSubsystems();
                DeinitializeLoader();
                AfterTest();
                m_MockRuntimeIsRunning = false;
            }
        }

        /// <summary>
        /// Adds an extension to the set of underlying mock runtime extensions that are provided by the runtime
        /// when the OpenXR xrEnumerateInstanceExtensionProperties() call is made.
        /// Throws an 'InvalidOperationException' if this is called while the MockOpenXREnvironment is running.
        /// NOTE: Any added mocked extension will *not* work when writing tests which rely on the generic Khronos
        /// loader for the mock runtime when executing on device.
        /// </summary>
        /// <param name="extensionName">The name of the extension to add.
        /// For example 'XR_FB_spatial_entity'.</param>
        /// <param name="extensionVersion">The version number of the extension to mock, usually defined in openxr.h
        /// For example, XR_FB_spatial_entity_SPEC_VERSION.</param>
        public void AddSupportedExtension(string extensionName, uint extensionVersion)
        {
            if (m_MockRuntimeIsRunning)
                throw new InvalidOperationException("Call AddSupportedExtension() prior to calling Start() on the MockOpenXREnvironment.");
            NativeApi.AddSupportedExtension(extensionName, extensionVersion);
        }

        /// <summary>
        /// Passes a mocked C# function that to the native Mock Runtime for handling the retrieval of system properties
        /// for an extension-specific/type-specific system properties structure.
        /// </summary>
        /// <param name="type">An OpenXR structure type ID which identifies the actual structure definition type.</param>
        /// <param name="function">
        /// A pointer to a C# mock function capable of modifying the typed system properties structure.
        /// Note: if IntPtr.Zero is passed, then that means to disregard usage of a mock function for the provided type.
        /// </param>
        public void SetSysPropertiesFunctionForXrStructureType(uint type, IntPtr function) =>
            NativeApi.SetSysPropertiesFunctionForXrStructureType(type, function);

        /// <summary>
        /// Binds a C# mock function to a function name that corresponds with an extension-specific and
        /// platform-specific function.
        /// </summary>
        /// <param name="methodName">The name of the OpenXR function to bind to the mock function.</param>
        /// <param name="function">The C# mock function to call for the function name.  Use null to remove it.</param>
        public void SetFunctionForInterceptor(string methodName, IntPtr function) =>
            NativeApi.SetFunctionForInterceptor(methodName, function);

        /// <summary>
        /// Places a mocked C# event structure converted to a marshaled native pointer into
        /// the mock runtime's event queue that will be polled through xrPollEvent.
        /// </summary>
        /// <param name="eventData">The marshaled event structure data.</param>
        public void EnqueueMockEventData(IntPtr eventData) =>
            NativeApi.EnqueueMockEventData(eventData);

        /// <summary>
        /// Processes events on the event queue by virtue of calling xrPollEvent until the event
        /// queue is empty
        /// </summary>
        public void ProcessEventQueue() =>
            NativeApi.ProcessEventQueue();

        static IntPtr InstallMockRuntimeFunctionInterceptor(IntPtr oldGetProcAddrFunc) =>
            NativeApi.InstallGetInstanceProcAddrInterceptor(oldGetProcAddrFunc);

        static class NativeApi
        {
            const string k_MockApiNativeLibName = "mock_api";
            const string k_OpenXrNativeLibName = "UnityOpenXR";

            /// <summary>
            /// Cleans up the underlying native current instance of the test fixture.
            /// </summary>
            [DllImport(k_MockApiNativeLibName, EntryPoint = "MockRuntimeTestApi_Cleanup")]
            internal static extern void Cleanup();

            /// <summary>
            /// Installs the native mock function interceptor for allowing C# mock functions to serve as
            /// real OpenXR functions that are extension-specific/platform-specific.
            /// </summary>
            /// <param name="oldFunc">The function to retain and call in the xrGetInstanceProcAddr call chain.</param>
            /// <returns></returns>
            [DllImport(k_MockApiNativeLibName, EntryPoint = "MockRuntimeTestApi_InstallGetInstanceProcAddrInterceptor")]
            internal static extern IntPtr InstallGetInstanceProcAddrInterceptor(IntPtr oldFunc);

            /// <summary>
            /// Adds an extension to the set of underlying mock runtime extensions that are provided by the runtime
            /// when the OpenXR xrEnumerateInstanceExtensionProperties() call is made.
            /// </summary>
            /// <param name="extensionName">The name of the extension to add.
            /// For example 'XR_FB_spatial_entity'.</param>
            /// <param name="extensionVersion">The version number of the extension to mock, usually defined in openxr.h
            /// For example, XR_FB_spatial_entity_SPEC_VERSION.</param>
            [DllImport(k_MockApiNativeLibName, EntryPoint = "MockRuntimeTestApi_AddSupportedExtension")]
            internal static extern void AddSupportedExtension(string extensionName, uint extensionVersion);

            /// <summary>
            /// Passes a mocked C# function that to the native Mock Runtime for handling the retrieval of system properties
            /// for an extension-specific/type-specific system properties structure.
            /// </summary>
            /// <param name="type">An OpenXR structure type ID which identifies the actual structure definition type.</param>
            /// <param name="function">
            /// A pointer to a C# mock function capable of modifying the typed system properties structure.
            /// Note: if IntPtr.Zero is passed, then that means to disregard usage of a mock function for the provided type.
            /// </param>
            [DllImport(k_MockApiNativeLibName, EntryPoint = "MockRuntimeTestApi_SetSysPropertiesFunctionForXrStructureType")]
            internal static extern void SetSysPropertiesFunctionForXrStructureType(uint type, IntPtr function);

            /// <summary>
            /// Binds a C# mock function to a function name that corresponds with an extension-specific and
            /// platform-specific function.
            /// </summary>
            /// <param name="methodName">The name of the OpenXR function to bind to the mock function.</param>
            /// <param name="function">The C# mock function to call for the function name.  Use null to remove it.</param>
            [DllImport(k_MockApiNativeLibName, EntryPoint = "MockRuntimeTestApi_SetFunctionForInterceptor")]
            internal static extern void SetFunctionForInterceptor(string methodName, IntPtr function);

            /// <summary>
            /// Places a mocked C# event structure converted to a marshaled native pointer into
            /// the mock runtime's event queue that will be polled through xrPollEvent.
            /// </summary>
            /// <param name="eventData">The marshaled event structure data.</param>
            [DllImport(k_MockApiNativeLibName, EntryPoint = "MockRuntimeTestApi_EnqueueMockEventData")]
            internal static extern void EnqueueMockEventData(IntPtr eventData);

            /// <summary>
            /// Processes events on the event queue by virtue of calling xrPollEvent until the event
            /// queue is empty
            /// </summary>
            [DllImport(k_OpenXrNativeLibName, EntryPoint = "messagepump_PumpMessageLoop")]
            internal static extern void ProcessEventQueue();
        }

        static bool SetMockRuntimeEnabled(bool enable)
        {
            var feature = MockRuntime.Instance;
            if (null == feature)
                return false;

            if (feature.enabled == enable)
                return true;

            feature.enabled = enable;
            if (enable)
            {
                feature.priority = 0;
                feature.required = false;
                feature.ignoreValidationErrors = true;

                MockRuntime.ResetDefaults();
            }

            return true;
        }

        void PrepareBeforeTest()
        {
            // Enable the mock runtime and reset it back to default state
            Assert.IsTrue(SetMockRuntimeEnabled(true));
#if UNITY_INCLUDE_TESTS
            OpenXRRuntime.ClearEvents();
#endif
            OpenXRRestarter.Instance.ResetCallbacks();

#if UNITY_EDITOR && OPENXR_USE_KHRONOS_LOADER
            var features = FeatureHelpersInternal.GetAllFeatureInfo(BuildTargetGroup.Standalone);
            foreach (var f in features.Features)
            {
                if (String.Compare(f.Feature.featureIdInternal, MockRuntime.featureId, true) == 0)
                {
                    m_BackupXRRuntimeJsonPath = Environment.GetEnvironmentVariable(k_RuntimeJsonEnvKey);
                    var path = System.IO.Path.GetFullPath(f.PluginPath + "/unity-mock-runtime.json");
                    Environment.SetEnvironmentVariable(k_RuntimeJsonEnvKey, path);
                }
            }
#endif
        }

        static void InitializeLoader()
        {
            if (TryGetXRManagerSettings(out var manager))
            {
                manager.InitializeLoaderSync();
            }
        }

        static void StartSubsystems()
        {
            if (TryGetXRManagerSettings(out var manager) && manager.activeLoader != null)
            {
                manager.StartSubsystems();
            }
        }

        static void StopSubsystems()
        {
            if (TryGetXRManagerSettings(out var manager) && manager.activeLoader != null)
            {
                manager.StopSubsystems();
            }
        }

        static void DeinitializeLoader()
        {
            if (TryGetXRManagerSettings(out var manager) && manager.activeLoader != null)
            {
                manager.DeinitializeLoader();
            }
        }

        static bool TryGetXRManagerSettings(out XRManagerSettings settings)
        {
            settings = XRGeneralSettings.Instance != null ? XRGeneralSettings.Instance.Manager : null;
            return settings != null;
        }

        void AfterTest()
        {
            OpenXRRestarter.Instance.ResetCallbacks();
#if UNITY_INCLUDE_TESTS
            MockRuntime.Instance.TestCallback = (methodName, param) => true;
            MockRuntime.KeepFunctionCallbacks = false;
#endif
            MockRuntime.ClearFunctionCallbacks();

#if UNITY_EDITOR && OPENXR_USE_KHRONOS_LOADER
            if (m_BackupXRRuntimeJsonPath != null)
            {
                Environment.SetEnvironmentVariable(k_RuntimeJsonEnvKey, m_BackupXRRuntimeJsonPath);
                m_BackupXRRuntimeJsonPath = null;
            }
#endif
        }

        static void CloseProjectSettingsWindow()
        {
#if UNITY_EDITOR
            var windowTypes = TypeCache.GetTypesDerivedFrom(typeof(EditorWindow));
            foreach (var wt in windowTypes)
            {
                if (wt.Name.Contains("ProjectSettingsWindow"))
                {
                    var projectSettingsWindow = EditorWindow.GetWindow(wt);
                    if (projectSettingsWindow != null)
                    {
                        Debug.Log($"{nameof(MockOpenXREnvironment)} is closing the Project Settings window to run tests.");
                        projectSettingsWindow.Close();
                    }
                }
            }
#endif
        }
    }
}
