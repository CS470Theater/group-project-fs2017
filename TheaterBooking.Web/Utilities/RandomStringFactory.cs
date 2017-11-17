using System.Security.Cryptography;

namespace TheaterBooking.Web.Utilities
{
    /// <summary>
    ///     Generates random strings using secure random base64 characters, safe for URLs and filenames.
    /// </summary>
    public class RandomStringFactory
    {
        /// <summary>
        ///     Gets or sets the number of characters in the randomly generated strings
        /// </summary>
        /// <remarks>
        ///     The default length is sufficient to allow 820 billion strings to be generated before a greater than 10^-15
        ///     probability of a collision occurs.
        /// </remarks>
        public int Length { get; set; } = 22;

        /// <summary>
        ///     Generates a new random url safe string of the desired length
        /// </summary>
        /// <returns>A new random string</returns>
        public string Generate()
        {
            using (var rnd = new RNGCryptoServiceProvider())
            {
                var buffer = new byte[(Length + 1)*3/4];
                rnd.GetBytes(buffer);
                return Base64.UrlSafeEncode(buffer).Substring(0, Length);
            }
        }
    }
}
