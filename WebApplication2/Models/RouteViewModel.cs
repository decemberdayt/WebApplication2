using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class RouteViewModel
    {
        public int RouteId { get; set; }
        public string UserId { get; set; }
        public string Origin { get; set; }
        public string OriginCoordinates { get; set; }
        public string Destination { get; set; }
        public string DestinationCoordinates { get; set; }
        public string RouteName { get; set; }
        public string RouteLength { get; set; }

    }
}