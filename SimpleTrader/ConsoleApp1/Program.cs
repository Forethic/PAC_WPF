using System;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using SimpleTrader.EntityFramework;
using SimpleTrader.EntityFramework.Services;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            IDataService<User> userService = new GenericDataService<User>(new SimpleTraderDbContextFactory());

            //userService.Create(new User { Username = "Test" }).Wait();

            //Console.WriteLine(userService.GetAll().Result.Count());

            //Console.WriteLine(userService.Get(1).Result);

            //Console.WriteLine(userService.Get(2).Result);

            //Console.WriteLine(userService.Update(1, new User { Username = "fasfas" }).Result);

            userService.Delete(1);

            Console.ReadLine();
        }
    }
}