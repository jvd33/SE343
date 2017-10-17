using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KennUTicket.API
{
    /* 
     *This class is based on our understanding of our external requirements and diagrams between the dev coordinators. 
     *It serves to accept our HTTP requests like other team's silos would for testing purposes
    */
    public class TestIntegrationController : ApiController
    {
        [HttpPost]
        public IHttpActionResult GetOrder(string orderNumber)
        {
            return this.Ok("Order retrieved");
        }

        [HttpPost]
        public IHttpActionResult RefundOrder(string orderNumber)
        {
            return this.Ok("Refund request accepted!");
        }

        [HttpPost]
        public IHttpActionResult OrderRequest(string orderNumber)
        {
            return this.Ok("Order information goes here");

        }

    }
}
