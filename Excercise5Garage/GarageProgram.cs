﻿using Excercise5Garage.Garage;
using Excercise5Garage.GarageHandler;
using Excercise5Garage.Menu;
using Excercise5Garage.UI;
using Excercise5Garage.Vehicle;
using Excercise5Garage.Vehicle.AirVehicle;
using Excercise5Garage.Vehicle.WheeledVehicle;
using System;
using System.Collections.Generic;

namespace Excercise5Garage
{
    /// <summary>
    /// Programmets huvudklass
    /// </summary>
    public class GarageProgram
    {
        /// <summary>
        /// Referens tilll factor för att skapa menyer
        /// </summary>
        public IMenuFactory MenuFactory { get; }

        /// <summary>
        /// Referense till ui
        /// </summary>
        public IUI Ui { get; }

        /// <summary>
        /// Lista med handlers som hanterar ett garage var
        /// </summary>
        public List<IGarageHandler>lsGarageHandlers = new List<IGarageHandler>();



        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="menuFactory">Referens till Factory där vi skapar olika menyer</param>
        /// <param name="ui">Referens till ui</param>
        public GarageProgram(IMenuFactory menuFactory, IUI ui)
        {
            MenuFactory = menuFactory;
            Ui = ui;
        }
        

        /// <summary>
        /// Metod som kör programmet
        /// Visar programmets huvudmeny
        /// </summary>
        public void Run()
        {
            MenuInputResult result = MenuInputResult.NA;
            MainMenu mainMenu = new MainMenu(MenuFactory, Ui, lsGarageHandlers);

            do
            {
                result = mainMenu.Show();
            } 
            while (result != MenuInputResult.EXIT);
        }


        /// <summary>
        /// TODO Remove sjit
        /// </summary>
        public void TestCodeRun()
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
