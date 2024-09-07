using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Usuario
    {
        public static ML.Result GetAll(int start, int length)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JGuevaraProgramacionNCapasEntities context = new DL.JGuevaraProgramacionNCapasEntities())
                {
                    var query = context.UsuarioGetAll(start, length).ToList();

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

        public static ML.Result Count()
        {
			ML.Result result = new ML.Result();
			try
			{
				using (DL.JGuevaraProgramacionNCapasEntities context = new DL.JGuevaraProgramacionNCapasEntities())
				{
					// Contar el número total de registros en la entidad Usuarios
					int totalCount = context.Usuarios.Count();

					result.Correct = true;
					result.Objects = new List<object> { totalCount };  // Almacenar el conteo total en Objects
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
