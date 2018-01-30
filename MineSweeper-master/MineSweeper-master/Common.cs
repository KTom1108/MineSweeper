using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Windows.Media;

namespace MineSweeper
{
    public class CommonCode
    {
        public static string REGKEY_LEVEL = "CLASSVALUE";
        public static string REGKEY_LEVELVALUE_LOW = "LOW";
        public static string REGKEY_LEVELVALUE_MIDDLE = "MIDDLE";
        public static string REGKEY_LEVELVALUE_HIGH = "HIGH";

        public static Dictionary<int, SolidColorBrush> numColorDi = new Dictionary<int, SolidColorBrush>() {
            {0, Brushes.Black},
            {1, Brushes.Blue},
            {2, Brushes.Green},
            {3, Brushes.Orange},
            {4, Brushes.Brown},
            {5, Brushes.Pink},
            {6, Brushes.Purple},
            {7, Brushes.Gray},
            {8, Brushes.Gold}
        };
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

        public static T GetNumByColorKey<T>(int numValue, Dictionary<int, T> numColorDi)
        {
            return numColorDi.Where(x => x.Key == numValue).Select(x => x.Value).FirstOrDefault();
        }
    }

}
