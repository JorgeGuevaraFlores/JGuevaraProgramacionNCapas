using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Result result = BL.Usuario.GetAll(0, 5);
            ML.Usuario usuario = new ML.Usuario();
            if (result.Correct)
            {
                usuario.Usuarios = result.Objects;
            } else
            {
                usuario.Usuarios = new List<object>();
            }

            return View(usuario);
        }

		[HttpPost]
		public JsonResult GetAll(int draw, int start, int length)
		{
			// Obtener los usuarios paginados
			ML.Result countResult = BL.Usuario.Count();
			// Obtener el conteo total de registros
			int totalRecords = countResult.Correct && countResult.Objects.Any()
				? (int)countResult.Objects.First()
				: 0;

			length = length == -1 ? totalRecords : length;

			ML.Result paginatedResult = BL.Usuario.GetAll(start, length);
			List<object> usuarios = paginatedResult.Objects ?? new List<object>();

			

			// Formatear los datos para DataTables
			var jsonResult = new
			{
				draw = draw,
				recordsTotal = totalRecords,  // Total de registros sin paginación
				recordsFiltered = totalRecords,  // Total de registros después de la paginación
				data = usuarios  // Datos paginados
			};

			return Json(jsonResult);
		}



	}
}