using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace KennUTicket.API
{
    /* 
     *This class is based on our understanding of our external requirements and diagrams between the dev coordinators. 
     *It serves to accept our HTTP requests like other team's silos would for testing purposes
    */

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TestIntegrationController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetOrder(string orderNumber)
        {
            return this.Ok("Get Order Result");
        }

        [HttpGet]
        public IHttpActionResult Refund(string orderNumber)
        {
            return this.Ok("Refund request confirmed");
        }

        [HttpGet]
        public IHttpActionResult OrderRequest(string productId, int quantity, string type="refurbish")
        {
            return this.Ok("Order information goes here");

        }

    }
}
