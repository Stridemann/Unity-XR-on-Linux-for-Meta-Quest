using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using Microsoft.Win32;

[assembly: InternalsVisibleTo("UnityEditor.XR.OpenXR.Tests")]
namespace UnityEditor.XR.OpenXR
{
    internal class OpenXRRuntimeSelector
    {
        internal class RuntimeDetector
        {
            internal const string k_RuntimeEnvKey = "XR_RUNTIME_JSON";

            public virtual string name { get; set; }
            public virtual string jsonPath { get; }
            public virtual string tooltip => jsonPath;

            public virtual bool detected
            {
                get
                {
                    return File.Exists(jsonPath);
                }
            }

            public virtual void PrepareRuntime()
            {
            }

            public virtual void Activate()
            {
                if (detected)
                {
                    Environment.SetEnvironmentVariable(k_RuntimeEnvKey, jsonPath);
                }
            }

            public virtual void Deactivate()
            {
                Environment.SetEnvironmentVariable(k_RuntimeEnvKey, "");
            }

            public RuntimeDetector(string _name)
            {
                name = _name;
            }
        };

        internal class SystemDefault : RuntimeDetector
        {
            public override string name
            {
                get
                {
                    string ret = "System Default";
                    if (string.IsNullOrEmpty(tooltip))
                    {
                        ret += " (None Set)";
                    }
                    return ret;
                }
                set
                {
                }
            }

            public override string jsonPath => "";

            public override string tooltip
            {
                get
                {
                    string str = (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Khronos\OpenXR\1", "ActiveRuntime", "");
#if UNITY_EDITOR_OSX
                    str = File.Exists("/usr/local/share/openxr/1/active_runtime.json") ? "/usr/local/share/openxr/1/active_runtime.json" : "";
#endif
                    return str;
                }
            }

            public override bool detected => true;

            public SystemDefault()
                : base(null)
            {
            }
        }

        internal class OtherRuntime : RuntimeDetector
        {
            public const string k_OtherRuntimeEnvKey = "OTHER_XR_RUNTIME_JSON";

            private string runtimeJsonPath = "";

            public override string jsonPath => runtimeJsonPath;

            public override void PrepareRuntime()
            {
                var selectedJson = EditorUtility.OpenFilePanel("Select OpenXR Runtime json", "", "json");
                SetOtherRuntimeJsonPath(selectedJson);
                base.PrepareRuntime();
            }

            public void SetOtherRuntimeJsonPath(string selectedJson)
            {
                if (!string.IsNullOrEmpty(selectedJson))
                {
                    runtimeJsonPath = selectedJson;
                    Environment.SetEnvironmentVariable(k_OtherRuntimeEnvKey, selectedJson);
                }
            }

            public override bool detected => true;

            public OtherRuntime()
                : base("Other")
            {
                string otherJsonPath = Environment.GetEnvironmentVariable(k_OtherRuntimeEnvKey);
                if (otherJsonPath != null)
                {
                    runtimeJsonPath = otherJsonPath;
                }
            }
        }

        internal class OculusDetector : RuntimeDetector
        {
            private const string installLocKey = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Oculus";
            private const string installLocValue = "InstallLocation";
            private const string jsonName = @"Support\oculus-runtime\oculus_openxr_64.json";

            public override string jsonPath
            {
                get
                {
                    var oculusPath = (string)Registry.GetValue(installLocKey, installLocValue, "");
                    if (string.IsNullOrEmpty(oculusPath)) return "";
                    return Path.Combine(oculusPath, jsonName);
                }
            }

            public OculusDetector()
                : base("Oculus")
            {
            }
        }

        internal class SteamVRDetector : RuntimeDetector
        {
            public override string jsonPath => @"C:\Program Files (x86)\Steam\steamapps\common\SteamVR\steamxr_win64.json";

            public SteamVRDetector()
                : base("SteamVR")
            {
            }
        }

        internal class WindowsMRDetector : RuntimeDetector
        {
            public override string jsonPath => @"C:\WINDOWS\system32\MixedRealityRuntime.json";

            public WindowsMRDetector()
                : base("Windows Mixed Reality")
            {
            }
        }

        internal class MetaXRSimDetector : RuntimeDetector
        {
            const string PackageName = "com.meta.xr.simulator";
            string runtimePath = string.Empty;
            SearchRequest searchRequest = null;

            public override string jsonPath => runtimePath;

            public MetaXRSimDetector()
                : base("Meta XR Simulator")
            {
                SubmitPackageRequest();
            }

            void SubmitPackageRequest()
            {
                searchRequest = Client.Search(PackageName);
                EditorApplication.update += OnEditorUpdate;
            }

            void OnEditorUpdate()
            {
                switch (searchRequest.Status)
                {
                    case StatusCode.Failure:
                    {
                        EditorApplication.update -= OnEditorUpdate;
                        return;
                    }

                    case StatusCode.InProgress:
                    {
                        return;
                    }
                }

                EditorApplication.update -= OnEditorUpdate;

                foreach (var packageInfo in searchRequest.Result)
                {
                    if (packageInfo.name == PackageName)
                    {
                        string packageAssetPath = Path.GetFullPath(packageInfo.assetPath);
                        runtimePath = GetRuntimeJsonPath(packageAssetPath);
                        break;
                    }
                }
            }

            private static string GetRuntimeJsonPath(string packageAssetPath)
            {
                var runtimePath = Path.Combine(packageAssetPath, Path.Combine("MetaXRSimulator", "meta_openxr_simulator_win64.json"));
#if UNITY_EDITOR_OSX
                runtimePath = Path.Combine(Path.GetFullPath(packageAssetPath), Path.Combine("MetaXRSimulator", "meta_openxr_simulator_posix.json"));
#endif
                if (!File.Exists(runtimePath))
                {
                    runtimePath = Path.Combine(packageAssetPath, Path.Combine("MetaXRSimulator", "meta_openxr_simulator.json"));
                }
                return runtimePath;
            }
        }

        internal class DiscoveredDetector : RuntimeDetector
        {
            public DiscoveredDetector(string _name, string jsonPath)
                : base(_name)
            {
                m_jsonPath = jsonPath;
            }

            public override string jsonPath => m_jsonPath;

            private string m_jsonPath;
        }

        internal static List<RuntimeDetector> runtimeDetectors;
        internal static List<RuntimeDetector> RuntimeDetectors
        {
            get
            {
                if (runtimeDetectors == null)
                {
                    runtimeDetectors = GenerateRuntimeDetectorList();
                }
                return runtimeDetectors;
            }
        }

        internal static void RefreshRuntimeDetectorList()
        {
            runtimeDetectors = GenerateRuntimeDetectorList();
        }

        const string k_availableRuntimesRegistryKey = @"SOFTWARE\Khronos\OpenXR\1\AvailableRuntimes";

        internal static List<RuntimeDetector> GenerateRuntimeDetectorList()
        {
            RegistryKey availableRuntimesKey = Registry.LocalMachine.OpenSubKey(k_availableRuntimesRegistryKey, false);
            Dictionary<string, int> runtimePathToValue = new Dictionary<string, int>();

            if (availableRuntimesKey != null)
            {
                foreach (string jsonPath in availableRuntimesKey.GetValueNames())
                {
                    var availableValue = (int)availableRuntimesKey.GetValue(jsonPath, "");
                    runtimePathToValue.Add(jsonPath, availableValue);
                }
            }

            return GenerateRuntimeDetectorList(runtimePathToValue);
        }

        private static void OverwriteRuntimeDetector(List<RuntimeDetector> runtimeList, string jsonPath, int registryValue)
        {
            int index = runtimeList.FindIndex(runtimeDetector => runtimeDetector.jsonPath.Equals(jsonPath));
            if (index < 0)
            {
                return;
            }

            if (registryValue == 0)
            {
                string name = GetName(jsonPath) ?? jsonPath;
                if (name != null)
                {
                    runtimeList[index] = new DiscoveredDetector(name, jsonPath);
                }
            }
            else
            {
                runtimeList.RemoveAt(index);
            }
        }

        internal static List<RuntimeDetector> GenerateRuntimeDetectorList(Dictionary<string, int> runtimePathToValue)
        {
            WindowsMRDetector windowsMRDetector = new WindowsMRDetector();
            SteamVRDetector steamVRDetector = new SteamVRDetector();
            OculusDetector oculusDetector = new OculusDetector();
            MetaXRSimDetector metaXRSimDetector = new MetaXRSimDetector();
            List<RuntimeDetector> runtimeList = new List<RuntimeDetector>(5)
            {
                new SystemDefault(),
                windowsMRDetector,
                steamVRDetector,
                oculusDetector,
                metaXRSimDetector
            };

            foreach (var pathValuePair in runtimePathToValue)
            {
                if (pathValuePair.Key.Equals(windowsMRDetector.jsonPath))
                {
                    OverwriteRuntimeDetector(runtimeList, windowsMRDetector.jsonPath, pathValuePair.Value);
                }
                else if (pathValuePair.Key.Equals(steamVRDetector.jsonPath))
                {
                    OverwriteRuntimeDetector(runtimeList, steamVRDetector.jsonPath, pathValuePair.Value);
                }
                else if (pathValuePair.Key.Equals(oculusDetector.jsonPath))
                {
                    OverwriteRuntimeDetector(runtimeList, oculusDetector.jsonPath, pathValuePair.Value);
                }
                else if (pathValuePair.Value == 0)
                {
                    string name = GetName(pathValuePair.Key) ?? pathValuePair.Key;
                    runtimeList.Add(new DiscoveredDetector(name, pathValuePair.Key));
                }
            }

            runtimeList.Add(new OtherRuntime());
            return runtimeList;
        }

        [System.Serializable]
        internal class RuntimeInformation
        {
            public string file_format_version;

            public RuntimeDetails runtime;

            [System.Serializable]
            internal class RuntimeDetails
            {
                public string library_path;

                public string name;
            }
        }

        internal static string GetName(string jsonPath)
        {
            if (!File.Exists(jsonPath))
            {
                return null;
            }

            string text = File.ReadAllText(jsonPath);
            RuntimeInformation runtimeInformation = JsonUtility.FromJson<RuntimeInformation>(text);
            if (runtimeInformation != null && runtimeInformation.runtime != null)
            {
                return runtimeInformation.runtime.name;
            }

            return null;
        }

        static class Content
        {
            public static readonly GUIContent k_ActiveRuntimeLabel = new GUIContent("Play Mode OpenXR Runtime", "Changing this value will only affect this instance of the editor.");
        }

        internal const string k_SelectedRuntimeEnvKey = "XR_SELECTED_RUNTIME_JSON";

        private static int GetActiveRuntimeIndex(List<RuntimeDetector> runtimes)
        {
            string envValue = Environment.GetEnvironmentVariable(k_SelectedRuntimeEnvKey);
            if (string.IsNullOrEmpty(envValue))
                return 0;

            for (int i = 0; i < runtimes.Count; i++)
            {
                if (String.Compare(runtimes[i].jsonPath, envValue, StringComparison.InvariantCulture) == 0)
                    return i;
            }
            return 0;
        }

        internal static void SetSelectedRuntime(string jsonPath)
        {
            Environment.SetEnvironmentVariable(k_SelectedRuntimeEnvKey, jsonPath);
        }

        public static void DrawSelector()
        {
            EditorGUIUtility.labelWidth = 200;
            GUILayout.BeginHorizontal();
            var runtimes = OpenXRRuntimeSelector.RuntimeDetectors.Where(runtime => runtime.detected).ToList();

            int selectedRuntimeIndex = GetActiveRuntimeIndex(runtimes);
            int index = EditorGUILayout.Popup(Content.k_ActiveRuntimeLabel, selectedRuntimeIndex, runtimes.Select(s => new GUIContent(s.name, s.tooltip)).ToArray());
            if (selectedRuntimeIndex != index)
            {
                selectedRuntimeIndex = index;
                runtimes[selectedRuntimeIndex].PrepareRuntime();
                SetSelectedRuntime(runtimes[selectedRuntimeIndex].jsonPath);
            }
            GUILayout.EndHorizontal();
            EditorGUIUtility.labelWidth = 0;
        }

        internal static void ActivateOrDeactivateRuntime(PlayModeStateChange state)
        {
            var runtimes = OpenXRRuntimeSelector.RuntimeDetectors.Where(runtime => runtime.detected).ToList();
            int runtimeIndex = GetActiveRuntimeIndex(runtimes);
            switch (state)
            {
                case PlayModeStateChange.ExitingEditMode:
                    if (runtimeIndex >= 0)
                    {
                        runtimes[runtimeIndex].Activate();
                    }
                    break;

                case PlayModeStateChange.EnteredEditMode:
                    if (runtimeIndex >= 0)
                    {
                        runtimes[runtimeIndex].Deactivate();
                    }
                    break;
            }
        }

        static OpenXRRuntimeSelector()
        {
            EditorApplication.playModeStateChanged -= ActivateOrDeactivateRuntime;
            EditorApplication.playModeStateChanged += ActivateOrDeactivateRuntime;
        }
    }
}
