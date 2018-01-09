using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.Collections.Generic;
using SalesOrder_Bot.Controllers;

namespace SalesOrder_Bot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        private string userid;

        private static List<CompanyidDetailsController> returnData;
        private static int itemCount;
        private static List<OrderDetailsController> Detail;

        private static string s1;
        private static string s2;
        private static string s3;
        private static string s4;

        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;


            // return our reply to the user
            await context.PostAsync($"Enter the userId\U0001F600");
            context.Wait(getuseridAsync);


        }

        private async Task getuseridAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            userid = activity.Text;

            GetDataController data = new GetDataController();
            returnData = data.get(userid);
            itemCount = data.getItems();
            if (itemCount == 0)
            {

                await context.PostAsync("Enter the correct userId!!!\U0001F61F\U0001F61F\U0001F61F\U0001F61F");


                context.Wait(getuseridAsync);
            }
            else
            {

                var message = context.MakeMessage();
                var attachment = attach1();
                message.Attachments.Add(attachment);
                await context.PostAsync(message);


                // return our reply to the user
                //await context.PostAsync($"Enter the companyid");
                context.Wait(GetcomidAsync);

            }
        }

        private async Task GetcomidAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;


            if (activity.Text.ToString().Contains("OrderBook Details"))
            {
                GetDataController getdata = new GetDataController();





                returnData = getdata.get(userid);
                itemCount = getdata.getItems();


                await context.PostAsync($"" + itemCount + "");



                for (int i = 0; i < itemCount; i++)
                {


                    s1 = returnData[i].COMPANYID.ToString();



                    var message = context.MakeMessage();
                    var attachment = attach2();
                    message.Attachments.Add(attachment);
                    await context.PostAsync(message);








                }
                context.Wait(SummaryAsync);
            }
        }
        private async Task SummaryAsync(IDialogContext context, IAwaitable<object> res)
        {
            var activity = await res as Activity;
            if (activity.Text.ToString().Contains(s1))
            {
                SummaryDetailController summary = new SummaryDetailController();





                Detail = summary.getfurther(userid, s1);
                itemCount = summary.getItems();


                await context.PostAsync($"" + itemCount + "");



                for (int i = 0; i < itemCount; i++)
                {


                    s2 = Detail[i].ORDERQUANTITY.ToString();
                    s3 = Detail[i].TOTALCM.ToString();
                    s4 = Detail[i].TOTALFOB.ToString();


                    var selectedCard = await res;
                    var message = context.MakeMessage();
                    var attachment = attach3();
                    message.Attachments.Add(attachment);
                    await context.PostAsync(message);
                }
            }
        }
        private static Attachment attach1()
        {

            var re = new HeroCard
            {
                Buttons = new List<CardAction>
                {
                    new CardAction(ActionTypes.ImBack,"OrderBook Details",value:"OrderBook Details"),
                    new CardAction(ActionTypes.ImBack,"Invoice Details",value:"Invoice Details"),
                },

            };
            return re.ToAttachment();
        }






        private static Attachment attach2()
        {








            var hero= new HeroCard
            {

                Title = $"Sales Order Details",


               Images = new List<CardImage>
                {
                 new CardImage(url:"https://thumb9.shutterstock.com/display_pic_with_logo/701173/108824516/stock-photo-cloud-computing-concept-isolated-on-white-background-d-rendered-108824516.jpg"),
                },
                Buttons = new List<CardAction> {
                new CardAction(ActionTypes.ImBack, s1,value:s1),
                new CardAction(ActionTypes.ImBack,"Thank you",value:"\U0001F642") },
            };











            return hero.ToAttachment();
        }

        private static Attachment attach3()
        {
        var re3 = new HeroCard
            {
                Title = $"OrderBook Details for " + s1,
                //Facts = new List<Fact> {
                //   new Fact("ORDERQUANTITY",$""+s2+""),

                //new Fact("TOTALCM", $""+s3+""),
                //new Fact("TOTALFOB", $""+s4+""),
                //},
                Text= ("Total CM from Invoice Details" + s2 + "<br /> Total FOB from Invoice Details" + s3 + "<br />Order Quantity from Invoice Details" + s4),

                Images = new List<CardImage>
                {
                  new CardImage("https://thumb9.shutterstock.com/display_pic_with_logo/701173/108824516/stock-photo-cloud-computing-concept-isolated-on-white-background-d-rendered-108824516.jpg"),
                },
                Buttons = new List<CardAction> {

                new CardAction(ActionTypes.ImBack,"Thank you",value:"\U0001F642") },
            };
            return re3.ToAttachment();


        }
    }
}