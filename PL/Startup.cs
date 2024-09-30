using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin;
using Microsoft.Owin.Security.Jwt;
using Owin;
using System.Text;
using System;
using System.Configuration;

[assembly: OwinStartupAttribute(typeof(PL.Startup))]
namespace PL
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
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
		}
    }
}
