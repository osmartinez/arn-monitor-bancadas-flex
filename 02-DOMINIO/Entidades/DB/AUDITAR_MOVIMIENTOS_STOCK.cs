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
    
    public partial class AUDITAR_MOVIMIENTOS_STOCK
    {
        public int Id { get; set; }
        public int IdStockArticulo { get; set; }
        public string CodigoArticulo { get; set; }
        public int IdStockArticuloTalla { get; set; }
        public string Talla { get; set; }
        public double CantidadAnterior { get; set; }
        public double Cantidad { get; set; }
        public System.DateTime FechaMovimiento { get; set; }
        public Nullable<int> IdTipoStock { get; set; }
    }
}
