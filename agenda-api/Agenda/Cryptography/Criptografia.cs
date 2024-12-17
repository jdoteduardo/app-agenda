using System.Security.Cryptography;
using System.Text;

namespace Agenda.Cryptography
{
    public static class Criptografia
    {
        public static string HashPassword(string password)
        {
            // Gerar um salt aleatório
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            // Derivar a chave usando PBKDF2
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            // Combinar o salt e o hash em um único array
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            // Converter para string Base64
            return Convert.ToBase64String(hashBytes);
        }

        public static bool VerifyPassword(string savedPasswordHash, string password)
        {
            // Extrair o array de bytes da string Base64
            byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);

            // Extrair o salt
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            // Derivar a chave usando PBKDF2 com o salt armazenado
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            // Comparar os hash
            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                    return false;
            }
            return true;
        }
    }
}
