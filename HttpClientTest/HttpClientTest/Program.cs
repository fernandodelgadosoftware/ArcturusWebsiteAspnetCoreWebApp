using Newtonsoft.Json;


namespace HttpClientDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            string myJsonResponse = DataFromApi();
            
            

            if (myJsonResponse != null)
            {
                
                //Bpi bpi = JsonConvert.DeserializeObject<Bpi>(myJsonResponse);
                //USD usd = JsonConvert.DeserializeObject<USD>(myJsonResponse);
                //Time time = JsonConvert.DeserializeObject<Time>(myJsonResponse);
                dynamic root = JsonConvert.DeserializeObject<dynamic>(myJsonResponse);
                string rate = root["bpi"]["USD"]["rate"];
                string updated = root["time"]["updated"];
                Console.WriteLine(rate);
                Console.WriteLine(updated);
                Console.WriteLine(root.chartName);
                Console.WriteLine(root.disclaimer);
                //Console.WriteLine(usd.description);
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("damnit");
            }
            //using (var client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri("https://api.coindesk.com/v1/bpi/currentprice.json");
            //    //HTTP GET
            //    var responseTask = client.GetAsync(client.BaseAddress);
            //    responseTask.Wait();

            //    var result = responseTask.Result;

            //    Console.WriteLine(result);


            //    if (result.IsSuccessStatusCode)
            //    {

            //        var readTask = result.Content.ReadAsStringAsync();
            //        readTask.Wait();

            //        var priceTicker = readTask.Result;

            //        Console.WriteLine(priceTicker);
            //        Console.ReadLine();

            //    }
            //}
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

                    var priceTicker = readTask.Result;

                    Console.WriteLine(priceTicker);
                    Console.ReadLine();
                    return priceTicker;
                }
                else
                {
                    string priceTicker = "error";
                    return priceTicker;
                }
                 
            }
            
        }

        public class Time
        {
            public string updated { get; set; }
            public DateTime updatedISO { get; set; }
            public string updateduk { get; set; }
        }

        public class USD
        {
            public string code { get; set; }
            public string symbol { get; set; }
            public string rate { get; set; }
            public string description { get; set; }
            public double rate_float { get; set; }
        }

        public class GBP
        {
            public string code { get; set; }
            public string symbol { get; set; }
            public string rate { get; set; }
            public string description { get; set; }
            public double rate_float { get; set; }
        }

        public class EUR
        {
            public string code { get; set; }
            public string symbol { get; set; }
            public string rate { get; set; }
            public string description { get; set; }
            public double rate_float { get; set; }
        }

        public class Bpi
        {
            public USD USD { get; set; }
            public GBP GBP { get; set; }
            public EUR EUR { get; set; }
        }

        public class Root
        {
            public string time { get; set; }
            public string disclaimer { get; set; }
            public string chartName { get; set; }
            public Bpi bpi { get; set; }
        }
    }
}