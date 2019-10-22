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
            return View(_db.Clients.ToList());
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.StylistId = new SelectList(_db.Stylists,"StylistId","Name");
            ViewBag.CheckList = _db.Stylists.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Create(Client client, int StylistId)
        {
            _db.Clients.Add(client);
            if(StylistId != 0)
            {
                _db.StylistClient.Add(new StylistClient() { StylistId = StylistId, ClientId = client.ClientId});
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
           var thisClient = _db.Clients
           .Include(client => client.Stylists)
           .ThenInclude(client => client.Stylist)
           .FirstOrDefault(client => client.ClientId == id);
           return View(thisClient);
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
            ViewBag.StylistId = new SelectList(_db.Stylists, "StylistId", "Name");
            return View(foundClient);
        }
        [HttpPost, ActionName("Update")]
        public ActionResult Update(Client client, int StylistId)
        {
            if(StylistId != 0)
            {
                _db.StylistClient.Add(new StylistClient() { StylistId = StylistId, ClientId = client.ClientId});
            }
            _db.Entry(client).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult AddStylist(int id)
        {
            var thisClient = _db.Clients.FirstOrDefault(clients => clients.ClientId == id);
            ViewBag.StylistId = new SelectList(_db.Stylists,"StylistId", "Name");
            return View(thisClient);
        }
        [HttpPost]
        public ActionResult AddStylist(Client client, int StylistId)
        {
            if (StylistId != 0)
            {
                _db.StylistClient.Add(new StylistClient() { StylistId = StylistId, ClientId = client.ClientId});
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult DeleteStylist(int joinId)
        {
            var joinEntry = _db.StylistClient.FirstOrDefault(entry=> entry.StylistClientId == joinId);
            _db.StylistClient.Remove(joinEntry);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
