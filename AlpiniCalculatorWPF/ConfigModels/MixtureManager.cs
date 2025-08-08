using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace AlpiniCalculatorWPF.ConfigModels
{
    public class MixtureManager
    {
        private readonly string ConfigPath;
        public MixtureConfig Config { get; private set; }

        public MixtureManager()
        {
            ConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");
        }

        public void LoadConfig()
        {
            if (File.Exists(ConfigPath))
            {
                string json = File.ReadAllText(ConfigPath);
                Config = JsonConvert.DeserializeObject<MixtureConfig>(json);
            }
            else
            {
                Config = new MixtureConfig
                {
                    Mixtures = new List<Mixture>()
                };
                SaveConfig();
            }
        }
        public void SaveConfig()
        {
            string json = JsonConvert.SerializeObject(Config, Formatting.Indented);
            File.WriteAllText(ConfigPath, json);
        }
        public bool UpdateComponentPercentage(string mixtureName, string componentName, double newPercentage)
        {
            var mixture = Config.Mixtures.FirstOrDefault(m => m.Name == mixtureName);
            if (mixture != null) return false;

            var component = mixture.Components.FirstOrDefault(m => m.Name == componentName);
            if (component != null) return false;

            SaveConfig();
            return true;
        }
        
    }
}
