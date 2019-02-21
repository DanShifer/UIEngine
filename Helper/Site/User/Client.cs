using System.Management;
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
        public static string GetMD5(string Data)
        {
            string Hash = "";

            foreach (byte Byte in new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(Data)))
            {
                Hash += $"{Byte:x2}";
            }
        
            return Hash.ToUpper();
        }

        public static string GenerateComputerID()
        {
            //Creates the HWID from the ProcessorID, Video Controller RAM and the size of the disk drive ; you can change this of course
            StringBuilder computerID = new StringBuilder();
            ManagementObjectSearcher searcher;

            searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor");
            foreach (ManagementObject queryObj in searcher.Get())
            {
                computerID.Append(queryObj["ProcessorId"]);
            }

            searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_VideoController");
            foreach (ManagementObject queryObj in searcher.Get())
            {
                computerID.Append(queryObj["AdapterRAM"]);
            }

            searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_DiskDrive");
            foreach (ManagementObject queryObj in searcher.Get())
            {
                computerID.Append(queryObj["Size"]);
            }

            return computerID.ToString();
        }
    }
}