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
    
    public partial class SP_BuscarTodasSecciones_Result
    {
        public string CodSeccion { get; set; }
        public string Nombre { get; set; }
        public Nullable<double> CosteTiempo { get; set; }
        public Nullable<double> CosteIndirecto { get; set; }
        public Nullable<double> CosteTiempoSubcontrata { get; set; }
        public Nullable<double> CosteIndirectoSubcontrata { get; set; }
        public Nullable<bool> EsMolde { get; set; }
        public Nullable<bool> EsCorte { get; set; }
        public Nullable<int> ParesPorDia { get; set; }
        public string Grupo { get; set; }
        public string CodigoEtiqueta { get; set; }
        public Nullable<int> DiasDesfase { get; set; }
    }
}
