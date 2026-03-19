using System;

namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// Represents a 128-bit Universally Unique Identifier. Provided by `XR_VERSION_1_1`.
    /// </summary>
    [Serializable]
    public readonly struct XrUuid : IEquatable<XrUuid>
    {
        /// <summary>
        /// A read-only instance of `XrUuid` whose value is all zeros.
        /// </summary>
        public static XrUuid empty => new(0, 0);

        /// <summary>
        /// The first 64 bits of the UUID.
        /// </summary>
        public ulong dataPart1 { get; }

        /// <summary>
        /// The second 64 bits of the UUID.
        /// </summary>
        public ulong dataPart2 { get; }

        /// <summary>
        /// Construct an instance from two 64-bit unsigned integers.
        /// </summary>
        /// <param name="dataPart1">The first 64 bits of the UUID.</param>
        /// <param name="dataPart2">The second 64 bits of the UUID.</param>
        public XrUuid(ulong dataPart1, ulong dataPart2)
        {
            this.dataPart1 = dataPart1;
            this.dataPart2 = dataPart2;
        }

        /// <summary>
        /// Tests for equality by value comparison.
        /// </summary>
        /// <param name="other">The other UUID.</param>
        /// <returns>`true` if the structs are equal. Otherwise, `false`.</returns>
        public bool Equals(XrUuid other)
        {
            return dataPart1 == other.dataPart1 && dataPart2 == other.dataPart2;
        }

        /// <summary>
        /// Generates a string representation suitable for debugging.
        /// </summary>
        /// <returns>The string.</returns>
        public override string ToString()
        {
            return $"{dataPart1:X16}-{dataPart2:X16}";
        }
    }
}
