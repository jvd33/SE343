using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KennUTicket.Models;
using KennUTicket.DAL;
using KennUTicket.Extensions;

namespace KennUTicket.API
{
    public class APIController : ApiController
    {
        private TicketContext db;
        public APIController()
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

        [HttpPost]
        public IHttpActionResult AcceptProductReceived(int TicketID)
        {
   
            try
            {
                var ticket = GetTicket(TicketID);
                ticket = ticket.InitiateRefundTicket();
                return Ok(ticket.Status.StatusName);
            } catch(HttpResponseException ex)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IHttpActionResult ConfirmProductRefunded(int TicketID)
        {
            try
            {
                var ticket = GetTicket(TicketID);
                ticket = ticket.CompleteRefundTicket();
                return Ok(ticket.Status.StatusName);

            } catch(HttpResponseException ex)
            {
                return NotFound();
            }
        }

    }
}
