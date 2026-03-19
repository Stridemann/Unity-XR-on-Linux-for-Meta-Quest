using System;

namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// Represents the status of a completed operation as a Unity-provided <see cref="statusCode"/> and a
    /// runtime-provided <see cref="nativeStatusCode"/>.
    /// </summary>
    public readonly struct OpenXRResultStatus
        : IEquatable<OpenXRResultStatus>, IComparable<OpenXRResultStatus>, IEquatable<XrResult>
    {
        /// <summary>
        /// Indicates whether the operation succeeded or failed as well as whether additional status information is
        /// available in <see cref="OpenXRResultStatus.nativeStatusCode"/>.
        /// </summary>
        /// <remarks>
        /// The integer value of this enum has the following meanings:
        /// <list type="table">
        ///   <listheader>
        ///     <term>Value</term>
        ///     <term>Meaning</term>
        ///   </listheader>
        ///   <item>
        ///     <description>Less than zero</description>
        ///     <description>The operation failed with an error.</description>
        ///   </item>
        ///   <item>
        ///     <description>Zero</description>
        ///     <description>The operation was an unqualified success.</description>
        ///   </item>
        ///   <item>
        ///     <description>Greater than zero</description>
        ///     <description>The operation succeeded.</description>
        ///   </item>
        /// </list>
        /// </remarks>
        public enum StatusCode
        {
            /// <summary>
            /// Indicates that the operation was successful, and additional information is available in
            /// <see cref="OpenXRResultStatus.nativeStatusCode"/>.
            /// </summary>
            PlatformQualifiedSuccess = 1,

            /// <summary>
            /// Indicates that the operation was successful, and no additional information is available.
            /// </summary>
            UnqualifiedSuccess = 0,

            /// <summary>
            /// Indicates that the operation failed with an error, and additional information is available in
            /// <see cref="OpenXRResultStatus.nativeStatusCode"/>.
            /// </summary>
            PlatformError = -1,

            /// <summary>
            /// Indicates that the operation failed with an unknown error, and no additional information is available.
            /// </summary>
            UnknownError = -2,

            /// <summary>
            /// Indicates that the operation failed because the provider was uninitialized.
            /// This may be because XR Plug-in Management has not yet initialized an XR loader, or because a
            /// platform-specific resource is not yet available.
            /// </summary>
            ProviderUninitialized = -3,

            /// <summary>
            /// Indicates that the operation failed because the provider was not started.
            /// This may be because you haven't enabled a necessary manager component, or because platform-specific
            /// requirements to start the provider were not met.
            /// </summary>
            ProviderNotStarted = -4,

            /// <summary>
            /// Indicates that the input parameters failed to meet Unity's validation requirements,
            /// and no additional information is available.
            /// </summary>
            ValidationFailure = -5,

            /// <summary>
            /// Indicates that Unity has determined that the operation is not supported on the device.
            /// </summary>
            /// <remarks>
            /// This error code allows Unity to design APIs where some source other than the OpenXR runtime
            /// defines whether an operation is supported.
            ///
            /// If a runtime returns `XrResult.FeatureUnsupported` or `XrResult.FunctionUnsupported` as the
            /// `nativeStatusCode` for an API call, the expected `StatusCode` value is `PlatformError`,
            /// as the runtime is the source of the error.
            /// </remarks>
            Unsupported = -6,
        }

        /// <summary>
        /// Get a default-initialized successful instance, convenient for testing.
        /// </summary>
        public static OpenXRResultStatus unqualifiedSuccess => new(0, 0);

        /// <summary>
        /// The Unity-provided status code.
        /// </summary>
        /// <value>The status code.</value>
        /// <remarks>
        /// If this value is <see cref="StatusCode.PlatformQualifiedSuccess"/> or
        /// <see cref="StatusCode.PlatformError"/>, read the <see cref="nativeStatusCode"/> for more detailed
        /// information.
        /// </remarks>
        public StatusCode statusCode { get; }

        /// <summary>
        /// The runtime-provided status code, if <see cref="statusCode"/> is
        /// <see cref="StatusCode.PlatformQualifiedSuccess"/> or <see cref="StatusCode.PlatformError"/>.
        /// </summary>
        /// <value>The native status code.</value>
        public XrResult nativeStatusCode { get; }

        /// <summary>
        /// Construct an instance with the given status code.
        /// </summary>
        /// <param name="statusCode">The Unity-provided status code.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="statusCode"/> is
        /// <see cref="StatusCode.PlatformError"/> or <see cref="StatusCode.PlatformQualifiedSuccess"/>, as these status
        /// codes require a corresponding `nativeStatusCode`.</exception>
        public OpenXRResultStatus(StatusCode statusCode)
        {
            if (statusCode is StatusCode.PlatformError or StatusCode.PlatformQualifiedSuccess)
                throw new ArgumentException(
                    $"Status code {statusCode} requires a corresponding nativeStatusCode. Use a different constructor.",
                    nameof(statusCode));

            this.statusCode = statusCode;
            nativeStatusCode = 0;
        }

        /// <summary>
        /// Construct an instance from an `XrResult`.
        /// </summary>
        /// <param name="nativeStatusCode">The `XrResult`.</param>
        public OpenXRResultStatus(XrResult nativeStatusCode)
        {
            this.nativeStatusCode = nativeStatusCode;
            statusCode = nativeStatusCode switch
            {
                > 0 => StatusCode.PlatformQualifiedSuccess,
                0 => StatusCode.UnqualifiedSuccess,
                < 0 => StatusCode.PlatformError
            };
        }

        /// <summary>
        /// Construct an instance with the given status code and native status code.
        /// </summary>
        /// <param name="statusCode">The status code.</param>
        /// <param name="nativeStatusCode">The native status code.</param>
        public OpenXRResultStatus(StatusCode statusCode, XrResult nativeStatusCode)
        {
            this.statusCode = statusCode;
            this.nativeStatusCode = nativeStatusCode;
        }

        /// <summary>
        /// Indicates whether the operation was an unqualified success. In other words, returns `true`
        /// if the operation succeeded and no additional status information is available.
        /// </summary>
        /// <returns>`true` if the operation was an unqualified success. Otherwise, `false`.</returns>
        public bool IsUnqualifiedSuccess() => statusCode == StatusCode.UnqualifiedSuccess;

        /// <summary>
        /// Indicates whether the operation was successful, inclusive of all success codes and
        /// <see cref="StatusCode.UnqualifiedSuccess"/>.
        /// </summary>
        /// <remarks>
        /// Equivalent to `!IsError()` or implicitly converting this instance to <see cref="bool"/>.
        /// </remarks>
        /// <returns>`true` if the operation was successful. Otherwise, `false`.</returns>
        public bool IsSuccess() => statusCode >= 0;

        /// <summary>
        /// Indicates whether the operation failed with an error.
        /// </summary>
        /// <remarks>
        /// Equivalent to `!IsSuccess()`.
        /// </remarks>
        /// <returns>`true` if the operation failed with error. Otherwise, `false`.</returns>
        public bool IsError() => statusCode < 0;

        /// <summary>
        /// Convert from `OpenXRResultStatus` to <see cref="bool"/> using <see cref="OpenXRResultStatus.IsSuccess"/>.
        /// </summary>
        /// <param name="status">The `OpenXRResultStatus`.</param>
        /// <returns>`true` if the operation was successful. Otherwise, `false`.</returns>
        public static implicit operator bool(OpenXRResultStatus status)
        {
            return status.IsSuccess();
        }

        /// <summary>
        /// Compares for equality.
        /// Two instances are equal if their <see cref="statusCode"/> and <see cref="nativeStatusCode"/> are equal.
        /// </summary>
        /// <param name="other">The other instance.</param>
        /// <returns>`true` if the objects are equal. Otherwise, `false`.</returns>
        public bool Equals(OpenXRResultStatus other)
        {
            return statusCode == other.statusCode && nativeStatusCode == other.nativeStatusCode;
        }

        /// <summary>
        /// Compares for equality with an `XrResult`.
        /// An `OpenXRResultStatus` is equal to an `XrResult` if its <see cref="nativeStatusCode"/> is equal to the
        /// `XrResult`, and its <see cref="statusCode"/> value correctly matches the `XrResult` value.
        ///
        /// For example, an `XrResult` error value must have a matching `statusCode` of
        /// <see cref="StatusCode.PlatformError"/>. Likewise, a qualified success must have a
        /// <see cref="StatusCode.PlatformQualifiedSuccess"/> status code, and an unqualified success must have
        /// <see cref="StatusCode.UnqualifiedSuccess"/> to be considered equal to the `XrResult`.
        /// </summary>
        /// <param name="other">The `XrResult` value.</param>
        /// <returns>`true` if this instance is equal to the `XrResult` value. Otherwise, `false`.</returns>
        public bool Equals(XrResult other)
        {
            if (other != nativeStatusCode)
                return false;

            return other switch
            {
                > 0 => statusCode == StatusCode.PlatformQualifiedSuccess,
                0 => statusCode == StatusCode.UnqualifiedSuccess,
                < 0 => statusCode == StatusCode.PlatformError
            };
        }

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates
        /// whether the current instance precedes, follows, or occurs in the same position in the sort order as the
        /// other object.
        ///
        /// `OpenXRResultStatus` objects are sorted first by their <see cref="nativeStatusCode"/>, then by their
        /// <see cref="statusCode"/>.
        /// </summary>
        /// <param name="other">An object to compare with this instance.</param>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return value has these meanings:
        /// <list type="table">
        ///   <listheader>
        ///     <term>Value</term>
        ///     <term>Meaning</term>
        ///   </listheader>
        ///   <item>
        ///     <description>Less than zero</description>
        ///     <description>The instance precedes <paramref name="other"/> in the sort order.</description>
        ///   </item>
        ///   <item>
        ///     <description>Zero</description>
        ///     <description>The instance occurs in the same position in the sort order as <paramref name="other"/>.</description>
        ///   </item>
        ///   <item>
        ///     <description>Greater than zero</description>
        ///     <description>This instance follows <paramref name="other"/> in the sort order.</description>
        ///   </item>
        /// </list>
        /// </returns>
        public int CompareTo(OpenXRResultStatus other)
        {
            var nativeComparison = nativeStatusCode.CompareTo(other.nativeStatusCode);
            return nativeComparison == 0 ? statusCode.CompareTo(other.statusCode) : nativeComparison;
        }

        /// <summary>
        /// Creates a string suitable for debugging purposes.
        /// </summary>
        /// <returns>The string.</returns>
        public override string ToString()
        {
            return statusCode is StatusCode.PlatformQualifiedSuccess or StatusCode.PlatformError
                ? $"({statusCode}, {nativeStatusCode})"
                : $"({statusCode})";
        }
    }
}
