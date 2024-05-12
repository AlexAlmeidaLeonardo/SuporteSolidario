using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using SuporteSolidarioBusiness.Domain.Enums;
using SuporteSolidarioBusiness.Application.Services;
using SuporteSolidarioBusiness.Domain.Entities;

public class TokenService : ITokenService
{
    private const string secret_key = "32894723894723894723894723894723";
    public string GetToken(UsuarioEntity usuario)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(secret_key);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
            [
                new Claim(ClaimTypes.Name, usuario.Login),
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Role, usuario.TipoDeUsuario.ToString()),
                new Claim(ClaimTypes.MobilePhone, usuario.Celular),
                new Claim(ClaimTypes.Email, usuario.Email)
            ]),
            Expires = DateTime.UtcNow.AddMinutes(5),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public string GetLoggedUser(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret_key)),
            ValidateIssuer = false,
            ValidateAudience = false
        };

        var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
        var userName = principal.FindFirstValue(ClaimTypes.Name);
        return userName;
    }

    public UsuarioEntity GetUsuarioDTO(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret_key)),
            ValidateIssuer = false,
            ValidateAudience = false
        };

        var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
        string login = principal.FindFirstValue(ClaimTypes.Name);
        long id = Convert.ToInt64(principal.FindFirstValue(ClaimTypes.NameIdentifier));
        //int tipoUsuario = Convert.ToInt32(principal.FindFirstValue(ClaimTypes.Role));
        TipoUsuario tipoUsuario = (TipoUsuario) Enum.Parse(typeof(TipoUsuario), principal.FindFirstValue(ClaimTypes.Role));

        string celular = principal.FindFirstValue(ClaimTypes.MobilePhone);
        string email = principal.FindFirstValue(ClaimTypes.Email);
        

        UsuarioEntity entity = new UsuarioEntity
        {
            Id = id,
            Login = login,
            TipoDeUsuario = tipoUsuario,
            Email = email,
            Celular = celular
        };

        return entity;
    }
}