using PaylerAPI;
using System;
using System.Net.Http;


namespace PaylerClient
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Console.WriteLine("Payler client started");

            string Server, Port, Readline;
            
            HttpClient webClient = new HttpClient();
            do
            {
                Console.Write(Environment.NewLine + "Enter server address(empty string - localhost):");
                Readline = Console.ReadLine();
                Server = (Readline.Length == 0) ? "localhost" : Readline;

                Console.Write(Environment.NewLine+ "Enter server port(empty string - 49497):");
                Readline = Console.ReadLine();
                Port = (Readline.Length == 0) ? "49497" : Readline;

                webClient.BaseAddress = new Uri("http://" + Server + ":" + Port + "/");
                webClient.DefaultRequestHeaders.Accept.Clear();
                webClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                
            }
            while (!IsAlive(webClient));

            int MenuItem;
            bool Fin = false;
            int Id;
            do
            {
                DrawMenu();
                MenuItem = int.Parse(Console.ReadLine());

                switch (MenuItem)
                {
                    case 1:
                        RawPayData RawPay = new RawPayData();
                        Console.Write(Environment.NewLine+"Enter order number:");
                        RawPay.Order_ID = int.Parse(Console.ReadLine());
                        Console.Write(Environment.NewLine + "Enter amount(kop):");
                        RawPay.Amount_Kop = long.Parse(Console.ReadLine());
                        Console.Write(Environment.NewLine + "Enter card number:");
                        RawPay.Card_Number = Console.ReadLine();
                        Console.Write(Environment.NewLine + "Enter cardholder name:");
                        RawPay.Cardholder_Name = Console.ReadLine();
                        Console.Write(Environment.NewLine + "Enter expiry month:");
                        RawPay.Expiry_Month = byte.Parse(Console.ReadLine());
                        Console.Write(Environment.NewLine + "Enter expiry year:");
                        RawPay.Expiry_Year = short.Parse(Console.ReadLine());
                        Console.Write(Environment.NewLine + "Enter CVV:");
                        RawPay.CVV = short.Parse(Console.ReadLine());

                        Pay(webClient,RawPay);
                        break;
                    case 2:
                        Console.WriteLine("Enter pay number:");
                        Id = Convert.ToInt32(Console.ReadLine());
                        GetStatus(webClient, Id);
                        break;
                    case 3:
                        Console.WriteLine("Enter pay number:");
                        Id = Convert.ToInt32(Console.ReadLine());
                        Refund(webClient, Id);
                        break;
                    case 4:
                        Fin = true;
                        break;
                    default:
                        Console.WriteLine("one more time please...");
                        break;
                }
            } while (!Fin);

                Console.WriteLine("Payler client stopped. Press any key to end...");
            Console.ReadKey();
        }
        
        private static void Pay(HttpClient httpclient, RawPayData RawPay)
        {
            HttpResponseMessage res = httpclient.PostAsJsonAsync("api/Pay", RawPay).Result;
            if (res.IsSuccessStatusCode)
            {
                Statuses.PaymentStatus stat = res.Content.ReadAsAsync<Statuses.PaymentStatus>().Result;
                Console.WriteLine("Pay status code - " + stat);
            }
        }
        
        private static void Refund(HttpClient httpclient, int id)
        {
            HttpResponseMessage res = httpclient.GetAsync("api/Refund/" + id).Result;
            if (res.IsSuccessStatusCode)
            {
                Statuses.RefundStatus stat = res.Content.ReadAsAsync<Statuses.RefundStatus>().Result;
                Console.WriteLine("Refund status code - " + stat);
            }
        }
        
        private static void GetStatus(HttpClient httpclient, int id)
        {
            HttpResponseMessage res =  httpclient.GetAsync("api/GetStatus/" + id).Result;
            if (res.IsSuccessStatusCode)
            {
                Statuses.TransactionStatus stat = res.Content.ReadAsAsync<Statuses.TransactionStatus>().Result;
                Console.WriteLine("Transaction status code - " + stat);
            }
        }
        
        private static void DrawMenu()
        {
            Console.WriteLine(Environment.NewLine + "Main menu" + Environment.NewLine + "1.Pay");
            Console.WriteLine("2.Get status" + Environment.NewLine + "3.Refund" + Environment.NewLine + "4.Quit");
        }
        
        private static bool IsAlive(HttpClient httpclient)
        {
            HttpResponseMessage res = httpclient.GetAsync("api/Alive").Result;
            if (res.IsSuccessStatusCode)
            {
                string tag = res.Content.ReadAsAsync<string>().Result;
                Console.WriteLine(tag);
                return true;
            }
            else
            {
                Console.WriteLine("No connection. Wrong IP/port?");
                return false;
            }
        }
    }
}
