using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KYHKassaSystem
{
    public class Product
    {
        public string productID { get; set; }
        public string productName { get; set; }
        public string priceType { get; set; }
        public decimal price { get; set; }
        public int quantity { get; set; }



        public Product FindProductFromProductID(List<Product> allProducts, string prodsID)
        {
            foreach (var prods in allProducts)
            {
                if (prods.productID == prodsID)
                    return prods;
            }
            return null;
        }

            public void FindProductID(List<Product> allProducts)
            {
                var kvitto = new Kvitto();
                Product product;
                var totalSum = 0m;
                var yourProducts = new List<string>();
                var buying = new List<string>();
            while (true)
            {
                Console.WriteLine("<ProduktID> <ANTAL>");
                foreach (var line in File.ReadAllLines("Products.txt"))
                    Console.WriteLine(line);
                var prodID = Console.ReadLine();
                try
                {
                    if (prodID.Length == 0)
                    {
                        Console.WriteLine("Felaktigt ProduktID eller Antal!");
                        continue;
                    }

                    var answer = prodID.Split(' ');
                    answer[0] = prodID.Substring(0, 3);
                    answer[1] = prodID.Substring(4);

                    if (answer.Length != 2)
                    {
                        Console.WriteLine("Ange 2 inmatningar! ProduktID och Antal");
                        continue;
                    }

                    if (answer[0].Length != 3)
                    {
                        Console.WriteLine("ProduktID måste inehålla 3 tecken!");
                        continue;
                    }
                    if (answer[1].Length == 0 || answer[1] == "0")
                    {
                        Console.WriteLine("Antal måste vara minst 1");
                        continue;
                    }

                    product = FindProductFromProductID(allProducts, answer[0]);

                    product.quantity = Convert.ToInt32(answer[1]);
                    if (product == null)
                    {
                        Console.WriteLine("Ogiltig ProduktID");
                        continue;
                    }

                    else
                    {
                        var reciptDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        var fileName = DateTime.Now.ToString("yyyy-MM-dd") + " Receipts.txt";

                        var sum = product.price * Convert.ToInt32(answer[1]);
                        totalSum += sum;

                        var line = $"{product.productName}: {Convert.ToInt32(answer[1])} * {product.price} = {sum}";
                        File.AppendAllText(fileName, line + "\n");

                        

                        Console.Clear();
                        var list = line;

                        buying.Add(list);
                        foreach (var buy in buying)
                            Console.WriteLine(buy);
                        Console.WriteLine("Totalt:" + totalSum);

                        Console.WriteLine("Vill du betala? Skriv (pay). För nya produkter tryck på ENTER!");
                        var sel = Console.ReadLine();
                        var pay = sel.ToLower().Trim();

                        yourProducts.Add(list);

                        if (pay == "pay")
                        {
                            Console.Clear();
                            Console.WriteLine("Godkänd Bestalning.\n");
                            int NR = kvitto.recentReceipt();
                            Console.WriteLine("Kvitto Nr" + NR);
                            File.AppendAllText(fileName, $"  Totalt: \n{totalSum} ");
                            File.AppendAllText(fileName, $"\n--------{reciptDate}--------");

                            foreach (var item in yourProducts)
                                Console.WriteLine(item);
                            Console.WriteLine("Totalt:" + totalSum);
                            Console.WriteLine($"\n------{reciptDate}--------");
                            File.AppendAllText(fileName, Environment.NewLine);

                            for (int i = 1; i <= product.quantity; i++)
                            {
                                File.AppendAllText(product.productName + ".txt", $"\n{DateTime.Now.ToString("yyyy-MM-dd")}");
                            }
                            break;
                        }
                    }
                }
                catch
                {
                    Console.WriteLine("Du angav fel kod!");
                }
                }
            }
    }
}
