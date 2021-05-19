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
    
    public partial class SP_ObtenerOrdenesFabricacionPendientes_Result
    {
        public string OrdenFabricacion { get; set; }
        public string Modelo { get; set; }
        public string CodUtillaje { get; set; }
        public Nullable<double> CantidadFabricar { get; set; }
        public Nullable<double> CantidadSaldos { get; set; }
        public double UnidadesPendientes { get; set; }
        public Nullable<double> UnidadesFabricadas { get; set; }
        public string Descripcion { get; set; }
        public Nullable<double> Duracion { get; set; }
        public string CodigoArticulo { get; set; }
        public string Talla { get; set; }
        public string Tallas { get; set; }
        public string CodSeccion { get; set; }
        public string NombreSeccion { get; set; }
        public Nullable<int> ArticuloID { get; set; }
        public int OrdenFabricacionID { get; set; }
        public Nullable<int> IdPedido { get; set; }
        public Nullable<System.DateTime> FechaPedido { get; set; }
        public Nullable<System.DateTime> FechaServicio { get; set; }
        public string Cliente { get; set; }
        public string NombreCliente { get; set; }
        public string Proceso { get; set; }
        public string NumeroOperacionAnterior { get; set; }
        public string NumeroOperacionSiguiente { get; set; }
        public Nullable<int> TipoProceso { get; set; }
        public string NumeroOperacion { get; set; }
        public Nullable<bool> Finalizado { get; set; }
        public Nullable<int> Prioridad { get; set; }
        public Nullable<int> IdOrdenFabricacion { get; set; }
        public Nullable<int> IdMaquina { get; set; }
        public string Observaciones { get; set; }
        public int IdEstado { get; set; }
        public Nullable<int> Revision { get; set; }
    }
}
