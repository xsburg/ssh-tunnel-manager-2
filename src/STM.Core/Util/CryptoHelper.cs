// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   CryptoHelper.cs
// </summary>
// ***********************************************************************

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace STM.Core.Util
{
    /// <summary>
    ///     A helper class for the System.Security.Cryptography algorithms presenting
    ///     a simple interface to encrypt and decrypt the provided data with a password.
    ///     The Rijndael algorithm with 256 bits key and 128 bits block size is used.
    ///     The encryption key is created with password and salt, as Rfc 2898 tells us to do.
    /// </summary>
    public class CryptoHelper
    {
        private static readonly byte[] Salt = Encoding.ASCII.GetBytes("o6816642kbM7c5");

        public static void DecryptAes(Stream inStream, Stream outStream, string password)
        {
            if (inStream == null)
            {
                throw new ArgumentNullException("inStream");
            }
            if (outStream == null)
            {
                throw new ArgumentNullException("outStream");
            }
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }

            // generate an encryption key with the shared secret and salt
            using (var key = new Rfc2898DeriveBytes(password, Salt))
            {
                using (var aesAlg = new RijndaelManaged())
                {
                    aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);
                    aesAlg.IV = key.GetBytes(aesAlg.BlockSize / 8);

                    using (var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV))
                    {
                        using (var csEncrypt = new CryptoStream(inStream, decryptor, CryptoStreamMode.Read))
                        {
                            csEncrypt.CopyTo(outStream);
                        }
                    }
                }
            }
        }

        public static string DecryptStringAes(string base64String, string password)
        {
            var bytes = Convert.FromBase64String(base64String);
            var inStream = new MemoryStream();
            inStream.Write(bytes, 0, bytes.Length);
            inStream.Seek(0, SeekOrigin.Begin);

            var outStream = new MemoryStream();

            DecryptAes(inStream, outStream, password);

            var result = Encoding.UTF8.GetString(outStream.ToArray());
            return result;
        }

        public static void EncryptAes(Stream inStream, Stream outStream, string password)
        {
            if (inStream == null)
            {
                throw new ArgumentNullException("inStream");
            }
            if (outStream == null)
            {
                throw new ArgumentNullException("outStream");
            }
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }

            // generate an encryption key with the shared secret and salt
            using (var key = new Rfc2898DeriveBytes(password, Salt))
            {
                using (var aesAlg = new RijndaelManaged())
                {
                    aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);
                    aesAlg.IV = key.GetBytes(aesAlg.BlockSize / 8);

                    using (var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV))
                    {
                        using (var csEncrypt = new CryptoStream(outStream, encryptor, CryptoStreamMode.Write))
                        {
                            inStream.CopyTo(csEncrypt);
                        }
                    }
                }
            }
        }

        public static string EncryptStringAes(string text, string password)
        {
            var inStream = ToMemoryStream(text);
            var outStream = new MemoryStream();

            EncryptAes(inStream, outStream, password);

            var result = Convert.ToBase64String(outStream.ToArray());
            return result;
        }

        private static MemoryStream ToMemoryStream(string source)
        {
            return ToMemoryStream(source, Encoding.UTF8);
        }

        private static MemoryStream ToMemoryStream(string source, Encoding encoding)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (encoding == null)
            {
                throw new ArgumentNullException("encoding");
            }

            var bytes = encoding.GetBytes(source);
            var stream = new MemoryStream();
            stream.Write(bytes, 0, bytes.Length);
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }
    }
}
