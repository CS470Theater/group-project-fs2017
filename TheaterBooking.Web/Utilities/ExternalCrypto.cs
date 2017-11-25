using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using JetBrains.Annotations;

namespace TheaterBooking.Web.Utilities
{
    /// <summary>
    ///     Provides fast crypto based on AES-256 and HMAC-SHA512, providing about 256 bits of security. Non-derived
    ///     implementations of this class provide a secure way to transport data between users.
    /// </summary>
    /// <remarks>
    ///     Uses AES-256 in Encrypt-then-HMAC. The salt is used in combination with a pepper, secret material, and possibly
    ///     additional entropy to create an AES-256 key. Each value to encrypt is encrypted with a unique IV.
    /// </remarks>
    public class ExternalCrypto
    {
        private readonly byte[] _salt;

        /// <summary>
        ///     Instantiates a new external crypto class, with a new randomly generated salt
        /// </summary>
        public ExternalCrypto()
        {
            _salt = GenerateRandomBytes(16);
        }

        /// <summary>
        ///     Instantiates a new external crypto class using the specified salt
        /// </summary>
        /// <param name="salt">The salt used to derive a unique AES-256 key</param>
        public ExternalCrypto([NotNull] string salt)
        {
            _salt = Base64.UrlSafeDecode(salt);
        }

        /// <summary>
        ///     Gets the salt used to derive this instance's encryption key
        /// </summary>
        [NotNull]
        public string Salt => Base64.UrlSafeEncode(_salt);

        /// <summary>
        ///     Gets a 1024-bit (128 bytes) HMAC key to use when initializing HMAC-SHA-512
        /// </summary>
        [NotNull]
        protected virtual byte[] HmacKey { get; } = Base64.Decode(
            "45ZaCBb9yyyW2VRbs5kTrMUZvTfCnBYcQyC4bchrhSNlogWZb80OLpXo3" +
            "DJW+dHYyB4yfWaByKsz1ABfd05up/0m+2wJPfGKqzZIMlQfOfGtisNO5b" +
            "VIZa0SEaPOG0Y/b+OQGf31veWSJPUwvEWgTpxk3k86l07Q55gjc11KOpU");

        /// <summary>
        ///     Gets the length, in characters (range between 1 and 86) of the HMAC segment of the encrypted value
        /// </summary>
        protected virtual int HmacLength => 86;

        /// <summary>
        ///     Gets the length, in characters, of the AES initialization vector segment of the encrypted value
        /// </summary>
        protected static int IvLength => 22;
        
        /// <summary>
        ///     Securely encrypts the specified value using a session-specific encryption key. Each encryption is done with a
        ///     unique IV, so multiple calls with the same salt never return the same value.
        /// </summary>
        /// <param name="value">The value to encrypt</param>
        /// <returns>The encrypted form of the specified value</returns>
        public string Encrypt([NotNull] string value)
        {
            var iv = GenerateRandomBytes(16);
            using (var aes = CreateAes(iv))
            using (var encryptor = aes.CreateEncryptor())
            {
                var bytes = Encoding.UTF8.GetBytes(value);
                var encrypted = Base64.UrlSafeEncode(iv) + Base64.UrlSafeEncode(encryptor.TransformFinalBlock(bytes, 0, bytes.Length));
                return Hmac(encrypted).Substring(0, HmacLength) + encrypted;
            }
        }

        /// <summary>
        ///     Decrypts the specified value
        /// </summary>
        /// <param name="value">The encrypted value to decrypt</param>
        /// <returns>The decrypted form of the specified value</returns>
        /// <exception cref="UnauthorizedAccessException">if HMAC validation failed</exception>
        public virtual string Decrypt([NotNull] string value)
        {
            if (Hmac(value.Substring(HmacLength)).Substring(0, HmacLength) != value.Substring(0, HmacLength))
            {
                throw new UnauthorizedAccessException("HMAC validation failed");
            }

            using (var aes = CreateAes(Base64.UrlSafeDecode(value.Substring(HmacLength, IvLength))))
            using (var decryptor = aes.CreateDecryptor())
            {
                var encrypted = Base64.UrlSafeDecode(value.Substring(HmacLength + IvLength));
                return Encoding.UTF8.GetString(decryptor.TransformFinalBlock(encrypted, 0, encrypted.Length));
            }
        }

        /// <summary>
        ///     Securely encrypts the specified value using a session-specific encryption key. Each encryption is done with a
        ///     unique IV, so multiple calls with the same salt never return the same value.
        /// </summary>
        /// <param name="value">The value to encrypt</param>
        /// <returns>The encrypted form of the specified value</returns>
        public byte[] Encrypt([NotNull] byte[] value)
        {
            var iv = GenerateRandomBytes(16);
            using (var aes = CreateAes(iv))
            using (var encryptor = aes.CreateEncryptor())
            using (var memory = new MemoryStream())
            {
                var encrypted = encryptor.TransformFinalBlock(value, 0, value.Length);
                memory.Write(iv, 0, iv.Length);
                memory.Write(encrypted, 0, encrypted.Length);

                var hmac = Hmac(memory.ToArray());
                memory.Write(hmac, 0, hmac.Length);
                return memory.ToArray();
            }
        }

        /// <summary>
        ///     Decrypts the specified value
        /// </summary>
        /// <param name="value">The encrypted value to decrypt</param>
        /// <returns>The decrypted form of the specified value</returns>
        /// <exception cref="UnauthorizedAccessException">if HMAC validation failed</exception>
        public byte[] Decrypt([NotNull] byte[] value)
        {
            byte[] payload;
            using (var memory = new MemoryStream(value, false))
            using (var reader = new BinaryReader(memory))
            {
                payload = reader.ReadBytes(value.Length - 64);
                var hmac = reader.ReadBytes(64);
                if (!Hmac(payload).SequenceEqual(hmac))
                {
                    throw new UnauthorizedAccessException("HMAC validation failed");
                }
            }

            using (var memory = new MemoryStream(payload, false))
            using (var reader = new BinaryReader(memory))
            using (var aes = CreateAes(reader.ReadBytes(16)))
            using (var decryptor = aes.CreateDecryptor())
            {
                var encrypted = reader.ReadBytes(payload.Length - 16);
                return decryptor.TransformFinalBlock(encrypted, 0, encrypted.Length);
            }
        }

        /// <summary>
        ///     Generates a series of cryptographically secure random bytes
        /// </summary>
        /// <param name="bytes">The number of bytes to produce</param>
        /// <returns>A new array of random bytes</returns>
        private static byte[] GenerateRandomBytes(int bytes)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var buffer = new byte[bytes];
                rng.GetBytes(buffer);
                return buffer;
            }
        }

        /// <summary>
        ///     Calculates an HMAC authentication code over the given encrypted value
        /// </summary>
        /// <param name="value">The value to authenticate</param>
        /// <returns>The HMAC authentication code</returns>
        private string Hmac([NotNull] string value) => Base64.UrlSafeEncode(Hmac(Encoding.UTF8.GetBytes(value)));

        /// <summary>
        ///     Calculates an HMAC authentication code over the given encrypted value
        /// </summary>
        /// <param name="value">The value to authenticate</param>
        /// <returns>The HMAC authentication code</returns>
        public byte[] Hmac([NotNull] byte[] value)
        {
            using (var hmac = new HMACSHA512(HmacKey))
            {
                hmac.TransformBlock(value, 0, value.Length, null, 0);
                hmac.TransformFinalBlock(_salt, 0, _salt.Length);
                return hmac.Hash;
            }
        }

        /// <summary>
        ///     Gets an additional random byte array to include when calculating an AES key
        /// </summary>
        /// <returns>A byte array with additional random material to combine with salt and pepper</returns>
        [NotNull]
        protected virtual byte[] GetAdditionalAesKeyEntropy() => Base64.Decode("DZjbMG9CdjVAbVfugdsW0i4Iq74jYuAmbuckmK9iXb8=");

        /// <summary>
        ///     Creates an AES implementation instance with the specified initialization vector
        /// </summary>
        /// <param name="iv">The 16-byte AES initialization vector</param>
        /// <returns>A new instance of an AES implementation</returns>
        private Aes CreateAes([NotNull] byte[] iv)
        {
            using (var sha256 = SHA256.Create())
            {
                var keyEntropy = GetAdditionalAesKeyEntropy();
                sha256.TransformBlock(keyEntropy, 0, keyEntropy.Length, null, 0);
                sha256.TransformFinalBlock(_salt, 0, _salt.Length);

                var aes = Aes.Create() ?? new AesCryptoServiceProvider();
                aes.Key = sha256.Hash;
                aes.IV = iv;
                return aes;
            }
        }
    }
}
