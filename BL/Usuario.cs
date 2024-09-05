using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Usuario
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JGuevaraProgramacionNCapasEntities context = new DL.JGuevaraProgramacionNCapasEntities())
                {
                    var query = context.UsuarioGetAll().ToList();

                    if (query != null)
                    {
                        result.Objects = new List<object>();

                        foreach(DL.UsuarioGetAll_Result item in query)
                        {
                            ML.Usuario usuario = new ML.Usuario();
                            usuario.IdUsuario = item.IdUsuario;
                            usuario.Nombre = item.Nombre;
                            usuario.ApellidoPaterno = item.ApellidoPaterno;
                            usuario.ApellidoMaterno = item.ApellidoMaterno;
                            usuario.FechaNacimiento = item.FechaNacimiento.Value.ToString("dd/MM/yyyy");
                            usuario.Status = item.Status.Value;

                            result.Objects.Add(usuario);
                        }

                        result.Correct = true;
                    } else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No hay registros";
                    }
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }
    }
}
