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
    
    public partial class Arn_Imputacion
    {
        public int Arn_Imputaciones_Id { get; set; }
        public Nullable<System.DateTime> Arn_Imputaciones_FechaIm { get; set; }
        public int Arn_Imputaciones_EmpresaOrigen { get; set; }
        public int Arn_Imputaciones_EjerOrigen { get; set; }
        public string Arn_Imputaciones_SerieOrigen { get; set; }
        public int Arn_Imputaciones_NumeroOrigen { get; set; }
        public int Arn_Imputaciones_LineaOrigen { get; set; }
        public string Arn_Imputaciones_ArticOrigen { get; set; }
        public int Arn_Imputaciones_EmprDestino { get; set; }
        public int Arn_Imputaciones_EjerDestino { get; set; }
        public string Arn_Imputaciones_SerieDestino { get; set; }
        public int Arn_Imputaciones_NumeroDestino { get; set; }
        public int Arn_Imputaciones_LineaDestino { get; set; }
        public string Arn_Imputaciones_ArtiDestino { get; set; }
        public decimal Arn_Imputaciones_PorcImputado { get; set; }
        public System.Guid Arn_Imputaciones_LineaPosicion { get; set; }
        public decimal Arn_Imputaciones_ImporteNeto { get; set; }
        public decimal Arn_Imputaciones_ImporteImputado { get; set; }
    }
}
