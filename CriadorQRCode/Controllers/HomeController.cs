using CriadorQRCode.Models;
using CriadorQRCode.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;

namespace CriadorQRCode.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly QRCodeService _serviceQRCode;

        public HomeController(ILogger<HomeController> logger, QRCodeService serviceQRCode)
        {
            _logger = logger;
            _serviceQRCode = serviceQRCode;
        }

        public IActionResult Index()
        {
            SetarTipoPix();
            return View(new DadosPIXView());
        }

        [HttpPost]
        public IActionResult Index(DadosPIXView data)
        {
            if (ModelState.IsValid)
            {   
                data = _serviceQRCode.GerarChavePix(data);
            }
            SetarTipoPix();
            return View(data);
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

        private void SetarTipoPix()
        {
            var values = from TipoPIX e in Enum.GetValues(typeof(TipoPIX))
                         select new { Id = e, Name = e.ToString() };
            ViewBag.TipoPix = new SelectList(values, "Id", "Name");

        }
    }
}
