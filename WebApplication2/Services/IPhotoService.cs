using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebApplication2.Models;

namespace WebApplication2.Services
{
    public interface IPhotoService
    {
        List<Photo> FindByRouteId(int id);
        void UpdateRouteIdOnDelete(int id);
    }

    public class PhotoService : IPhotoService
    {
        private Entities db = new Entities();

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
    }
}