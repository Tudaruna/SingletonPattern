using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLib
{
    public class SettingsManager
    {
        private static SettingsManager instance;
        private Dictionary<string, string> settings;

        private SettingsManager()
        {
            settings = new Dictionary<string, string>();
            LoadSettings();
        }

        public static SettingsManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SettingsManager();
                }
                return instance;
            }
        }

        public string GetSetting(string key)
        {
            if (settings.ContainsKey(key))
            {
                return settings[key];
            }
            return null;
        }

        public void LoadSettings()
        {
            settings["language"] = "en";
            settings["theme"] = "light";
        }
    }

}
