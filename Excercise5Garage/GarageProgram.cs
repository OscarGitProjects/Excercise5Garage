using Excercise5Garage.GarageHandler.Interface;
using Excercise5Garage.Menu;
using Excercise5Garage.Menu.Interface;
using Excercise5Garage.RegistrationNumber.Interface;
using Excercise5Garage.UI.Interface;
using Excercise5Garage.Vehicle.Interface;
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
        /// Factory där man kan skapa fordon
        /// </summary>
        public IVehicleFactory VehicleFactory { get; }

        /// <summary>
        /// Referense till ui
        /// </summary>
        public IUI Ui { get; }

        /// <summary>
        /// Lista med handlers som hanterar ett garage var
        /// </summary>
        public List<IGarageHandler>lsGarageHandlers = new List<IGarageHandler>();

        /// <summary>
        /// Register där använda registreringsnummer finns lagrade
        /// </summary>
        public IRegistrationNumberRegister RegistrationNumberRegister { get; }



        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="menuFactory">Referens till Factory där vi skapar olika menyer</param>
        /// <param name="vehicleFactory">referense till en factor där man kan skapa fordon</param>
        /// <param name="ui">Referens till ui</param>
        /// <param name="registrationNumberRegister">Referense till register där använda registreringsnummer finns</param>
        /// <exception cref="System.NullReferenceException">Kan kastas om referensen till menuFactory, ui eller registrationNumberRegister är null</exception>
        public GarageProgram(IMenuFactory menuFactory, IVehicleFactory vehicleFactory, IUI ui, IRegistrationNumberRegister registrationNumberRegister)
        {
            if (menuFactory == null)
                throw new NullReferenceException("NullReferenceException. GarageProgram.GarageProgram(). menuFactory referensen är null");

            if (vehicleFactory == null)
                throw new NullReferenceException("NullReferenceException. GarageProgram.GarageProgram(). vehicleFactory referensen är null");

            if (ui == null)
                throw new NullReferenceException("NullReferenceException. GarageProgram.v(). ui referensen är null");

            if (registrationNumberRegister == null)
                throw new NullReferenceException("NullReferenceException. GarageProgram.GarageProgram(). registrationNumberRegister referensen är null");


            MenuFactory = menuFactory;
            VehicleFactory = vehicleFactory;
            Ui = ui;
            RegistrationNumberRegister = registrationNumberRegister;
        }
        

        /// <summary>
        /// Metod som kör programmet
        /// Visar programmets huvudmeny
        /// </summary>
        public void Run()
        {
            MenuInputResult result = MenuInputResult.NA;
            MainMenu mainMenu = new MainMenu(this.MenuFactory, this.VehicleFactory, this.Ui, this.lsGarageHandlers, this.RegistrationNumberRegister);

            do
            {
                result = mainMenu.Show();
            } 
            while (result != MenuInputResult.EXIT);
        }
    }
}
