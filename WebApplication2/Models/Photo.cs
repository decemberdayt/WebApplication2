//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Photo
    {
        public int PhotoId { get; set; }
        public string UserId { get; set; }
        public string Coordinates { get; set; }
        public string PhotoUrl { get; set; }
        public Nullable<int> RouteId { get; set; }
    
        public virtual AspNetUsers AspNetUsers { get; set; }
        public virtual Route Route { get; set; }
    }
}
