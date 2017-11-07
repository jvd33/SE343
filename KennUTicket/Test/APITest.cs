using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using System.Configuration;
using System.Web;
using System.Collections.Specialized;

namespace Test
{
    [TestFixture]
    public class APITest
    {

        private HttpClient APIClient;
        private HttpClient TestClient;
        private NameValueCollection ValidTest;
        private NameValueCollection InvalidTest;
        private NameValueCollection ValidAPI;
        private NameValueCollection InvalidAPI;
        private NameValueCollection ValidOrder;
        private NameValueCollection InvalidOrder;

        private List<String> endpoints = new List<String> { "/test/", "/api/" };


        [SetUp]
        public void SetUp()
        {
            APIClient = new HttpClient();
            TestClient = new HttpClient();
            Uri api_uri = new Uri("http://vm343g.se.rit.edu/api/");
            APIClient.BaseAddress = api_uri;
            TestClient.BaseAddress = new Uri("http://vm343g.se.rit.edu/test/");

            ValidTest = new NameValueCollection() {
                { "orderNumber", "1" }
             };

            InvalidTest = new NameValueCollection() {
             };

            ValidAPI = new NameValueCollection() {
                { "ticketID", "1" }
             };

            InvalidAPI = new NameValueCollection() {
                { "ticket_ID", null }
             };

            ValidOrder = new NameValueCollection() {
                { "productId", "1" },
                { "quantity", "1" },
                { "t", "refurbish" }
             };

            InvalidOrder = new NameValueCollection() {
                { "product_Id", null },
                { "quantity", null },
                { "t", null }
             };
        }


        [Test]
        public async Task GetOrderIsSuccess()
        {
            string queryString = String.Join("&",
                ValidTest.AllKeys.Select(a => a + "=" + HttpUtility.UrlEncode(ValidTest[a])));
            var response = TestClient.GetAsync(String.Format("getorder?{0}", queryString)).Result;
            var res = await response.Content.ReadAsStringAsync();
            TestContext.Write(res);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            TestContext.Write(res);
            Assert.AreEqual(res, "\"Get Order Result\"");
            await Task.Delay(0);
        }

        [Test]
        public async Task RefundIsSuccess()
        {
            string queryString = String.Join("&",
                ValidTest.AllKeys.Select(a => a + "=" + HttpUtility.UrlEncode(ValidTest[a])));
            var response = TestClient.GetAsync(String.Format("refund?{0}", queryString)).Result;
            var res = await response.Content.ReadAsStringAsync();
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            TestContext.Write(res);
            Assert.AreEqual(res, "\"Refund request confirmed\"");
            await Task.Delay(0);
        }


        [Test]
        public async Task GetOrderInfoIsSuccess()
        {
            string queryString = String.Join("&",
                ValidOrder.AllKeys.Select(a => a + "=" + HttpUtility.UrlEncode(ValidOrder[a])));
            var response = TestClient.GetAsync(String.Format("orderrequest?{0}", queryString)).Result;
            var res = await response.Content.ReadAsStringAsync();
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            TestContext.Write(res);
            Assert.AreEqual(res, "\"Order information goes here\"");
            await Task.Delay(0);
        }

        [Test]
        public async Task AcceptProductReceivedIsSuccess()
        {
            string queryString = String.Join("&",
                ValidAPI.AllKeys.Select(a => a + "=" + HttpUtility.UrlEncode(ValidAPI[a])));
            var response = APIClient.GetAsync(String.Format("acceptproductreceived?{0}", queryString)).Result;
            var res = await response.Content.ReadAsStringAsync();
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            TestContext.Write(res);
            Assert.AreEqual(res, "\"Product received, status updated: Refurbish in progress\"");
            await Task.Delay(0);

        }

        [Test]
        public async Task CompleteRefundIsSuccess()
        {
            string queryString = String.Join("&",
                ValidAPI.AllKeys.Select(a => a + "=" + HttpUtility.UrlEncode(ValidAPI[a])));
            var response = APIClient.GetAsync(String.Format("completerefund?{0}", queryString)).Result;
            var res = await response.Content.ReadAsStringAsync();
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            TestContext.Write(res);
            Assert.AreEqual(res, "\"Refund completed, status updated: Refurbish successful\"");
            await Task.Delay(0);
        }




        [Test]
        public async Task GetOrderIsNotFound()
        {
            string queryString = String.Join("&",
                InvalidTest.AllKeys.Select(a => a + "=" + HttpUtility.UrlEncode(InvalidTest[a])));
            var response = TestClient.GetAsync(String.Format("getorder?{0}", queryString)).Result;
            var res = await response.Content.ReadAsStringAsync();
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NotFound);
            await Task.Delay(0);
        }

        [Test]
        public async Task RefundIsNotFound()
        {
            string queryString = String.Join("&",
                InvalidTest.AllKeys.Select(a => a + "=" + HttpUtility.UrlEncode(InvalidTest[a])));
            var response = TestClient.GetAsync(String.Format("refund?{0}", queryString)).Result;
            var res = await response.Content.ReadAsStringAsync();
            TestContext.Write(res);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NotFound);
            await Task.Delay(0);
        }


        [Test]
        public async Task GetOrderInfoIsNotFound()
        {
            string queryString = String.Join("&",
                InvalidOrder.AllKeys.Select(a => a + "=" + HttpUtility.UrlEncode(InvalidOrder[a])));
            var response = TestClient.GetAsync(String.Format("orderrequest?{0}", queryString)).Result;
            var res = await response.Content.ReadAsStringAsync();
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NotFound);
            await Task.Delay(0);
        }

        [Test]
        public async Task AcceptProductReceivedIsNotFound()
        {
            string queryString = String.Join("&",
                InvalidAPI.AllKeys.Select(a => a + "=" + HttpUtility.UrlEncode(InvalidAPI[a])));
            var response = APIClient.GetAsync(String.Format("acceptproductreceived?{0}", queryString)).Result;
            var res = await response.Content.ReadAsStringAsync();
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NotFound);
            await Task.Delay(0);

        }

        [Test]
        public async Task CompleteRefundIsNotFound()
        {
            string queryString = String.Join("&",
                InvalidAPI.AllKeys.Select(a => a + "=" + HttpUtility.UrlEncode(InvalidAPI[a])));
            var response = APIClient.GetAsync(String.Format("completerefund?{0}", queryString)).Result;
            var res = await response.Content.ReadAsStringAsync();
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NotFound);
            await Task.Delay(0);
        }
    }
}
