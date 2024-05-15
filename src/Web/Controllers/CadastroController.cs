using Microsoft.AspNetCore.Mvc;

namespace SuporteSolidario.Controllers
{
    public class CadastroController : BaseController
    {
        // GET: CadastroController
        public ActionResult Cliente()
        {
            return View();
        }

        public ActionResult Colaborador()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SalvarDadosCliente()
        {
            return RedirectToAction("Index","Cliente");
        }

        [HttpPost]
        public ActionResult SalvarDadosColaborador()
        {
            return RedirectToAction("Index","Colaborador");
        }

    }
}
