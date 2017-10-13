using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KennUTicket.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace KennUTicket.DAL
{
    public class TicketContext : DbContext
    {
        public TicketContext() : base("KennUTicket_Data")
        {
            Database.SetInitializer(new TicketInitializer());
        }
        public enum TicketPriority { Low=0, Medium=1, High=2 };
        public enum TicketCategory { Hardware=0, Software=1, Complaint=2, Other=3 };
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<TicketStatus> TicketStatuses { get; set; }
        public DbSet<TicketEvent> TicketEvents { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}