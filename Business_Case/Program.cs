using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
using Microsoft.CSharp;

namespace Business_Case
{
    public class Product
    {
       public string name { get; set; }
    public bool domestic { get; set; }
    
        public float price { get; set; }
    
        public int? weight { get; set; }
    
        public string description { get; set; }
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

            StreamReader objReader = new StreamReader(objStream);

            string jsonString = objReader.ReadToEnd();

            List<Product> products = JsonSerializer.Deserialize<List<Product>>(jsonString);

            List<Product> domestic = new List<Product>();
            List<Product> imported = new List<Product>();

            foreach (Product product in products)
            {
                if(product.domestic == true)
                {
                    domestic.Add(product);
                } else
                {
                    imported.Add(product);
                }
            }

            domestic.Sort((p1, p2) => p1.name.CompareTo(p2.name));
            imported.Sort((p1, p2) => p1.name.CompareTo(p2.name));



            Console.WriteLine(".Domestic");
            foreach (Product product in domestic)
            {
                Console.WriteLine($"...Name: {product.name}");
                Console.WriteLine($"   Price: ${product.price}");
                if (product.weight != null)
                {
                    Console.WriteLine($"   Weight: {product.weight}g");
                } else
                {
                    Console.WriteLine($"   Weight: N/A");
                }
                Console.WriteLine($"   {product.description}");
            }

            Console.WriteLine(".Imported");
            foreach (Product product in imported)
            {
                Console.WriteLine($"...Name: {product.name}");
                Console.WriteLine($"   Price: ${product.price}");
                if (product.weight != null)
                {
                    Console.WriteLine($"   Weight: {product.weight}g");
                }
                else
                {
                    Console.WriteLine($"   Weight: N/A");
                }
                Console.WriteLine($"   {product.description}");
            }

            Console.ReadLine();
        }
    }
}
