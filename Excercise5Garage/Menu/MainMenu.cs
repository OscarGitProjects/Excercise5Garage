using Excercise5Garage.Garage;
using Excercise5Garage.UI;
using System;
using System.Collections.Generic;
using Excercise5Garage.GarageHandler;
using Excercise5Garage.Vehicle;

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
        /// <returns></returns>
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

                    // TODO Implementera Skapa garage

                }
                else if (strInput.StartsWith('2'))
                {// Gå till ett Garage

                    // TODO Implementera Gå till garage

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
            var garage = garageFactory.CreateGarage(guid, "Första garaget", 10);

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


            garageHandler.PrintInformation();


            // TODO Simulera vad som händer när garaget är fullt


            // TODO Simulera när fordon lämnar garaget

            Ui.ReadLine();
        }
    }
}