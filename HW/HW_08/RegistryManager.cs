using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_08
{
    public static class RegistryManager
    {
        public readonly static string keyPath = @"Software\MyProgram";
        public static T ReadRegistryValue<T>(string valueName, T defaultValue)
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(keyPath))
            {
                if (key != null)
                {
                    object value = key.GetValue(valueName);
                    if (value != null)
                    {
                        return (T)Convert.ChangeType(value, typeof(T));
                    }
                }
            }
            return defaultValue;
        }

        public static void WriteRegistryValue<T>(string valueName, T value)
        {
            using (RegistryKey key = Registry.CurrentUser.CreateSubKey(keyPath))
            {
                if (key != null)
                {
                    key.SetValue(valueName, value);
                }
            }
        }
    }
}
