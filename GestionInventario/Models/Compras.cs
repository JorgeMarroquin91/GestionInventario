//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GestionInventario.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Compras
    {
        public int idCompra { get; set; }
        public Nullable<decimal> cantidad { get; set; }
        public Nullable<decimal> precioCompra { get; set; }
        public Nullable<int> idLote { get; set; }
        public Nullable<int> idKardex { get; set; }
    
        public virtual Lote Lote { get; set; }
        public virtual Kardex Kardex { get; set; }
    }
}
