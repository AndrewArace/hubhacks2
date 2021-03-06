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
    
    public partial class Survey
    {
        public Survey()
        {
            this.Questions = new HashSet<Question>();
            this.Takens = new HashSet<Taken>();
            this.Locations = new HashSet<Location>();
        }
    
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public Nullable<System.DateTime> FromDate { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }
        public int SurveyTypeId { get; set; }
        public string Description { get; set; }
        public string SmallImageUrl { get; set; }
        public string LargeImageUrl { get; set; }
        public string Category { get; set; }
    
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<Taken> Takens { get; set; }
        public virtual ICollection<Location> Locations { get; set; }
    }
}
