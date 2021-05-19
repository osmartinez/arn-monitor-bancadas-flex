using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Local
{
    public class ConfiguracionGlobal
    {
        public Guid IdConfiguracionActiva { get; set; }
        public string Modo { get; set; } = "pegado";
        public List<ConfiguracionLayout> Configuraciones { get; set; } = new List<ConfiguracionLayout>();
        public static ConfiguracionGlobal Default
        {
            get
            {
                Guid guidConfig = Guid.NewGuid();
                return new ConfiguracionGlobal
                {
                    IdConfiguracionActiva = guidConfig,
                    Modo = "- NUEVA -",
                    Configuraciones = new List<ConfiguracionLayout>
                    {
                        new ConfiguracionLayout
                        {
                            Id = guidConfig,
                            Pantallas = new List<Pantalla>{ new Pantalla { Id = Guid.NewGuid(), Maquinas = new List<DB.Maquinas>()} },
                        }
                    }
                };
            }
        }

        [JsonIgnore]
        public ConfiguracionLayout ConfiguracionLayoutActiva
        {
            get
            {
                return Configuraciones.FirstOrDefault(x => x.Id == IdConfiguracionActiva);
            }
        }
    }
}
