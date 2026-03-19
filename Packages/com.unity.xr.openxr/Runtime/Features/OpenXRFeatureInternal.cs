using System;
using System.Runtime.InteropServices;
using UnityEngine.XR.OpenXR.NativeTypes;

namespace UnityEngine.XR.OpenXR.Features
{
    public abstract partial class OpenXRFeature
    {
        const string Library = "UnityOpenXR";

        [DllImport(Library, EntryPoint = "Internal_PathToString")]
        [return: MarshalAs(UnmanagedType.U1)]
        static extern bool Internal_PathToStringPtr(ulong pathId, out IntPtr path);

        [DllImport(Library, EntryPoint = "Internal_StringToPath")]
        [return: MarshalAs(UnmanagedType.U1)]
        static extern bool Internal_StringToPath([MarshalAs(UnmanagedType.LPStr)] string str, out ulong pathId);

        [DllImport(Library, EntryPoint = "Internal_GetCurrentInteractionProfile")]
        [return: MarshalAs(UnmanagedType.U1)]
        static extern bool Internal_GetCurrentInteractionProfile(ulong pathId, out ulong interactionProfile);

        [DllImport(Library, EntryPoint = "NativeConfig_GetFormFactor")]
        static extern int Internal_GetFormFactor();

        [DllImport(Library, EntryPoint = "NativeConfig_GetViewConfigurationType")]
        static extern int Internal_GetViewConfigurationType();

        [DllImport(Library, EntryPoint = "NativeConfig_GetViewTypeFromRenderIndex")]
        static extern int Internal_GetViewTypeFromRenderIndex(int renderPassIndex);

        [DllImport(Library, EntryPoint = "OpenXRInputProvider_GetXRSession")]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool Internal_GetXRSession(out ulong xrSession);

        [DllImport(Library, EntryPoint = "session_GetSessionState")]
        static extern void Internal_GetSessionState(out int oldState, out int newState);

        [DllImport(Library, EntryPoint = "NativeConfig_GetEnvironmentBlendMode")]
        static extern XrEnvironmentBlendMode Internal_GetEnvironmentBlendMode();

        [DllImport(Library, EntryPoint = "NativeConfig_SetEnvironmentBlendMode")]
        static extern void Internal_SetEnvironmentBlendMode(XrEnvironmentBlendMode xrEnvironmentBlendMode);

        [DllImport(Library, EntryPoint = "OpenXRInputProvider_GetAppSpace")]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool Internal_GetAppSpace(out ulong appSpace);

        [DllImport(Library, EntryPoint = "NativeConfig_GetProcAddressPtr")]
        internal static extern IntPtr Internal_GetProcAddressPtr([MarshalAs(UnmanagedType.I1)] bool loaderDefault);

        [DllImport(Library, EntryPoint = "NativeConfig_SetProcAddressPtrAndLoadStage1")]
        internal static extern void Internal_SetProcAddressPtrAndLoadStage1(IntPtr func);

        [DllImport(Library)]
        internal static extern ulong runtime_RegisterStatsDescriptor(string statName, StatFlags statFlags);

        [DllImport(Library)]
        internal static extern void runtime_SetStatAsFloat(ulong statId, float value);

        [DllImport(Library)]
        internal static extern void runtime_SetStatAsUInt(ulong statId, uint value);
    }
}
