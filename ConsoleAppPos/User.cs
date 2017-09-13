using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppPos
{
    class User
    {
        public void UserCheck()
        {
            Console.WriteLine("\nEnter User Type \n1. Admin \n2. User ");
            var user = Int16.Parse(Console.ReadLine());
            Pos pos = new Pos();

            if (user == 1)
            {
                pos.Start(1);
            }
            else if (user == 2)
            {
                pos.Start();
            }
            else
            {
                Console.WriteLine("\nWrong Input, try again\n");
                UserCheck();
            }
        }
    }
}
