using Excercise5Garage.Garage;
using Excercise5Garage.Vehicle;
using Excercise5Garage.Vehicle.AirVehicle;
using Excercise5Garage.Vehicle.WheeledVehicle;
using System;

namespace Excercise5Garage
{
    public class GarageProgram
    {
        public void Run()
        {
            Console.WriteLine("Run GarageProgram");

            Guid guid = Guid.NewGuid();

            Garage<ICanBeParkedInGarage> garage = new Garage<ICanBeParkedInGarage>(guid, "Garage 1", 5);

            IVehicle plane = new Airplane("AAA 111", "Grön", 4);

            ICanBeParkedInGarage veh = new Car("BBB 222", "Röd", 4);
            garage.Add(veh);

            veh = new Car("CCC 333", "Röd", 4);
            garage.Add(veh);

            veh = new Car("DDD 444", "Röd", 4);
            garage.Add(veh);

            veh = new Car("DDD 444", "Röd", 4);
            garage.Add(veh);

            //veh = new Car("EEE 555", "Röd", 4);
            //garage.Add(veh);

            //veh = new Car("FFF 666", "Röd", 4);
            //garage.Add(veh);


            Console.WriteLine("Name: " + garage.GarageName + ", Capacity: " + garage.Capacity + ", Count: " + garage.Count + ", IsEmpty: " + garage.IsEmpty + ", IsFull: " + garage.IsFull);

            foreach (var item in garage)
            {
                Console.WriteLine(item);
            }

            ICanBeParkedInGarage veh1 = new Car("CCC 333", "Röd", 4);
            garage.Add(veh1);

            Console.WriteLine("***** *****" + System.Environment.NewLine);
            Console.WriteLine("Name: " + garage.GarageName + ", Capacity: " + garage.Capacity + ", Count: " + garage.Count + ", IsEmpty: " + garage.IsEmpty + ", IsFull: " + garage.IsFull);
            foreach (var item in garage)
            {
                Console.WriteLine(item);
            }

            garage.Remove(veh);

            string str = "abc";

            Console.WriteLine("***** *****" + System.Environment.NewLine);
            Console.WriteLine("Name: " + garage.GarageName + ", Capacity: " + garage.Capacity + ", Count: " + garage.Count + ", IsEmpty: " + garage.IsEmpty + ", IsFull: " + garage.IsFull);
            foreach (var item in garage)
            {
                Console.WriteLine(item);
            }


            garage.Remove(veh);
            Console.WriteLine("***** *****" + System.Environment.NewLine);           
            Console.WriteLine("Name: " + garage.GarageName + ", Capacity: " + garage.Capacity + ", Count: " + garage.Count + ", IsEmpty: " + garage.IsEmpty + ", IsFull: " + garage.IsFull);
            foreach (var item in garage)
            {
                Console.WriteLine(item);
            }
        }
    }
}
