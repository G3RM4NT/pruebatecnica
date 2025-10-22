using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pruebatecnica.Modelos
{
    public class Producto
    {
        public int ProductoID { get; set; }
        public string NombrePrducto { get; set; }

        public string Descripcion { get; set; }

        public decimal precio { get; set; }


        public ICollection<SubProducto> SubProductos { get; set; }

    }

}


