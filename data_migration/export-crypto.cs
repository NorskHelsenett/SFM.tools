using System;
using System.Security.Cryptography;
using System.IO;
using System.Text;

namespace Crypto
{
    public class EncryptionKeyNotFoundOrMissing : Exception
    {
        public override string Message => "No encryption key has been provided for encryption";
    }

    public class NoWriteAccessForCryptoManger : Exception
    {
        public override string Message => "Crypto manager unable to write to supplied stream";
    }

    public class StreamNotAtBeginningForCryptoManager : Exception
    {
        public override string Message => "Crypto manager needs the entire stream to encrypt reliably, i.e. to be at start of stream (think open file stream in truncate mode)";
    }

    public class NoReadAccessForCryptoManager : Exception
    {
        public override string Message => "Crypt manager needs read access to input stream to decrypt";
    }

    public class NoCryptoGraphicObjectCreated : Exception
    {
        public override string Message => "An unexpected error when calling Aes.Create() returns null, should not happen";
    }

    public class EncryptedStreamIncompatibleForCryptoManager : Exception
    {
        public override string Message => "Cant locate encrypted key in encrypted stream";
    }

    public interface IEncryptionKeys
    {
        /// <summary>
        /// Gets or sets the encryption key to encrypt with symmetric key with
        /// </summary>
        RSA EncryptionKey { get; set; }

        /// <summary>
        /// Creates an AES cryptographic object for encryption
        /// </summary>
        Aes CreateCryptographicObject();

        /// <summary>
        /// Method to encrypt asymmetric key to store in encrypted output
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        byte[] EncryptKey(byte[] key);
    }

    public class EncryptionKeys : IEncryptionKeys
    {
        // Padding scheme for encrypting symmetric key
        private static readonly RSAEncryptionPadding rsaPadding = RSAEncryptionPadding.OaepSHA512;
        // Key size
        private const int KeySize = 256;
        // BlockSize
        private const int BlockSize = 128;
        // Padding scheme for data length
        private const PaddingMode PaddingMode = System.Security.Cryptography.PaddingMode.PKCS7;
        // block chaining scheme
        private const CipherMode CipherMode = System.Security.Cryptography.CipherMode.CBC;

        public RSA EncryptionKey { get; set; }

        // returns a managed Aes object
        public Aes CreateCryptographicObject()
        {
            Aes ret = null;

            if (EncryptionKey != null)
            {
                ret = new AesManaged
                      {
                          KeySize = KeySize, 
                          BlockSize = BlockSize, 
                          Mode = CipherMode, 
                          Padding = PaddingMode
                      };
            }

            return ret;
        }

        public byte[] EncryptKey(byte[] key)
        {
            if (EncryptionKey != null)
            {
                return EncryptionKey.Encrypt(key, rsaPadding);
            }

            throw new EncryptionKeyNotFoundOrMissing();
        }
    }

    /// <summary>
    /// Cryptographic manager to encrypt files
    /// Based on MS walk-through: https://docs.microsoft.com/en-us/dotnet/standard/security/walkthrough-creating-a-cryptographic-application 
    /// </summary>
    public class CryptoManager : CryptoStream
    {
        // length info size
        private const int LenInfoSize = 4;

        private CryptoManager(Stream stream, ICryptoTransform transform, CryptoStreamMode mode, IDisposable algorithm) : base(stream, transform, mode)
        {
            Transform = transform;
            Algorithm = algorithm;
        }

        public IDisposable Algorithm { get; }
        public ICryptoTransform Transform { get; }

        public static CryptoStream PrepareAndReturnAesEncryptionStream(IEncryptionKeys encryptionKeys, Stream stream, string fileExtension)
        {
            var extension = Encoding.ASCII.GetBytes(Convert.ToBase64String(Encoding.UTF8.GetBytes(!string.IsNullOrEmpty(fileExtension) ? fileExtension : "zip")));
            if (encryptionKeys?.EncryptionKey != null)
            {
                var algorithm = encryptionKeys.CreateCryptographicObject();
                if (algorithm != null)
                {
                    if (stream.CanWrite)
                    {
                        if (stream.Position == 0)
                        {
                            // Generate local key
                            // encrypt key using the supplied RSA key
                            // write key and IV info to start of supplied stream
                            // Set up symmetric encryptor
                            algorithm.GenerateKey();
                            algorithm.GenerateIV();
                            var transform = algorithm.CreateEncryptor();

                            // Use RSA service provider to encrypt symmetric key
                            var encryptedKey = encryptionKeys.EncryptKey(algorithm.Key);

                            // Create byte arrays to contain length values
                            // to correctly read up values such as key etc. when decrypting
                            int lKey = encryptedKey.Length;
                            var lenK = CopyMaxFirstBytes(BitConverter.GetBytes(lKey), LenInfoSize);
                            int lIv = algorithm.IV.Length;
                            var lenIv = CopyMaxFirstBytes(BitConverter.GetBytes(lIv), LenInfoSize);
                            int lExt = extension.Length;
                            var lenExtension = CopyMaxFirstBytes(BitConverter.GetBytes(lExt), LenInfoSize);

                            // Write length info to start of stream
                            stream.Write(lenK, 0, LenInfoSize);
                            stream.Write(lenIv, 0, LenInfoSize);
                            stream.Write(lenExtension, 0, LenInfoSize);

                            // Write key values required for decrypting which fit the size data
                            stream.Write(encryptedKey, 0, lKey);
                            stream.Write(algorithm.IV, 0, lIv);
                            stream.Write(extension, 0, lExt);

                            return new CryptoManager(stream, transform, CryptoStreamMode.Write, algorithm);
                        }

                        throw new StreamNotAtBeginningForCryptoManager();
                    }
                    throw new NoWriteAccessForCryptoManger();
                }
                throw  new NoCryptoGraphicObjectCreated();
            }

            throw new EncryptionKeyNotFoundOrMissing();
        }

        /// <summary>
        /// Copies max give bytes to return array
        /// </summary>
        /// <param name="toCopy">array to read from</param>
        /// <param name="length">maximum bytes to read</param>
        /// <returns></returns>
        private static byte[] CopyMaxFirstBytes(byte[] toCopy, int length)
        {
            var ret = new byte[length];
            for(var idx = 0; idx < length && idx < toCopy.Length; idx++)
            {
                ret[idx] = toCopy[idx];
            }

            return ret;
        }
    }
}
