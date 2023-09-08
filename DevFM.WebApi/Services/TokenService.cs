using DevFM.Domain.ViewModels;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DevFM.WebApi.Services
{
    public class TokenService
    {
        public static object GenerateToken(UsuarioLogadoVM usuario)
        {
            var key = Encoding.ASCII.GetBytes(Key.Secret);

            var tokenConfig = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UsuarioId", usuario.UsuarioId.ToString()),
                    new Claim("DescricaoPerfil", usuario.DescricaoPerfil),
                    new Claim(ClaimTypes.Role, usuario.DescricaoPerfil),
                    new Claim("Email", usuario.Email),
                    new Claim("Nome", usuario.Nome),
                }),
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenConfig);
            var tokenString = tokenHandler.WriteToken(token);

            return new
            {
                token = tokenString
            };

        }
    }
}
