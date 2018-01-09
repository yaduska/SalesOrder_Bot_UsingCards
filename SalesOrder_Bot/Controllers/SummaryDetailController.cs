using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SalesOrder_Bot.Controllers
{
    public class SummaryDetailController : ApiController
    {
        List<OrderDetailsController> list = new List<OrderDetailsController>();
        string URL_OrderDetail;

        public List<OrderDetailsController> getfurther(string userid, string comid)
        {
            URL_OrderDetail = (@"http://hgerpmisdashboard.azurewebsites.net/api/BOT/GetOrderBook?UserId=" + userid + "&CompanyId=" + comid);
            try
            {
                // Get HTML data 
                using (WebClient webClient = new WebClient())

                {

                    WebClient b = new WebClient();

                    var js2 = b.DownloadString(URL_OrderDetail);



                    DataSet data2 = JsonConvert.DeserializeObject<DataSet>(js2);



                    foreach (DataRow dr in data2.Tables[0].Rows)

                    {
                        //SalesDetailsController sales = new SalesDetailsController();
                        //DataTable table = ds.Tables[0];
                        //var a = table.Rows.Find(comid);
                        //var b = table.Rows.Find(siteid);
                        //if (a == b) {

                        OrderDetailsController order = new OrderDetailsController();





                        order.ORDERQUANTITY = dr["OrderQty"].ToString();

                        order.TOTALCM = dr["Total FOB"].ToString();

                        order.TOTALFOB = dr["Total CM"].ToString();






                        list.Add(order);

                    }
                }



                return list;





            }
            catch (WebException exp)
            {
                Console.WriteLine(exp);
            }


            return list;
        }
        public int getItems()
        {
            return list.Count();
        }
    }

}
        
    

