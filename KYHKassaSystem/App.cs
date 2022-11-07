using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KYHKassaSystem
{
    public class App
    {
        public void Run()
        {
            var allProducts = new List<Product>();
            allProducts = ReadProductFromFile();

            while (true)
            {
                Console.WriteLine("KASSA");
                Console.WriteLine("1. Ny Kund");
                Console.WriteLine("0. Avsluta");
                Console.WriteLine("Ange val");
                var selection = Console.ReadLine();
                if (selection == "1")
                {
                    Product getInfo = new Product();
                    getInfo.FindProductID(allProducts);
                }
                if (selection == "0")
                {
                    Console.WriteLine("Tack för att du handlar hos oss!");
                    break;
                }
            }
           
        }
        public List<Product> ReadProductFromFile()
        {
            var result = new List<Product>();

            foreach (var line in File.ReadLines("Products.txt"))
            {
                var parts = line.Split(';');
                var prod = new Product();
                prod.productID = parts[0];
                prod.productName = parts[1];
                prod.priceType = parts[2];
                prod.price = Convert.ToDecimal(parts[3]);
                prod.quantity = 0;
                result.Add(prod);
            }

            return result;
        }
    }
}
