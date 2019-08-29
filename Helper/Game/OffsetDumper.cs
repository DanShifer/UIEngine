using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

using UIEngine.API;

namespace UIEngine.Helper.Game
{
    public class OffsetDumper
    {
        #region Params
        private static Dictionary<string, int> GetOffsetDictionary = null;
        private static string UrlOffset;
        #endregion

        public OffsetDumper(string Url) => UrlOffset = Url;

        /// <summary>
        /// Чтение смещений в файле
        /// </summary>
        /// <param name="Section">Секция</param>
        /// <param name="Key">Ключ</param>
        /// <param name="FileInfo">Имя файла</param>
        /// <returns></returns>
        public static int GetReadOffset(string Section, string Key, FileInfo FileInfo)
        {
            StringBuilder Offset = new StringBuilder(255);

            KernelAPI.GetPrivateProfileString(Section, Key, "", Offset, 255, FileInfo.FullName);

            return int.Parse(Offset.ToString());
        }

        /// <summary>
        /// Чтение смещений
        /// </summary>
        /// <param name="Url">Адрес скачивания смещений</param>
        /// <returns></returns>
        public static Dictionary<string, int> GetOffset()
        {
            if (GetOffsetDictionary == null)
            {
                GetOffsetDictionary = new Dictionary<string, int>();

                string GetValueUrl = new WebClient().DownloadString(UrlOffset);

                foreach (var Value in GetValueUrl.Split('\n'))
                {
                    try
                    {
                        GetOffsetDictionary.Add(Value.Split('=')[0].TrimEnd(' '), int.Parse(Value.Split('=')[1].TrimStart(' ')));
                    }
                    catch
                    {
                        continue;
                    }
                }

                return GetOffsetDictionary;
            }
            else
            {
                return GetOffsetDictionary;
            }
        }
    }
}