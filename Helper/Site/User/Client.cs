using System;
using System.IO;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;

namespace UIEngine.Helper.Site.User
{
    public static class Client
    {
        /// <summary>
        /// Получение MD5
        /// </summary>
        /// <param name="Data">Данные для криптования</param>
        /// <returns></returns>
        public static string GetMD5(this string Data)
        {
            string Hash = "";

            foreach (byte Byte in new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(Data)))
            {
                Hash += $"{Byte:x2}";
            }

            return Hash.ToUpper();
        }

        /// <summary>
        /// Получение MD5 приложения
        /// </summary>
        /// <param name="Path">Путь до приложения</param>
        /// <returns></returns>
        public static string GetAppMD5(string Path)
        {
            using (FileStream FileStream = File.OpenRead(Path))
            {
                MD5 MD5 = new MD5CryptoServiceProvider();

                byte[] FileData = new byte[FileStream.Length];

                FileStream.Read(FileData, 0, (int)FileStream.Length);

                byte[] HashSum = MD5.ComputeHash(FileData);

                return BitConverter.ToString(HashSum).Replace('-', '\0');
            }
        }

        /// <summary>
        /// Получение ID процессора
        /// </summary>
        /// <returns></returns>
        public static string GetProcessorId() => (from Processor in new ManagementObjectSearcher("SELECT * FROM Win32_Processor").Get().OfType<ManagementBaseObject>() select Processor.GetPropertyValue("ProcessorId")).First().ToString();

        /// <summary>
        /// Получение операционной системы
        /// </summary>
        /// <returns></returns>
        public static string GetOperatingSystem() => (from OperatingSystem in new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem").Get().OfType<ManagementObject>() select OperatingSystem.GetPropertyValue("Caption")).First().ToString();

        /// <summary>
        /// Генерация уникального ID компьютера
        /// </summary>
        /// <returns></returns>
        public static string GetComputerID()
        {
            StringBuilder ComputerID = new StringBuilder();

            foreach (ManagementObject queryObj in new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor").Get())
            {
                ComputerID.Append(queryObj["ProcessorId"]);
            }

            foreach (ManagementObject queryObj in new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_VideoController").Get())
            {
                ComputerID.Append(queryObj["AdapterRAM"]);
            }

            foreach (ManagementObject queryObj in new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_DiskDrive").Get())
            {
                ComputerID.Append(queryObj["Size"]);
            }

            return ComputerID.ToString();
        }
    }
}