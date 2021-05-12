//sagat17,  Sagarika Telaprolu

using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqAssignment2
{
    class Program
    {

        static void Main(string[] args)
        {
            AssignmentActions();
        }

        public static void AssignmentActions()
        {
            var users = new List<Models.User>();

            users.Add(new Models.User { Name = "Dave", Password = "hello" });
            users.Add(new Models.User { Name = "Steve", Password = "steve" });
            users.Add(new Models.User { Name = "Lisa", Password = "hello" });

            // Printing all Users data
            Console.WriteLine("Printing all Users data:");
            users.ForEach(u => { Console.WriteLine("UserName: " + u.Name + " AND Password:  " + u.Password); });
            Console.WriteLine(new string('*', 100));

            // Printing Names of the Users with hello Password
            Console.WriteLine("Printing Names of the Users with hello Password:");
            users.Where(u => u.Password.Equals("hello"))
                    .ToList()
                    .ForEach(u => Console.WriteLine(u.Name));
            Console.WriteLine(new string('*', 100));

            // Deleting users having the lower cased Name as thier Password
            Console.WriteLine("Deleting users having the lower cased Name as thier Password : ");
            int removedUserCount = users.RemoveAll(u => u.Name.ToLowerInvariant() == u.Password);
            Console.WriteLine("Deleted {0} of Users having the lower cased Name as thier Password", removedUserCount);
            Console.WriteLine(new string('*', 100));

            // Deleting the first User that has the password
            Console.WriteLine("Deleting the first User that has the password 'hello':");
            bool isFirstHelloPasswordUserDeleted = users.Remove(users.FirstOrDefault(u => u.Password.Equals("hello")));
            Console.WriteLine("Is first user with hello password deleted? Answer: {0}", isFirstHelloPasswordUserDeleted);
            Console.WriteLine(new string('*', 100));

            // Printing remaining Users data
            Console.WriteLine("Printing remaining Users data:");
            users.ForEach(u => { Console.WriteLine("UserName: " + u.Name + " AND Password:  " + u.Password); });
            Console.WriteLine(new string('*', 100));
            
            Console.ReadLine();
        }
    }
}
