using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppPos
{
    public interface IPos
    {
        void Start();
        void UserInput();
        void UpdateCart(Produts Pr, int quantity);
        void CheckOut();
    }

    class Pos : IPos
    {
        List<Produts> productList = new List<Produts>{};
        Dictionary<string, int> Cart = new Dictionary<string, int>();

        public Pos()
        {
            productList.Add( new Produts() { ProductName ="Pen", Price = 5, Quantity = 5 });
            productList.Add(new Produts() { ProductName = "Shart", Price = 100, Quantity = 5 });
            productList.Add(new Produts() { ProductName = "Cap", Price = 50, Quantity = 5 });
        }

        public void Start()
        {
            Console.WriteLine("\nSelect your product \n");
            Console.WriteLine("No \t Item \t Price ");

            var counter = 1;
            foreach (var item in productList)
            {
                Console.WriteLine(counter +" \t "+ item.ProductName + " \t " + item.Price);
                counter++;
            }
            Console.WriteLine("0. \t to checkout\n");

            UserInput();
            
        }

        public void UserInput()
        {
            Console.WriteLine("\nInput Item");
            var itemInput = Int32.Parse(Console.ReadLine());

            if (itemInput == 0) { CheckOut(); }

            Console.WriteLine("\nInput quantity");
            var quantityInput = Int32.Parse(Console.ReadLine());
            
            UpdateCart(productList[itemInput - 1], quantityInput);
        }

        public void UpdateCart(Produts Pr, int quantity)
        {
            if(Cart.ContainsKey(Pr.ProductName))
            {
                Cart[Pr.ProductName] += quantity; 
            }
            else
            {
                Cart[Pr.ProductName] = quantity;
            }
            Start();
        }

        public void CheckOut()
        {
            Console.WriteLine("\nYou chose the following products: ");
            Console.WriteLine("\nNo \t Item \t Price \t Quantity \t Total");            

            var count = 1;
            double total = 0;
            foreach (var item in Cart)
            {
                var ListIndex = productList.FindIndex(pd => pd.ProductName == item.Key);
                Console.WriteLine(count + 1 + ". \t " + item.Key + " \t " + productList[ListIndex].Price + " \t " + item.Value + " \t\t " + productList[ListIndex].Price * item.Value );
                total += productList[ListIndex].Price * item.Value ;
                count++; 
            }
            Console.WriteLine("\n\nTotal Price " + total + "\n\n\n");
            Cart.Clear();
            Start();
        }
    }
}
