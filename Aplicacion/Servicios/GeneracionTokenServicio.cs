using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Aplicacion.Configuraciones;
using Aplicacion.DTOs;
using Infraestructura.Modelos;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Aplicacion.Servicios
{
    public  class GeneracionTokenServicio
    {

        private readonly IConfiguration _configuration;



        public GeneracionTokenServicio(IConfiguration _configuration)
        {

            this._configuration = _configuration;

        }



        //Configuracion de json web token para darle seguimiento al usuario
        public string GenerarToken(string nombre, string apellido, string correo, string id)
        {


            var jwt = _configuration.GetSection("Jwt").Get<Jwt>();




            if (jwt == null)
            {
                throw new Exception("token invalido");
            }


         
          

            var claims = new[]
            {

                    new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
                    new Claim("id", id),
                    new Claim("Nombre", nombre),
                    new Claim("Apellido", apellido),
                    new Claim("Correo", correo),
                    

            };



            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
            var singing = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);


            var token = new JwtSecurityToken(
                jwt.Issuer,
                jwt.Audience,
                claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: singing
                );


            return new JwtSecurityTokenHandler().WriteToken(token);

        }




    }
}
