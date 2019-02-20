using System.Security.Cryptography;
using System.Text;

namespace UIEngine.Helper.Site.User
{
    public class Client
    {
        /// <summary>
        /// Получение MD5
        /// </summary>
        /// <param name="Data">Данные для криптования</param>
        /// <returns></returns>
        private static string GetMD5(string Data)
        {
            string Hash = "";

            foreach (byte Byte in new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(Data)))
            {
                Hash += $"{Byte:x2}";
            }
        
            return Hash.ToUpper();
        }
    }
}