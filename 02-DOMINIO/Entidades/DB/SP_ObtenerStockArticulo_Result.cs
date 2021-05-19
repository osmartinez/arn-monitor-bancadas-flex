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
    
    public partial class SP_ObtenerStockArticulo_Result
    {
        public short CodigoEmpresa { get; set; }
        public short Ejercicio { get; set; }
        public string CodigoArticulo { get; set; }
        public string CodigoAlmacen { get; set; }
        public short Periodo { get; set; }
        public string Partida { get; set; }
        public string TipoUnidadMedida { get; set; }
        public string CodigoColor { get; set; }
        public string CodigoTalla { get; set; }
        public decimal UnidadEntrada { get; set; }
        public decimal UnidadSalida { get; set; }
        public decimal UnidadSaldo { get; set; }
        public decimal UnidadCompra { get; set; }
        public decimal UnidadConsumo { get; set; }
        public decimal UnidadEntradaTipo { get; set; }
        public decimal UnidadSalidaTipo { get; set; }
        public decimal UnidadSaldoTipo { get; set; }
        public decimal UnidadCompraTipo { get; set; }
        public decimal UnidadConsumoTipo { get; set; }
        public decimal ImporteEntrada { get; set; }
        public decimal CosteSalida { get; set; }
        public decimal ImporteSaldo { get; set; }
        public decimal ImporteSalida { get; set; }
        public decimal ImporteCompra { get; set; }
        public decimal ImporteConsumo { get; set; }
        public decimal PrecioMedio { get; set; }
        public decimal PrecioUltimaEntrada { get; set; }
        public decimal PrecioUltimaSalida { get; set; }
        public Nullable<System.DateTime> FechaUltimaEntrada { get; set; }
        public Nullable<System.DateTime> FechaUltimaSalida { get; set; }
        public Nullable<System.DateTime> FechaCaducidad { get; set; }
        public string Ubicacion { get; set; }
        public short StatusRecalculo { get; set; }
        public System.Guid IdAcumuladoStock { get; set; }
    }
}
