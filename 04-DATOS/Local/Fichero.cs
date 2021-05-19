using Entidades.Local;
using JsonResolvers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Local
{
    public static  class Fichero
    {
        private static string ruta = Path.Combine(Environment.GetFolderPath(
Environment.SpecialFolder.ApplicationData), "config_monitor_flex.json");

        private static JsonSerializerSettings settings = new JsonSerializerSettings
        {
             ContractResolver = new CustomResolver(),
            PreserveReferencesHandling = PreserveReferencesHandling.None,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            Formatting = Formatting.Indented
        };

        public static void EscribirConfiguracion(ConfiguracionGlobal cfg)
        {
            string json = JsonConvert.SerializeObject(cfg, settings);
            File.WriteAllText(ruta, json);
        }
        public static ConfiguracionGlobal LeerConfiguracion()
        {
            if (!File.Exists(ruta))
            {
                ConfiguracionGlobal cfg = ConfiguracionGlobal.Default;

                string json = JsonConvert.SerializeObject(cfg, settings);
                File.WriteAllText(ruta, json);
                return cfg;
            }
            else
            {
                string json = File.ReadAllText(ruta);
                ConfiguracionGlobal cfg = JsonConvert.DeserializeObject<ConfiguracionGlobal>(json);

                if (cfg == null)
                {
                    cfg = ConfiguracionGlobal.Default;
                    EscribirConfiguracion(cfg);
                }

                return cfg;
            }
        }
    }
}
