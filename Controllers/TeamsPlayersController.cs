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
    public class TeamsPlayersController : Controller
    {
        private BasketBallContext db = new BasketBallContext();

        // GET: TeamsPlayers
        public ActionResult Index()
        {
            return View(db.Teams.ToList());
        }

        // GET: TeamsPlayers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Teams.Find(id);
            team = db.Teams.Include(t => t.Players).FirstOrDefault(t => t.Id == id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        //-------------частичное представление
        [ChildActionOnly]

        public ActionResult PlayersInTeam(int id)
        {
            var teamPlayers = db.Players.Where(p => p.TeamId == id);
            return PartialView(teamPlayers);
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
