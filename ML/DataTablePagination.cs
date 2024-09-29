using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
	public class DataTablePagination
	{
        public string draw { get; set; } //true o false
        public string start { get; set; } //pagina de inicio
        public string length { get; set; } //longitud de coleccion de datos
        public string sortColumn { get; set; } //columna ordenada
        public string sortColumnDir { get; set; } //asc o desc
        public string searchValue { get; set; } //si se ha buscado un dato

        //estatus del listado
        public int pageSize { get; set; } //cuantas paginas hay
        public int skip { get; set; } //ignorar los demas registros
        public int recordsTotal { get; set; } //cuantos registros hay
    }
}
