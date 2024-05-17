namespace SuporteSolidario.Controllers;

using System.Security.Claims;
using System.Security.Policy;
using Microsoft.AspNetCore.Mvc;
using SuporteSolidarioBusiness.Domain.Enums;

public class BaseController: Controller
{
    public long IdUsuarioAutenticado
    {
        get
        {
            string valueClaimIdUsuario = HttpContext.User.FindFirstValue("IdUsuario");
            if(string.IsNullOrEmpty(valueClaimIdUsuario))
            {
                throw new PolicyException("Login não encontrado!");
            }

            return Int64.Parse(valueClaimIdUsuario);
        }
    }

    public TipoUsuario TipoUsuarioAutenticado
    {
        get
        {
            string valueClaimTipoUsuario = HttpContext.User.FindFirstValue("TipoDeUsuario");
            if(string.IsNullOrEmpty(valueClaimTipoUsuario))
            {
                return TipoUsuario.NaoAutenticado;
            }

            return Enum.Parse<TipoUsuario>(valueClaimTipoUsuario);
        }
    }
}