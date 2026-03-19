using UnityEditor;
using UnityEngine;

namespace UnityEditor.XR.OpenXR
{
    class BuildProfileUtility : ScriptableSingleton<BuildProfileUtility>
    {
        const string oculusTouchFeatureId = "com.unity.openxr.feature.input.oculustouch";
        const string questProFeatureId = "com.unity.openxr.feature.input.metaquestpro";
        const string questPlusFeatureId = "com.unity.openxr.feature.input.metaquestplus";

        [SerializeField, HideInInspector]
        bool isMetaQuestInitialized = false;

        [InitializeOnLoadMethod]
        static void OnLoad()
        {
#if UNITY_6000_1_OR_NEWER && UNITY_META_QUEST
            instance.InitializeMetaQuestSettings();
#endif
        }

        void InitializeMetaQuestSettings()
        {
            if (!isMetaQuestInitialized)
            {
                var oculusTouchFeature = Features.FeatureHelpers.GetFeatureWithIdForBuildTarget(BuildTargetGroup.Android, oculusTouchFeatureId);

                if (oculusTouchFeature != null)
                    oculusTouchFeature.enabled = true;
                else
                    Debug.LogWarning("Oculus Touch interaction profile could not be enabled in OpenXR settings.");

                var questProFeature = Features.FeatureHelpers.GetFeatureWithIdForBuildTarget(BuildTargetGroup.Android, questProFeatureId);

                if (questProFeature != null)
                    questProFeature.enabled = true;
                else
                    Debug.LogWarning("Meta Quest Pro interaction profile could not be enabled in OpenXR settings.");

                var questPlusFeature = Features.FeatureHelpers.GetFeatureWithIdForBuildTarget(BuildTargetGroup.Android, questPlusFeatureId);

                if (questPlusFeature != null)
                    questPlusFeature.enabled = true;
                else
                    Debug.LogWarning("Meta Quest Plus interaction profile could not be enabled in OpenXR settings.");

                Features.OpenXRFeatureSetManager.SetFeaturesFromEnabledFeatureSets(BuildTargetGroup.Android);

                isMetaQuestInitialized = true;
            }
        }

    }
}
