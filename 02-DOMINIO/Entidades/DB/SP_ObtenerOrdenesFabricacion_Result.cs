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
    
    public partial class SP_ObtenerOrdenesFabricacion_Result
    {
        public string Codigo { get; set; }
        public int ID { get; set; }
        public Nullable<int> IdPedido { get; set; }
        public string CodigoArticulo { get; set; }
        public string NombreArticulo { get; set; }
        public Nullable<System.DateTime> FechaServicio { get; set; }
        public Nullable<System.DateTime> FechaSage { get; set; }
        public string Cliente { get; set; }
        public Nullable<short> Orden { get; set; }
        public Nullable<System.Guid> LineasPosicion { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public bool Acabada { get; set; }
        public string Observaciones { get; set; }
        public Nullable<int> Revision { get; set; }
    }
}
