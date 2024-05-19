using Microsoft.AspNetCore.Mvc;
using ShiftManagement.Data.Providers;

namespace ShiftManagement.WebPortal.Pages
{
    public class ClientsController : Controller
    {
        private readonly IClientDataProvider _clientDataProvider;
        public ClientsController(IWebHostEnvironment webHostEnvironment,
           IClientDataProvider clientDataProvider)
        {
            _clientDataProvider = clientDataProvider;
        }
        public IActionResult Index()
        {          
            return View();
        }
        public IActionResult clientpage()
        {
            var clients = _clientDataProvider.GetClientsList();
            return View(clients);
            //return new JsonResult(users);
        }
        public IActionResult clientservice()
        {
            var clients = _clientDataProvider.GetClientsList();
            
            return View(clients);
            //return new JsonResult(users);
        }
    }
    
}
