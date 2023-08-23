namespace SeatsProject.Controllers
{
    using System.Diagnostics;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using SeatsProject.Models;

    public class HomeController : Controller
    {
        private readonly SeatsProjectContext _dbContext;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, SeatsProjectContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {

            ViewBag.Sedi = ListaSedi(null);
            return View();
        }

        [HttpPost]

        public ActionResult Index(FormCollection form)
        {

            ViewBag.YouSelected = form["Sedi"];

            string selectedValues = form["Sedi"];

            ViewBag.Countrieslist = ListaSedi(selectedValues.Split(','));

            return View();
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