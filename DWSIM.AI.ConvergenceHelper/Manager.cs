using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWSIM.AI.ConvergenceHelper
{
    public class Manager
    {

        public static ConvergenceHelper Instance = new ConvergenceHelper();

        public static bool Enabled = false;

        public static bool ProvideSolutionOnError = false;

        public static string HomeDirectory = Path.Combine(GlobalSettings.Settings.GetConfigFileDir(), "ConvergenceHelper");

        public static void Initialize()
        { 
            if (!Directory.Exists(HomeDirectory)) { Directory.CreateDirectory(HomeDirectory); }
            var modelsdir = Path.Combine(HomeDirectory, "models");
            if (!Directory.Exists(modelsdir)) { Directory.CreateDirectory(modelsdir); }
            var configdir = Path.Combine(HomeDirectory, "config");
            if (!Directory.Exists(configdir)) { Directory.CreateDirectory(configdir); }
            LoadSettings();
        }

        public static void LoadSettings() {
            var configfile = Path.Combine(HomeDirectory, "config", "settings.json");
            if (File.Exists(configfile)) { 
            
            }
        }

        public static void SaveSettings() { 
        
        }

    }
}
