using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic;

namespace BL
{
	public class Usuario
	{
		public static ML.Result Add(ML.Usuario usuario)
		{
			ML.Result result = new ML.Result();
			try
			{
				using(DL.JGuevaraProgramacionNCapasEntities context = new DL.JGuevaraProgramacionNCapasEntities())
				{
					int filasAfectadas = context.UsuarioAdd(usuario.Nombre, usuario.ApellidoPaterno, usuario.ApellidoMaterno, Convert.ToDateTime(usuario.FechaNacimiento), usuario.IdAspNetUser);

					if(filasAfectadas > 0)
					{
						result.Correct = true;
					} else
					{
						result.Correct = false;
						result.ErrorMessage = "Error al insertar Usuario";
					}
				}
				
			} catch (Exception ex)
			{
				result.Correct = false;
				result.ErrorMessage = ex.Message;
				result.Ex = ex;
			}

			return result;
		}
		public static ML.Result GetAllLINQ(ML.DataTablePagination pagination)
		{
			ML.Result result = new ML.Result();
			try
			{
				using (DL.JGuevaraProgramacionNCapasEntities context = new DL.JGuevaraProgramacionNCapasEntities())
				{
					IQueryable<DL.Usuario> query = (from user in context.Usuarios
											  where user.Nombre.Contains(pagination.searchValue) ||
											  user.ApellidoPaterno.Contains(pagination.searchValue) ||
											  user.ApellidoMaterno.Contains(pagination.searchValue)
											  select user);

					if (!(string.IsNullOrEmpty(pagination.sortColumn) && string.IsNullOrEmpty(pagination.sortColumnDir)))
					{
						query = query.OrderBy(pagination.sortColumn + " " + pagination.sortColumnDir);

					}

					pagination.recordsTotal = query.Count();
					if (pagination.pageSize <= 0)
					{
						pagination.pageSize = pagination.recordsTotal;
					}
					var list = query.Skip(pagination.skip).Take(pagination.pageSize).ToList();


					if (list != null)
					{
						result.Objects = new List<object>();

						foreach (DL.Usuario item in list)
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
					}
					else
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
