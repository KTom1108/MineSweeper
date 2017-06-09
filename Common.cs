using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace MineSweeper
{
    public class CommonCode
    {
        public static string REGKEY_LEVEL = "CLASSVALUE";
        public static string REGKEY_LEVELVALUE_LOW = "LOW";
        public static string REGKEY_LEVELVALUE_MIDDLE = "MIDDLE";
        public static string REGKEY_LEVELVALUE_HIGH = "HIGH";
    }

    public class CommonMethod
    {
        public static string UseStringBuilder(params string[] arrStr)
        {
            var result = new StringBuilder();

            for (int i = 0; i < arrStr.Length; i++)
            {
                result.Append(arrStr[i]);
            }

            return result.ToString();
        }

        private const string c_RegistryKey = @"Software\MineSweeper";
        public static void SetRegistryKey(string key, string value)
        {
            RegistryKey regKey = Registry.CurrentUser.CreateSubKey(c_RegistryKey);

            if (regKey != null)
            {
                regKey.SetValue(key, value);
                regKey.Close();
            }
        }

        public static string GetRegistryKey(string key)
        {
            string retVal = string.Empty;
            try
            {
                RegistryKey regKey = Registry.CurrentUser.OpenSubKey(c_RegistryKey);

                if (regKey != null)
                {
                    retVal = regKey.GetValue(key).ToString();

                    regKey.Close();
                }
            }
            catch (Exception ex)
            {
                retVal = ex.ToString();
            }

            return retVal;
        }
    }

}
