using JetBrains.Annotations;

namespace TheaterBooking.Web.Utilities
{
    /// <summary>
    ///     Common helper methods for using modified Base62 in applications
    /// </summary>
    public static class Base62
    {
        /// <summary>
        ///     Encodes the specified byte array into its base62 representation, trimming the padding at the end. The resulting
        ///     string contains only A-Z, a-z, and 0-9.
        /// </summary>
        /// <param name="bytes">The array of bytes to encode</param>
        /// <returns>The unpadded base62 string representation</returns>
        public static string Encode([NotNull] byte[] bytes)
            => Base64.Encode(bytes).Replace("A", "^").Replace("/", "AB").Replace("+", "AC").Replace("^", "AA");

        /// <summary>
        ///     Decodes the specified base62 string into the original byte array, adding additional padding at the end of the
        ///     string if necessary.
        /// </summary>
        /// <param name="base62">The base62 string to decode</param>
        /// <returns>The original byte array</returns>
        public static byte[] Decode([NotNull] string base62)
            => Base64.Decode(base62.Replace("AA", "^").Replace("AC", "+").Replace("AB", "/").Replace("^", "A"));
    }
}
