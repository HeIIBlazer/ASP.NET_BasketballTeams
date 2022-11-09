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
    public class TeamsMenuController : Controller
    {
        private BasketBallContext db = new BasketBallContext();

        // GET: TeamsMenu
        public ActionResult Index()
        {
            return View(db.Teams.ToList());
        }


        [ChildActionOnly]

        public ActionResult TeamsMenuList()
        {
            var teamsName = db.Teams.ToList();
            return PartialView(teamsName);
        }

        public ActionResult Browse(int id)
        {
            var teamPlayers = db.Teams.Include("Players").Single(g => g.Id == id);
            return View(teamPlayers);
        }


    }
}
