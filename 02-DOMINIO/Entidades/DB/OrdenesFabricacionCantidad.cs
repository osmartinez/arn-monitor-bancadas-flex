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
    
    public partial class OrdenesFabricacionCantidad
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OrdenesFabricacionCantidad()
        {
            this.TicketsCantidad = new HashSet<TicketsCantidad>();
        }
    
        public int ID { get; set; }
        public int OrdenFabricacionID { get; set; }
        public string Talla { get; set; }
        public double Cantidad { get; set; }
        public double Saldos { get; set; }
    
        public virtual OrdenesFabricacion OrdenesFabricacion { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TicketsCantidad> TicketsCantidad { get; set; }
    }
}
