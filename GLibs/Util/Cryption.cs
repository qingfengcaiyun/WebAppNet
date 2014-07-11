using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Glibs.Util
{
    public class Cryption
    {
        public static string OneWayEncryption(string src, EncryptionFormat encryptionFormat)
        {
            HashAlgorithm hash = null;

            switch (encryptionFormat)
            {
                case EncryptionFormat.MD5: hash = MD5.Create(); break;
                case EncryptionFormat.SHA1: hash = SHA1.Create(); break;
                case EncryptionFormat.SHA256: hash = SHA256.Create(); break;
                case EncryptionFormat.SHA384: hash = SHA384.Create(); break;
                case EncryptionFormat.SHA512: hash = SHA512.Create(); break;
                default: hash = MD5.Create(); break;
            }

            byte[] data = hash.ComputeHash(System.Text.Encoding.Default.GetBytes(src));
            return BitConverter.ToString(data).Replace("-", "").ToLower();
        }

        public static string GetPassword(string src)
        {
            src = OneWayEncryption(src, EncryptionFormat.SHA512);

            StringBuilder s = new StringBuilder();
            int m = 0;
            for (int i = 0, j = src.Length; i < (j / 2); i++)
            {
                m = i % 4;
                switch (m)
                {
                    case 0: s.Append(OneWayEncryption(src.Substring(i, j - i), EncryptionFormat.MD5)); break;
                    case 1: s.Append(OneWayEncryption(src.Substring(i, j - i), EncryptionFormat.SHA1)); break;
                    case 2: s.Append(OneWayEncryption(src.Substring(i, j - i), EncryptionFormat.SHA256)); break;
                    case 3: s.Append(OneWayEncryption(src.Substring(i, j - i), EncryptionFormat.SHA384)); break;
                    default: s.Append(OneWayEncryption(src.Substring(i, j - i), EncryptionFormat.SHA512)); break;
                }
            }

            return OneWayEncryption(s.ToString(), EncryptionFormat.SHA512);
        }
    }

    public enum EncryptionFormat { MD5, SHA1, SHA256, SHA384, SHA512 }
}
