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
    
    public partial class AlmacenSobras
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AlmacenSobras()
        {
            this.AlmacenSobrasUbicaciones = new HashSet<AlmacenSobrasUbicaciones>();
        }
    
        public long ID { get; set; }
        public string CodigoArticulo { get; set; }
        public string Observaciones { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AlmacenSobrasUbicaciones> AlmacenSobrasUbicaciones { get; set; }
    }
}
