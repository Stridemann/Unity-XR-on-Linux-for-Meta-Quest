namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// Convenience type for iterating (read only).
    /// </summary>
    public unsafe struct XrBaseInStructure
    {
        /// <summary>
        /// The <see cref="XrStructureType"/> of this structure.
        /// </summary>
        public XrStructureType type { get; }

        /// <summary>
        /// `null` or a pointer to the next structure in a structure chain.
        /// </summary>
        public void* next { get; set; }

        /// <summary>
        /// Construct an instance.
        /// </summary>
        /// <param name="type">The structure type.</param>
        /// <param name="next">The next pointer.</param>
        public XrBaseInStructure(XrStructureType type, void* next)
        {
            this.type = type;
            this.next = next;
        }

        /// <summary>
        /// Construct an instance with a `null` next pointer.
        /// </summary>
        /// <param name="type">The structure type.</param>
        public XrBaseInStructure(XrStructureType type)
            : this(type, null) { }
    }
}
