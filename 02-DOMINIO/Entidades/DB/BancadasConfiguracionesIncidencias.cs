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
    
    public partial class BancadasConfiguracionesIncidencias
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Habilitada { get; set; }
        public string PinNotificacion1 { get; set; }
        public string PinNotificacion2 { get; set; }
        public string AvisarA { get; set; }
        public bool Corregible { get; set; }
        public int SegundosEjecucion { get; set; }
        public int IdBancada { get; set; }
        public bool Bloqueante { get; set; }
    
        public virtual Bancadas Bancadas { get; set; }
    }
}
