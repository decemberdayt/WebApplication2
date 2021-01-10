using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class RoutesController : Controller
    {
        //private ApplicationDbContext db = new ApplicationDbContext();
        private Entities db = new Entities();

        // GET: Routes
        public ActionResult Index()
        {
            String user = User.Identity.GetUserId();
            var route = db.Route.Include(r => r.AspNetUsers).Where(r => r.UserId == user);
            return View(route.ToList());
        }

        // GET: Routes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Route route = db.Route.Find(id);
            if (route == null)
            {
                return HttpNotFound();
            }
            return View(route);
        }

        public Route FindMaxRouteId()
        {
            Route route = db.Route.OrderByDescending(u => u.RouteId).FirstOrDefault();
            if(route==null)
            {
                route = new Route();
                route.RouteId = 0;
            }
            return route;
        }

        // GET: Routes/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: Routes/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,Origin,OriginCoordinates,Destination,DestinationCoordinates")] Route route)
        {
            if (ModelState.IsValid)
            {
                route.RouteId = FindMaxRouteId().RouteId + 1;
                db.Route.Add(route);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", route.UserId);
            return View(route);
        }

        // główna funckja wywolywana z poziomu JS
        [HttpPost]
        public ActionResult CreateJS(String Origin, String OriginCoordinates, String Destination, String DestinationCoordinates, String RouteLength)
        {
            Route route = new Route();
                route.RouteId = FindMaxRouteId().RouteId + 1;
                route.OriginCoordinates = OriginCoordinates;
                route.Origin = Origin;
                route.UserId = User.Identity.GetUserId();
                route.RouteLength = RouteLength;
                route.Destination = Destination;
                route.DestinationCoordinates = DestinationCoordinates;
                db.Route.Add(route);
                db.SaveChanges();
                return RedirectToAction("Index");
        }

        // GET: Routes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Route route = db.Route.Find(id);
            if (route == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", route.UserId);
            return View(route);
        }

        // POST: Routes/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RouteId,RouteName,UserId,Origin,OriginCoordinates,Destination,DestinationCoordinates,RouteLength")] Route route)
        {
            if (ModelState.IsValid)
            {
                db.Entry(route).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", route.UserId);
            return View(route);
        }

        // GET: Routes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Route route = db.Route.Find(id);
            if (route == null)
            {
                return HttpNotFound();
            }
            return View(route);
        }

        // POST: Routes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Route route = db.Route.Find(id);
            db.Route.Remove(route);
            db.SaveChanges();
            return RedirectToAction("Index");
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
