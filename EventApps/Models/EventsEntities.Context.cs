﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EventApps.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DBEventEntities : DbContext
    {
        public DBEventEntities()
            : base("name=DBEventEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<EventType> EventTypes { get; set; }
        public virtual DbSet<ParticipantType> ParticipantTypes { get; set; }
        public virtual DbSet<Registration> Registrations { get; set; }
        public virtual DbSet<UserAccess> UserAccesses { get; set; }
        public virtual DbSet<Participant> Participants { get; set; }
        public virtual DbSet<Certificate> Certificates { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<SnackEvent> SnackEvents { get; set; }
        public virtual DbSet<Attendance> Attendances { get; set; }
    }
}