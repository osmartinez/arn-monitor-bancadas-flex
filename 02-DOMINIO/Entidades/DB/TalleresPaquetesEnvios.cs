//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Entidades.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class TalleresPaquetesEnvios
    {
        public int Id { get; set; }
        public int IdTaller { get; set; }
        public int IdOrdenFabricacion { get; set; }
        public int IdOrdenFabricacionOperacion { get; set; }
        public string Talla { get; set; }
        public double Cantidad { get; set; }
        public System.DateTime FechaHoraPreparado { get; set; }
        public Nullable<System.DateTime> FechaHoraEnviado { get; set; }
        public string CodigoEtiqueta { get; set; }
    
        public virtual OrdenesFabricacion OrdenesFabricacion { get; set; }
        public virtual OrdenesFabricacionOperaciones OrdenesFabricacionOperaciones { get; set; }
        public virtual Talleres Talleres { get; set; }
    }
}
