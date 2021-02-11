﻿using Excercise5Garage.Garage;
using Excercise5Garage.Garage.Interface;
using Excercise5Garage.GarageHandler.Interface;
using Excercise5Garage.Menu.Interface;
using Excercise5Garage.UI.Interface;
using Excercise5Garage.Vehicle;
using System;
using System.Collections.Generic;

namespace Excercise5Garage.Menu
{
    public class MainMenu
    {
        /// <summary>
        /// Factory där man kan skapa menyer
        /// </summary>
        public IMenuFactory MenuFactory { get; }

        /// <summary>
        /// Reference till ui
        /// </summary>
        public IUI Ui { get; }

        /// <summary>
        /// Referens till en lista med handlers av olika garage
        /// </summary>
        public List<IGarageHandler> GarageHandlers { get; }


        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="menuFactory">Referens till factory för att skapa menyer</param>
        /// <param name="ui">Referens till ui</param>
        /// <param name="lsGarageHandlers">Referense till lista med handlers för olika garage</param>
        public MainMenu(IMenuFactory menuFactory, IUI ui, List<IGarageHandler> lsGarageHandlers)
        {
            MenuFactory = menuFactory;
            Ui = ui;
            GarageHandlers = lsGarageHandlers;
        }


        /// <summary>
        /// Metoden visar huvudmenyn
        /// </summary>
        /// <returns>enum MenuInputResult med olika värden beroende på användarens kommando</returns>
        public MenuInputResult Show()
        {
            MenuInputResult result = MenuInputResult.NA;

            do
            {
                this.Ui.Clear();

                if (result == MenuInputResult.WRONG_INPUT)
                    this.Ui.WriteLine("Felaktig inmatning");

                this.Ui.WriteLine(this.MenuFactory.GetMainMenu(MainMenuType.MAIN_MENU));
                result = HandleInput();
            } 
            while (result != MenuInputResult.EXIT);

            return result;
        }


        /// <summary>
        /// Metoden hanterar inmatning av kommandon från ui
        /// </summary>
        /// <returns>enum MenuInputResult med olika värden beroende på användarens kommando</returns>
        private MenuInputResult HandleInput()
        {
            int iSelectedGarage = 0;
            MenuInputResult result = MenuInputResult.NA;
            string strInput = this.Ui.ReadLine();

            if (!String.IsNullOrWhiteSpace(strInput))
            {
                strInput = strInput.Trim();
                
                if (strInput.StartsWith('0'))
                {// Användaren har valt att avsluta programmet
                    result = MenuInputResult.EXIT;
                }
                else if (strInput.StartsWith('1'))
                {// Skapa garage

                    CreateGarageMenu createGarageMenu = new CreateGarageMenu(MenuFactory, Ui, GarageHandlers);
                    result = createGarageMenu.Show();
                }
                else if (strInput.StartsWith('2'))
                {// Gå till ett Garage

                    // Låt användaren välja gararge
                    SelectGarageMenu selectGarageMenu = new SelectGarageMenu(MenuFactory, Ui, GarageHandlers);
                    iSelectedGarage = selectGarageMenu.Show();

                    // Låt användaren interagera med garaget
                    if(iSelectedGarage > 0)
                    {// Användaren har valt ett garage
                        // TODO ANVÄNDAREN HAR VALT ETT GARAGE. GÅ DIT
                    }


                }
                else if (strInput.StartsWith('3'))
                {// Simulering av ett garage

                    SimulateGarage();
                }
                else
                {
                    result = MenuInputResult.WRONG_INPUT;
                }
            }
            else
            {
                result = MenuInputResult.WRONG_INPUT;
            }

            return result;
        }


        /// <summary>
        /// Metoden simulerar skapandet av ett garage.
        /// Skapande av bilar som parkerar och lämnar garaget
        /// Visar även vad som händer om garaget är full och någon vill parkera
        /// </summary>
        private void SimulateGarage()
        {
            // GetMainMenu
            Ui.WriteLine("Simulering av att skapa ett garage. Parkera fordon och fordon lämnar garaget");

            // Skapa en factory där jag kan skapa garage
            GarageFactory garageFactory = new GarageFactory();
            Guid guid = Guid.NewGuid();
            
            // Skapa ett garage
            var garage = garageFactory.CreateGarage(guid, "Första garaget", 5);

            // Skapa en GarageHandler som hantera allt om ett garage
            this.GarageHandlers.Add(new GarageHandler.GarageHandler(garage, this.Ui));

            Ui.WriteLine($"Har skapat ett nytt garage. " + garage);


            // Vid simuleringen har jag bara en garagehandler och ett garage. Hämta den handlern
            var garageHandlers = this.GarageHandlers;
            IGarageHandler garageHandler = garageHandlers[0];

            // Börja skapa lite fordon som parkeras i garaget            
            VehicleFactory vehicleFactory = new VehicleFactory();

            ICanBeParkedInGarage vehicle = vehicleFactory.CreateRandomVehicleForGarage();
            garageHandler.ParkVehicle(vehicle);

            ICanBeParkedInGarage vehicle1 = vehicleFactory.CreateRandomVehicleForGarage();
            garageHandler.ParkVehicle(vehicle1);

            ICanBeParkedInGarage vehicle2 = vehicleFactory.CreateRandomVehicleForGarage();
            garageHandler.ParkVehicle(vehicle2);

            ICanBeParkedInGarage vehicle3 = vehicleFactory.CreateRandomVehicleForGarage();
            garageHandler.ParkVehicle(vehicle3);

            ICanBeParkedInGarage vehicle4 = vehicleFactory.CreateRandomVehicleForGarage();
            garageHandler.ParkVehicle(vehicle4);

            // Garaget är fullt, men vi försöker parkera ett fordon till
            ICanBeParkedInGarage vehicle5 = vehicleFactory.CreateRandomVehicleForGarage();
            garageHandler.ParkVehicle(vehicle5);

            garageHandler.PrintInformation();


            // Ett fordon lämnar garaget
            garageHandler.RemoveVehicle(vehicle1);

            garageHandler.PrintInformation();

            // Ett fordon som inte finns i garaget lämnar
            garageHandler.RemoveVehicle(vehicle5);

            garageHandler.PrintInformation();

            Ui.ReadLine();
        }
    }
}