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
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.StylistId = new SelectList(_db.Stylists,"StylistId","Name");
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
        [HttpGet]
        public ActionResult Details(int id)
        {
            Client foundClient = _db.Clients.FirstOrDefault(client => client.ClientId == id);
            return View(foundClient);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            Client foundClient = _db.Clients.FirstOrDefault(client => client.ClientId == id);
            return View(foundClient);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult Destroy(int id)
        {
            Client foundClient = _db.Clients.FirstOrDefault(client => client.ClientId == id);
            _db.Clients.Remove(foundClient);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Update(int id)
        {
            Client foundClient = _db.Clients.FirstOrDefault( client => client.ClientId == id);
            ViewBag.BigBagOStylist = new SelectList(_db.Stylists, "StylistId", "Name");
            return View(foundClient);
        }
        [HttpPost]
        public ActionResult Update(Client client)
        {
            _db.Entry(client).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");}
    }
}
