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
    
    public partial class SP_ListadoControlProductoTerminadoPendienteEnvio_Result
    {
        public string CodigoArticulo { get; set; }
        public string Descripcion { get; set; }
        public string NombreCliente { get; set; }
        public Nullable<System.DateTime> Fecha_creacion_minima { get; set; }
        public string Ubicacion { get; set; }
        public Nullable<double> Cantidad_total { get; set; }
        public Nullable<int> Bultos { get; set; }
        public int IdTipoStock { get; set; }
    }
}
