using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
	[Authorize(Roles = "Administrador")]
	public class UsuarioController : Controller
	{
		// GET: Usuario
		[HttpGet]
		public ActionResult GetAll()
		{
			if (!User.IsInRole("Administrador"))
			{
				return View("AccessDenied"); // Redirigir a la vista de acceso denegado
			}

			return View();
		}

		[HttpPost]
		public JsonResult Pagination()
		{
			ML.DataTablePagination pagination = new ML.DataTablePagination();

			pagination.draw = Request.Form.GetValues("draw").FirstOrDefault();
			pagination.start = Request.Form.GetValues("start").FirstOrDefault();
			pagination.length = Request.Form.GetValues("length").FirstOrDefault();

			if (Request.Form.AllKeys.Contains("order[0][dir]"))
			{
				pagination.sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();

			}

			pagination.searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();

			if (Request.Form.AllKeys.Contains("order[0][column]"))
			{
				pagination.sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();

			} else
			{
				pagination.sortColumn = "IdUsuario";
			}


			pagination.pageSize = pagination.length != null ? Convert.ToInt32(pagination.length) : 0;
			pagination.skip = pagination.start != null ? Convert.ToInt32(pagination.start) : 0;

			pagination.recordsTotal = 0;

			ML.Result result = BL.Usuario.GetAllLINQ(pagination);


			return Json(new
			{
				draw = pagination.draw,
				recordsFiltered = pagination.recordsTotal,
				recordsTotal = pagination.recordsTotal,
				data = result.Objects
			});
		}
	}
}