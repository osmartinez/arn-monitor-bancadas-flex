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
    
    public partial class PrePaquetes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PrePaquetes()
        {
            this.PrepaquetesConsumidos = new HashSet<PrepaquetesConsumidos>();
        }
    
        public string CodigoEtiqueta { get; set; }
        public int IdOrdenFabricacion { get; set; }
        public double Cantidad { get; set; }
        public string Talla { get; set; }
        public string CodigoUbicacion { get; set; }
        public Nullable<System.DateTime> FechaAsociacion { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string CodigoAgrupacion { get; set; }
        public Nullable<int> IdEspacioVagon { get; set; }
        public Nullable<int> IdAgrupacionMaquina { get; set; }
        public Nullable<int> IdOrdenFabricacionOperacionTallaCantidad { get; set; }
    
        public virtual OrdenesFabricacion OrdenesFabricacion { get; set; }
        public virtual OrdenesFabricacionOperacionesTallasCantidad OrdenesFabricacionOperacionesTallasCantidad { get; set; }
        public virtual PrePaquetesAgrupaciones PrePaquetesAgrupaciones { get; set; }
        public virtual TrenesVagonesEspacios TrenesVagonesEspacios { get; set; }
        public virtual Ubicaciones Ubicaciones { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PrepaquetesConsumidos> PrepaquetesConsumidos { get; set; }
    }
}
