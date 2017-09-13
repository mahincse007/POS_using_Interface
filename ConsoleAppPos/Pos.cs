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
        void Start(int user);
        void UserInput();
        void UpdateCart(Produts Pr, int quantity);
        void UpdateCart(Produts Pr, int quantity, int user);
        void CheckOut();
    }

    class Pos : IPos
    {
        private int UserType = 2;
        List<Produts> productList = new List<Produts> { };
        Dictionary<string, int> Cart = new Dictionary<string, int>();

        public Pos()
        {
            productList.Add(new Produts() { ProductName = "Pen", Price = 5, Quantity = 20 });
            productList.Add(new Produts() { ProductName = "Shart", Price = 100, Quantity = 20 });
            productList.Add(new Produts() { ProductName = "Cap", Price = 50, Quantity = 20 });
        }

        public void Start()
        {
            Console.WriteLine("\nSelect your product \n");
            Console.WriteLine("No \t Item \t Price \t Stock");

            var counter = 1;
            foreach (var item in productList)
            {
                Console.WriteLine(counter + " \t " + item.ProductName + " \t " + item.Price + " \t " + item.Quantity);
                counter++;
            }
            Console.WriteLine("\n0. \t to checkout");
            Console.WriteLine("99. \t to Exit\n");
            UserInput();
        }

        public void Start(int userType)
        {
            Console.WriteLine("\nSelect product to update \n");
            Console.WriteLine("No \t Item \t Price \t Stock");
            UserType = userType;

            var counter = 1;
            foreach (var item in productList)
            {
                Console.WriteLine(counter + " \t " + item.ProductName + " \t " + item.Price + " \t " + item.Quantity);
                counter++;
            }
            Console.WriteLine("\n88. \t to Update Stock");
            Console.WriteLine("99. \t to Logout\n");
            UserInput();
        }

        public void UserInput()
        {
            Console.WriteLine("\nSelect your option");
            var itemInput = Int32.Parse(Console.ReadLine());

            if (itemInput == 0)
            {
                CheckOut();
            }
            else if (itemInput == 99)
            {
                User user = new User();
                user.UserCheck();
                return;
            }
            else if (itemInput == 88)
            {
                UpdateStock();
            }

            Console.WriteLine("\nInput quantity");
            var quantityInput = Int32.Parse(Console.ReadLine());

            if (UserType == 1)
            {
                UpdateCart(productList[itemInput - 1], quantityInput, UserType);
            }
            else
            {
                UpdateCart(productList[itemInput - 1], quantityInput);
            }
        }

        public void UpdateCart(Produts Pr, int quantity)
        {
            if (Cart.ContainsKey(Pr.ProductName))
            {
                Cart[Pr.ProductName] += quantity;
            }
            else
            {
                Cart[Pr.ProductName] = quantity;
            }
            Pr.Quantity -= quantity;
            Start();
        }

        public void UpdateCart(Produts Pr, int quantity, int user)
        {
            Pr.Quantity += quantity;
            Start(user);
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
                Console.WriteLine(count + 1 + ". \t " + item.Key + " \t " + productList[ListIndex].Price + " \t " + item.Value + " \t\t " + productList[ListIndex].Price * item.Value);
                total += productList[ListIndex].Price * item.Value;
                count++;
            }
            Console.WriteLine("\n\nTotal Price " + total + "\n\n\n");
            Cart.Clear();
            Start();
        }

        public void UpdateStock()
        {
            Console.WriteLine("\nEneter Product Name ");
            var product = Console.ReadLine();

            Console.WriteLine("\nEneter Product Price ");
            var price = double.Parse(Console.ReadLine());

            Console.WriteLine("\nEneter Product Quantity ");
            var quantity = Int32.Parse(Console.ReadLine());

            if (productList.Exists(x => x.ProductName.Contains(product)))
            {
                var item = productList.Find(x => x.ProductName.Contains(product));
                item.Quantity += quantity;
            }
            else
            {
                productList.Add(new Produts() { ProductName = product, Price = price, Quantity = quantity });
            }
            Start(1);
        }
    }
}
