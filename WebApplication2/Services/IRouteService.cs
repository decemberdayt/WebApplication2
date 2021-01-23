using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebApplication2.Models;

namespace WebApplication2.Services
{
    public interface IRouteService
    {
        List<Route> GetAll(String user);
        int FindMaxRouteId();
        Route FindById(int? id);
        Route CreateJS(int RouteId, String Origin, String OriginCoordinates, String Destination, String DestinationCoordinates, String RouteLength, String user);
    }

    public class RouteService : IRouteService
    {
        private Entities db = new Entities();
        public List<Route> GetAll(String user)
        {
            var route = db.Route.Include(r => r.AspNetUsers)
                .Where(s => s.UserId == user)
                .ToList();
            return route;
        }

        public int FindMaxRouteId()
        {
            Route route = db.Route.OrderByDescending(u => u.RouteId).FirstOrDefault();
            if (route == null)
            {
                route = new Route();
                route.RouteId = 0;
            }
            return route.RouteId;
        }

        public Route FindById(int? id)
        {
            Route route = db.Route.Find(id);
            return route;
        }

        public Route CreateJS(int RouteId, String Origin, String OriginCoordinates, String Destination, String DestinationCoordinates, String RouteLength, String user)
        {
            Route route = new Route();
            route.RouteId = RouteId;
            route.OriginCoordinates = OriginCoordinates;
            route.Origin = Origin;
            route.UserId = user;
            route.RouteLength = RouteLength;
            route.Destination = Destination;
            route.DestinationCoordinates = DestinationCoordinates;
            db.Route.Add(route);
            db.SaveChanges();
            return route;
        }
    }

}