using System.Web.Mvc;

namespace ProjetoWebBibliotecaDeFilme.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }        
    }
}