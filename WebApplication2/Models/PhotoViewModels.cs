using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class PhotoViewModels
    {
        public int PhotoId { get; set; }
        public string UserId { get; set; }
        public string Coordinates { get; set; }
        public string PhotoUrl { get; set; }
        public Nullable<int> RouteId { get; set; }
    }
}