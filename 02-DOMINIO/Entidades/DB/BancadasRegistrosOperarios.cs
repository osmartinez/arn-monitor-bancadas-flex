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
    
    public partial class BancadasRegistrosOperarios
    {
        public int Id { get; set; }
        public int IdOperario { get; set; }
        public int IdBancada { get; set; }
        public System.DateTime FechaEntradaReal { get; set; }
        public Nullable<System.DateTime> FechaSalidaReal { get; set; }
        public Nullable<System.DateTime> FechaEntradaCorregida { get; set; }
        public Nullable<System.DateTime> FechaSalidaCorregida { get; set; }
        public Nullable<System.DateTime> FechaEntradaValidada { get; set; }
        public Nullable<System.DateTime> FechaSalidaValidada { get; set; }
    
        public virtual Bancadas Bancadas { get; set; }
    }
}
