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
        private RouteService _routeService = new RouteService();

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

        // główna funkcja wywolywana z poziomu JS
        [HttpPost]
        public ActionResult CreateJS(int RouteId, String Origin, String OriginCoordinates, String Destination, String DestinationCoordinates, String RouteLength)
        {
            var result = _routeService.CreateJS(RouteId, Origin, OriginCoordinates, Destination, DestinationCoordinates, RouteLength, User.Identity.GetUserId());
            return RedirectToAction("Index");
        }
        // GET do dodawania z poziomu JS id drogi do zdjec
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
            Route route = _routeService.FindById(id);
            if (route == null)
            {
                return HttpNotFound();
            }
            return View(route);
        }

        // POST: Routes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RouteId,RouteName,UserId,Origin,OriginCoordinates,Destination,DestinationCoordinates,RouteLength")] Route route)
        {
            if (ModelState.IsValid)
            {
                _routeService.EditRoute(route);
                return RedirectToAction("Index");
            }
            return View(route);
        }

        // GET: Routes/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Routes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _routeService.DeleteRoute(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _routeService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
