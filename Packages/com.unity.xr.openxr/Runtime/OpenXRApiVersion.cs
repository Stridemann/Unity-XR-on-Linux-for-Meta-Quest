using System;

namespace UnityEngine.XR.OpenXR
{
    /// <summary>
    /// Represents the version number of an OpenXR API, with
    /// major, minor and patch numbers compliant with API specifications
    /// regarding number size.
    /// </summary>
    [Serializable]
    class OpenXRApiVersion
    {
        internal static OpenXRApiVersion Current => new(1, 1, 53);

        [SerializeField]
        ushort m_major;
        internal ushort Major => m_major;

        [SerializeField]
        ushort m_minor;
        internal ushort Minor => m_minor;

        [SerializeField]
        uint m_patch;
        internal uint Patch => m_patch;

        internal OpenXRApiVersion(ushort major, ushort minor, uint patch)
        {
            m_major = major;
            m_minor = minor;
            m_patch = patch;
        }

        public static bool operator ==(OpenXRApiVersion lhs, OpenXRApiVersion rhs)
        {
            return
                (lhs is null && rhs is null) ||
                (lhs is not null && rhs is not null &&
                lhs.m_major == rhs.m_major &&
                lhs.m_minor == rhs.m_minor &&
                lhs.m_patch == rhs.m_patch);
        }

        public static bool operator !=(OpenXRApiVersion lhs, OpenXRApiVersion rhs)
        {
            return !(lhs == rhs);
        }

        public static bool operator >(OpenXRApiVersion lhs, OpenXRApiVersion rhs)
        {
            if (lhs is null || rhs is null)
                throw new ArgumentNullException();
            return
                lhs.m_major > rhs.m_major ||
                (lhs.m_major == rhs.m_major && lhs.m_minor > rhs.m_minor) ||
                (lhs.m_major == rhs.m_major && lhs.m_minor == rhs.m_minor && lhs.m_patch > rhs.m_patch);
        }

        public static bool operator <(OpenXRApiVersion lhs, OpenXRApiVersion rhs)
        {
            if (lhs is null || rhs is null)
                throw new ArgumentNullException();
            return
                lhs.m_major < rhs.m_major ||
                (lhs.m_major == rhs.m_major && lhs.m_minor < rhs.m_minor) ||
                (lhs.m_major == rhs.m_major && lhs.m_minor == rhs.m_minor && lhs.m_patch < rhs.m_patch);
        }

        public static bool operator >=(OpenXRApiVersion lhs, OpenXRApiVersion rhs)
        {
            if (lhs is null || rhs is null)
                throw new ArgumentNullException();

            if (lhs.m_major != rhs.m_major) return lhs.m_major > rhs.m_major;
            if (lhs.m_minor != rhs.m_minor) return lhs.m_minor > rhs.m_minor;
            return lhs.m_patch >= rhs.m_patch;
        }

        public static bool operator <=(OpenXRApiVersion lhs, OpenXRApiVersion rhs)
        {
            if (lhs is null || rhs is null)
                throw new ArgumentNullException();

            if (lhs.m_major != rhs.m_major) return lhs.m_major < rhs.m_major;
            if (lhs.m_minor != rhs.m_minor) return lhs.m_minor < rhs.m_minor;
            return lhs.m_patch <= rhs.m_patch;
        }

        public override bool Equals(object obj) => obj is OpenXRApiVersion version && this == version;

        public override int GetHashCode() => HashCode.Combine(Major, Minor, Patch);

        internal static bool TryParse(string customRuntimeLoaderVersion, out OpenXRApiVersion version)
        {
            if (string.IsNullOrEmpty(customRuntimeLoaderVersion))
            {
                version = null;
                return false;
            }

            var tokens = customRuntimeLoaderVersion.Split('.', StringSplitOptions.RemoveEmptyEntries);

            if (tokens.Length != 3)
            {
                version = null;
                return false;
            }

            ushort major, minor;
            uint patch;
            try
            {
                major = ushort.Parse(tokens[0]);
                minor = ushort.Parse(tokens[1]);
                patch = uint.Parse(tokens[2]);
            }
            catch
            {
                version = null;
                return false;
            }

            version = new OpenXRApiVersion(major, minor, patch);
            return true;
        }

        public override string ToString()
        {
            return $"{m_major}.{m_minor}.{m_patch}";
        }
    }
}
