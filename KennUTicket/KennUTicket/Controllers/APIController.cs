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
            return ticket;
        }

        private Ticket GetTicketBySKU(string SKU)
        {
            Ticket ticket = db.Tickets.FirstOrDefault(c => c.ProductID == SKU);
            return ticket;
        }

        [HttpGet]
        public IHttpActionResult AcceptProductReceived(int TicketID)
        {
   
            try
            {
                var ticket = GetTicket(TicketID);
                if(ticket == null)
                {
                    return NotFound();
                }
                ticket = ticket.InitiateRefundTicket();
                return Ok(new { ID = ticket.ID, status = ticket.Status.StatusName, SKU = ticket.ProductID, orderNumber = ticket.OrderNumber, title = ticket.Title });
            } catch(HttpResponseException ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        public IHttpActionResult CompleteRefund(int TicketID)
        {
            try
            {
                var ticket = GetTicket(TicketID);
                if (ticket == null)
                {
                    return NotFound();
                }
                ticket = ticket.CompleteRefundTicket();
                return Ok(new { ID = ticket.ID, status = ticket.Status.StatusName, SKU = ticket.ProductID, orderNumber = ticket.OrderNumber, title = ticket.Title });
            }
            catch (HttpResponseException ex)
            {
                return InternalServerError(ex);
            }
        }

    }
}
