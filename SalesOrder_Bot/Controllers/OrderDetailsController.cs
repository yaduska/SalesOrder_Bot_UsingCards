using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SalesOrder_Bot.Controllers
{
    public class OrderDetailsController : ApiController
    {
        public string ORDERQUANTITY { get; set; }
        public string TOTALCM { get; set; }
        public string TOTALFOB { get; set; }
    }
}
