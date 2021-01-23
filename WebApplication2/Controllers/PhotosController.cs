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
    [Authorize]
    public class PhotosController : Controller
    {
        private Entities db = new Entities();

        // GET: Photos
        public ActionResult Index()
        {
            String user = User.Identity.GetUserId();
            var photo = db.Photo.Include(p => p.AspNetUsers).Where(r => r.UserId == user);
            return View(photo.ToList());
        }

        // GET: Photos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photo photo = db.Photo.Find(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }

        // GET: Photos/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: Photos/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PhotoId,UserId,Coordinates")] Photo photo)
        {
            if (ModelState.IsValid)
            {
                db.Photo.Add(photo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", photo.UserId);
            return View(photo);
        }

        public int FindMaxPhotoId()
        {
            Photo photo = db.Photo.OrderByDescending(u => u.PhotoId).FirstOrDefault();
            if (photo==null)
            {
                photo = new Photo();
                photo.PhotoId = 0;
            }
            return photo.PhotoId;
        }


        [HttpPost]
        public ActionResult SavePhoto(String Coordinates, String PhotoUrl, int RouteId)
        {
            Photo photo = new Photo();
            photo.PhotoId = FindMaxPhotoId() + 1;
            photo.Coordinates = Coordinates;
            photo.PhotoUrl = PhotoUrl;
            db.Photo.Add(photo);
            photo.UserId = User.Identity.GetUserId();
            photo.RouteId = RouteId;
            db.SaveChanges();
            return RedirectToAction("Index","Home");
        }

        // GET: Photos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photo photo = db.Photo.Find(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", photo.UserId);
            return View(photo);
        }

        // POST: Photos/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PhotoId,UserId,Coordinates")] Photo photo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(photo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", photo.UserId);
            return View(photo);
        }

        // GET: Photos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photo photo = db.Photo.Find(id);
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
            Photo photo = db.Photo.Find(id);
            db.Photo.Remove(photo);
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
