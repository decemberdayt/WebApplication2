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
    public class PhotosController : Controller
    {
        private PhotoService _photoService = new PhotoService();

        // GET: Photos
        public ActionResult Index()
        {
            var photos = _photoService.GetAll(User.Identity.GetUserId());
            return View(photos);
        }

        // GET: Photos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photo photo = _photoService.FindById(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }

        [HttpPost]
        public ActionResult SavePhoto(String Coordinates, String PhotoUrl, int RouteId)
        {
            _photoService.SavePhoto(Coordinates, PhotoUrl, RouteId, User.Identity.GetUserId());
            return RedirectToAction("Index","Home");
        }


        // GET: Photos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photo photo = _photoService.FindById(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }

        // POST: Photos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _photoService.DeletePhoto(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _photoService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
