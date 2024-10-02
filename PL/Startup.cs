using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin;
using Microsoft.Owin.Security.Jwt;
using Owin;
using System.Text;
using System;
using System.Configuration;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using PL.Models;
using Microsoft.AspNet.Identity.Owin;

[assembly: OwinStartupAttribute(typeof(PL.Startup))]
namespace PL
{
	public partial class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			// Configuración de autenticación
			ConfigureAuth(app);

			// Configuración del middleware JWT
			var key = Encoding.ASCII.GetBytes(ConfigurationManager.AppSettings["JwtSecretKey"]);

			app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
			{
				TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ValidateIssuer = false,
					ValidateAudience = false,
					ValidateLifetime = true,
					ClockSkew = TimeSpan.Zero // Opcional, para reducir la tolerancia de tiempo
				}
			});

			// Configuración de RoleManager
			app.CreatePerOwinContext<ApplicationDbContext>(ApplicationDbContext.Create); // Registrar el contexto de la base de datos
			app.CreatePerOwinContext<RoleManager<IdentityRole>>(CreateRoleManager);      // Registrar RoleManager
		}

		// Método Create para RoleManager
		private static RoleManager<IdentityRole> CreateRoleManager(IdentityFactoryOptions<RoleManager<IdentityRole>> options, IOwinContext context)
		{
			var roleStore = new RoleStore<IdentityRole>(context.Get<ApplicationDbContext>());
			return new RoleManager<IdentityRole>(roleStore);
		}
	}
}
