using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("UnityEditor.XR.OpenXR.Tests")]
namespace UnityEngine.XR.OpenXR
{
    public partial class OpenXRSettings
    {
        /// <summary>
        /// Name of the custom loader library.
        /// </summary>
        [SerializeField]
        [HideInInspector]
        internal string customLoaderName = string.Empty;
    }
}
