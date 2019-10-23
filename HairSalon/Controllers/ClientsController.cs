using Microsoft.AspNetCore.Mvc;
using Salon.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Salon.Controllers
{
    [Authorize]
    public class ClientController : Controller
    {
        private readonly SalonContext _db;
        private readonly UserManager<ApplicationUser> _userManager; 
        public ClientController(SalonContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            var userItems = _db.Clients.Where(entry => entry.User.Id == currentUser.Id);
            return View(userItems);
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.StylistId = new SelectList(_db.Stylists,"StylistId","Name");
            ViewBag.CheckList = _db.Stylists.ToList();
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(Client client, int StylistId)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            client.User = currentUser;
            _db.Clients.Add(client);
            if (StylistId != 0)
            {
                _db.StylistClient.Add(new StylistClient() { StylistId = StylistId, ClientId = client.ClientId });
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
