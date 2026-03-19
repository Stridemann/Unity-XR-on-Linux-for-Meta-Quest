#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor.XR.Management;
using UnityEditor.Build;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR.OpenXR;
using UnityEngine.XR.OpenXR.Features;
#if XR_COMPOSITION_LAYERS
using UnityEngine.XR.OpenXR.Features.CompositionLayers;
#endif
#if UNITY_RENDER_PIPELINES_UNIVERSAL
using UnityEngine.Rendering.Universal;
#endif // UNITY_RENDER_PIPELINES_UNIVERSAL

namespace UnityEditor.XR.OpenXR
{
    /// <summary>
    /// OpenXR project validation details.
    /// </summary>
    public static class OpenXRProjectValidation
    {
        const string K_openXrLoaderLibName = "openxr_loader";
        const string K_unsupportedLoaderNameChar = ".";

        private static bool isValidationMessagePrinted = false;
        private static bool isValidPath = false;
        private static string invalidPath = "";

        static readonly OpenXRFeature.ValidationRule[] BuiltinValidationRules =
        {
            new()
            {
                message = "The OpenXR package has been updated and Unity must be restarted to complete the update.",
                checkPredicate = () => OpenXRSettings.Instance == null || !OpenXRSettings.Instance.versionChanged,
                fixIt = RequireRestart,
                error = true,
                errorEnteringPlaymode = true,
                buildTargetGroup = BuildTargetGroup.Standalone,
            },
            new()
            {
                message = "The OpenXR Package Settings asset has duplicate settings and must be regenerated.",
                checkPredicate = AssetHasNoDuplicates,
                fixIt = RegenerateXRPackageSettingsAsset,
                error = false,
                errorEnteringPlaymode = false
            },
            new()
            {
                message = "[Optional] OpenXR settings assets are stored inside a package and won't persist changes. Please move it under Assets/XR/Settings.",
                checkPredicate = () =>
                {
                    invalidPath = CheckSettingsPathUnderAssets(out isValidPath);

                    if (!isValidPath && !string.IsNullOrEmpty(invalidPath))
                    {
                        if (!isValidationMessagePrinted)
                        {
                            Debug.Log($"OpenXR Settings asset is stored at {invalidPath}. This is inside a package and settings inside will not persist. Please move it under Assets/XR/Settings.");
                            isValidationMessagePrinted = true;
                        }
                        return false;
                    }

                    return true;
                },
                fixIt = () =>
                {
                    HighlightFolderInProjectWindow(invalidPath);
                },
                fixItAutomatic = false,
                error = false,
                errorEnteringPlaymode = false
            },
            new()
            {
                message = "Gamma Color Space is not supported when using OpenGLES.",
                checkPredicate = () =>
                {
                    if (PlayerSettings.colorSpace == ColorSpace.Linear)
                        return true;

                    return !Enum.GetValues(typeof(BuildTarget))
                        .Cast<BuildTarget>()
                        .Where(t =>
                        {
                            var buildTargetGroup = BuildPipeline.GetBuildTargetGroup(t);
                            if (!BuildPipeline.IsBuildTargetSupported(buildTargetGroup, t))
                                return false;

                            var settings = XRGeneralSettingsPerBuildTarget.XRGeneralSettingsForBuildTarget(buildTargetGroup);
                            if (null == settings)
                                return false;

                            var manager = settings.Manager;
                            if (null == manager)
                                return false;

                            return manager.activeLoaders.OfType<OpenXRLoader>().Any();
                        })
#if UNITY_2023_1_OR_NEWER
                        .Any(buildTarget => PlayerSettings.GetGraphicsAPIs(buildTarget).Any(g => g == GraphicsDeviceType.OpenGLES3));
#else
                        // Keeping OpenGL ES 2 support for 2022 and older versions.
                        .Any(buildTarget => PlayerSettings.GetGraphicsAPIs(buildTarget).Any(g => g == GraphicsDeviceType.OpenGLES2 || g == GraphicsDeviceType.OpenGLES3));
#endif
                },
                fixIt = () => PlayerSettings.colorSpace = ColorSpace.Linear,
                fixItMessage = "Set PlayerSettings.colorSpace to ColorSpace.Linear",
                error = true,
                errorEnteringPlaymode = true,
                buildTargetGroup = BuildTargetGroup.Android,
            },
            new()
            {
                message = "At least one interaction profile must be added.  Please select which controllers you will be testing against in the Features menu.",
                checkPredicate = () =>
                {
                    var settings = OpenXRSettings.GetSettingsForBuildTargetGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
                    return settings == null || settings.GetFeatures<OpenXRInteractionFeature>().Any(f => f.enabled);
                },
                fixIt = OpenProjectSettings,
                fixItAutomatic = false,
                fixItMessage = "Open Project Settings to select one or more interaction profiles.",
                highlighterFocus = new OpenXRFeature.ValidationRule.HighlighterFocusData
                {
                    searchText = "Enabled Interaction Profiles",
                    windowTitle = "Project Settings",
                }
            },
            new()
            {
                message = "An active OpenXR feature is defining a custom loader with a customized name containing \"openxr_loader\". This may cause build problems where the custom loader is not included in the built project. Please remove the CustomLoaderName property from the conflicting feature's attribute, or change the library name.\nNon-compliant features:" + GetLoaderFeaturesNamesWithString(K_openXrLoaderLibName),
                checkPredicate = () =>
                {
                    var openXrSettings = OpenXRSettings.GetSettingsForBuildTargetGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
                    if (openXrSettings != null)
                    {
                        return !CheckLoadersWithString(openXrSettings.features, K_openXrLoaderLibName).Any();
                    }
                    return true;
                },
                error = true,
                helpText = "By default, a feature that has a custom loader can use a name containing \"openxr_loader\" to be considered a custom OpenXR loader library, or alternatively, define a custom name for the plugin."
            },
            new()
            {
                message = $"The customized loader name of an active OpenXR feature contains \"{K_unsupportedLoaderNameChar}\" characters in its name. " +
                $"Please remove any \"{K_unsupportedLoaderNameChar}\" characters from the specified name in the CustomLoaderName property.\n" +
                "Non-compliant features: " + GetLoaderFeaturesNamesWithString(K_unsupportedLoaderNameChar),
                checkPredicate = () =>
                {
                    var openXrSettings = OpenXRSettings.GetSettingsForBuildTargetGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
                    if (openXrSettings != null)
                    {
                        return !CheckLoadersWithString(openXrSettings.features, K_unsupportedLoaderNameChar).Any();
                    }
                    return true;
                },
                error = true
            },
            new()
            {
                message = "Only arm64 or x86_x64 is supported on Android with OpenXR.  Other architectures are not supported.",
                checkPredicate = () => (EditorUserBuildSettings.activeBuildTarget != BuildTarget.Android) || ((PlayerSettings.Android.targetArchitectures & (~(AndroidArchitecture.ARM64 | AndroidArchitecture.X86_64))) == 0) && (PlayerSettings.Android.targetArchitectures != AndroidArchitecture.None),
                fixIt = () =>
                {
                    PlayerSettings.SetScriptingBackend(NamedBuildTarget.Android, ScriptingImplementation.IL2CPP);
                    PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARM64;
                },
                fixItMessage = "Change android build to arm64 or x86_64 and enable il2cpp.",
                error = true,
                buildTargetGroup = BuildTargetGroup.Android,
            },
            new()
            {
                message = "The only standalone targets supported are Windows x64 and OSX with OpenXR.  Other architectures and operating systems are not supported at this time.",
                checkPredicate = () => true,
                fixIt = () => EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, BuildTarget.StandaloneWindows64),
                fixItMessage = "Switch active build target to StandaloneWindows64.",
                error = false,
                errorEnteringPlaymode = false,
                buildTargetGroup = BuildTargetGroup.Standalone,
            },
#if !UNITY_6000_3_OR_NEWER
            new()
            {
                message = "The project's minimum Android API level is lower than 24, which is the lowest level supported by the OpenXR plug-in.",
                helpText = "This API level is required by the OpenXR plug-in's loader library. If your project is using a custom loader library, ensure that the project's minimum API level is supported by your library, which may be lower than 24.",
                checkPredicate = () => PlayerSettings.Android.minSdkVersion >= AndroidSdkVersions.AndroidApiLevel24,
                fixIt = () => PlayerSettings.Android.minSdkVersion = AndroidSdkVersions.AndroidApiLevel24,
                fixItMessage = "Set Player Settings minimum Android API Level to 24.",
                buildTargetGroup = BuildTargetGroup.Android,
            },
#endif
#if ENABLE_INPUT_SYSTEM
            new()
            {
                message = "Lock Input to Game View in order for tracked pose driver to work in editor playmode.",
                checkPredicate = () =>
                {
                    var cls = typeof(UnityEngine.InputSystem.InputDevice).Assembly.GetType("UnityEngine.InputSystem.Editor.InputEditorUserSettings");
                    if (cls == null) return true;
                    var prop = cls.GetProperty("lockInputToGameView", BindingFlags.Static | BindingFlags.Public);
                    if (prop == null) return true;
                    return (bool)prop.GetValue(null);
                },
                fixItMessage =  "Enables the 'Lock Input to Game View' setting in Window -> Analysis -> Input Debugger -> Options",
                fixIt = () =>
                {
                    var cls = typeof(UnityEngine.InputSystem.InputDevice).Assembly.GetType("UnityEngine.InputSystem.Editor.InputEditorUserSettings");
                    if (cls == null) return;
                    var prop = cls.GetProperty("lockInputToGameView", BindingFlags.Static | BindingFlags.Public);
                    if (prop == null) return;
                    prop.SetValue(null, true);
                },
                errorEnteringPlaymode = true,
                buildTargetGroup = BuildTargetGroup.Standalone,
            },
#endif // ENABLE_INPUT_SYSTEM
#if UNITY_RENDER_PIPELINES_UNIVERSAL
            new() {
                message = "[Optional] Enabling URP upscaling decreases performance significantly because it is currently not supported by XR. Please disable it by setting Upscaling Filter to Automatic in the Universal Render Pipeline Asset.",
                checkPredicate = URPUpscalingValidationPredicate,
                fixIt = URPUpscalingFix,
                fixItMessage = "URP upscaling has been disabled by setting the Upscaling Filter to Automatic in the Universal Render Pipeline Asset.",
                error = false,
                errorEnteringPlaymode = false,
            },
#endif
            new()
            {
                message = "[Optional] Switch to use InputSystem.XR.PoseControl instead of OpenXR.Input.PoseControl, which will be deprecated in a future release.",
                checkPredicate = () =>
                {
#if !USE_INPUT_SYSTEM_POSE_CONTROL && INPUT_SYSTEM_POSE_VALID
                    return false;
#else
                    return true;
#endif
                },
                fixIt = EnableInputSystemPoseControlDefine,
                error = false,
                errorEnteringPlaymode = false,
            },
            new()
            {
                message = "[Optional] Switch to use StickControl thumbsticks instead of Vector2Control, but may break existing projects that have code dependencies to the Vector2Control type. StickControl allows more input options for thumbstick-based control, such as acting as both a combined 2D vector, two independent axes or a four-way Dpad with 4 independent buttons.",
                checkPredicate = () =>
                {
#if USE_STICK_CONTROL_THUMBSTICKS
                    return true;
#else
                    return false;
#endif
                },
                fixIt = EnableStickControlThumbsticksDefine,
                error = false,
                errorEnteringPlaymode = false,
            },
            new()
            {
                message = "[Optional] Soft shadows can negatively impact performance on HoloLens, disabling soft shadows is recommended",
                checkPredicate = SoftShadowValidationPredicate,
                fixItMessage =
@"When using the Built-In Render Pipeline, enable hard shadows only.

When using the Universal Render Pipeline, open the Render Pipeline Asset in Editor for modification.",
                fixIt = SoftShadowFixItButtonPress,
                error = false,
                buildTargetGroup = BuildTargetGroup.WSA
            },
#if XR_COMPOSITION_LAYERS
            new()
            {
                message = $"The <b>{OpenXRCompositionLayersFeature.FeatureName}</b> feature is required to use the Composition Layers package.",
                checkPredicate = () =>
                {
                    var settings = OpenXRSettings.GetSettingsForBuildTargetGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
                    return settings == null || settings.GetFeatures<OpenXRCompositionLayersFeature>().Any(f => f.enabled);
                },
                fixItMessage =
                    $"Go to <b>Project Settings</b> > <b>XR Plug-in Management</b> > <b>OpenXR</b> > <b>{EditorUserBuildSettings.selectedBuildTargetGroup}</b> tab. In the list of OpenXR Features, enable <b>{OpenXRCompositionLayersFeature.FeatureName}</b>.",
                fixIt = () =>
                {
                    var settings = OpenXRSettings.GetSettingsForBuildTargetGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
                    var feature = settings.GetFeature<OpenXRCompositionLayersFeature>();
                    feature.enabled = true;
                },
                error = true,
            },
#endif

#if UNITY_6000_1_OR_NEWER
            new()
            {
                message = MagicLeapDeprecationMessage,
                checkPredicate = () =>
                {
                    return !ExistsMagicLeapOpenXRFeaturesEnabledForBuildTarget(EditorUserBuildSettings.selectedBuildTargetGroup) || !IsMagicLeapAndroidArchitectureSupportEnabled();
                },
                fixItMessage = "This validation rule cannot be fixed and is intended to warn developers that from Unity 6.3, the Magic Leap (x86_64) target will be limited to existing projects only."
            }
#endif
        };

        static readonly List<OpenXRFeature.ValidationRule> CachedValidationList = new(BuiltinValidationRules.Length);

        internal static void EnableInputSystemPoseControlDefine()
        {
            AddDefineToBuildTarget("USE_INPUT_SYSTEM_POSE_CONTROL");
        }

        internal static void EnableStickControlThumbsticksDefine()
        {
            AddDefineToBuildTarget("USE_STICK_CONTROL_THUMBSTICKS");
        }

        static void AddDefineToBuildTarget(string defineName)
        {
            NamedBuildTarget[] targets = { NamedBuildTarget.Android, NamedBuildTarget.Standalone, NamedBuildTarget.WindowsStoreApps };
            for (var index = 0; index < targets.Length; index++)
            {
                var defines = PlayerSettings.GetScriptingDefineSymbols(targets[index]);
                defines += $";{defineName}";
                PlayerSettings.SetScriptingDefineSymbols(targets[index], defines);
            }
        }

        internal static bool AssetHasNoDuplicates()
        {
            var packageSettings = PackageSettingsLocator.GetPackageSettings();
            if (packageSettings == null)
            {
                return true;
            }

            string path = packageSettings.PackageSettingsAssetPath();
            var loadedAssets = AssetDatabase.LoadAllAssetsAtPath(path);
            if (loadedAssets == null)
            {
                return true;
            }

            // Check for duplicate "full" feature name (Feature class type name + Feature name (contains BuildTargetGroup))
            HashSet<string> fullFeatureNames = new HashSet<string>();
            foreach (var loadedAsset in loadedAssets)
            {
                OpenXRFeature individualFeature = loadedAsset as OpenXRFeature;
                if (individualFeature != null)
                {
                    Type type = individualFeature.GetType();
                    string featureName = individualFeature.name;
                    string fullFeatureName = type.FullName + "\\" + featureName;

                    if (fullFeatureNames.Contains(fullFeatureName))
                        return false;
                    fullFeatureNames.Add(fullFeatureName);
                }
            }

            return true;
        }

        internal static string CheckSettingsPathUnderAssets(out bool isValidPath)
        {
            string[] guids = AssetDatabase.FindAssets("t:OpenXRSettings");
            foreach (string guid in guids)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guid);
                var assets = AssetDatabase.LoadAllAssetsAtPath(assetPath);
                foreach (var asset in assets)
                {
                    if (asset is OpenXRSettings)
                    {
                        if (assetPath.StartsWith("Packages/"))
                        {
                            isValidPath = false;
                            return assetPath;
                        }
                    }
                }
            }
            isValidPath = true;
            return null;
        }

        internal static void RegenerateXRPackageSettingsAsset()
        {
            // Deleting the OpenXR PackageSettings asset also destroys the OpenXRPackageSettings object.
            // Need to get the static method to create the new asset and object before deleting the asset.
            var packageSettings = PackageSettingsLocator.GetPackageSettings();
            if (packageSettings == null)
            {
                return;
            }

            string path = packageSettings.PackageSettingsAssetPath();

            Action createAssetCallback = packageSettings.RefreshFeatureSets;
            AssetDatabase.DeleteAsset(path);

            EditorBuildSettings.RemoveConfigObject(Constants.k_SettingsKey);

            createAssetCallback();
        }

        static string GetLoaderFeaturesNamesWithString(string text)
        {
            var openXrSettings = OpenXRSettings.GetSettingsForBuildTargetGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
            if (openXrSettings == null)
                return string.Empty;

            var featureNames = CheckLoadersWithString(openXrSettings.features, text)
                .Select(feature => feature.name);
            return string.Join(",", featureNames);
        }

        static IEnumerable<OpenXRFeature> CheckLoadersWithString(IEnumerable<OpenXRFeature> features, string text) =>
            features.Where(feature => feature != null &&
            feature.enabled &&
            !string.IsNullOrWhiteSpace(feature.customRuntimeLoaderName) &&
            feature.customRuntimeLoaderName.Contains(text));

        internal static void HighlightFolderInProjectWindow(string assetPath)
        {
            if (string.IsNullOrEmpty(assetPath))
            {
                Debug.LogWarning("[OpenXR Validation] Cannot highlight folder: asset path is null or empty.");
                return;
            }

            string folderPath = System.IO.Path.GetDirectoryName(assetPath);
            if (string.IsNullOrEmpty(folderPath))
            {
                Debug.LogWarning($"[OpenXR Validation] Could not determine folder path of asset: {assetPath}");
                return;
            }

            string folderAssetPath = folderPath.Replace("\\", "/");
            UnityEngine.Object folder = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(folderAssetPath);
            if (folder != null)
            {
                Selection.activeObject = folder;
                EditorApplication.ExecuteMenuItem("Window/General/Project");
                EditorGUIUtility.PingObject(folder);
                CheckSettingsPathUnderAssets(out isValidPath);
            }
            else
            {
                Debug.LogWarning($"[OpenXR Validation] Failed to locate folder at path: {folderPath}");
            }
        }

        /// <summary>
        /// Open the OpenXR project settings
        /// </summary>
        internal static void OpenProjectSettings() => SettingsService.OpenProjectSettings("Project/XR Plug-in Management/OpenXR");

        internal static void GetAllValidationIssues(List<OpenXRFeature.ValidationRule> issues, BuildTargetGroup buildTargetGroup)
        {
            issues.Clear();
            issues.AddRange(BuiltinValidationRules.Where(s => s.buildTargetGroup == buildTargetGroup || s.buildTargetGroup == BuildTargetGroup.Unknown));
            OpenXRFeature.GetValidationList(issues, buildTargetGroup);
        }

        /// <summary>
        /// Gathers and evaluates validation issues and adds them to a list.
        /// </summary>
        /// <param name="issues">List of validation issues to populate. List is cleared before populating.</param>
        /// <param name="buildTarget">Build target group to check for validation issues</param>
        public static void GetCurrentValidationIssues(List<OpenXRFeature.ValidationRule> issues, BuildTargetGroup buildTargetGroup)
        {
            var settings = XRGeneralSettingsPerBuildTarget.XRGeneralSettingsForBuildTarget(buildTargetGroup);
            // Ignore validations if the OpenXRLoader is not active.
            if (settings == null || !settings.Manager.activeLoaders.Any(loader => loader is OpenXRLoader))
                return;

            GetAllValidationIssues(CachedValidationList, buildTargetGroup);

            issues.Clear();
            foreach (var validation in CachedValidationList)
            {
                if (!validation.checkPredicate?.Invoke() ?? false)
                {
                    issues.Add(validation);
                }
            }
        }

        /// <summary>
        /// Logs validation issues to console.
        /// </summary>
        /// <param name="targetGroup"></param>
        /// <returns>true if there were any errors that should stop the build</returns>
        internal static bool LogBuildValidationIssues(BuildTargetGroup targetGroup)
        {
            var failures = new List<OpenXRFeature.ValidationRule>();
            GetCurrentValidationIssues(failures, targetGroup);

            if (failures.Count <= 0) return false;

            bool anyErrors = false;
            foreach (var result in failures)
            {
                if (result.error)
                    Debug.LogError(result.message);
                else
                    Debug.LogWarning(result.message);
                anyErrors |= result.error;
            }

            if (anyErrors)
            {
                Debug.LogError("Double click to fix OpenXR Project Validation Issues.");
            }

            return anyErrors;
        }

        /// <summary>
        /// Logs playmode validation issues (anything rule that fails with errorEnteringPlaymode set to true).
        /// </summary>
        /// <returns>true if there were any errors that should prevent openxr starting in editor playmode</returns>
        internal static bool LogPlaymodeValidationIssues()
        {
            var failures = new List<OpenXRFeature.ValidationRule>();
            GetCurrentValidationIssues(failures, BuildTargetGroup.Standalone);

            if (failures.Count <= 0) return false;

            bool playmodeErrors = false;
            foreach (var result in failures)
            {
                if (result.errorEnteringPlaymode)
                    Debug.LogError(result.message);
                playmodeErrors |= result.errorEnteringPlaymode;
            }

            return playmodeErrors;
        }

        static void RequireRestart()
        {
            // There is no public way to change the input handling backend .. so resorting to non-public way for now.
            if (!EditorUtility.DisplayDialog("Unity editor restart required", "The Unity editor must be restarted for this change to take effect.  Cancel to revert changes.", "Apply", "Cancel"))
                return;

            typeof(EditorApplication).GetMethod("RestartEditorAndRecompileScripts", BindingFlags.NonPublic | BindingFlags.Static)?.Invoke(null, null);
        }

        static bool SoftShadowValidationPredicate()
        {
            RenderPipelineAsset currentRenderPipelineAsset = GraphicsSettings.currentRenderPipeline;
            // If current render pipeline is Built-In Render Pipeline
            if (currentRenderPipelineAsset == null)
                return QualitySettings.shadows != UnityEngine.ShadowQuality.All;
#if UNITY_RENDER_PIPELINES_UNIVERSAL
            UniversalRenderPipelineAsset urpAsset = currentRenderPipelineAsset as UniversalRenderPipelineAsset;
            if (urpAsset != null)
                return urpAsset.supportsSoftShadows == false;
#endif // UNITY_RENDER_PIPELINES_UNIVERSAL
            return true;
        }

        static bool URPUpscalingValidationPredicate()
        {
            RenderPipelineAsset currentRenderPipelineAsset = GraphicsSettings.currentRenderPipeline;
            if (currentRenderPipelineAsset == null)
            {
                return false;
            }
#if UNITY_RENDER_PIPELINES_UNIVERSAL
            UniversalRenderPipelineAsset urpAsset = currentRenderPipelineAsset as UniversalRenderPipelineAsset;
            if (urpAsset != null) {
                return urpAsset.upscalingFilter == UpscalingFilterSelection.Auto;
            }
#endif
            return false;
        }

        static void SoftShadowFixItButtonPress()
        {
            RenderPipelineAsset currentRenderPipelineAsset = GraphicsSettings.currentRenderPipeline;
            // If current render pipeline is Built-In Render Pipeline
            if (currentRenderPipelineAsset == null)
            {
                QualitySettings.shadows = UnityEngine.ShadowQuality.HardOnly;
                return;
            }
#if UNITY_RENDER_PIPELINES_UNIVERSAL
            UniversalRenderPipelineAsset urpAsset = currentRenderPipelineAsset as UniversalRenderPipelineAsset;
            if (urpAsset != null)
            {
#if UNITY_6000_3_OR_NEWER
                var urpAssetID = urpAsset.GetEntityId();
#else
                var urpAssetID = urpAsset.GetInstanceID();
#endif
                if (AssetDatabase.CanOpenAssetInEditor(urpAssetID))
                    AssetDatabase.OpenAsset(urpAssetID);
                else
                    Debug.LogWarning("Unable to open URP asset in Editor.");
                return;
            }
#endif // UNITY_RENDER_PIPELINES_UNIVERSAL
            Debug.LogWarning("Unable to disable soft shadows.");
        }

        static void URPUpscalingFix()
        {
            RenderPipelineAsset currentRenderPipelineAsset = GraphicsSettings.currentRenderPipeline;
            // If current render pipeline is Built-In Render Pipeline
            if (currentRenderPipelineAsset == null)
            {
                Debug.LogWarning("No Render Pipeline Asset was found. Please assign a Default Render Pipeline Asset in the Graphics settings or assign a Render Pipeline Asset in the active Quality level.");
                return;
            }
#if UNITY_RENDER_PIPELINES_UNIVERSAL
            UniversalRenderPipelineAsset urpAsset = currentRenderPipelineAsset as UniversalRenderPipelineAsset;
            if (urpAsset != null)
            {
#if UNITY_6000_3_OR_NEWER
                var urpAssetID = urpAsset.GetEntityId();
#else
                var urpAssetID = urpAsset.GetInstanceID();
#endif
                if (AssetDatabase.CanOpenAssetInEditor(urpAssetID))
                {
                    AssetDatabase.OpenAsset(urpAssetID);
                    urpAsset.upscalingFilter = UpscalingFilterSelection.Auto;
                }
                else
                    Debug.LogWarning("Unable to open URP asset in Editor.");
                return;
            }
#endif // UNITY_RENDER_PIPELINES_UNIVERSAL
            Debug.LogWarning("Unable to disable URP upscaling.");
        }

#if UNITY_6000_1_OR_NEWER
        const string MagicLeapDeprecationMessage = "From Unity 6.3, the Magic Leap (x86_64) target will be limited to existing projects only.";

        static bool ExistsMagicLeapOpenXRFeaturesEnabledForBuildTarget(BuildTargetGroup buildTargetGroup)
        {
            var magicLeapFeatureId = "magicleap";
            var openXrSettings = OpenXRSettings.GetSettingsForBuildTargetGroup(buildTargetGroup);
            
            // ADD THIS NULL CHECK
            if (openXrSettings == null || openXrSettings.features == null)
                return false;

            foreach (var feature in openXrSettings.features)
            {
                if (feature != null && feature.enabled && (feature.name.ToLower().Contains(magicLeapFeatureId) || feature.featureIdInternal.ToLower().Contains(magicLeapFeatureId)))
                {
                    return true;
                }
            }
            return false;
        }

        static bool IsMagicLeapAndroidArchitectureSupportEnabled()
        {
            return EditorUserBuildSettings.selectedBuildTargetGroup == BuildTargetGroup.Android
                && PlayerSettings.Android.targetArchitectures.HasFlag(AndroidArchitecture.X86_64);
        }
#endif

#if UNITY_6000_3_OR_NEWER
        [PostProcessBuild(1)]
        static void OnPostprocessBuildMagicLeapDeprecation(BuildTarget target, string pathToBuiltProject)
        {
            BuildTargetGroup buildTargetGroup = BuildTargetGroup.Unknown;
            switch (target)
            {
                case BuildTarget.StandaloneLinux64:
                case BuildTarget.StandaloneOSX:
                case BuildTarget.StandaloneWindows:
                case BuildTarget.StandaloneWindows64:
                    buildTargetGroup = BuildTargetGroup.Standalone;
                    break;

                case BuildTarget.Android:
                    buildTargetGroup = BuildTargetGroup.Android;
                    break;
            }

            if (buildTargetGroup != BuildTargetGroup.Standalone && buildTargetGroup != BuildTargetGroup.Android)
                return;

            if (!ExistsMagicLeapOpenXRFeaturesEnabledForBuildTarget(buildTargetGroup))
                return;

            Debug.LogWarning(MagicLeapDeprecationMessage);
        }
#endif
    }
}
#endif
