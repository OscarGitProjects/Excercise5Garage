using System;

namespace Excercise5Garage
{
    public class Program
    {
        static void Main(string[] args)
        {
            var startup = new StartUp();
            startup.SetUp();

            Console.WriteLine("Enter för att avsluta");
            Console.ReadLine(); ;
        }
    }
}
