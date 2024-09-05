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
            ML.Result result = BL.Usuario.GetAll();
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
    }
}