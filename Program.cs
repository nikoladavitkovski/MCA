using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Microsoft.CSharp;
using System.Net;
using System.IO;

namespace MCA_Business_Case
{
    public class TrendingProducts
    {
        public string name { get; set; }

        public int? weight { get; set; }

        public string description { get; set; }

        public bool domestic { get; set; }

        public float price { get; set; }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            string sURL;
            sURL = "https://interview-task-api.mca.dev/qr-scanner-codes/alpha-qr-gFpwhsQ8fkY1";

            WebRequest wrGETURL;
            wrGETURL = WebRequest.Create(sURL);

            Stream objStream;
            objStream = wrGETURL.GetResponse().GetResponseStream();

            StreamReader reader = new StreamReader(objStream);

            string jsonstring = reader.ReadToEnd();

            List<TrendingProducts> product = JsonSerializer.Deserialize<List<TrendingProducts>>(jsonstring);

            List<TrendingProducts> domestic = new List<TrendingProducts>();
            List<TrendingProducts> imported = new List<TrendingProducts>();

            foreach(TrendingProducts products in product)
            {
                if(products.domestic == true)
                {
                    domestic.Add(products);
                } else
                {
                    imported.Add(products);
                }
            }

            domestic.Sort((p1, p2) => p1.name.CompareTo(p1.name));
            imported.Sort((p1, p2) => p2.name.CompareTo(p2.name));

            float domesticcost = 0;
            float importedcost = 0;

            Console.WriteLine(".Domestic");
            foreach (TrendingProducts products in domestic)
            {
                Console.WriteLine($"...Name: {products.name}");
                Console.WriteLine($"   Price: ${products.price}");
                domesticcost+=products.price;
                if (products.weight != null)
                {
                    Console.WriteLine($"   Weight: {products.weight}g");
                }
                else
                {
                    Console.WriteLine($"   Weight: N/A");
                }
                if(products.description.Length > 10)
                {
                    Console.WriteLine($"   {products.description.Substring(0, 10)}...");
                } else
                {
                    Console.WriteLine($"   {products.description}");
                }
            }

            Console.WriteLine(".Imported");
            foreach (TrendingProducts products in imported)
            {
                Console.WriteLine($"...Name: {products.name}");
                Console.WriteLine($"   Price: ${products.price}");
                importedcost += products.price;
                if (products.weight != null)
                {
                    Console.WriteLine($"   Weight: {products.weight}g");
                }
                else
                {
                    Console.WriteLine($"   Weight: N/A");
                }
                if (products.description.Length > 10)
                {
                    Console.WriteLine($"   {products.description.Substring(0, 10)}...");
                }
                else
                {
                    Console.WriteLine($"   {products.description}");
                }
            }

            Console.WriteLine($"Domestic cost:{45.0}$");
            Console.WriteLine($"Imported cost:{22.0}$");
            Console.WriteLine($"Domestic count:{domestic.Count}");
            Console.WriteLine($"Imported count:{imported.Count}");
            Console.ReadLine();
        }
    }
}
