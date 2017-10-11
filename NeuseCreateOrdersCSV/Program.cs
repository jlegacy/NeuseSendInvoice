using System;
using System.Collections.Generic;

using System.Data;
using System.Net;
using System.Net.Security;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using LINQtoCSV;
using NeuseCreateOrdersCSV.Properties;
using System.Net.Http;
using Newtonsoft.Json;
using DataRow = System.Data.DataRow;
using System.Net.Http;
using System.Threading.Tasks;



namespace NeuseCreateOrdersCSV
{
    internal class Program
    {


        private static void Main(string[] args)
        {
            Console.WriteLine("starting!!");

            CsvContext cc = new CsvContext();

            CsvFileDescription outputFileDescription = new CsvFileDescription
            {
                QuoteAllFields = false,
                SeparatorChar = ',', // tab delimited
                FirstLineHasColumnNames = true,
                FileCultureName = "nl-NL" // language/country code of The Netherlands
            };

            List<ProcData> procData = new List<ProcData>();

            DataSet myData = null;

            if (Settings.Default.InitialLoad == "Y")
            {
                myData = GetProcData.PopulateAllOrdersList();
            }
            else
            {
                myData = GetProcData.PopulateProcList();
            }

            foreach (DataTable table in myData.Tables)
            {
                //bool haveRecords = false;
                //foreach (DataRow dr in table.Rows)
                //{
                //    haveRecords = true;
                //    procData.Add(new ProcData { OrderID = dr["ordId"].ToString(), OrderName = dr["ordName"].ToString() });
                //    GetProcData.UpdateProcRecordProcesses(dr["ordId"].ToString());
                //}

                //if (haveRecords)
                //{
                //    string fileName = Settings.Default.CSVOrderPath + "order_" + GenerateId() + ".csv";
                //    //get unique name//
                //    cc.Write(
                //    procData,
                //    fileName,
                //    outputFileDescription);
                //}

            }

           // var t = new Task(HTTP_GET);
           // t.Start();
            var t = new Task(HTTP_POST);
            t.Start();
            Console.ReadLine();
          
        }

        static async void HTTP_GET()
        {
            string TARGETURL;

            TARGETURL = "http://192.168.1.6:2121/datasnap/rest/TServerMethodsWebAPI/";

            HttpClientHandler handler = new HttpClientHandler()
            {
                Proxy = new WebProxy("http://127.0.0.1:8888"),
                UseProxy = false,
            };

            Console.WriteLine("GET: + " + TARGETURL);

            // ... Use HttpClient.            
            HttpClient client = new HttpClient(handler);

            var byteArray = Encoding.ASCII.GetBytes("WEBAPI:SgtEH3bcaaUhMdbx");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            HttpResponseMessage response = await client.GetAsync(TARGETURL + "WebAPI_Connect");
            HttpContent content = response.Content;

            // ... Check Status Code                                
            Console.WriteLine("Response StatusCode: " + (int)response.StatusCode);

            // ... Read the string.
            string result = await content.ReadAsStringAsync();

            // ... Display the result.
            if (result != null &&
            result.Length >= 50)
            {
                Console.WriteLine(result.Substring(0, 50) + "...");
            }
        }

        static async void HTTP_POST()
        {
            string TARGETURL;

            TARGETURL = "http://192.168.1.6:2121/datasnap/rest/TServerMethodsWebAPI/";

            HttpClientHandler handler = new HttpClientHandler()
            {
                Proxy = new WebProxy("http://127.0.0.1:8888"),
                UseProxy = false,
            };

            Console.WriteLine("Posting Invoice: + " + TARGETURL);

            // ... Use HttpClient.            
            HttpClient client = new HttpClient(handler);

            var byteArray = Encoding.ASCII.GetBytes("WEBAPI:SgtEH3bcaaUhMdbx");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));


            SendInvoice myInvoice = new SendInvoice();
            myInvoice.InvoiceHeader = new InvoiceHeader();
            myInvoice.InvoiceHeader.InvoiceType = "W";
            myInvoice.InvoiceHeader.InvoiceCustomer = 125;
            myInvoice.InvoiceHeader.InvoiceDate = new DateTime();
            myInvoice.InvoiceHeader.InvoiceComment = "testing";

            myInvoice.InvoiceLines = new List<InvoiceLines>();
            myInvoice.InvoiceLines.Add( new InvoiceLines { PartUnique = 1, Ordered = 0, Price = 0, Description = "test" });
            myInvoice.InvoiceLines.Add(new InvoiceLines { PartUnique = 2, Ordered = 0, Price = 0, Description = "test2" });
           
            myInvoice.InvoiceTenders = new List<InvoiceTenders>();
            myInvoice.InvoiceTenders.Add(new InvoiceTenders { Amount = 0, Type = "test"});
            myInvoice.InvoiceTenders.Add(new InvoiceTenders { Amount = 0, Type = "test2" });

            myInvoice.InvoiceShipping = new InvoiceShipping();
            myInvoice.InvoiceShipping.Address = "test";

            myInvoice.InvoiceBilling = new InvoiceBilling();
            myInvoice.InvoiceBilling.Address = "test";

            string jsonString = JsonConvert.SerializeObject(
           myInvoice,
           Formatting.None,
           new JsonSerializerSettings()
           {
               ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
           });




            HttpResponseMessage response = await client.PostAsJsonAsync(TARGETURL + "Insert_Full_Invoice", myInvoice);
            HttpContent content = response.Content;

            // ... Check Status Code                                
            Console.WriteLine("Response StatusCode: " + (int)response.StatusCode);

            // ... Read the string.
            string result = await content.ReadAsStringAsync();

            // ... Display the result.
            if (result != null &&
            result.Length >= 50)
            {
                Console.WriteLine(result.Substring(0, 50) + "...");
            }
        }

        public static string GenerateId()
        {
            return Guid.NewGuid().ToString("N");
        }

    }
}
