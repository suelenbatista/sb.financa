using System;
using System.Security.Cryptography;
using System.Text;

namespace SB.Financa.Generics.Seguranca
{
    public static class Criptografia 
    {
        public static string ObterHashMd5(string texto)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(texto));

                StringBuilder sbHash = new StringBuilder();

                for (int i = 0; i < data.Length; i++)
                {
                    sbHash.Append(data[i].ToString("x2"));
                }

                return sbHash.ToString();
            }
        }

        public static bool CompararHashMd5(string texto_original, string hashMd5)
        {
            string novoHash = ObterHashMd5(texto_original);

            StringComparer comp = StringComparer.OrdinalIgnoreCase;

            if (comp.Compare(novoHash, hashMd5) == 0) {
                return true;
            }
            else {
                return false;
            }
        }
    }
}
