using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebApplication2.Models;

namespace WebApplication2.Services
{
    public interface IPhotoService
    {
        List<Photo> GetAll(String user);
        Photo FindById(int? id);
        int FindMaxPhotoId();
        List<Photo> FindByRouteId(int id);
        void UpdateRouteIdOnDelete(int id);
        void SavePhoto(String Coordinates, String PhotoUrl, int RouteId, String user);
        void DeletePhoto(int id);
        void Dispose();
    }

    public class PhotoService : IPhotoService
    {
        private Entities db = new Entities();
        public List<Photo> GetAll(String user)
        {
            var photos = db.Photo.Include(p => p.AspNetUsers)
                .Where(r => r.UserId == user)
                .ToList();
            return photos;
        }
        public Photo FindById(int? id)
        {
            Photo photo = db.Photo.Find(id);
            return photo;
        }
        public int FindMaxPhotoId()
        {
            Photo photo = db.Photo.OrderByDescending(u => u.PhotoId).FirstOrDefault();
            if (photo == null)
            {
                photo = new Photo();
                photo.PhotoId = 0;
            }
            return photo.PhotoId;
        }
        public List<Photo>FindByRouteId(int id)
        {
            List<Photo> photos = db.Photo.Where(p => p.RouteId == id).ToList();
            return photos;
        }

        public void UpdateRouteIdOnDelete(int id)
        {
            List<Photo> photos = FindByRouteId(id);
            foreach (Photo photo in photos)
            {
                photo.RouteId = null;
                db.SaveChanges();
            }

        }
        public void SavePhoto(String Coordinates, String PhotoUrl, int RouteId, String user)
        {
            Photo photo = new Photo();
            photo.PhotoId = FindMaxPhotoId() + 1;
            photo.Coordinates = Coordinates;
            photo.PhotoUrl = PhotoUrl;
            photo.UserId = user;
            photo.RouteId = RouteId;
            db.Photo.Add(photo);
            db.SaveChanges();
        }
        public void DeletePhoto(int id)
        {
            Photo photo = db.Photo.Find(id);
            db.Photo.Remove(photo);
            db.SaveChanges();
        }
        public void Dispose()
        {
            db.Dispose();
        }
    }
}