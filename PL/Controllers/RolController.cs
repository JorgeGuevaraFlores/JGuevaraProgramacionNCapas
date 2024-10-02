using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PL.Models;
using System.Reflection;
using System.Threading.Tasks;

namespace PL.Controllers
{
	//[Authorize]
	public class RolController : Controller
	{
		private RoleManager<IdentityRole> _roleManager;

		public RolController()
		{
			// Inicializar RoleManager manualmente
			var roleStore = new RoleStore<IdentityRole>(new ApplicationDbContext());
			_roleManager = new RoleManager<IdentityRole>(roleStore);
		}

		// GET: Rol
		[HttpGet]
		public ActionResult GetAll()
		{
			var roles = _roleManager.Roles.ToList();
			return View(roles);
			//return View();
		}

		[HttpGet]
		public ActionResult Form(string Id)
		{
			ML.AspNetRoles rol = new ML.AspNetRoles();

			if (Id != null)
			{
				// Llama a FindByIdAsync de manera sincrónica
				var role = _roleManager.FindByIdAsync(Id).GetAwaiter().GetResult();

				if (role != null)
				{
					rol = new ML.AspNetRoles
					{
						Id = role.Id,
						Name = role.Name
						// Asigna otras propiedades según sea necesario
					};
				}
				else
				{
					// Manejar el caso donde no se encuentra el rol
				}
			}

			return View(rol);
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Form(ML.AspNetRoles model)
		{
			if (ModelState.IsValid)
			{
				if (model.Id == null)
				{
					var role = new IdentityRole { Name = model.Name };
					var result = await _roleManager.CreateAsync(role);

					if (result.Succeeded)
					{
						return RedirectToAction(nameof(GetAll));
					}

					foreach (var error in result.Errors)
					{
						ModelState.AddModelError("", error);
					}
				}
				else //update
				{
					var role = await _roleManager.FindByIdAsync(model.Id);
					if (role == null)
					{
						return HttpNotFound();
					}

					role.Name = model.Name;
					var result = await _roleManager.UpdateAsync(role);

					if (result.Succeeded)
					{
						return RedirectToAction(nameof(GetAll));
					}

					foreach (var error in result.Errors)
					{
						ModelState.AddModelError("", error);
					}
				}
			}

			return View(model);
		}
	}
}