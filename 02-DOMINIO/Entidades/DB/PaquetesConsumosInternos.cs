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
    
    public partial class PaquetesConsumosInternos
    {
        public long ID { get; set; }
        public long IdPaquete { get; set; }
        public decimal CantidadConsumida { get; set; }
        public string Observaciones { get; set; }
        public System.DateTime FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public Nullable<System.DateTime> FechaRetorno { get; set; }
        public Nullable<decimal> CantidadRetorno { get; set; }
        public Nullable<int> IdOperacion { get; set; }
    
        public virtual OrdenesFabricacionOperaciones OrdenesFabricacionOperaciones { get; set; }
        public virtual Paquetes Paquetes { get; set; }
    }
}
