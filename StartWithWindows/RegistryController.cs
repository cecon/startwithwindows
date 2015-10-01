using System.Windows.Forms;
using Microsoft.Win32;

namespace StartWithWindows
{
    public static class RegistryController
    {
        const string RunKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
        /// <summary>
        /// Adicionar/Remover entradas do windows startup
        /// </summary>
        /// <param name="appName">Nome da aplicação.</param>
        /// <param name="enable">habilitar/desabilitar a entrada</param>
        public static void SetStartup(string appName, bool enable)
        {
            if (enable)
            {
                if (Exists(appName)) return;
                SetRegistry(appName);
            }
            else
            {
                // remove startup
                RemoveRegistry(appName);
            }
        }

        public static void SetRegistry(string appName, string key = RunKey)
        {
            using (var startupKey = Registry.LocalMachine.OpenSubKey(key, true))
            {
                if (startupKey == null) return;
                startupKey.SetValue(appName, Application.ExecutablePath);

            }
        }
        public static void RemoveRegistry(string appName, string key = RunKey)
        {
            using (var startupKey = Registry.LocalMachine.OpenSubKey(key, true))
            {
                if (startupKey == null) return;
                startupKey.DeleteValue(appName, false);
            }
        }

        public static bool Exists(string appName, string key = RunKey)
        {
            using (var startupKey = Registry.LocalMachine.OpenSubKey(key))
            {
                if (startupKey == null) return false;
                return (startupKey.GetValue(appName) != null);
            }
        }
    }
}