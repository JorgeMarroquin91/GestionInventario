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
    
    public partial class Kardex
    {
        public int idKardex { get; set; }
        public Nullable<decimal> saldo { get; set; }
        public Nullable<System.DateTime> fecha { get; set; }
        public int idVenta { get; set; }
        public int idCompra { get; set; }
    
        public virtual Compras Compras { get; set; }
        public virtual Ventas Ventas { get; set; }
    }
}
