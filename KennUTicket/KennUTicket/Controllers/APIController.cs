using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KennUTicket.Models;
using KennUTicket.DAL;
using KennUTicket.Extensions;
using System.Web.Http.Cors;

namespace KennUTicket.API
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
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

        private Ticket GetTicketBySKU(string SKU)
        {
            Ticket ticket = db.Tickets.FirstOrDefault(c => c.ProductID == SKU);
            if (ticket == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return ticket;
        }

        [HttpGet]
        public IHttpActionResult AcceptProductReceived(string ProductID)
        {
   
            try
            {
                var ticket = GetTicketBySKU(ProductID);
                ticket = ticket.InitiateRefundTicket();
                return Ok("Product received, status updated: " + ticket.Status.StatusName);
            } catch(HttpResponseException ex)
            {
                return NotFound();
            }
        }

        [HttpGet]
        public IHttpActionResult CompleteRefund(int TicketID)
        {
            try
            {
                var ticket = GetTicket(TicketID);
                ticket = ticket.CompleteRefundTicket();
                return Ok("Refund completed, status updated: " + ticket.Status.StatusName);

            } catch(HttpResponseException ex)
            {
                return NotFound();
            }
        }

    }
}
