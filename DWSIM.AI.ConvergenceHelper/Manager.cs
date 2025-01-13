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
        
        }

        public static void SetupDirectories()
        { 
        
        }

        public static void LoadSettings() { 
        
        }

        public static void SaveSettings() { 
        
        }

    }
}
