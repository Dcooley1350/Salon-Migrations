using Microsoft.AspNetCore.Mvc;
using Salon.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Salon.Controllers
{
    public class StylistController : Controller{
        private readonly SalonContext _db;
        public StylistController(SalonContext db)
        {
            _db = db;
        }
        [HttpGet]
        public ActionResult Index()
        {
            List<Stylist> model = _db.Stylists.ToList();
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Stylist stylist)
        {
            _db.Stylists.Add(stylist);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            var thisStylist = _db.Stylists
            .Include(stylist => stylist.Clients)
            .ThenInclude(join => join.Client)
            .FirstOrDefault(stylist => stylist.StylistId == id);
            return View(thisStylist);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            Stylist foundStylist = _db.Stylists.FirstOrDefault( stylist => stylist.StylistId == id);
            ViewBag.AssociatedClients = _db.Clients.ToList();
            //.Where( client => client.StylistId == id);
            return View(foundStylist);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult Destroy(int id)
        {
            Stylist foundStylist = _db.Stylists.FirstOrDefault( stylist => stylist.StylistId == id);
            _db.Stylists.Remove(foundStylist);
            List<Client> foundClients = _db.Clients.ToList();
            //.Where(client => client.StylistId == id).ToList();
            foreach(Client client in foundClients)
            {
                _db.Clients.Remove(client);
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Update(int id)
        {
            Stylist foundStylist = _db.Stylists.FirstOrDefault(stylist =>stylist.StylistId == id);
            return View(foundStylist);
        }
        [HttpPost]
        public ActionResult Update(Stylist stylist)
        {
            _db.Entry(stylist).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
