//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace gov.cityofboston.hubhacks2.api
{
    using System;
    using System.Collections.Generic;
    
    public partial class Location
    {
        public Location()
        {
            this.Surveys = new HashSet<Survey>();
        }
    
        public System.Guid Id { get; set; }
        public Nullable<System.DateTime> FromDate { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }
        public decimal Lat { get; set; }
        public decimal Lng { get; set; }
        public Nullable<decimal> Radius { get; set; }
        public string Name { get; set; }
        public bool Enabled { get; set; }
    
        public virtual ICollection<Survey> Surveys { get; set; }
    }
}
