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
    
    public partial class OrdenesFabricacionObservaciones
    {
        public int Id { get; set; }
        public int IdOrdenFabricacion { get; set; }
        public string Usuario { get; set; }
        public string Observacion { get; set; }
    
        public virtual OrdenesFabricacion OrdenesFabricacion { get; set; }
    }
}
