﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SurveyEntities : DbContext
    {
        public SurveyEntities()
            : base("name=SurveyEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Activation> Activations { get; set; }
        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<Demographic> Demographics { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<QuestionType> QuestionTypes { get; set; }
        public virtual DbSet<Survey> Surveys { get; set; }
        public virtual DbSet<Taken> Takens { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<CloudData> CloudDatas { get; set; }
    }
}
