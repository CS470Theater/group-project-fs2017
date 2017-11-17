using System;
using JetBrains.Annotations;

namespace TheaterBooking.Web.Utilities
{
    /// <summary>
    ///     Common helper methods for using Base64 in applications
    /// </summary>
    public static class Base64
    {
        /// <summary>
        ///     Encodes the specified byte array into its base64 representation using the url-safe alphabet in RFC 4648, while also
        ///     trimming the padding at the end
        /// </summary>
        /// <param name="bytes">The array of bytes to encode</param>
        /// <returns>The equivalent base64 string representation in RFC 4648</returns>
        public static string UrlSafeEncode([NotNull] byte[] bytes) => Encode(bytes).Replace("+", "-").Replace("/", "_");

        /// <summary>
        ///     Decodes the specified base64 string into the original byte array using the url-safe alphabet in RFC 4648, adding
        ///     additional padding at the end of the string if necessary.
        /// </summary>
        /// <param name="base64">The base64 string to decode</param>
        /// <returns>The original byte array</returns>
        public static byte[] UrlSafeDecode([NotNull] string base64) => Decode(base64.Replace("-", "+").Replace("_", "/"));

        /// <summary>
        ///     Encodes the specified byte array into its base64 representation, trimming the padding at the end. The resulting
        ///     string is not compatible with Convert.FromBase64String.
        /// </summary>
        /// <param name="bytes">The array of bytes to encode</param>
        /// <returns>The unpadded base64 string representation</returns>
        public static string Encode([NotNull] byte[] bytes) => Convert.ToBase64String(bytes).TrimEnd('=');

        /// <summary>
        ///     Decodes the specified base64 string into the original byte array, adding additional padding at the end of the
        ///     string if necessary.
        /// </summary>
        /// <param name="base64">The base64 string to decode</param>
        /// <returns>The original byte array</returns>
        public static byte[] Decode([NotNull] string base64) => Convert.FromBase64String(Pad(base64));

        /// <summary>
        ///     Removes new line characters and spaces from the specified text.
        /// </summary>
        /// <param name="text">The text to clean</param>
        /// <returns>The cleaned up text</returns>
        private static string Clean([NotNull] string text) => text.Replace("\r\n", "").Replace(" ", "");

        /// <summary>
        ///     Pads the specified base64 string with the appropriate number of = sign padding
        /// </summary>
        /// <param name="base64">The base64 string to pad</param>
        /// <returns>The padded base64 string</returns>
        private static string Pad([NotNull] string base64) => base64 + new string('=', (4 - Clean(base64).Length % 4) % 4);

    }
}
