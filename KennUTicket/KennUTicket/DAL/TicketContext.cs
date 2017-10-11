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
        public TicketContext() : base("TicketContext")
        {

        }

        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<TicketStatus> TicketStatuses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}