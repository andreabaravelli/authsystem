namespace SeatsProject.Controllers
{
    using System.Diagnostics;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using SeatsProject.Models;
    using System.Threading.Tasks;

    using SeatsProject.repostiory_folder;

    public class HomeController : Controller
    {
        private readonly SeatsProjectContext _dbContext;
        private readonly ILogger<HomeController> _logger;
        private readonly ILogin _loginUser;

        public HomeController(/*ILogger<HomeController> logger, SeatsProjectContext dbContext,*/ILogin loguser)
        {
           // _logger = logger;
            //_dbContext = dbContext;
            _loginUser = loguser;
        }

        public IActionResult Index()
        {

            ViewBag.Sedi = ListaSedi(null);
            return View();
        }

        [HttpPost]
        public ActionResult Index(string username, string passcode)
        {

          var issuccess = _loginUser.AuthenticateUser(username, passcode);

          if (issuccess.Result!=null)
            {
                ViewBag.username = string.Format("successfully logged-in",username);
                TempData["username"] = "lo user";
                return RedirectToAction("Index","Layout");
            }else
            {
                ViewBag.username = string.Format("Login failed", username);
                return View();
            }
        }

        public MultiSelectList ListaSedi(string[] selectedValues)
        {
            List<Sede> Sedi = new List<Sede> {
                new Sede{Descrizione="uffici sede cesena", CodiceArea="CS001", Prenotabile=false,Id=1 },
                new Sede{Descrizione="piano terra", CodiceArea="CSPT01",IdPadre=1,Prenotabile=false,Id=2}
            };
            return new MultiSelectList(Sedi, "Id", "Descrizione");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}