using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BasketBall_JPTV20.Models;

namespace BasketBall_JPTV20.Controllers
{
    public class PlayersController : Controller
    {
        private BasketBallContext db = new BasketBallContext();

        // GET: Players
        public ActionResult Index()
        {
            var players = db.Players.Include(p => p.Team);
            return View(players.ToList());
        }

        // GET: Players/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = db.Players.Find(id);
            player = db.Players.Include(p => p.Team).FirstOrDefault(t => t.Id == id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        // GET: Players/Create
        public ActionResult Create()
        {
            ViewBag.TeamId = new SelectList(db.Teams, "Id", "Name");
            return View();
        }

        // POST: Players/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Age,Position,Photo,PhotoType,TeamId")] Player player, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                //если выбрано и передано изображение
                if (Image != null)
                {
                    player.PhotoType = Image.ContentType;
                    player.Photo = new byte[Image.ContentLength];
                    Image.InputStream.Read(player.Photo, 0, Image.ContentLength);
                }
                db.Players.Add(player);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TeamId = new SelectList(db.Teams, "Id", "Name", player.TeamId);
            return View(player);
        }

        // GET: Players/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            ViewBag.TeamId = new SelectList(db.Teams, "Id", "Name", player.TeamId);
            return View(player);
        }

        // POST: Players/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Age,Position,Photo,PhotoType,TeamId")] Player player, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    player.PhotoType = Image.ContentType;
                    player.Photo = new byte[Image.ContentLength];
                    Image.InputStream.Read(player.Photo, 0, Image.ContentLength);
                }
                db.Entry(player).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TeamId = new SelectList(db.Teams, "Id", "Name", player.TeamId);
            return View(player);
        }

        // GET: Players/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = db.Players.Find(id);
            player = db.Players.Include(p => p.Team).FirstOrDefault(t => t.Id == id);

            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }
        //---------------------------GetImage
        public FileContentResult GetImage(int id)
        {
            //Запрос в БД таблица Players по переданному id
            Player player = db.Players.FirstOrDefault(g => g.Id == id);
            if(player != null)
            {
                return File(player.Photo, player.PhotoType);
            }
            return null;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
