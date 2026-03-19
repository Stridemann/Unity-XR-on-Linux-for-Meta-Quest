using System;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using XrFutureEXT = System.UInt64;
using XrInstance = System.UInt64;
using XrSession = System.UInt64;
using XrSpatialContextEXT = System.UInt64;
using XrSpatialEntityEXT = System.UInt64;
using XrSpatialEntityIdEXT = System.UInt64;
using XrSpatialSnapshotEXT = System.UInt64;
using XrSystemId = System.UInt64;

namespace UnityEngine.XR.OpenXR.NativeTypes
{
    public static partial class OpenXRNativeApi
    {
        /// <summary>
        /// Enumerates the spatial capabilities that are supported by the given OpenXR instance and system ID.
        /// Provided by `XR_EXT_spatial_entity`.
        /// </summary>
        /// <param name="instance">The OpenXR instance.</param>
        /// <param name="systemId">The ID of the system whose spatial capabilities will be enumerated.</param>
        /// <param name="capabilityCountInput">The capacity of <paramref name="capabilities"/>, or 0 to indicate a
        /// request to retrieve the required capacity.</param>
        /// <param name="capabilityCountOutput">The count of elements in <paramref name="capabilities"/>, or the
        /// required capacity if <paramref name="capabilityCountInput"/> is insufficient.</param>
        /// <param name="capabilities">Pointer to an array of capabilities. Can be null if
        /// <paramref name="capabilityCountInput"/> is `0`.</param>
        /// <returns>The result of the operation.\
        /// \
        /// Success codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.Success"/></description></item>
        /// </list>
        /// Failure codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.FunctionUnsupported"/></description></item>
        ///   <item><description><see cref="XrResult.ValidationFailure"/></description></item>
        ///   <item><description><see cref="XrResult.RuntimeFailure"/></description></item>
        ///   <item><description><see cref="XrResult.HandleInvalid"/></description></item>
        ///   <item><description><see cref="XrResult.InstanceLost"/></description></item>
        ///   <item><description><see cref="XrResult.SizeInsufficient"/></description></item>
        ///   <item><description><see cref="XrResult.SystemInvalid"/></description></item>
        /// </list>
        /// </returns>
        /// <remarks>
        /// > [!IMPORTANT]
        /// > Output parameters are only valid if the returned result `.IsSuccess()`.
        /// > Don't read the output if an error is returned.
        /// </remarks>
        [DllImport(
            InternalConstants.openXRLibrary, EntryPoint = "EXT_spatial_entity_xrEnumerateSpatialCapabilitiesEXT")]
        public static extern unsafe XrResult xrEnumerateSpatialCapabilitiesEXT(
            XrInstance instance,
            XrSystemId systemId,
            uint capabilityCountInput,
            out uint capabilityCountOutput,
            XrSpatialCapabilityEXT* capabilities);

        /// <summary>
        /// Enumerates the spatial capabilities that are supported by the given OpenXR instance and system ID.
        /// Provided by `XR_EXT_spatial_entity`.
        /// </summary>
        /// <param name="instance">The OpenXR instance.</param>
        /// <param name="systemId">The ID of the system whose spatial capabilities will be enumerated.</param>
        /// <param name="allocator">The allocation strategy to use for <paramref name="capabilities"/>.</param>
        /// <param name="capabilities">The array of capabilities.</param>
        /// <returns>The result of the operation.\
        /// \
        /// Success codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.Success"/></description></item>
        /// </list>
        /// Failure codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.FunctionUnsupported"/></description></item>
        ///   <item><description><see cref="XrResult.ValidationFailure"/></description></item>
        ///   <item><description><see cref="XrResult.RuntimeFailure"/></description></item>
        ///   <item><description><see cref="XrResult.HandleInvalid"/></description></item>
        ///   <item><description><see cref="XrResult.InstanceLost"/></description></item>
        ///   <item><description><see cref="XrResult.SizeInsufficient"/></description></item>
        ///   <item><description><see cref="XrResult.SystemInvalid"/></description></item>
        /// </list>
        /// </returns>
        /// <remarks>
        /// > [!IMPORTANT]
        /// > Output parameters are only valid if the returned result `.IsSuccess()`.
        /// > Don't read the output if an error is returned.
        ///
        /// You are responsible to `Dispose` the output native array if you pass `Allocator.Persistent` as the
        /// <paramref name="allocator"/> value.
        /// </remarks>
        /// <exception cref="OverflowException">Thrown if `capabilities.Length` would exceed
        /// <see cref="Int32.MaxValue"/>.</exception>
        public static unsafe XrResult xrEnumerateSpatialCapabilitiesEXT(
            XrInstance instance,
            XrSystemId systemId,
            Allocator allocator,
            out NativeArray<XrSpatialCapabilityEXT> capabilities)
        {
            var result = xrEnumerateSpatialCapabilitiesEXT(instance, systemId, 0, out var capabilityCountOutput, null);
            if (result.IsError())
            {
                capabilities = default;
                return result;
            }

            capabilities = new NativeArray<XrSpatialCapabilityEXT>(checked((int)capabilityCountOutput), allocator);
            return xrEnumerateSpatialCapabilitiesEXT(
                instance, systemId, capabilityCountOutput, out _, (XrSpatialCapabilityEXT*)capabilities.GetUnsafePtr());
        }

        /// <summary>
        /// Enumerates the spatial capabilities that are supported by the current system.
        /// Provided by `XR_EXT_spatial_entity`.
        /// </summary>
        /// <param name="capabilityCountInput">The capacity of <paramref name="capabilities"/>, or 0 to indicate a
        /// request to retrieve the required capacity.</param>
        /// <param name="capabilityCountOutput">The count of elements in <paramref name="capabilities"/>, or the
        /// required capacity if <paramref name="capabilityCountInput"/> is insufficient.</param>
        /// <param name="capabilities">Pointer to an array of capabilities. Can be null if
        /// <paramref name="capabilityCountInput"/> is `0`.</param>
        /// <returns>The result of the operation.\
        /// \
        /// `nativeStatusCode` success codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.Success"/></description></item>
        /// </list>
        /// `nativeStatusCode` failure codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.FunctionUnsupported"/></description></item>
        ///   <item><description><see cref="XrResult.ValidationFailure"/></description></item>
        ///   <item><description><see cref="XrResult.RuntimeFailure"/></description></item>
        ///   <item><description><see cref="XrResult.HandleInvalid"/></description></item>
        ///   <item><description><see cref="XrResult.InstanceLost"/></description></item>
        ///   <item><description><see cref="XrResult.SizeInsufficient"/></description></item>
        ///   <item><description><see cref="XrResult.SystemInvalid"/></description></item>
        /// </list>
        /// </returns>
        /// <remarks>
        /// > [!IMPORTANT]
        /// > Output parameters are only valid if the returned result `.IsSuccess()`.
        /// > Don't read the output if an error is returned.
        /// </remarks>
        [DllImport(
            InternalConstants.openXRLibrary,
            EntryPoint = "EXT_spatial_entity_xrEnumerateSpatialCapabilitiesEXT_usingContext")]
        public static extern unsafe OpenXRResultStatus xrEnumerateSpatialCapabilitiesEXT(
            uint capabilityCountInput,
            out uint capabilityCountOutput,
            XrSpatialCapabilityEXT* capabilities);

        /// <summary>
        /// Enumerates the spatial capabilities that are supported by the current system.
        /// Provided by `XR_EXT_spatial_entity`.
        /// </summary>
        /// <param name="allocator">The allocation strategy to use for <paramref name="capabilities"/>.</param>
        /// <param name="capabilities">The array of capabilities.</param>
        /// <returns>The result of the operation.\
        /// \
        /// `nativeStatusCode` success codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.Success"/></description></item>
        /// </list>
        /// `nativeStatusCode` failure codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.FunctionUnsupported"/></description></item>
        ///   <item><description><see cref="XrResult.ValidationFailure"/></description></item>
        ///   <item><description><see cref="XrResult.RuntimeFailure"/></description></item>
        ///   <item><description><see cref="XrResult.HandleInvalid"/></description></item>
        ///   <item><description><see cref="XrResult.InstanceLost"/></description></item>
        ///   <item><description><see cref="XrResult.SizeInsufficient"/></description></item>
        ///   <item><description><see cref="XrResult.SystemInvalid"/></description></item>
        /// </list>
        /// </returns>
        /// <remarks>
        /// > [!IMPORTANT]
        /// > Output parameters are only valid if the returned result `.IsSuccess()`.
        /// > Don't read the output if an error is returned.
        ///
        /// You are responsible to `Dispose` the output native array if you pass `Allocator.Persistent` as the
        /// <paramref name="allocator"/> value.
        /// </remarks>
        /// <exception cref="OverflowException">Thrown if `capabilities.Length` would exceed
        /// <see cref="Int32.MaxValue"/>.</exception>
        public static unsafe OpenXRResultStatus xrEnumerateSpatialCapabilitiesEXT(
            Allocator allocator, out NativeArray<XrSpatialCapabilityEXT> capabilities)
        {
            var result = xrEnumerateSpatialCapabilitiesEXT(0, out var capabilityCountOutput, null);
            if (result.IsError())
            {
                capabilities = default;
                return result;
            }

            capabilities = new NativeArray<XrSpatialCapabilityEXT>(checked((int)capabilityCountOutput), allocator);
            return xrEnumerateSpatialCapabilitiesEXT(
                capabilityCountOutput, out _, (XrSpatialCapabilityEXT*)capabilities.GetUnsafePtr());
        }

        /// <summary>
        /// Enumerates the component types that a given capability provides on its entities for the given system.
        /// Provided by `XR_EXT_spatial_entity`.
        /// </summary>
        /// <param name="instance">The OpenXR instance.</param>
        /// <param name="systemId">The system whose spatial capability components will be enumerated.</param>
        /// <param name="capability">The capability for which the components will be enumerated.</param>
        /// <param name="capabilityComponents">The component types that <paramref name="capability"/> supports.</param>
        /// <returns>The result of the operation.\
        /// \
        /// Success codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.Success"/></description></item>
        /// </list>
        /// Failure codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.FunctionUnsupported"/></description></item>
        ///   <item><description><see cref="XrResult.ValidationFailure"/></description></item>
        ///   <item><description><see cref="XrResult.RuntimeFailure"/></description></item>
        ///   <item><description><see cref="XrResult.HandleInvalid"/></description></item>
        ///   <item><description><see cref="XrResult.InstanceLost"/></description></item>
        ///   <item><description><see cref="XrResult.SizeInsufficient"/></description></item>
        ///   <item><description><see cref="XrResult.SystemInvalid"/></description></item>
        ///   <item><description><see cref="XrResult.SpatialCapabilityUnsupportedEXT"/></description></item>
        /// </list>
        /// </returns>
        [DllImport(
            InternalConstants.openXRLibrary,
            EntryPoint = "EXT_spatial_entity_xrEnumerateSpatialCapabilityComponentTypesEXT")]
        public static extern XrResult xrEnumerateSpatialCapabilityComponentTypesEXT(
            XrInstance instance,
            XrSystemId systemId,
            XrSpatialCapabilityEXT capability,
            ref XrSpatialCapabilityComponentTypesEXT capabilityComponents);

        /// <summary>
        /// Enumerates the component types that the given capability provides on its entities for the given system.
        /// Provided by `XR_EXT_spatial_entity`.
        /// </summary>
        /// <param name="instance">The OpenXR instance.</param>
        /// <param name="systemId">The system whose spatial capability components will be enumerated.</param>
        /// <param name="capability">The capability for which the components will be enumerated.</param>
        /// <param name="allocator">The allocation strategy to use for <paramref name="componentTypes"/></param>
        /// <param name="componentTypes">The component types that <paramref name="capability"/> supports.</param>
        /// <returns>The result of the operation.\
        /// \
        /// Success codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.Success"/></description></item>
        /// </list>
        /// Failure codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.FunctionUnsupported"/></description></item>
        ///   <item><description><see cref="XrResult.ValidationFailure"/></description></item>
        ///   <item><description><see cref="XrResult.RuntimeFailure"/></description></item>
        ///   <item><description><see cref="XrResult.HandleInvalid"/></description></item>
        ///   <item><description><see cref="XrResult.InstanceLost"/></description></item>
        ///   <item><description><see cref="XrResult.SizeInsufficient"/></description></item>
        ///   <item><description><see cref="XrResult.SystemInvalid"/></description></item>
        ///   <item><description><see cref="XrResult.SpatialCapabilityUnsupportedEXT"/></description></item>
        /// </list>
        /// </returns>
        /// <remarks>
        /// > [!IMPORTANT]
        /// > Output parameters are only valid if the returned result `.IsSuccess()`.
        /// > Don't read the output if an error is returned.
        ///
        /// You are responsible to `Dispose` the output native array if you pass `Allocator.Persistent` as the
        /// <paramref name="allocator"/> value.
        /// </remarks>
        /// <exception cref="OverflowException">Thrown if `componentTypes.Length` would exceed
        /// <see cref="Int32.MaxValue"/>.</exception>
        public static unsafe XrResult xrEnumerateSpatialCapabilityComponentTypesEXT(
            XrInstance instance,
            XrSystemId systemId,
            XrSpatialCapabilityEXT capability,
            Allocator allocator,
            out NativeArray<XrSpatialComponentTypeEXT> componentTypes)
        {
            var inputTypes = XrSpatialCapabilityComponentTypesEXT.defaultValue;
            var result = xrEnumerateSpatialCapabilityComponentTypesEXT(instance, systemId, capability, ref inputTypes);
            if (result.IsError())
            {
                componentTypes = default;
                return result;
            }

            inputTypes.componentTypeCapacityInput = inputTypes.componentTypeCountOutput;
            componentTypes = new NativeArray<XrSpatialComponentTypeEXT>(
                checked((int)inputTypes.componentTypeCountOutput), allocator);

            inputTypes.componentTypes = (XrSpatialComponentTypeEXT*)componentTypes.GetUnsafePtr();
            return xrEnumerateSpatialCapabilityComponentTypesEXT(instance, systemId, capability, ref inputTypes);
        }

        /// <summary>
        /// Enumerates the component types that the given capability provides on its entities for the current system.
        /// Provided by `XR_EXT_spatial_entity`.
        /// </summary>
        /// <param name="capability">The capability for which the components will be enumerated.</param>
        /// <param name="capabilityComponents">The component types that <paramref name="capability"/> supports.</param>
        /// <returns>The result of the operation.\
        /// \
        /// `nativeStatusCode` success codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.Success"/></description></item>
        /// </list>
        /// `nativeStatusCode` failure codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.FunctionUnsupported"/></description></item>
        ///   <item><description><see cref="XrResult.ValidationFailure"/></description></item>
        ///   <item><description><see cref="XrResult.RuntimeFailure"/></description></item>
        ///   <item><description><see cref="XrResult.HandleInvalid"/></description></item>
        ///   <item><description><see cref="XrResult.InstanceLost"/></description></item>
        ///   <item><description><see cref="XrResult.SizeInsufficient"/></description></item>
        ///   <item><description><see cref="XrResult.SystemInvalid"/></description></item>
        ///   <item><description><see cref="XrResult.SpatialCapabilityUnsupportedEXT"/></description></item>
        /// </list>
        /// </returns>
        [DllImport(
            InternalConstants.openXRLibrary,
            EntryPoint = "EXT_spatial_entity_xrEnumerateSpatialCapabilityComponentTypesEXT_usingContext")]
        public static extern OpenXRResultStatus xrEnumerateSpatialCapabilityComponentTypesEXT(
            XrSpatialCapabilityEXT capability, ref XrSpatialCapabilityComponentTypesEXT capabilityComponents);

        /// <summary>
        /// Enumerates the component types that the given capability provides on its entities for the current system.
        /// Provided by `XR_EXT_spatial_entity`.
        /// </summary>
        /// <param name="capability">The capability for which the components will be enumerated.</param>
        /// <param name="allocator">The allocation strategy to use for <paramref name="componentTypes"/></param>
        /// <param name="componentTypes">The component types that <paramref name="capability"/> supports.</param>
        /// <returns>The result of the operation.\
        /// \
        /// `nativeStatusCode` success codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.Success"/></description></item>
        /// </list>
        /// `nativeStatusCode` failure codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.FunctionUnsupported"/></description></item>
        ///   <item><description><see cref="XrResult.ValidationFailure"/></description></item>
        ///   <item><description><see cref="XrResult.RuntimeFailure"/></description></item>
        ///   <item><description><see cref="XrResult.HandleInvalid"/></description></item>
        ///   <item><description><see cref="XrResult.InstanceLost"/></description></item>
        ///   <item><description><see cref="XrResult.SizeInsufficient"/></description></item>
        ///   <item><description><see cref="XrResult.SystemInvalid"/></description></item>
        ///   <item><description><see cref="XrResult.SpatialCapabilityUnsupportedEXT"/></description></item>
        /// </list>
        /// </returns>
        /// <remarks>
        /// > [!IMPORTANT]
        /// > Output parameters are only valid if the returned result `.IsSuccess()`.
        /// > Don't read the output if an error is returned.
        ///
        /// You are responsible to `Dispose` the output native array if you pass `Allocator.Persistent` as the
        /// <paramref name="allocator"/> value.
        /// </remarks>
        /// <exception cref="OverflowException">Thrown if `componentTypes.Length` would exceed
        /// <see cref="Int32.MaxValue"/>.</exception>
        public static unsafe OpenXRResultStatus xrEnumerateSpatialCapabilityComponentTypesEXT(
            XrSpatialCapabilityEXT capability,
            Allocator allocator,
            out NativeArray<XrSpatialComponentTypeEXT> componentTypes)
        {
            var inputTypes = XrSpatialCapabilityComponentTypesEXT.defaultValue;
            var result = xrEnumerateSpatialCapabilityComponentTypesEXT(capability, ref inputTypes);
            if (result.IsError())
            {
                componentTypes = default;
                return result;
            }

            inputTypes.componentTypeCapacityInput = inputTypes.componentTypeCountOutput;
            componentTypes = new NativeArray<XrSpatialComponentTypeEXT>(
                checked((int)inputTypes.componentTypeCountOutput), allocator);

            inputTypes.componentTypes = (XrSpatialComponentTypeEXT*)componentTypes.GetUnsafePtr();
            return xrEnumerateSpatialCapabilityComponentTypesEXT(capability, ref inputTypes);
        }

        /// <summary>
        /// Enumerates the configurable features that the given capability supports for the given system.
        /// Provided by `XR_EXT_spatial_entity`.
        /// </summary>
        /// <param name="instance">The OpenXR instance.</param>
        /// <param name="systemId">The system for which the features will be enumerated.</param>
        /// <param name="capability">The capability for which the features will be enumerated.</param>
        /// <param name="capabilityFeatureCapacityInput">The capacity of <paramref name="capabilityFeatures"/>, or 0 to
        /// indicate a request to retrieve the required capacity.</param>
        /// <param name="capabilityFeatureCountOutput">The count of elements in <paramref name="capabilityFeatures"/>.</param>
        /// <param name="capabilityFeatures">Pointer to an array of features.</param>
        /// <returns>The result of the operation.\
        /// \
        /// Success codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.Success"/></description></item>
        /// </list>
        /// Failure codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.FunctionUnsupported"/></description></item>
        ///   <item><description><see cref="XrResult.ValidationFailure"/></description></item>
        ///   <item><description><see cref="XrResult.RuntimeFailure"/></description></item>
        ///   <item><description><see cref="XrResult.HandleInvalid"/></description></item>
        ///   <item><description><see cref="XrResult.InstanceLost"/></description></item>
        ///   <item><description><see cref="XrResult.SizeInsufficient"/></description></item>
        ///   <item><description><see cref="XrResult.SystemInvalid"/></description></item>
        ///   <item><description><see cref="XrResult.SpatialCapabilityUnsupportedEXT"/></description></item>
        /// </list>
        /// </returns>
        /// <remarks>
        /// > [!IMPORTANT]
        /// > Output parameters are only valid if the returned result `.IsSuccess()`.
        /// > Don't read the output if an error is returned.
        /// </remarks>
        [DllImport(
            InternalConstants.openXRLibrary,
            EntryPoint = "EXT_spatial_entity_xrEnumerateSpatialCapabilityFeaturesEXT")]
        public static extern unsafe XrResult xrEnumerateSpatialCapabilityFeaturesEXT(
            XrInstance instance,
            XrSystemId systemId,
            XrSpatialCapabilityEXT capability,
            uint capabilityFeatureCapacityInput,
            out uint capabilityFeatureCountOutput,
            XrSpatialCapabilityFeatureEXT* capabilityFeatures);

        /// <summary>
        /// Enumerates the configurable features that the given capability supports for the given system.
        /// Provided by `XR_EXT_spatial_entity`.
        /// </summary>
        /// <param name="instance">The OpenXR instance.</param>
        /// <param name="systemId">The system for which the features will be enumerated.</param>
        /// <param name="capability">The capability for which the features will be enumerated.</param>
        /// <param name="allocator">The allocation strategy to use for <paramref name="capabilityFeatures"/></param>
        /// <param name="capabilityFeatures">The output array of features.</param>
        /// <returns>The result of the operation.\
        /// \
        /// Success codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.Success"/></description></item>
        /// </list>
        /// Failure codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.FunctionUnsupported"/></description></item>
        ///   <item><description><see cref="XrResult.ValidationFailure"/></description></item>
        ///   <item><description><see cref="XrResult.RuntimeFailure"/></description></item>
        ///   <item><description><see cref="XrResult.HandleInvalid"/></description></item>
        ///   <item><description><see cref="XrResult.InstanceLost"/></description></item>
        ///   <item><description><see cref="XrResult.SizeInsufficient"/></description></item>
        ///   <item><description><see cref="XrResult.SystemInvalid"/></description></item>
        ///   <item><description><see cref="XrResult.SpatialCapabilityUnsupportedEXT"/></description></item>
        /// </list>
        /// </returns>
        /// <remarks>
        /// > [!IMPORTANT]
        /// > Output parameters are only valid if the returned result `.IsSuccess()`.
        /// > Don't read the output if an error is returned.
        ///
        /// You are responsible to `Dispose` the output native array if you pass `Allocator.Persistent` as the
        /// <paramref name="allocator"/> value.
        /// </remarks>
        /// <exception cref="OverflowException">Thrown if `capabilityFeatures.Length` would exceed
        /// <see cref="Int32.MaxValue"/>.</exception>
        public static unsafe XrResult xrEnumerateSpatialCapabilityFeaturesEXT(
            XrInstance instance,
            XrSystemId systemId,
            XrSpatialCapabilityEXT capability,
            Allocator allocator,
            out NativeArray<XrSpatialCapabilityFeatureEXT> capabilityFeatures)
        {
            var result = xrEnumerateSpatialCapabilityFeaturesEXT(
                instance, systemId, capability, 0, out var featureCount, null);

            if (result.IsError())
            {
                capabilityFeatures = default;
                return result;
            }

            capabilityFeatures = new NativeArray<XrSpatialCapabilityFeatureEXT>(checked((int)featureCount), allocator);
            return xrEnumerateSpatialCapabilityFeaturesEXT(
                instance,
                systemId,
                capability,
                featureCount,
                out _,
                (XrSpatialCapabilityFeatureEXT*)capabilityFeatures.GetUnsafePtr());
        }

        /// <summary>
        /// Enumerates the configurable features that the given capability supports for the current system.
        /// Provided by `XR_EXT_spatial_entity`.
        /// </summary>
        /// <param name="capability">The capability for which the features will be enumerated.</param>
        /// <param name="capabilityFeatureCapacityInput">The capacity of <paramref name="capabilityFeatures"/>, or 0 to
        /// indicate a request to retrieve the required capacity.</param>
        /// <param name="capabilityFeatureCountOutput">The count of elements in <paramref name="capabilityFeatures"/>.</param>
        /// <param name="capabilityFeatures">Pointer to an array of features.</param>
        /// <returns>The result of the operation.\
        /// \
        /// `nativeStatusCode` success codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.Success"/></description></item>
        /// </list>
        /// `nativeStatusCode` failure codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.FunctionUnsupported"/></description></item>
        ///   <item><description><see cref="XrResult.ValidationFailure"/></description></item>
        ///   <item><description><see cref="XrResult.RuntimeFailure"/></description></item>
        ///   <item><description><see cref="XrResult.HandleInvalid"/></description></item>
        ///   <item><description><see cref="XrResult.InstanceLost"/></description></item>
        ///   <item><description><see cref="XrResult.SizeInsufficient"/></description></item>
        ///   <item><description><see cref="XrResult.SystemInvalid"/></description></item>
        ///   <item><description><see cref="XrResult.SpatialCapabilityUnsupportedEXT"/></description></item>
        /// </list>
        /// </returns>
        /// <remarks>
        /// > [!IMPORTANT]
        /// > Output parameters are only valid if the returned result `.IsSuccess()`.
        /// > Don't read the output if an error is returned.
        /// </remarks>
        [DllImport(
            InternalConstants.openXRLibrary,
            EntryPoint = "EXT_spatial_entity_xrEnumerateSpatialCapabilityFeaturesEXT_usingContext")]
        public static extern unsafe OpenXRResultStatus xrEnumerateSpatialCapabilityFeaturesEXT(
            XrSpatialCapabilityEXT capability,
            uint capabilityFeatureCapacityInput,
            out uint capabilityFeatureCountOutput,
            XrSpatialCapabilityFeatureEXT* capabilityFeatures);

        /// <summary>
        /// Enumerates the configurable features that the given capability supports for the current system.
        /// Provided by `XR_EXT_spatial_entity`.
        /// </summary>
        /// <param name="capability">The capability for which the features will be enumerated.</param>
        /// <param name="allocator">The allocation strategy to use for <paramref name="capabilityFeatures"/></param>
        /// <param name="capabilityFeatures">The output array of features.</param>
        /// <returns>The result of the operation.\
        /// \
        /// `nativeStatusCode` success codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.Success"/></description></item>
        /// </list>
        /// `nativeStatusCode` failure codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.FunctionUnsupported"/></description></item>
        ///   <item><description><see cref="XrResult.ValidationFailure"/></description></item>
        ///   <item><description><see cref="XrResult.RuntimeFailure"/></description></item>
        ///   <item><description><see cref="XrResult.HandleInvalid"/></description></item>
        ///   <item><description><see cref="XrResult.InstanceLost"/></description></item>
        ///   <item><description><see cref="XrResult.SizeInsufficient"/></description></item>
        ///   <item><description><see cref="XrResult.SystemInvalid"/></description></item>
        ///   <item><description><see cref="XrResult.SpatialCapabilityUnsupportedEXT"/></description></item>
        /// </list>
        /// </returns>
        /// <remarks>
        /// > [!IMPORTANT]
        /// > Output parameters are only valid if the returned result `.IsSuccess()`.
        /// > Don't read the output if an error is returned.
        ///
        /// You are responsible to `Dispose` the output native array if you pass `Allocator.Persistent` as the
        /// <paramref name="allocator"/> value.
        /// </remarks>
        /// <exception cref="OverflowException">Thrown if `capabilityFeatures.Length` would exceed
        /// <see cref="Int32.MaxValue"/>.</exception>
        public static unsafe OpenXRResultStatus xrEnumerateSpatialCapabilityFeaturesEXT(
            XrSpatialCapabilityEXT capability,
            Allocator allocator,
            out NativeArray<XrSpatialCapabilityFeatureEXT> capabilityFeatures)
        {
            var result = xrEnumerateSpatialCapabilityFeaturesEXT(capability, 0, out var featureCount, null);
            if (result.IsError())
            {
                capabilityFeatures = default;
                return result;
            }

            capabilityFeatures = new NativeArray<XrSpatialCapabilityFeatureEXT>(checked((int)featureCount), allocator);
            return xrEnumerateSpatialCapabilityFeaturesEXT(
                capability, featureCount, out _, (XrSpatialCapabilityFeatureEXT*)capabilityFeatures.GetUnsafePtr());
        }

        /// <summary>
        /// Creates a spatial context scoped to the given session. Provided by `XR_EXT_spatial_entity`.
        /// </summary>
        /// <param name="session">The session in which the spatial context will be active.</param>
        /// <param name="createInfo">The information used to specify the spatial context parameters.</param>
        /// <param name="future">The output future.</param>
        /// <returns>The result of the operation.\
        /// \
        /// Success codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.Success"/></description></item>
        ///   <item><description><see cref="XrResult.LossPending"/></description></item>
        /// </list>
        /// Failure codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.FunctionUnsupported"/></description></item>
        ///   <item><description><see cref="XrResult.ValidationFailure"/></description></item>
        ///   <item><description><see cref="XrResult.RuntimeFailure"/></description></item>
        ///   <item><description><see cref="XrResult.HandleInvalid"/></description></item>
        ///   <item><description><see cref="XrResult.InstanceLost"/></description></item>
        ///   <item><description><see cref="XrResult.SessionLost"/></description></item>
        ///   <item><description><see cref="XrResult.OutOfMemory"/></description></item>
        ///   <item><description><see cref="XrResult.LimitReached"/></description></item>
        ///   <item><description><see cref="XrResult.SpatialComponentUnsupportedForCapabilityEXT"/></description></item>
        ///   <item><description><see cref="XrResult.SpatialCapabilityUnsupportedEXT"/></description></item>
        ///   <item><description><see cref="XrResult.SpatialCapabilityConfigurationInvalidEXT"/></description></item>
        ///   <item><description><see cref="XrResult.PermissionInsufficient"/></description></item>
        /// </list>
        /// </returns>
        /// <remarks>
        /// > [!IMPORTANT]
        /// > Output parameters are only valid if the returned result `.IsSuccess()`.
        /// > Don't read the output if an error is returned.
        /// </remarks>
        [DllImport(
            InternalConstants.openXRLibrary, EntryPoint = "EXT_spatial_entity_xrCreateSpatialContextAsyncEXT")]
        public static extern XrResult xrCreateSpatialContextAsyncEXT(
            XrSession session, in XrSpatialContextCreateInfoEXT createInfo, out XrFutureEXT future);

        /// <summary>
        /// Creates a spatial context scoped to the current session. Provided by `XR_EXT_spatial_entity`.
        /// </summary>
        /// <param name="createInfo">The information used to specify the spatial context parameters.</param>
        /// <param name="future">The output future.</param>
        /// <returns>The result of the operation.\
        /// \
        /// `nativeStatusCode` success codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.Success"/></description></item>
        ///   <item><description><see cref="XrResult.LossPending"/></description></item>
        /// </list>
        /// `nativeStatusCode` failure codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.FunctionUnsupported"/></description></item>
        ///   <item><description><see cref="XrResult.ValidationFailure"/></description></item>
        ///   <item><description><see cref="XrResult.RuntimeFailure"/></description></item>
        ///   <item><description><see cref="XrResult.HandleInvalid"/></description></item>
        ///   <item><description><see cref="XrResult.InstanceLost"/></description></item>
        ///   <item><description><see cref="XrResult.SessionLost"/></description></item>
        ///   <item><description><see cref="XrResult.OutOfMemory"/></description></item>
        ///   <item><description><see cref="XrResult.LimitReached"/></description></item>
        ///   <item><description><see cref="XrResult.SpatialComponentUnsupportedForCapabilityEXT"/></description></item>
        ///   <item><description><see cref="XrResult.SpatialCapabilityUnsupportedEXT"/></description></item>
        ///   <item><description><see cref="XrResult.SpatialCapabilityConfigurationInvalidEXT"/></description></item>
        ///   <item><description><see cref="XrResult.PermissionInsufficient"/></description></item>
        /// </list>
        /// </returns>
        /// <remarks>
        /// > [!IMPORTANT]
        /// > Output parameters are only valid if the returned result `.IsSuccess()`.
        /// > Don't read the output if an error is returned.
        /// </remarks>
        [DllImport(
            InternalConstants.openXRLibrary,
            EntryPoint = "EXT_spatial_entity_xrCreateSpatialContextAsyncEXT_usingContext")]
        public static extern OpenXRResultStatus xrCreateSpatialContextAsyncEXT(
            in XrSpatialContextCreateInfoEXT createInfo, out XrFutureEXT future);

        /// <summary>
        /// Completes the asynchronous operation started by `OpenXRNativeAPI.xrCreateSpatialContextAsyncEXT`.
        /// Provided by `XR_EXT_spatial_entity`.
        /// </summary>
        /// <param name="session">The `XrSession` previously passed to
        /// `OpenXRNativeAPI.xrCreateSpatialContextAsyncEXT`.</param>
        /// <param name="future">The future received from `OpenXRNativeAPI.xrCreateSpatialContextAsyncEXT`.</param>
        /// <param name="completion">The output completion struct.</param>
        /// <returns>The result of the operation.\
        /// \
        /// Success codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.Success"/></description></item>
        ///   <item><description><see cref="XrResult.LossPending"/></description></item>
        /// </list>
        /// Failure codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.FunctionUnsupported"/></description></item>
        ///   <item><description><see cref="XrResult.ValidationFailure"/></description></item>
        ///   <item><description><see cref="XrResult.RuntimeFailure"/></description></item>
        ///   <item><description><see cref="XrResult.HandleInvalid"/></description></item>
        ///   <item><description><see cref="XrResult.InstanceLost"/></description></item>
        ///   <item><description><see cref="XrResult.SessionLost"/></description></item>
        ///   <item><description><see cref="XrResult.OutOfMemory"/></description></item>
        ///   <item><description><see cref="XrResult.LimitReached"/></description></item>
        ///   <item><description><see cref="XrResult.FuturePendingEXT"/></description></item>
        ///   <item><description><see cref="XrResult.FutureInvalidEXT"/></description></item>
        /// </list>
        /// </returns>
        /// <remarks>
        /// > [!IMPORTANT]
        /// > `completion.spatialContext` is only valid if the returned result `.IsSuccess()`.
        /// > Don't read the output if an error is returned.
        /// </remarks>
        /// <seealso cref="M:UnityEngine.XR.OpenXR.NativeTypes.OpenXRNativeApi.xrCreateSpatialContextAsyncEXT(System.UInt64,UnityEngine.XR.OpenXR.NativeTypes.XrSpatialContextCreateInfoEXT@,System.UInt64@)"/>
        public static XrResult xrCreateSpatialContextCompleteEXT(
            XrSession session, XrFutureEXT future, out XrCreateSpatialContextCompletionEXT completion)
        {
            completion = XrCreateSpatialContextCompletionEXT.defaultValue;
            return xrCreateSpatialContextCompleteEXT_native(session, future, ref completion);
        }

        [DllImport(
            InternalConstants.openXRLibrary, EntryPoint = "EXT_spatial_entity_xrCreateSpatialContextCompleteEXT")]
        static extern XrResult xrCreateSpatialContextCompleteEXT_native(
            XrSession session, XrFutureEXT future, ref XrCreateSpatialContextCompletionEXT completion);

        /// <summary>
        /// Completes the asynchronous operation started by `OpenXRNativeAPI.xrCreateSpatialContextAsyncEXT`.
        /// Provided by `XR_EXT_spatial_entity`.
        /// </summary>
        /// <param name="future">The future received from `OpenXRNativeAPI.xrCreateSpatialContextAsyncEXT`.</param>
        /// <param name="completion">The output completion struct.</param>
        /// <returns>The result of the operation.\
        /// \
        /// `nativeStatusCode` success codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.Success"/></description></item>
        ///   <item><description><see cref="XrResult.LossPending"/></description></item>
        /// </list>
        /// `nativeStatusCode` failure codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.FunctionUnsupported"/></description></item>
        ///   <item><description><see cref="XrResult.ValidationFailure"/></description></item>
        ///   <item><description><see cref="XrResult.RuntimeFailure"/></description></item>
        ///   <item><description><see cref="XrResult.HandleInvalid"/></description></item>
        ///   <item><description><see cref="XrResult.InstanceLost"/></description></item>
        ///   <item><description><see cref="XrResult.SessionLost"/></description></item>
        ///   <item><description><see cref="XrResult.OutOfMemory"/></description></item>
        ///   <item><description><see cref="XrResult.LimitReached"/></description></item>
        ///   <item><description><see cref="XrResult.FuturePendingEXT"/></description></item>
        ///   <item><description><see cref="XrResult.FutureInvalidEXT"/></description></item>
        /// </list>
        /// </returns>
        /// <remarks>
        /// > [!IMPORTANT]
        /// > `completion.spatialContext` is only valid if the returned result `.IsSuccess()`.
        /// > Don't read the output if an error is returned.
        /// </remarks>
        /// <seealso cref="M:UnityEngine.XR.OpenXR.NativeTypes.OpenXRNativeApi.xrCreateSpatialContextAsyncEXT(UnityEngine.XR.OpenXR.NativeTypes.XrSpatialContextCreateInfoEXT@,System.UInt64@)"/>
        public static OpenXRResultStatus xrCreateSpatialContextCompleteEXT(
            XrFutureEXT future, out XrCreateSpatialContextCompletionEXT completion)
        {
            completion = XrCreateSpatialContextCompletionEXT.defaultValue;
            return xrCreateSpatialContextCompleteEXT_usingContext(future, ref completion);
        }

        [DllImport(
            InternalConstants.openXRLibrary,
            EntryPoint = "EXT_spatial_entity_xrCreateSpatialContextCompleteEXT_usingContext")]
        static extern OpenXRResultStatus xrCreateSpatialContextCompleteEXT_usingContext(
            XrFutureEXT future, ref XrCreateSpatialContextCompletionEXT completion);

        /// <summary>
        /// Destroys the given spatial context. Provided by `XR_EXT_spatial_entity`.
        /// </summary>
        /// <param name="spatialContext">A spatial context previously created using
        /// `OpenXRNativeAPI.xrCreateSpatialContextAsyncEXT`.</param>
        /// <returns>The result of the operation.\
        /// \
        /// Success codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.Success"/></description></item>
        /// </list>
        /// Failure codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.FunctionUnsupported"/></description></item>
        ///   <item><description><see cref="XrResult.RuntimeFailure"/></description></item>
        ///   <item><description><see cref="XrResult.HandleInvalid"/></description></item>
        /// </list>
        /// </returns>
        /// <remarks>
        /// > [!WARNING]
        /// > You are responsible to synchronize this operation with any other threads you created that use this
        /// > spatial context.
        /// </remarks>
        [DllImport(
            InternalConstants.openXRLibrary, EntryPoint = "EXT_spatial_entity_xrDestroySpatialContextEXT")]
        public static extern XrResult xrDestroySpatialContextEXT(XrSpatialContextEXT spatialContext);

        /// <summary>
        /// Creates a spatial entity handle from the given spatial entity ID. Provided by `XR_EXT_spatial_entity`.
        /// </summary>
        /// <param name="spatialContext">A valid spatial context.</param>
        /// <param name="createInfo">The creation info.</param>
        /// <param name="spatialEntity">The output spatial entity handle.</param>
        /// <returns>The result of the operation.\
        /// \
        /// Success codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.Success"/></description></item>
        ///   <item><description><see cref="XrResult.LossPending"/></description></item>
        /// </list>
        /// Failure codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.FunctionUnsupported"/></description></item>
        ///   <item><description><see cref="XrResult.ValidationFailure"/></description></item>
        ///   <item><description><see cref="XrResult.RuntimeFailure"/></description></item>
        ///   <item><description><see cref="XrResult.HandleInvalid"/></description></item>
        ///   <item><description><see cref="XrResult.InstanceLost"/></description></item>
        ///   <item><description><see cref="XrResult.SessionLost"/></description></item>
        ///   <item><description><see cref="XrResult.OutOfMemory"/></description></item>
        ///   <item><description><see cref="XrResult.LimitReached"/></description></item>
        ///   <item><description><see cref="XrResult.SpatialEntityIdInvalidEXT"/></description></item>
        /// </list>
        /// </returns>
        /// <remarks>
        /// > [!IMPORTANT]
        /// > Output parameters are only valid if the returned result `.IsSuccess()`.
        /// > Don't read the output if an error is returned.
        /// </remarks>
        [DllImport(
            InternalConstants.openXRLibrary, EntryPoint = "EXT_spatial_entity_xrCreateSpatialEntityFromIdEXT")]
        public static extern XrResult xrCreateSpatialEntityFromIdEXT(
            XrSpatialContextEXT spatialContext,
            in XrSpatialEntityFromIdCreateInfoEXT createInfo,
            out XrSpatialEntityEXT spatialEntity);

        /// <summary>
        /// Destroys the given spatial entity handle. Provided by `XR_EXT_spatial_entity`.
        /// </summary>
        /// <param name="spatialEntity">The spatial entity handle.</param>
        /// <returns>The result of the operation.\
        /// \
        /// Success codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.Success"/></description></item>
        /// </list>
        /// Failure codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.FunctionUnsupported"/></description></item>
        ///   <item><description><see cref="XrResult.RuntimeFailure"/></description></item>
        ///   <item><description><see cref="XrResult.HandleInvalid"/></description></item>
        /// </list>
        /// </returns>
        /// <remarks>
        /// > [!WARNING]
        /// > You are responsible to synchronize this operation with any other threads you created that use this
        /// > spatial entity handle.
        /// </remarks>
        [DllImport(
            InternalConstants.openXRLibrary, EntryPoint = "EXT_spatial_entity_xrDestroySpatialEntityEXT")]
        public static extern XrResult xrDestroySpatialEntityEXT(XrSpatialEntityEXT spatialEntity);

        /// <summary>
        /// Creates a discovery snapshot for the given spatial context. Provided by `XR_EXT_spatial_entity`.
        /// </summary>
        /// <param name="spatialContext">A spatial context previously created by using an overload of
        /// `xrCreateSpatialContextAsyncEXT`.</param>
        /// <param name="createInfo">The creation info for the discovery snapshot.</param>
        /// <param name="future">The output future.</param>
        /// <returns>The result of the operation.\
        /// \
        /// Success codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.Success"/></description></item>
        ///   <item><description><see cref="XrResult.LossPending"/></description></item>
        /// </list>
        /// Failure codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.FunctionUnsupported"/></description></item>
        ///   <item><description><see cref="XrResult.ValidationFailure"/></description></item>
        ///   <item><description><see cref="XrResult.RuntimeFailure"/></description></item>
        ///   <item><description><see cref="XrResult.HandleInvalid"/></description></item>
        ///   <item><description><see cref="XrResult.InstanceLost"/></description></item>
        ///   <item><description><see cref="XrResult.SessionLost"/></description></item>
        ///   <item><description><see cref="XrResult.OutOfMemory"/></description></item>
        ///   <item><description><see cref="XrResult.LimitReached"/></description></item>
        ///   <item><description><see cref="XrResult.SpatialComponentNotEnabledEXT"/></description></item>
        /// </list>
        /// </returns>
        /// <remarks>
        /// > [!IMPORTANT]
        /// > Output parameters are only valid if the returned result `.IsSuccess()`.
        /// > Don't read the output if an error is returned.
        /// </remarks>
        [DllImport(
            InternalConstants.openXRLibrary,
            EntryPoint = "EXT_spatial_entity_xrCreateSpatialDiscoverySnapshotAsyncEXT")]
        public static extern XrResult xrCreateSpatialDiscoverySnapshotAsyncEXT(
            XrSpatialContextEXT spatialContext,
            in XrSpatialDiscoverySnapshotCreateInfoEXT createInfo,
            out XrFutureEXT future);

        /// <summary>
        /// Completes the asynchronous operation started by
        /// <see cref="xrCreateSpatialDiscoverySnapshotAsyncEXT">xrCreateSpatialDiscoverySnapshotAsyncEXT</see>.
        /// Provided by `XR_EXT_spatial_entity`.
        /// </summary>
        /// <param name="spatialContext">The spatial context previously passed to
        /// <see cref="xrCreateSpatialDiscoverySnapshotAsyncEXT">xrCreateSpatialDiscoverySnapshotAsyncEXT</see>.</param>
        /// <param name="createSnapshotCompletionInfo">The completion info.</param>
        /// <param name="completion">The output completion struct.</param>
        /// <returns>The result of the operation.\
        /// \
        /// Success codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.Success"/></description></item>
        ///   <item><description><see cref="XrResult.LossPending"/></description></item>
        /// </list>
        /// Failure codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.FunctionUnsupported"/></description></item>
        ///   <item><description><see cref="XrResult.ValidationFailure"/></description></item>
        ///   <item><description><see cref="XrResult.RuntimeFailure"/></description></item>
        ///   <item><description><see cref="XrResult.HandleInvalid"/></description></item>
        ///   <item><description><see cref="XrResult.InstanceLost"/></description></item>
        ///   <item><description><see cref="XrResult.SessionLost"/></description></item>
        ///   <item><description><see cref="XrResult.OutOfMemory"/></description></item>
        ///   <item><description><see cref="XrResult.LimitReached"/></description></item>
        ///   <item><description><see cref="XrResult.TimeInvalid"/></description></item>
        ///   <item><description><see cref="XrResult.FuturePendingEXT"/></description></item>
        ///   <item><description><see cref="XrResult.FutureInvalidEXT"/></description></item>
        /// </list>
        /// </returns>
        /// <remarks>
        /// > [!IMPORTANT]
        /// > Output parameters are only valid if the returned result `.IsSuccess()`.
        /// > Don't read the output if an error is returned.
        /// </remarks>
        public static XrResult xrCreateSpatialDiscoverySnapshotCompleteEXT(
            XrSpatialContextEXT spatialContext,
            in XrCreateSpatialDiscoverySnapshotCompletionInfoEXT createSnapshotCompletionInfo,
            out XrCreateSpatialDiscoverySnapshotCompletionEXT completion)
        {
            completion = XrCreateSpatialDiscoverySnapshotCompletionEXT.defaultValue;
            return xrCreateSpatialDiscoverySnapshotCompleteEXT_native(
                spatialContext, createSnapshotCompletionInfo, ref completion);
        }

        [DllImport(
            InternalConstants.openXRLibrary,
            EntryPoint = "EXT_spatial_entity_xrCreateSpatialDiscoverySnapshotCompleteEXT")]
        static extern XrResult xrCreateSpatialDiscoverySnapshotCompleteEXT_native(
            XrSpatialContextEXT spatialContext,
            in XrCreateSpatialDiscoverySnapshotCompletionInfoEXT createSnapshotCompletionInfo,
            ref XrCreateSpatialDiscoverySnapshotCompletionEXT completion);

        /// <summary>
        /// Completes the asynchronous operation started by
        /// <see cref="xrCreateSpatialDiscoverySnapshotAsyncEXT">xrCreateSpatialDiscoverySnapshotAsyncEXT</see>,
        /// using your app space and frame's predicted display time. Provided by `XR_EXT_spatial_entity`.
        /// </summary>
        /// <param name="spatialContext">The spatial context previously passed to
        /// <see cref="xrCreateSpatialDiscoverySnapshotAsyncEXT">xrCreateSpatialDiscoverySnapshotAsyncEXT</see>.</param>
        /// <param name="future">The future.</param>
        /// <param name="completion">The output completion struct.</param>
        /// <returns>The result of the operation.\
        /// \
        /// `nativeStatusCode` success codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.Success"/></description></item>
        ///   <item><description><see cref="XrResult.LossPending"/></description></item>
        /// </list>
        /// `nativeStatusCode` failure codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.FunctionUnsupported"/></description></item>
        ///   <item><description><see cref="XrResult.ValidationFailure"/></description></item>
        ///   <item><description><see cref="XrResult.RuntimeFailure"/></description></item>
        ///   <item><description><see cref="XrResult.HandleInvalid"/></description></item>
        ///   <item><description><see cref="XrResult.InstanceLost"/></description></item>
        ///   <item><description><see cref="XrResult.SessionLost"/></description></item>
        ///   <item><description><see cref="XrResult.OutOfMemory"/></description></item>
        ///   <item><description><see cref="XrResult.LimitReached"/></description></item>
        ///   <item><description><see cref="XrResult.TimeInvalid"/></description></item>
        ///   <item><description><see cref="XrResult.FuturePendingEXT"/></description></item>
        ///   <item><description><see cref="XrResult.FutureInvalidEXT"/></description></item>
        /// </list>
        /// </returns>
        /// <remarks>
        /// > [!IMPORTANT]
        /// > Output parameters are only valid if the returned result `.IsSuccess()`.
        /// > Don't read the output if an error is returned.
        /// </remarks>
        public static OpenXRResultStatus xrCreateSpatialDiscoverySnapshotCompleteEXT(
            XrSpatialContextEXT spatialContext,
            XrFutureEXT future,
            out XrCreateSpatialDiscoverySnapshotCompletionEXT completion)
        {
            completion = XrCreateSpatialDiscoverySnapshotCompletionEXT.defaultValue;
            return xrCreateSpatialDiscoverySnapshotCompleteEXT_usingContext(spatialContext, future, ref completion);
        }

        [DllImport(
            InternalConstants.openXRLibrary,
            EntryPoint = "EXT_spatial_entity_xrCreateSpatialDiscoverySnapshotCompleteEXT_usingContext")]
        static extern OpenXRResultStatus xrCreateSpatialDiscoverySnapshotCompleteEXT_usingContext(
            XrSpatialContextEXT spatialContext,
            XrFutureEXT future,
            ref XrCreateSpatialDiscoverySnapshotCompletionEXT completion);

        /// <summary>
        /// Queries the component data of the entities in a given snapshot based on the query condition.
        /// Provided by `XR_EXT_spatial_entity`.
        /// </summary>
        /// <param name="snapshot">A spatial snapshot previously created by
        /// `xrCreateSpatialDiscoverySnapshotCompleteEXT` or `xrCreateSpatialUpdateSnapshotEXT`.</param>
        /// <param name="queryCondition">The query condition.</param>
        /// <param name="queryResult">The query result.</param>
        /// <returns>The result of the operation.\
        /// \
        /// Success codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.Success"/></description></item>
        ///   <item><description><see cref="XrResult.LossPending"/></description></item>
        /// </list>
        /// Failure codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.FunctionUnsupported"/></description></item>
        ///   <item><description><see cref="XrResult.ValidationFailure"/></description></item>
        ///   <item><description><see cref="XrResult.RuntimeFailure"/></description></item>
        ///   <item><description><see cref="XrResult.HandleInvalid"/></description></item>
        ///   <item><description><see cref="XrResult.InstanceLost"/></description></item>
        ///   <item><description><see cref="XrResult.SessionLost"/></description></item>
        ///   <item><description><see cref="XrResult.OutOfMemory"/></description></item>
        ///   <item><description><see cref="XrResult.SizeInsufficient"/></description></item>
        /// </list>
        /// </returns>
        [DllImport(
            InternalConstants.openXRLibrary, EntryPoint = "EXT_spatial_entity_xrQuerySpatialComponentDataEXT")]
        public static extern XrResult xrQuerySpatialComponentDataEXT(
            XrSpatialSnapshotEXT snapshot,
            in XrSpatialComponentDataQueryConditionEXT queryCondition,
            ref XrSpatialComponentDataQueryResultEXT queryResult);

        /// <summary>
        /// Gets the contents of a buffer of type <see cref="XrSpatialBufferTypeEXT.String"/>.
        /// Provided by `XR_EXT_spatial_entity`.
        /// </summary>
        /// <param name="snapshot">The handle to a spatial snapshot.</param>
        /// <param name="info">The information regarding the buffer to query.</param>
        /// <param name="bufferCapacityInput">The capacity of <paramref name="buffer"/>, or 0 to indicate a request
        /// to retrieve the required capacity.</param>
        /// <param name="bufferCountOutput">The number of UTF-8 `char` elements in <paramref name="buffer"/>, or the
        /// required capacity if <paramref name="bufferCapacityInput"/> is insufficient.</param>
        /// <param name="buffer">Pointer to an array of `byte` elements. Can be `null` if
        /// <paramref name="bufferCapacityInput"/> is `0`.</param>
        /// <returns>The result of the operation.\
        /// \
        /// Success codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.Success"/></description></item>
        ///   <item><description><see cref="XrResult.LossPending"/></description></item>
        /// </list>
        /// Failure codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.FunctionUnsupported"/></description></item>
        ///   <item><description><see cref="XrResult.ValidationFailure"/></description></item>
        ///   <item><description><see cref="XrResult.RuntimeFailure"/></description></item>
        ///   <item><description><see cref="XrResult.HandleInvalid"/></description></item>
        ///   <item><description><see cref="XrResult.InstanceLost"/></description></item>
        ///   <item><description><see cref="XrResult.SessionLost"/></description></item>
        ///   <item><description><see cref="XrResult.OutOfMemory"/></description></item>
        ///   <item><description><see cref="XrResult.SizeInsufficient"/></description></item>
        ///   <item><description><see cref="XrResult.SpatialBufferIdInvalidEXT"/></description></item>
        /// </list>
        /// </returns>
        /// <remarks>
        /// > [!IMPORTANT]
        /// > Output parameters are only valid if the returned result `.IsSuccess()`.
        /// > Don't read the output if an error is returned.
        /// </remarks>
        [DllImport(
            InternalConstants.openXRLibrary, EntryPoint = "EXT_spatial_entity_xrGetSpatialBufferStringEXT")]
        public static extern unsafe XrResult xrGetSpatialBufferStringEXT(
            XrSpatialSnapshotEXT snapshot,
            in XrSpatialBufferGetInfoEXT info,
            uint bufferCapacityInput,
            out uint bufferCountOutput,
            byte* buffer);

        /// <summary>
        /// Gets the contents of a buffer of type <see cref="XrSpatialBufferTypeEXT.String"/>
        /// as a native array of bytes, allowing you to read the string and then dispose the array when you no longer
        /// need the string in memory. Provided by `XR_EXT_spatial_entity`.
        /// </summary>
        /// <param name="snapshot">The handle to a spatial snapshot.</param>
        /// <param name="info">The information regarding the buffer to query.</param>
        /// <param name="allocator">The allocation strategy to use for <paramref name="buffer"/></param>
        /// <param name="buffer">The output array.</param>
        /// <returns>The result of the operation.\
        /// \
        /// Success codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.Success"/></description></item>
        ///   <item><description><see cref="XrResult.LossPending"/></description></item>
        /// </list>
        /// Failure codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.FunctionUnsupported"/></description></item>
        ///   <item><description><see cref="XrResult.ValidationFailure"/></description></item>
        ///   <item><description><see cref="XrResult.RuntimeFailure"/></description></item>
        ///   <item><description><see cref="XrResult.HandleInvalid"/></description></item>
        ///   <item><description><see cref="XrResult.InstanceLost"/></description></item>
        ///   <item><description><see cref="XrResult.SessionLost"/></description></item>
        ///   <item><description><see cref="XrResult.OutOfMemory"/></description></item>
        ///   <item><description><see cref="XrResult.SizeInsufficient"/></description></item>
        ///   <item><description><see cref="XrResult.SpatialBufferIdInvalidEXT"/></description></item>
        /// </list>
        /// </returns>
        /// <remarks>
        /// > [!IMPORTANT]
        /// > Output parameters are only valid if the returned result `.IsSuccess()`.
        /// > Don't read the output if an error is returned.
        ///
        /// To read the output as a `string`, use the following example code:
        /// ```
        /// var result = xrGetSpatialBufferStringEXT(snapshot, info, allocator, out var buffer);
        /// if (result.IsError()) return;
        /// var myString = Encoding.UTF8.GetString(buffer.AsReadOnlySpan());
        /// ```
        ///
        /// You are responsible to `Dispose` the output native array if you pass `Allocator.Persistent` as the
        /// <paramref name="allocator"/> value.
        /// </remarks>
        /// <exception cref="OverflowException">Thrown if `buffer.Length` would exceed
        /// <see cref="Int32.MaxValue"/>.</exception>
        public static unsafe XrResult xrGetSpatialBufferStringEXT(
            XrSpatialSnapshotEXT snapshot,
            in XrSpatialBufferGetInfoEXT info,
            Allocator allocator,
            out NativeArray<byte> buffer)
        {
            var result = xrGetSpatialBufferStringEXT(snapshot, info, 0, out var bufferCountOutput, null);
            if (result.IsError())
            {
                buffer = default;
                return result;
            }

            buffer = new NativeArray<byte>(checked((int)bufferCountOutput), allocator);
            return xrGetSpatialBufferStringEXT(
                snapshot, info, bufferCountOutput, out _, (byte*)buffer.GetUnsafePtr());
        }

        /// <summary>
        /// Gets the contents of a buffer of type <see cref="XrSpatialBufferTypeEXT.Uint8"/>.
        /// Provided by `XR_EXT_spatial_entity`.
        /// </summary>
        /// <param name="snapshot">The handle to a spatial snapshot.</param>
        /// <param name="info">The information regarding the buffer to query.</param>
        /// <param name="bufferCapacityInput">The capacity of <paramref name="buffer"/>, or `0` to indicate a request
        /// to retrieve the required capacity.</param>
        /// <param name="bufferCountOutput">The count of elements in <paramref name="buffer"/>, or the required
        /// capacity if <paramref name="bufferCapacityInput"/> is insufficient.</param>
        /// <param name="buffer">Pointer to an array of `byte` elements. Can be `null` if
        /// <paramref name="bufferCapacityInput"/> is `0`.</param>
        /// <returns>The result of the operation.\
        /// \
        /// Success codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.Success"/></description></item>
        ///   <item><description><see cref="XrResult.LossPending"/></description></item>
        /// </list>
        /// Failure codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.FunctionUnsupported"/></description></item>
        ///   <item><description><see cref="XrResult.ValidationFailure"/></description></item>
        ///   <item><description><see cref="XrResult.RuntimeFailure"/></description></item>
        ///   <item><description><see cref="XrResult.HandleInvalid"/></description></item>
        ///   <item><description><see cref="XrResult.InstanceLost"/></description></item>
        ///   <item><description><see cref="XrResult.SessionLost"/></description></item>
        ///   <item><description><see cref="XrResult.OutOfMemory"/></description></item>
        ///   <item><description><see cref="XrResult.SizeInsufficient"/></description></item>
        ///   <item><description><see cref="XrResult.SpatialBufferIdInvalidEXT"/></description></item>
        /// </list>
        /// </returns>
        /// <remarks>
        /// > [!IMPORTANT]
        /// > Output parameters are only valid if the returned result `.IsSuccess()`.
        /// > Don't read the output if an error is returned.
        /// </remarks>
        [DllImport(InternalConstants.openXRLibrary, EntryPoint = "EXT_spatial_entity_xrGetSpatialBufferUint8EXT")]
        public static extern unsafe XrResult xrGetSpatialBufferUint8EXT(
            XrSpatialSnapshotEXT snapshot,
            in XrSpatialBufferGetInfoEXT info,
            uint bufferCapacityInput,
            out uint bufferCountOutput,
            byte* buffer);

        /// <summary>
        /// Gets the contents of a buffer of type <see cref="XrSpatialBufferTypeEXT.Uint8"/>.
        /// Provided by `XR_EXT_spatial_entity`.
        /// </summary>
        /// <param name="snapshot">The handle to a spatial snapshot.</param>
        /// <param name="info">The information regarding the buffer to query.</param>
        /// <param name="allocator">The allocation strategy to use for <paramref name="buffer"/></param>
        /// <param name="buffer">The output array.</param>
        /// <returns>The result of the operation.\
        /// \
        /// Success codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.Success"/></description></item>
        ///   <item><description><see cref="XrResult.LossPending"/></description></item>
        /// </list>
        /// Failure codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.FunctionUnsupported"/></description></item>
        ///   <item><description><see cref="XrResult.ValidationFailure"/></description></item>
        ///   <item><description><see cref="XrResult.RuntimeFailure"/></description></item>
        ///   <item><description><see cref="XrResult.HandleInvalid"/></description></item>
        ///   <item><description><see cref="XrResult.InstanceLost"/></description></item>
        ///   <item><description><see cref="XrResult.SessionLost"/></description></item>
        ///   <item><description><see cref="XrResult.OutOfMemory"/></description></item>
        ///   <item><description><see cref="XrResult.SizeInsufficient"/></description></item>
        ///   <item><description><see cref="XrResult.SpatialBufferIdInvalidEXT"/></description></item>
        /// </list>
        /// </returns>
        /// <remarks>
        /// > [!IMPORTANT]
        /// > Output parameters are only valid if the returned result `.IsSuccess()`.
        /// > Don't read the output if an error is returned.
        ///
        /// You are responsible to `Dispose` the output native array if you pass `Allocator.Persistent` as the
        /// <paramref name="allocator"/> value.
        /// </remarks>
        /// <exception cref="OverflowException">Thrown if `buffer.Length` would exceed
        /// <see cref="Int32.MaxValue"/>.</exception>
        public static unsafe XrResult xrGetSpatialBufferUint8EXT(
            XrSpatialSnapshotEXT snapshot,
            in XrSpatialBufferGetInfoEXT info,
            Allocator allocator,
            out NativeArray<byte> buffer)
        {
            var result = xrGetSpatialBufferUint8EXT(snapshot, info, 0, out var bufferCountOutput, null);
            if (result.IsError())
            {
                buffer = default;
                return result;
            }

            buffer = new NativeArray<byte>(checked((int)bufferCountOutput), allocator);
            return xrGetSpatialBufferUint8EXT(snapshot, info, bufferCountOutput, out _, (byte*)buffer.GetUnsafePtr());
        }

        /// <summary>
        /// Gets the contents of a buffer of type <see cref="XrSpatialBufferTypeEXT.Uint16"/>.
        /// Provided by `XR_EXT_spatial_entity`.
        /// </summary>
        /// <param name="snapshot">The handle to a spatial snapshot.</param>
        /// <param name="info">The information regarding the buffer to query.</param>
        /// <param name="bufferCapacityInput">The capacity of <paramref name="buffer"/>, or `0` to indicate a request
        /// to retrieve the required capacity.</param>
        /// <param name="bufferCountOutput">The count of elements in <paramref name="buffer"/>, or the required
        /// capacity if <paramref name="bufferCapacityInput"/> is insufficient.</param>
        /// <param name="buffer">Pointer to an array of `ushort` elements. Can be `null` if
        /// <paramref name="bufferCapacityInput"/> is `0`.</param>
        /// <returns>The result of the operation.\
        /// \
        /// Success codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.Success"/></description></item>
        ///   <item><description><see cref="XrResult.LossPending"/></description></item>
        /// </list>
        /// Failure codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.FunctionUnsupported"/></description></item>
        ///   <item><description><see cref="XrResult.ValidationFailure"/></description></item>
        ///   <item><description><see cref="XrResult.RuntimeFailure"/></description></item>
        ///   <item><description><see cref="XrResult.HandleInvalid"/></description></item>
        ///   <item><description><see cref="XrResult.InstanceLost"/></description></item>
        ///   <item><description><see cref="XrResult.SessionLost"/></description></item>
        ///   <item><description><see cref="XrResult.OutOfMemory"/></description></item>
        ///   <item><description><see cref="XrResult.SizeInsufficient"/></description></item>
        ///   <item><description><see cref="XrResult.SpatialBufferIdInvalidEXT"/></description></item>
        /// </list>
        /// </returns>
        /// <remarks>
        /// > [!IMPORTANT]
        /// > Output parameters are only valid if the returned result `.IsSuccess()`.
        /// > Don't read the output if an error is returned.
        /// </remarks>
        [DllImport(InternalConstants.openXRLibrary, EntryPoint = "EXT_spatial_entity_xrGetSpatialBufferUint16EXT")]
        public static extern unsafe XrResult xrGetSpatialBufferUint16EXT(
            XrSpatialSnapshotEXT snapshot,
            in XrSpatialBufferGetInfoEXT info,
            uint bufferCapacityInput,
            out uint bufferCountOutput,
            ushort* buffer);

        /// <summary>
        /// Gets the contents of a buffer of type <see cref="XrSpatialBufferTypeEXT.Uint16"/>.
        /// Provided by `XR_EXT_spatial_entity`.
        /// </summary>
        /// <param name="snapshot">The handle to a spatial snapshot.</param>
        /// <param name="info">The information regarding the buffer to query.</param>
        /// <param name="allocator">The allocation strategy to use for <paramref name="buffer"/></param>
        /// <param name="buffer">The output array.</param>
        /// <returns>The result of the operation.\
        /// \
        /// Success codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.Success"/></description></item>
        ///   <item><description><see cref="XrResult.LossPending"/></description></item>
        /// </list>
        /// Failure codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.FunctionUnsupported"/></description></item>
        ///   <item><description><see cref="XrResult.ValidationFailure"/></description></item>
        ///   <item><description><see cref="XrResult.RuntimeFailure"/></description></item>
        ///   <item><description><see cref="XrResult.HandleInvalid"/></description></item>
        ///   <item><description><see cref="XrResult.InstanceLost"/></description></item>
        ///   <item><description><see cref="XrResult.SessionLost"/></description></item>
        ///   <item><description><see cref="XrResult.OutOfMemory"/></description></item>
        ///   <item><description><see cref="XrResult.SizeInsufficient"/></description></item>
        ///   <item><description><see cref="XrResult.SpatialBufferIdInvalidEXT"/></description></item>
        /// </list>
        /// </returns>
        /// <remarks>
        /// > [!IMPORTANT]
        /// > Output parameters are only valid if the returned result `.IsSuccess()`.
        /// > Don't read the output if an error is returned.
        ///
        /// You are responsible to `Dispose` the output native array if you pass `Allocator.Persistent` as the
        /// <paramref name="allocator"/> value.
        /// </remarks>
        /// <exception cref="OverflowException">Thrown if `buffer.Length` would exceed
        /// <see cref="Int32.MaxValue"/>.</exception>
        public static unsafe XrResult xrGetSpatialBufferUint16EXT(
            XrSpatialSnapshotEXT snapshot,
            in XrSpatialBufferGetInfoEXT info,
            Allocator allocator,
            out NativeArray<ushort> buffer)
        {
            var result = xrGetSpatialBufferUint16EXT(snapshot, info, 0, out var bufferCountOutput, null);
            if (result.IsError())
            {
                buffer = default;
                return result;
            }

            buffer = new NativeArray<ushort>(checked((int)bufferCountOutput), allocator);
            return xrGetSpatialBufferUint16EXT(
                snapshot, info, bufferCountOutput, out _, (ushort*)buffer.GetUnsafePtr());
        }

        /// <summary>
        /// Gets the contents of a buffer of type <see cref="XrSpatialBufferTypeEXT.Uint32"/>.
        /// Provided by `XR_EXT_spatial_entity`.
        /// </summary>
        /// <param name="snapshot">The handle to a spatial snapshot.</param>
        /// <param name="info">The information regarding the buffer to query.</param>
        /// <param name="bufferCapacityInput">The capacity of <paramref name="buffer"/>, or `0` to indicate a request
        /// to retrieve the required capacity.</param>
        /// <param name="bufferCountOutput">The count of elements in <paramref name="buffer"/>, or the required
        /// capacity if <paramref name="bufferCapacityInput"/> is insufficient.</param>
        /// <param name="buffer">Pointer to an array of `uint` elements. Can be `null` if
        /// <paramref name="bufferCapacityInput"/> is `0`.</param>
        /// <returns>The result of the operation.\
        /// \
        /// Success codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.Success"/></description></item>
        ///   <item><description><see cref="XrResult.LossPending"/></description></item>
        /// </list>
        /// Failure codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.FunctionUnsupported"/></description></item>
        ///   <item><description><see cref="XrResult.ValidationFailure"/></description></item>
        ///   <item><description><see cref="XrResult.RuntimeFailure"/></description></item>
        ///   <item><description><see cref="XrResult.HandleInvalid"/></description></item>
        ///   <item><description><see cref="XrResult.InstanceLost"/></description></item>
        ///   <item><description><see cref="XrResult.SessionLost"/></description></item>
        ///   <item><description><see cref="XrResult.OutOfMemory"/></description></item>
        ///   <item><description><see cref="XrResult.SizeInsufficient"/></description></item>
        ///   <item><description><see cref="XrResult.SpatialBufferIdInvalidEXT"/></description></item>
        /// </list>
        /// </returns>
        /// <remarks>
        /// > [!IMPORTANT]
        /// > Output parameters are only valid if the returned result `.IsSuccess()`.
        /// > Don't read the output if an error is returned.
        /// </remarks>
        [DllImport(InternalConstants.openXRLibrary, EntryPoint = "EXT_spatial_entity_xrGetSpatialBufferUint32EXT")]
        public static extern unsafe XrResult xrGetSpatialBufferUint32EXT(
            XrSpatialSnapshotEXT snapshot,
            in XrSpatialBufferGetInfoEXT info,
            uint bufferCapacityInput,
            out uint bufferCountOutput,
            uint* buffer);

        /// <summary>
        /// Gets the contents of a buffer of type <see cref="XrSpatialBufferTypeEXT.Uint32"/>.
        /// Provided by `XR_EXT_spatial_entity`.
        /// </summary>
        /// <param name="snapshot">The handle to a spatial snapshot.</param>
        /// <param name="info">The information regarding the buffer to query.</param>
        /// <param name="allocator">The allocation strategy to use for <paramref name="buffer"/>.</param>
        /// <param name="buffer">The output array.</param>
        /// <returns>The result of the operation.\
        /// \
        /// Success codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.Success"/></description></item>
        ///   <item><description><see cref="XrResult.LossPending"/></description></item>
        /// </list>
        /// Failure codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.FunctionUnsupported"/></description></item>
        ///   <item><description><see cref="XrResult.ValidationFailure"/></description></item>
        ///   <item><description><see cref="XrResult.RuntimeFailure"/></description></item>
        ///   <item><description><see cref="XrResult.HandleInvalid"/></description></item>
        ///   <item><description><see cref="XrResult.InstanceLost"/></description></item>
        ///   <item><description><see cref="XrResult.SessionLost"/></description></item>
        ///   <item><description><see cref="XrResult.OutOfMemory"/></description></item>
        ///   <item><description><see cref="XrResult.SizeInsufficient"/></description></item>
        ///   <item><description><see cref="XrResult.SpatialBufferIdInvalidEXT"/></description></item>
        /// </list>
        /// </returns>
        /// <remarks>
        /// > [!IMPORTANT]
        /// > Output parameters are only valid if the returned result `.IsSuccess()`.
        /// > Don't read the output if an error is returned.
        ///
        /// You are responsible to `Dispose` the output native array if you pass `Allocator.Persistent` as the
        /// <paramref name="allocator"/> value.
        /// </remarks>
        /// <exception cref="OverflowException">Thrown if `buffer.Length` would exceed
        /// <see cref="Int32.MaxValue"/>.</exception>
        public static unsafe XrResult xrGetSpatialBufferUint32EXT(
            XrSpatialSnapshotEXT snapshot,
            in XrSpatialBufferGetInfoEXT info,
            Allocator allocator,
            out NativeArray<uint> buffer)
        {
            var result = xrGetSpatialBufferUint32EXT(snapshot, info, 0, out var bufferCountOutput, null);
            if (result.IsError())
            {
                buffer = default;
                return result;
            }

            buffer = new NativeArray<uint>(checked((int)bufferCountOutput), allocator);
            return xrGetSpatialBufferUint32EXT(
                snapshot, info, bufferCountOutput, out _, (uint*)buffer.GetUnsafePtr());
        }

        /// <summary>
        /// Gets the contents of a buffer of type <see cref="XrSpatialBufferTypeEXT.Float"/>.
        /// Provided by `XR_EXT_spatial_entity`.
        /// </summary>
        /// <param name="snapshot">The handle to a spatial snapshot.</param>
        /// <param name="info">The information regarding the buffer to query.</param>
        /// <param name="bufferCapacityInput">The capacity of <paramref name="buffer"/>, or `0` to indicate a request
        /// to retrieve the required capacity.</param>
        /// <param name="bufferCountOutput">The count of elements in <paramref name="buffer"/>, or the required capacity
        /// if <paramref name="bufferCapacityInput"/> is insufficient.</param>
        /// <param name="buffer">Pointer to an array of `float` elements. Can be `null` if
        /// <paramref name="bufferCapacityInput"/> is `0`.</param>
        /// <returns>The result of the operation.\
        /// \
        /// Success codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.Success"/></description></item>
        ///   <item><description><see cref="XrResult.LossPending"/></description></item>
        /// </list>
        /// Failure codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.FunctionUnsupported"/></description></item>
        ///   <item><description><see cref="XrResult.ValidationFailure"/></description></item>
        ///   <item><description><see cref="XrResult.RuntimeFailure"/></description></item>
        ///   <item><description><see cref="XrResult.HandleInvalid"/></description></item>
        ///   <item><description><see cref="XrResult.InstanceLost"/></description></item>
        ///   <item><description><see cref="XrResult.SessionLost"/></description></item>
        ///   <item><description><see cref="XrResult.OutOfMemory"/></description></item>
        ///   <item><description><see cref="XrResult.SizeInsufficient"/></description></item>
        ///   <item><description><see cref="XrResult.SpatialBufferIdInvalidEXT"/></description></item>
        /// </list>
        /// </returns>
        /// <remarks>
        /// > [!IMPORTANT]
        /// > Output parameters are only valid if the returned result `.IsSuccess()`.
        /// > Don't read the output if an error is returned.
        /// </remarks>
        [DllImport(InternalConstants.openXRLibrary, EntryPoint = "EXT_spatial_entity_xrGetSpatialBufferFloatEXT")]
        public static extern unsafe XrResult xrGetSpatialBufferFloatEXT(
            XrSpatialSnapshotEXT snapshot,
            in XrSpatialBufferGetInfoEXT info,
            uint bufferCapacityInput,
            out uint bufferCountOutput,
            float* buffer);

        /// <summary>
        /// Gets the contents of a buffer of type <see cref="XrSpatialBufferTypeEXT.Float"/>.
        /// Provided by `XR_EXT_spatial_entity`.
        /// </summary>
        /// <param name="snapshot">The handle to a spatial snapshot.</param>
        /// <param name="info">The information regarding the buffer to query.</param>
        /// <param name="allocator">The allocation strategy to use for <paramref name="buffer"/>.</param>
        /// <param name="buffer">The output array.</param>
        /// <returns>The result of the operation.\
        /// \
        /// Success codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.Success"/></description></item>
        ///   <item><description><see cref="XrResult.LossPending"/></description></item>
        /// </list>
        /// Failure codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.FunctionUnsupported"/></description></item>
        ///   <item><description><see cref="XrResult.ValidationFailure"/></description></item>
        ///   <item><description><see cref="XrResult.RuntimeFailure"/></description></item>
        ///   <item><description><see cref="XrResult.HandleInvalid"/></description></item>
        ///   <item><description><see cref="XrResult.InstanceLost"/></description></item>
        ///   <item><description><see cref="XrResult.SessionLost"/></description></item>
        ///   <item><description><see cref="XrResult.OutOfMemory"/></description></item>
        ///   <item><description><see cref="XrResult.SizeInsufficient"/></description></item>
        ///   <item><description><see cref="XrResult.SpatialBufferIdInvalidEXT"/></description></item>
        /// </list>
        /// </returns>
        /// <remarks>
        /// > [!IMPORTANT]
        /// > Output parameters are only valid if the returned result `.IsSuccess()`.
        /// > Don't read the output if an error is returned.
        ///
        /// You are responsible to `Dispose` the output native array if you pass `Allocator.Persistent` as the
        /// <paramref name="allocator"/> value.
        /// </remarks>
        /// <exception cref="OverflowException">Thrown if `buffer.Length` would exceed
        /// <see cref="Int32.MaxValue"/>.</exception>
        public static unsafe XrResult xrGetSpatialBufferFloatEXT(
            XrSpatialSnapshotEXT snapshot,
            in XrSpatialBufferGetInfoEXT info,
            Allocator allocator,
            out NativeArray<float> buffer)
        {
            var result = xrGetSpatialBufferFloatEXT(snapshot, info, 0, out var bufferCountOutput, null);
            if (result.IsError())
            {
                buffer = default;
                return result;
            }

            buffer = new NativeArray<float>(checked((int)bufferCountOutput), allocator);
            return xrGetSpatialBufferFloatEXT(
                snapshot, info, bufferCountOutput, out _, (float*)buffer.GetUnsafePtr());
        }

        /// <summary>
        /// Gets the contents of a buffer of type <see cref="XrSpatialBufferTypeEXT.Vector2f"/>.
        /// Provided by `XR_EXT_spatial_entity`.
        /// </summary>
        /// <param name="snapshot">The handle to a spatial snapshot.</param>
        /// <param name="info">The information regarding the buffer to query.</param>
        /// <param name="bufferCapacityInput">The capacity of <paramref name="buffer"/>, or `0` to indicate a request
        /// to retrieve the required capacity.</param>
        /// <param name="bufferCountOutput">The count of elements in <paramref name="buffer"/>, or the required capacity
        /// if <paramref name="bufferCapacityInput"/> is insufficient.</param>
        /// <param name="buffer">Pointer to an array of `XrVector2f` elements. Can be `null` if
        /// <paramref name="bufferCapacityInput"/> is `0`.</param>
        /// <returns>The result of the operation.\
        /// \
        /// Success codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.Success"/></description></item>
        ///   <item><description><see cref="XrResult.LossPending"/></description></item>
        /// </list>
        /// Failure codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.FunctionUnsupported"/></description></item>
        ///   <item><description><see cref="XrResult.ValidationFailure"/></description></item>
        ///   <item><description><see cref="XrResult.RuntimeFailure"/></description></item>
        ///   <item><description><see cref="XrResult.HandleInvalid"/></description></item>
        ///   <item><description><see cref="XrResult.InstanceLost"/></description></item>
        ///   <item><description><see cref="XrResult.SessionLost"/></description></item>
        ///   <item><description><see cref="XrResult.OutOfMemory"/></description></item>
        ///   <item><description><see cref="XrResult.SizeInsufficient"/></description></item>
        ///   <item><description><see cref="XrResult.SpatialBufferIdInvalidEXT"/></description></item>
        /// </list>
        /// </returns>
        /// <remarks>
        /// > [!IMPORTANT]
        /// > Output parameters are only valid if the returned result `.IsSuccess()`.
        /// > Don't read the output if an error is returned.
        /// </remarks>
        [DllImport(InternalConstants.openXRLibrary, EntryPoint = "EXT_spatial_entity_xrGetSpatialBufferVector2fEXT")]
        public static extern unsafe XrResult xrGetSpatialBufferVector2fEXT(
            XrSpatialSnapshotEXT snapshot,
            in XrSpatialBufferGetInfoEXT info,
            uint bufferCapacityInput,
            out uint bufferCountOutput,
            XrVector2f* buffer);

        /// <summary>
        /// Gets the contents of a buffer of type <see cref="XrSpatialBufferTypeEXT.Vector2f"/>.
        /// Provided by `XR_EXT_spatial_entity`.
        /// </summary>
        /// <param name="snapshot">The handle to a spatial snapshot.</param>
        /// <param name="info">The information regarding the buffer to query.</param>
        /// <param name="allocator">The allocation strategy to use for <paramref name="buffer"/>.</param>
        /// <param name="buffer">The output array.</param>
        /// <returns>The result of the operation.\
        /// \
        /// Success codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.Success"/></description></item>
        ///   <item><description><see cref="XrResult.LossPending"/></description></item>
        /// </list>
        /// Failure codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.FunctionUnsupported"/></description></item>
        ///   <item><description><see cref="XrResult.ValidationFailure"/></description></item>
        ///   <item><description><see cref="XrResult.RuntimeFailure"/></description></item>
        ///   <item><description><see cref="XrResult.HandleInvalid"/></description></item>
        ///   <item><description><see cref="XrResult.InstanceLost"/></description></item>
        ///   <item><description><see cref="XrResult.SessionLost"/></description></item>
        ///   <item><description><see cref="XrResult.OutOfMemory"/></description></item>
        ///   <item><description><see cref="XrResult.SizeInsufficient"/></description></item>
        ///   <item><description><see cref="XrResult.SpatialBufferIdInvalidEXT"/></description></item>
        /// </list>
        /// </returns>
        /// <remarks>
        /// > [!IMPORTANT]
        /// > Output parameters are only valid if the returned result `.IsSuccess()`.
        /// > Don't read the output if an error is returned.
        ///
        /// You are responsible to `Dispose` the output native array if you pass `Allocator.Persistent` as the
        /// <paramref name="allocator"/> value.
        /// </remarks>
        /// <exception cref="OverflowException">Thrown if `buffer.Length` would exceed
        /// <see cref="Int32.MaxValue"/>.</exception>
        public static unsafe XrResult xrGetSpatialBufferVector2fEXT(
            XrSpatialSnapshotEXT snapshot,
            in XrSpatialBufferGetInfoEXT info,
            Allocator allocator,
            out NativeArray<XrVector2f> buffer)
        {
            var result = xrGetSpatialBufferVector2fEXT(snapshot, info, 0, out var bufferCountOutput, null);
            if (result.IsError())
            {
                buffer = default;
                return result;
            }

            buffer = new NativeArray<XrVector2f>(checked((int)bufferCountOutput), allocator);
            return xrGetSpatialBufferVector2fEXT(
                snapshot, info, bufferCountOutput, out _, (XrVector2f*)buffer.GetUnsafePtr());
        }

        /// <summary>
        /// Gets the contents of a buffer of type <see cref="XrSpatialBufferTypeEXT.Vector3f"/>.
        /// Provided by `XR_EXT_spatial_entity`.
        /// </summary>
        /// <param name="snapshot">The handle to a spatial snapshot.</param>
        /// <param name="info">The information regarding the buffer to query.</param>
        /// <param name="bufferCapacityInput">The capacity of <paramref name="buffer"/>, or `0` to indicate a request
        /// to retrieve the required capacity.</param>
        /// <param name="bufferCountOutput">The count of elements in <paramref name="buffer"/>, or the required capacity
        /// if <paramref name="bufferCapacityInput"/> is insufficient.</param>
        /// <param name="buffer">Pointer to an array of `XrVector3f` elements. Can be `null` if
        /// <paramref name="bufferCapacityInput"/> is `0`.</param>
        /// <returns>The result of the operation.\
        /// \
        /// Success codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.Success"/></description></item>
        ///   <item><description><see cref="XrResult.LossPending"/></description></item>
        /// </list>
        /// Failure codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.FunctionUnsupported"/></description></item>
        ///   <item><description><see cref="XrResult.ValidationFailure"/></description></item>
        ///   <item><description><see cref="XrResult.RuntimeFailure"/></description></item>
        ///   <item><description><see cref="XrResult.HandleInvalid"/></description></item>
        ///   <item><description><see cref="XrResult.InstanceLost"/></description></item>
        ///   <item><description><see cref="XrResult.SessionLost"/></description></item>
        ///   <item><description><see cref="XrResult.OutOfMemory"/></description></item>
        ///   <item><description><see cref="XrResult.SizeInsufficient"/></description></item>
        ///   <item><description><see cref="XrResult.SpatialBufferIdInvalidEXT"/></description></item>
        /// </list>
        /// </returns>
        /// <remarks>
        /// > [!IMPORTANT]
        /// > Output parameters are only valid if the returned result `.IsSuccess()`.
        /// > Don't read the output if an error is returned.
        /// </remarks>
        [DllImport(InternalConstants.openXRLibrary, EntryPoint = "EXT_spatial_entity_xrGetSpatialBufferVector3fEXT")]
        public static extern unsafe XrResult xrGetSpatialBufferVector3fEXT(
            XrSpatialSnapshotEXT snapshot,
            in XrSpatialBufferGetInfoEXT info,
            uint bufferCapacityInput,
            out uint bufferCountOutput,
            XrVector3f* buffer);

        /// <summary>
        /// Gets the contents of a buffer of type <see cref="XrSpatialBufferTypeEXT.Vector3f"/>.
        /// Provided by `XR_EXT_spatial_entity`.
        /// </summary>
        /// <param name="snapshot">The handle to a spatial snapshot.</param>
        /// <param name="info">The information regarding the buffer to query.</param>
        /// <param name="allocator">The allocation strategy to use for <paramref name="buffer"/>.</param>
        /// <param name="buffer">The output array.</param>
        /// <returns>The result of the operation.\
        /// \
        /// Success codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.Success"/></description></item>
        ///   <item><description><see cref="XrResult.LossPending"/></description></item>
        /// </list>
        /// Failure codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.FunctionUnsupported"/></description></item>
        ///   <item><description><see cref="XrResult.ValidationFailure"/></description></item>
        ///   <item><description><see cref="XrResult.RuntimeFailure"/></description></item>
        ///   <item><description><see cref="XrResult.HandleInvalid"/></description></item>
        ///   <item><description><see cref="XrResult.InstanceLost"/></description></item>
        ///   <item><description><see cref="XrResult.SessionLost"/></description></item>
        ///   <item><description><see cref="XrResult.OutOfMemory"/></description></item>
        ///   <item><description><see cref="XrResult.SizeInsufficient"/></description></item>
        ///   <item><description><see cref="XrResult.SpatialBufferIdInvalidEXT"/></description></item>
        /// </list>
        /// </returns>
        /// <remarks>
        /// > [!IMPORTANT]
        /// > Output parameters are only valid if the returned result `.IsSuccess()`.
        /// > Don't read the output if an error is returned.
        ///
        /// You are responsible to `Dispose` the output native array if you pass `Allocator.Persistent` as the
        /// <paramref name="allocator"/> value.
        /// </remarks>
        /// <exception cref="OverflowException">Thrown if `buffer.Length` would exceed
        /// <see cref="Int32.MaxValue"/>.</exception>
        public static unsafe XrResult xrGetSpatialBufferVector3fEXT(
            XrSpatialSnapshotEXT snapshot,
            in XrSpatialBufferGetInfoEXT info,
            Allocator allocator,
            out NativeArray<XrVector3f> buffer)
        {
            var result = xrGetSpatialBufferVector3fEXT(snapshot, info, 0, out var bufferCountOutput, null);
            if (result.IsError())
            {
                buffer = default;
                return result;
            }

            buffer = new NativeArray<XrVector3f>(checked((int)bufferCountOutput), allocator);
            return xrGetSpatialBufferVector3fEXT(
                snapshot, info, bufferCountOutput, out _, (XrVector3f*)buffer.GetUnsafePtr());
        }

        /// <summary>
        /// Creates a spatial update snapshot, which contains the latest component data for given entities
        /// as specified by the creation info. Provided by `XR_EXT_spatial_entity`.
        /// </summary>
        /// <param name="spatialContext">A spatial context previously created using an overload of
        /// `xrCreateSpatialContextAsyncEXT`.</param>
        /// <param name="createInfo">The creation info.</param>
        /// <param name="snapshot">Pointer to the created spatial update snapshot if this method returns a success code.
        /// Otherwise, `null`.</param>
        /// <returns>The result of the operation.\
        /// \
        /// Success codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.Success"/></description></item>
        ///   <item><description><see cref="XrResult.LossPending"/></description></item>
        /// </list>
        /// Failure codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.FunctionUnsupported"/></description></item>
        ///   <item><description><see cref="XrResult.ValidationFailure"/></description></item>
        ///   <item><description><see cref="XrResult.RuntimeFailure"/></description></item>
        ///   <item><description><see cref="XrResult.HandleInvalid"/></description></item>
        ///   <item><description><see cref="XrResult.InstanceLost"/></description></item>
        ///   <item><description><see cref="XrResult.SessionLost"/></description></item>
        ///   <item><description><see cref="XrResult.OutOfMemory"/></description></item>
        ///   <item><description><see cref="XrResult.LimitReached"/></description></item>
        ///   <item><description><see cref="XrResult.TimeInvalid"/></description></item>
        ///   <item><description><see cref="XrResult.SpatialComponentNotEnabledEXT"/></description></item>
        /// </list>
        /// </returns>
        /// <seealso cref="M:UnityEngine.XR.OpenXR.NativeTypes.OpenXRNativeApi.xrCreateSpatialContextAsyncEXT(UnityEngine.XR.OpenXR.NativeTypes.XrSpatialContextCreateInfoEXT@,System.UInt64@)"/>
        /// <remarks>
        /// > [!WARNING]
        /// > You can create any number of snapshots according to your use case, but be mindful of the memory that is
        /// > allocated for each new snapshot. Destroy snapshots once you no longer need them using
        /// > <see cref="xrDestroySpatialSnapshotEXT">xrDestroySpatialSnapshotEXT</see>.
        /// </remarks>
        [DllImport(InternalConstants.openXRLibrary, EntryPoint = "EXT_spatial_entity_xrCreateSpatialUpdateSnapshotEXT")]
        public static extern XrResult xrCreateSpatialUpdateSnapshotEXT(
            XrSpatialContextEXT spatialContext,
            in XrSpatialUpdateSnapshotCreateInfoEXT createInfo,
            out XrSpatialSnapshotEXT snapshot);

        /// <summary>
        /// Creates a spatial update snapshot using your app space and the frame's predicted display time.
        /// A spatial update snapshot contains the latest component data for given entities.
        /// Provided by `XR_EXT_spatial_entity`.
        /// </summary>
        /// <param name="spatialContext">A spatial context previously created using an overload of
        /// `xrCreateSpatialContextAsyncEXT`.</param>
        /// <param name="entityCount">The count of elements in <paramref name="entities"/>.</param>
        /// <param name="entities">Pointer to an array of entities for which the runtime must include component data
        /// in the snapshot.</param>
        /// <param name="componentTypeCount">The count of elements in <paramref name="componentTypes"/>.</param>
        /// <param name="componentTypes">Pointer to an array of component types for which the runtime must include data
        /// in the snapshot.</param>
        /// <param name="snapshot">The output snapshot.</param>
        /// <returns>The result of the operation.\
        /// \
        /// `nativeStatusCode` success codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.Success"/></description></item>
        ///   <item><description><see cref="XrResult.LossPending"/></description></item>
        /// </list>
        /// `nativeStatusCode` failure codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.FunctionUnsupported"/></description></item>
        ///   <item><description><see cref="XrResult.ValidationFailure"/></description></item>
        ///   <item><description><see cref="XrResult.RuntimeFailure"/></description></item>
        ///   <item><description><see cref="XrResult.HandleInvalid"/></description></item>
        ///   <item><description><see cref="XrResult.InstanceLost"/></description></item>
        ///   <item><description><see cref="XrResult.SessionLost"/></description></item>
        ///   <item><description><see cref="XrResult.OutOfMemory"/></description></item>
        ///   <item><description><see cref="XrResult.LimitReached"/></description></item>
        ///   <item><description><see cref="XrResult.TimeInvalid"/></description></item>
        ///   <item><description><see cref="XrResult.SpatialComponentNotEnabledEXT"/></description></item>
        /// </list>
        /// </returns>
        /// <seealso cref="M:UnityEngine.XR.OpenXR.NativeTypes.OpenXRNativeApi.xrCreateSpatialContextAsyncEXT(UnityEngine.XR.OpenXR.NativeTypes.XrSpatialContextCreateInfoEXT@,System.UInt64@)"/>
        /// <remarks>
        /// > [!IMPORTANT]
        /// > Output parameters are only valid if the returned result `.IsSuccess()`.
        /// > Don't read the output if an error is returned.
        ///
        /// > [!WARNING]
        /// > You can create any number of snapshots according to your use case, but be mindful of the memory that is
        /// > allocated for each new snapshot. Destroy snapshots once you no longer need them using
        /// > <see cref="xrDestroySpatialSnapshotEXT"/>.
        /// </remarks>
        [DllImport(InternalConstants.openXRLibrary,
            EntryPoint = "EXT_spatial_entity_xrCreateSpatialUpdateSnapshotEXT_usingContext")]
        public static extern unsafe OpenXRResultStatus xrCreateSpatialUpdateSnapshotEXT(
            XrSpatialContextEXT spatialContext,
            uint entityCount,
            XrSpatialEntityEXT* entities,
            uint componentTypeCount,
            XrSpatialComponentTypeEXT* componentTypes,
            out XrSpatialSnapshotEXT snapshot);

        /// <summary>
        /// Creates a spatial update snapshot using your app space and the frame's predicted display time.
        /// A spatial update snapshot contains the latest component data for given entities.
        /// Provided by `XR_EXT_spatial_entity`.
        /// </summary>
        /// <param name="spatialContext">A spatial context previously created using an overload of
        /// `xrCreateSpatialContextAsyncEXT`.</param>
        /// <param name="entities">Array of entities for which the runtime must include component data in the
        /// snapshot.</param>
        /// <param name="componentTypes">Array of component types for which the runtime must include data in the
        /// snapshot.</param>
        /// <param name="snapshot">The output snapshot.</param>
        /// <returns>The result of the operation.\
        /// \
        /// `nativeStatusCode` success codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.Success"/></description></item>
        ///   <item><description><see cref="XrResult.LossPending"/></description></item>
        /// </list>
        /// `nativeStatusCode` failure codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.FunctionUnsupported"/></description></item>
        ///   <item><description><see cref="XrResult.ValidationFailure"/></description></item>
        ///   <item><description><see cref="XrResult.RuntimeFailure"/></description></item>
        ///   <item><description><see cref="XrResult.HandleInvalid"/></description></item>
        ///   <item><description><see cref="XrResult.InstanceLost"/></description></item>
        ///   <item><description><see cref="XrResult.SessionLost"/></description></item>
        ///   <item><description><see cref="XrResult.OutOfMemory"/></description></item>
        ///   <item><description><see cref="XrResult.LimitReached"/></description></item>
        ///   <item><description><see cref="XrResult.TimeInvalid"/></description></item>
        ///   <item><description><see cref="XrResult.SpatialComponentNotEnabledEXT"/></description></item>
        /// </list>
        /// </returns>
        /// <seealso cref="M:UnityEngine.XR.OpenXR.NativeTypes.OpenXRNativeApi.xrCreateSpatialContextAsyncEXT(UnityEngine.XR.OpenXR.NativeTypes.XrSpatialContextCreateInfoEXT@,System.UInt64@)"/>
        /// <remarks>
        /// > [!IMPORTANT]
        /// > Output parameters are only valid if the returned result `.IsSuccess()`.
        /// > Don't read the output if an error is returned.
        ///
        /// > [!WARNING]
        /// > You can create any number of snapshots according to your use case, but be mindful of the memory that is
        /// > allocated for each new snapshot. Destroy snapshots once you no longer need them using
        /// > <see cref="xrDestroySpatialSnapshotEXT"/>.
        /// </remarks>
        public static unsafe OpenXRResultStatus xrCreateSpatialUpdateSnapshotEXT(
            XrSpatialContextEXT spatialContext,
            NativeArray<XrSpatialEntityEXT> entities,
            NativeArray<XrSpatialComponentTypeEXT> componentTypes,
            out XrSpatialSnapshotEXT snapshot)
        {
            return xrCreateSpatialUpdateSnapshotEXT(
                spatialContext,
                (uint)entities.Length,
                (XrSpatialEntityEXT*)entities.GetUnsafePtr(),
                (uint)componentTypes.Length,
                (XrSpatialComponentTypeEXT*)componentTypes.GetUnsafePtr(),
                out snapshot);
        }

        /// <summary>
        /// Destroys the given spatial snapshot handle and the resources associated with it.
        /// Provided by `XR_EXT_spatial_entity`.
        /// </summary>
        /// <param name="snapshot">A spatial snapshot previously created by methods such as
        /// `xrCreateSpatialDiscoverySnapshotCompleteEXT` or
        /// `xrCreateSpatialDiscoverySnapshotCompleteEXT`.</param>
        /// <returns>The result of the operation.\
        /// \
        /// Success codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.Success"/></description></item>
        /// </list>
        /// Failure codes:
        /// <list type="bullet">
        ///   <item><description><see cref="XrResult.FunctionUnsupported"/></description></item>
        ///   <item><description><see cref="XrResult.RuntimeFailure"/></description></item>
        ///   <item><description><see cref="XrResult.HandleInvalid"/></description></item>
        /// </list>
        /// </returns>
        /// <remarks>
        /// > [!WARNING]
        /// > You are responsible to synchronize this operation with any other threads you created that use this
        /// > spatial snapshot.
        /// </remarks>
        /// <seealso cref="M:UnityEngine.XR.OpenXR.NativeTypes.OpenXRNativeApi.xrCreateSpatialDiscoverySnapshotCompleteEXT(System.UInt64,UnityEngine.XR.OpenXR.NativeTypes.XrCreateSpatialDiscoverySnapshotCompletionInfoEXT@,UnityEngine.XR.OpenXR.NativeTypes.XrCreateSpatialDiscoverySnapshotCompletionEXT@)"/>
        /// <seealso cref="M:UnityEngine.XR.OpenXR.NativeTypes.OpenXRNativeApi.xrCreateSpatialDiscoverySnapshotCompleteEXT(System.UInt64,System.UInt64,UnityEngine.XR.OpenXR.NativeTypes.XrCreateSpatialDiscoverySnapshotCompletionEXT@)"/>
        /// <seealso cref="M:UnityEngine.XR.OpenXR.NativeTypes.OpenXRNativeApi.xrCreateSpatialUpdateSnapshotEXT(System.UInt64,UnityEngine.XR.OpenXR.NativeTypes.XrSpatialUpdateSnapshotCreateInfoEXT@,System.UInt64@)"/>
        /// <seealso cref="M:UnityEngine.XR.OpenXR.NativeTypes.OpenXRNativeApi.xrCreateSpatialUpdateSnapshotEXT(System.UInt64,System.UInt32,System.UInt64*,System.UInt32,UnityEngine.XR.OpenXR.NativeTypes.XrSpatialComponentTypeEXT*,System.UInt64@)"/>
        /// <seealso cref="M:UnityEngine.XR.OpenXR.NativeTypes.OpenXRNativeApi.xrCreateSpatialUpdateSnapshotEXT(System.UInt64,Unity.Collections.NativeArray{System.UInt64},Unity.Collections.NativeArray{UnityEngine.XR.OpenXR.NativeTypes.XrSpatialComponentTypeEXT},System.UInt64@)"/>
        [DllImport(InternalConstants.openXRLibrary, EntryPoint = "EXT_spatial_entity_xrDestroySpatialSnapshotEXT")]
        public static extern XrResult xrDestroySpatialSnapshotEXT(XrSpatialSnapshotEXT snapshot);
    }
}
