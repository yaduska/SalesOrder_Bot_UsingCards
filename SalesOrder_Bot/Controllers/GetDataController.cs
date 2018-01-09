using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Data;

namespace SalesOrder_Bot.Controllers
{
    public class GetDataController : ApiController
    {
        string URL_UserId;
        string URL_OrderDetail;
        List<CompanyidDetailsController> returnList = new List<CompanyidDetailsController>();
        //List<OrderDetailsController> list = new List<OrderDetailsController>();
        public List<CompanyidDetailsController> get(string userid)
        {



            URL_UserId = (@"http://hgerpmisdashboard.azurewebsites.net/api/BOT/GetCpmanyId?UserId=" + userid);


            try
            {
                // Get HTML data 
                using (WebClient webClient = new WebClient())

                {

                    WebClient a = new WebClient();


                    var js1 = a.DownloadString(URL_UserId);




                    DataSet data1 = JsonConvert.DeserializeObject<DataSet>(js1);



                    CompanyidDetailsController sales = new CompanyidDetailsController();
                    int x = getItems();
                    foreach (DataRow dr in data1.Tables[x].Rows)

                    {


                        //DataTable table = ds.Tables[0];
                        //var c = table.Rows.Find(companyID);
                        //var b = table.Rows.Find(salesOrderID);
                        //if (c == b)
                        //{

                        CompanyidDetailsController item = new CompanyidDetailsController();







                        item.COMPANYID = dr["COMPANYID"].ToString();







                        returnList.Add(item);

                    }



                    return returnList;

                }

            }



            //while (str != null)
            //{
            //    Console.WriteLine(str);
            //    str = reader.ReadLine();
            //}

            catch (WebException exp)
            {
                Console.WriteLine(exp);
            }


            return returnList;
        }

        public int getItems()
        {
            return returnList.Count();
        }
    }
}
        //public List<OrderDetailsController> getfurther(string userid, string comid)
        //{
        //    URL_OrderDetail = (@"http://hgerpmisdashboard.azurewebsites.net/api/BOT/GetOrderBook?UserId="+userid+"&CompanyId="+comid);
        //    try
        //    {
        //        // Get HTML data 
        //        using (WebClient webClient = new WebClient())

        //        {

        //            WebClient b = new WebClient();

        //            var js2 = b.DownloadString(URL_OrderDetail);



        //            DataSet data2 = JsonConvert.DeserializeObject<DataSet>(js2);



        //            foreach (DataRow dr in data2.Tables[0].Rows)

        //            {
        //                //SalesDetailsController sales = new SalesDetailsController();
        //                //DataTable table = ds.Tables[0];
        //                //var a = table.Rows.Find(comid);
        //                //var b = table.Rows.Find(siteid);
        //                //if (a == b) {

        //                OrderDetailsController order = new OrderDetailsController();



                        

        //                    order.ORDERQUANTITY = dr["OrderQty"].ToString();

        //                    order.TOTALCM = dr["Total FOB"].ToString();

        //                    order.TOTALFOB = dr["Total CM"].ToString();






        //                    list.Add(order);

        //                }
        //        }



        //        return list;

                



        //    }
        //    catch (WebException exp)
        //    {
        //        Console.WriteLine(exp);
        //    }

           
        //    return list;
        //}}
         
        //}
        
    


   