using Microsoft.AspNetCore.Mvc;
using Salon.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Salon.Controllers
{
    public class ClientController : Controller
    {
        private readonly SalonContext _db;

        public ClientController(SalonContext db)
        {
            _db = db;
        }
        
        [HttpGet]
        public ActionResult Index()
        {
            List<Client> model = _db.Clients.Include(clients => clients.Stylist).ToList();
            return View("Index", model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.SelectList =new SelectList(_db.Stylists,"StylistId","Specialty");
            ViewBag.CheckList = _db.Stylists.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Create(Client client)
        {
            _db.Clients.Add(client);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
