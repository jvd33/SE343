using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using KennUTicket.Models;

namespace KennUTicket.DAL
{
    public class TicketInitializer : DropCreateDatabaseIfModelChanges<TicketContext>
    {
        protected override void Seed(TicketContext context)
        {

        }
    }
}