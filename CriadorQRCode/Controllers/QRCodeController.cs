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
    public class QRCodeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly QRCodeService _serviceQRCode;

        public QRCodeController(ILogger<HomeController> logger, QRCodeService serviceQRCode)
        {
            _logger = logger;
            _serviceQRCode = serviceQRCode;
        }

        public IActionResult Index()
        {
            SetarTipoPix();
            return View(new DadoPIXViewModel());
        }

        [HttpPost]
        public IActionResult Index(DadoPIXViewModel data)
        {
            if (ModelState.IsValid)
            {
                data = _serviceQRCode.GerarChavePix(data);
            }
            SetarTipoPix();
            return View(data);
        }

        public IActionResult Wifi()
        {
            SetarTipoAuthWifi();
            return View(new DadoWifiViewModel());
        }
        [HttpPost]
        public IActionResult Wifi(DadoWifiViewModel data)
        {
            if (ModelState.IsValid)
            {
                data = _serviceQRCode.GerarWifi(data);
            }
            SetarTipoAuthWifi();
            return View(data);
        }

        private void SetarTipoPix()
        {
            var values = from TipoPIX e in Enum.GetValues(typeof(TipoPIX))
                         select new { Id = e, Name = e.ToString() };
            ViewBag.TipoPix = new SelectList(values, "Id", "Name");

        }

        private void SetarTipoAuthWifi()
        {
            var values = from TipoAuthWifi e in Enum.GetValues(typeof(TipoAuthWifi))
                         select new { Id = e, Name = e.ToString() };
            ViewBag.TipoAuthWifi = new SelectList(values, "Id", "Name");

        }
    }
}
