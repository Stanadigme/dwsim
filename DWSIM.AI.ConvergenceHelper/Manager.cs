using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DWSIM.FileStorage;

namespace DWSIM.AI.ConvergenceHelper
{
    public class Manager
    {

        public static ConvergenceHelper Instance = new ConvergenceHelper();

        public static FileDatabaseProvider Database = new FileDatabaseProvider();

        public static bool Enabled = false;

        public static bool ProvideSolutionOnError = false;

        public static string HomeDirectory = Path.Combine(GlobalSettings.Settings.GetConfigFileDir(), "ConvergenceHelper");

        public static void Initialize()
        { 
            if (!Directory.Exists(HomeDirectory)) { Directory.CreateDirectory(HomeDirectory); }
            var datadir = Path.Combine(HomeDirectory, "data");
            if (!Directory.Exists(datadir)) { Directory.CreateDirectory(datadir); }
            var modelsdir = Path.Combine(HomeDirectory, "models");
            if (!Directory.Exists(modelsdir)) { Directory.CreateDirectory(modelsdir); }
            var configdir = Path.Combine(HomeDirectory, "config");
            if (!Directory.Exists(configdir)) { Directory.CreateDirectory(configdir); }
            LoadSettings();

            var dbfile = Path.Combine(datadir, "data.db");
            if (!File.Exists(dbfile)) {
                Database.CreateDatabase();
                Database.GetDatabaseObject().GetCollection<ConvergenceHelperTrainingData>("TrainingData");
                Database.ExportDatabase(dbfile);
            }
            else
                Database.LoadDatabase(dbfile);
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
