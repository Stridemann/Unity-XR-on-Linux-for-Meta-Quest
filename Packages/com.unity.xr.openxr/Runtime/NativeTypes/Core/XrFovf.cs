namespace UnityEngine.XR.OpenXR.NativeTypes
{
    /// <summary>
    /// Field of view.
    /// </summary>
    public struct XrFovf
    {
        /// <summary>
        /// The angle of the left side of the field of view. For a symmetric field of view this value is negative.
        /// </summary>
        public float AngleLeft;

        /// <summary>
        /// The angle of the right side of the field of view.
        /// </summary>
        public float AngleRight;

        /// <summary>
        /// The angle of the top part of the field of view.
        /// </summary>
        public float AngleUp;

        /// <summary>
        /// The angle of the bottom part of the field of view. For a symmetric field of view this value is negative.
        /// </summary>
        public float AngleDown;

        /// <summary>
        /// Creates a new field of view definition with the specified left, right, up, and down angles.
        /// </summary>
        /// <param name="angleLeft">The angle of the left side of the field of view in radians.</param>
        /// <param name="angleRight">The angle of the right side of the field of view in radians.</param>
        /// <param name="angleUp">The angle of the top part of the field of view in radians.</param>
        /// <param name="angleDown">The angle of the bottom part of the field of view in radians.</param>
        public XrFovf(float angleLeft, float angleRight, float angleUp, float angleDown)
        {
            AngleLeft = angleLeft;
            AngleRight = angleRight;
            AngleUp = angleUp;
            AngleDown = angleDown;
        }
    }
}
