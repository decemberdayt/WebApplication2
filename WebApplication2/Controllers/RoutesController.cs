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
using WebApplication2.Services;

namespace WebApplication2.Controllers
{
    [Authorize]
    public class RoutesController : Controller
    {
        private Entities db = new Entities();
        private RouteService _routeService = new RouteService();
        private PhotoService _photoService = new PhotoService();


        // GET: Routes
        public ActionResult Index()
        {
            var items = _routeService.GetAll(User.Identity.GetUserId());
            return View(items);
        }

        // GET: Routes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Route route = _routeService.FindById(id);
            if (route == null)
            {
                return HttpNotFound();
            }
            return View(route);
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
                route.RouteId = _routeService.FindMaxRouteId() + 1;
                db.Route.Add(route);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", route.UserId);
            return View(route);
        }

        // główna funkcja wywolywana z poziomu JS
        [HttpPost]
        public ActionResult CreateJS(int RouteId, String Origin, String OriginCoordinates, String Destination, String DestinationCoordinates, String RouteLength)
        {
            String user = User.Identity.GetUserId();
            var result = _routeService.CreateJS(RouteId, Origin, OriginCoordinates, Destination, DestinationCoordinates, RouteLength, user);
            return RedirectToAction("Index");
        }

        public int FindMaxRouteId()
        {
            return _routeService.FindMaxRouteId();
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
            _photoService.UpdateRouteIdOnDelete(id);
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
