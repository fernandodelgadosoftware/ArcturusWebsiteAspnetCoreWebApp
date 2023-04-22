using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace ArcturusWebsiteAspnetCoreWebApp.Pages
{
    public class CryptoModel : PageModel
    {
        public void OnGet()
        {
            
        }

        public string BtcPrice()
        {
            string priceTicker = DataFromApi();
            string decimalVar = DeserialTicker(priceTicker);
            decimal btcPrice;
            decimal.TryParse(decimalVar, out btcPrice);
            decimal btcPriceRound = decimal.Round(btcPrice, 2, MidpointRounding.AwayFromZero);
            string dollarValue = string.Format("{0:C2}", btcPrice);

            //return btcPriceRound;
            return dollarValue;
        }

        public static string DeserialTicker(string priceTicker)
        {
            string myJsonResponse = priceTicker;

            if (myJsonResponse != null)
            {
                dynamic? root = JsonConvert.DeserializeObject<dynamic>(myJsonResponse);
                string rate = root["bpi"]["USD"]["rate"];
                string lastUpdated = root["time"]["updated"];
                //Console.WriteLine(rate);
                //Console.WriteLine(updated);
                //Console.WriteLine(root.chartName);
                //Console.WriteLine(root.disclaimer);

                return rate;
                //return lastUpdated;
                //return root.chartName;
                //return root.disclaimer;
            }
            else
            {
                return "error";
            }
        }
        public static string DataFromApi()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.coindesk.com/v1/bpi/currentprice.json");
                //HTTP GET
                var responseTask = client.GetAsync(client.BaseAddress);
                responseTask.Wait();

                var result = responseTask.Result;

                Console.WriteLine(result);


                if (result.IsSuccessStatusCode)
                {

                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    string priceTicker = readTask.Result;

                    return priceTicker;
                }
                else
                {
                    string priceTicker = "error";
                    return priceTicker;
                }
            }
        }
    }
}

        


