namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// The base header for events. Use the <see cref="type"/> field to identify which event type this struct contains,
    /// then cast the pointer to the corresponding struct type.
    /// </summary>
    public readonly unsafe struct XrEventDataBaseHeader
    {
        /// <summary>
        /// The `XrStructureType` of this struct.
        /// </summary>
        public XrStructureType type { get; }

        /// <summary>
        /// `null` or a pointer to the next structure in a structure chain.
        /// No such structures are defined in core OpenXR.
        /// </summary>
        public void* next { get; }

        /// <summary>
        /// Construct an instance.
        /// </summary>
        /// <param name="type">The `XrStructureType`.</param>
        /// <param name="next">The next pointer.</param>
        public XrEventDataBaseHeader(XrStructureType type, void* next)
        {
            this.type = type;
            this.next = next;
        }

        /// <summary>
        /// Construct an instance with a `null` next pointer.
        /// </summary>
        /// <param name="type">The `XrStructureType`.</param>
        public XrEventDataBaseHeader(XrStructureType type)
            : this(type, null) { }
    }
}
