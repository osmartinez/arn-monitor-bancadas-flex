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
    
    public partial class Impresos
    {
        public int ID { get; set; }
        public string NombreFichero { get; set; }
        public int IdAplicacion { get; set; }
    
        public virtual Aplicacion Aplicacion { get; set; }
    }
}
