using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KennUTicket.Models;
using KennUTicket.DAL;

namespace KennUTicket.API
{
    public class KennUTicketController : ApiController
    {
        private TicketContext db;
        public KennUTicketController()
        { 
            db = new TicketContext();
        }

        private Ticket GetTicket(int TicketID)
        {
            Ticket ticket = db.Tickets.FirstOrDefault(c => c.ID == TicketID);
            if (ticket == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return ticket;
        }

        public IHttpActionResult AcceptProductReceived(int TicketID)
        {
   
            try
            {

                return Ok();
            } catch(HttpResponseException ex)
            {
                return NotFound();
            }
        }

        public IHttpActionResult ConfirmProductRefunded(int TicketID)
        {
            try
            {
                var ticket = GetTicket(TicketID);
                return Ok();

            } catch(HttpResponseException ex)
            {
                return NotFound();
            }
        }

    }
}
