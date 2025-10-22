using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pruebatecnica.Modelos
{
    public class SubProducto
    {
        public int SubProductoID { get; set; }
        public string NombreSubPrducto { get; set; }

        public string DescripcionSubProducto { get; set; }

        public decimal PrecioSubProducto { get; set; }

        public int ProductoID { get; set; }
        public Producto Producto { get; set; }

    }
}
