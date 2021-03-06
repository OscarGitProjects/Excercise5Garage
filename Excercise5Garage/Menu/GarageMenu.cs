﻿using Excercise5Garage.Garage.Interface;
using Excercise5Garage.GarageHandler.Interface;
using Excercise5Garage.Menu.Interface;
using Excercise5Garage.RegistrationNumber.Interface;
using Excercise5Garage.UI.Interface;
using Excercise5Garage.Vehicle;
using Excercise5Garage.Vehicle.Interface;
using Excercise5Garage.Vehicle.WheeledVehicle;
using System;
using System.Collections.Generic;
using System.Linq;
using Excercise5Garage.Extensions;

namespace Excercise5Garage.Menu
{
    /// <summary>
    /// Klass med funktioner som gör att en användare kan interagera med ett garage
    /// </summary>
    public class GarageMenu
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
        public IList<IGarageHandler> GarageHandlers { get; private set; }

        /// <summary>
        /// Guid för vald garagehandler
        /// </summary>
        public Guid SelectedGarageHandlerGuid { get; }

        /// <summary>
        /// Register där använda registreringsnummer finns lagrade
        /// </summary>
        public IRegistrationNumberRegister RegistrationNumberRegister { get; }



        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="menuFactory">Referense till en factory där man kan hämta text till olika menyer</param>
        /// <param name="ui">Referense till objekt för att skriva och hämta indata</param>
        /// <param name="lsGarageHandlers">lista med olika garagehandlers. Varje garagehandler hanterar ett garage</param>
        /// <param name="guidSelectedGarageHandlerGuid">Guid för vald GarageHandler</param>
        /// <param name="registrationNumberRegister">Referense till register där använda registreringsnummer finns</param>
        /// <exception cref="System.NullReferenceException">Kan kastas om referensen till menuFactory, vehicleFactory, ui, lsGarageHandlers eller registrationNumberRegister är null</exception>
        public GarageMenu(IMenuFactory menuFactory, IUI ui, IList<IGarageHandler> lsGarageHandlers, Guid guidSelectedGarageHandlerGuid, IRegistrationNumberRegister registrationNumberRegister)
        {
            if (menuFactory == null)
                throw new NullReferenceException("NullReferenceException. GarageMenu.GarageMenu(). menuFactory referensen är null");

            if (ui == null)
                throw new NullReferenceException("NullReferenceException. GarageMenu.GarageMenu(). ui referensen är null");

            if (lsGarageHandlers == null)
                throw new NullReferenceException("NullReferenceException. GarageMenu.GarageMenu(). lsGarageHandlers referensen är null");

            if (registrationNumberRegister == null)
                throw new NullReferenceException("NullReferenceException. GarageMenu.GarageMenu(). registrationNumberRegister referensen är null");

            MenuFactory = menuFactory;
            Ui = ui;
            GarageHandlers = lsGarageHandlers;
            SelectedGarageHandlerGuid = guidSelectedGarageHandlerGuid;
            RegistrationNumberRegister = registrationNumberRegister;
        }


        /// <summary>
        /// Metoden visar menyer där användaren kan välja vad hen  vill göra med ett garage
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

                result = MenuInputResult.NA;

                // Hämta vald garagehandler
                IGarageHandler garageHandler = this.GarageHandlers.FirstOrDefault(g => g.GuidId.Equals(this.SelectedGarageHandlerGuid));

                if (garageHandler != null)
                {
                    var (strId, strName, bIsFull, iCapacity, iNumberOfParkedVehicle) = garageHandler.GetGarageInfo();

                    // Skapa en lämplig utskrift för menyn
                    string strIsFull = bIsFull ? "Nej" : "Ja";
                    this.Ui.WriteLine($"{strName}. Har lediga platser {strIsFull}. Antal bilar i garaget {iNumberOfParkedVehicle}");
                }
                
                // Visa menyn
                this.Ui.WriteLine(this.MenuFactory.GetMenu(MenuType.GARAGE_MENU));

                // Hantera inmatning från användaren
                result = HandleInput();
            }
            while (result != MenuInputResult.TO_MAIN_MENU);

            return result;
        }


        /// <summary>
        /// Metoden hanterar inmatning av kommandon från ui
        /// </summary>
        /// <returns>enum MenuInputResult med olika värden beroende på användarens kommando</returns>
        private MenuInputResult HandleInput()
        {
            MenuInputResult result = MenuInputResult.NA;


            // Inläsning av kommando från ui
            string strInput = this.Ui.ReadLine();
            if (!String.IsNullOrWhiteSpace(strInput))
            {
                strInput = strInput.Trim();

                if (strInput.StartsWith('0'))
                {// Användaren har valt att avsluta programmet. Återgå till huvudmenyn
                    result = MenuInputResult.TO_MAIN_MENU;
                }
                else if (strInput.StartsWith('1'))
                {// Skapa och parkera ett fordon 

                    CreateAndParkVehicleMenu createAndParkVehicleMenu = new CreateAndParkVehicleMenu(this.MenuFactory, this.Ui, this.GarageHandlers, this.SelectedGarageHandlerGuid, this.RegistrationNumberRegister);
                    result = createAndParkVehicleMenu.Show();
                }
                else if (strInput.StartsWith('2'))
                {// Lämna garaget med ett fordon

                    LeaveTheGarageMenu leaveTheGarageMenu = new LeaveTheGarageMenu(this.MenuFactory, this.Ui, this.GarageHandlers, this.SelectedGarageHandlerGuid, this.RegistrationNumberRegister);
                    result = leaveTheGarageMenu.Show();
                }
                else if (strInput.StartsWith('3'))
                {// Skapa ett antal fordon

                    Simulering();
                    //Simulering(6);
                }
                else if (strInput.StartsWith('4'))
                {// Lista alla fordon

                    ListAllVehicle();
                }
                else if(strInput.StartsWith('5'))
                {// Lista alla fordon per typ

                    ListAllVehicleByType();
                }
                else if (strInput.StartsWith('6'))
                {// Sök efter fordon

                    SearchVehicleMenu searchVehicleMenu = new SearchVehicleMenu(this.MenuFactory, this.Ui, this.GarageHandlers, this.SelectedGarageHandlerGuid, this.RegistrationNumberRegister);
                    result = searchVehicleMenu.Show();
                }
            }
            else
            {
                result = MenuInputResult.TO_GARAGE_MENU;
            }

            return result;
        }


        /// <summary>
        /// Metoden listar alla fordons typer och antal
        /// </summary>
        private void ListAllVehicleByType()
        {
            // Hämta vald garagehandler
            IGarageHandler garageHandler = this.GarageHandlers.FirstOrDefault(g => g.GuidId.Equals(this.SelectedGarageHandlerGuid));
            if (garageHandler != null)
            {
                if (garageHandler.NumberOfVehicleInGarage() > 0)
                {// Det finns parkerade fordon i garaget. Gruppera dom på typ och räkna antalet av varje typ

                    var results = garageHandler.Garage.GroupBy(v => v.GetType().Name)
                        .Select(group => new 
                        { 
                            Name = group.Key, 
                            Count = group.Count() 
                        })
                        .OrderBy(x => x.Name);

                    foreach (var result in results)
                    {
                        Ui.WriteLine($"Det finns {result.Count} {result.Name} i garaget");
                    }
                }
                else
                {
                    Ui.WriteLine("Det finns inga fordon i garaget");
                }

                Ui.WriteLine("Return för att fortsätta");
                Ui.ReadLine();
            }
        }


        /// <summary>
        /// Metoden listar information om alla parkerade bilar
        /// </summary>
        private void ListAllVehicle()
        {
            Car car = null;
            Bus bus = null;
            MotorCycle motorCycle = null;
            WheeledVehicle wheeledVehicle = null;
            IVehicle tmpVehicle = null;
            int iNumberOfSeatedPassengers = 0;

            // Hämta vald garagehandler
            IGarageHandler garageHandler = this.GarageHandlers.FirstOrDefault(g => g.GuidId.Equals(this.SelectedGarageHandlerGuid));
            if(garageHandler != null)
            {
                if (garageHandler.Garage.Count() > 0)
                {
                    foreach (ICanBeParkedInGarage vehicle in garageHandler.Garage)
                    {
                        tmpVehicle = vehicle as IVehicle;
                        wheeledVehicle = vehicle as WheeledVehicle;
                        car = vehicle as Car;
                        bus = vehicle as Bus;
                        motorCycle = vehicle as MotorCycle;

                        if (car != null)
                            iNumberOfSeatedPassengers = car.NumberOfSeatedPassengers;
                        else if(bus != null)
                            iNumberOfSeatedPassengers = bus.NumberOfSeatedPassengers;
                        else if(motorCycle != null)
                            iNumberOfSeatedPassengers = motorCycle.NumberOfSeatedPassengers;

                        Ui.WriteLine($"{tmpVehicle.Color?.ToLower()?.FirstToUpper()} {vehicle.GetType().Name} med registreringsnummer {tmpVehicle?.RegistrationNumber ?? "?"}. Har {wheeledVehicle?.NumberOfWheels ?? '?'} hjul och {iNumberOfSeatedPassengers} sittplatser");
                    }
                }
                else
                {
                    Ui.WriteLine("Det finns inga fordon i garaget");
                }

                Ui.WriteLine("Return för att fortsätta");
                Ui.ReadLine();
            }           
        }


        /// <summary>
        /// Metoden skapar och parkerar fordon
        /// Simulerar också att garaget är fullt
        /// </summary>
        private void Simulering()
        {
            // Hämta vald garagehandler
            IGarageHandler garageHandler = this.GarageHandlers.FirstOrDefault(g => g.GuidId.Equals(this.SelectedGarageHandlerGuid));
            if(garageHandler != null)
            {
                var (strId, strName, bIsFull, iCapacity, iNumberOfParkedVehicle) = garageHandler.GetGarageInfo();
                Simulering((iCapacity - iNumberOfParkedVehicle) + 2);
            }
        }


        /// <summary>
        /// Metoden skapar och parkerar önskat antal fordon
        /// </summary>
        /// <param name="iNumberOfVehicle">Antal fordon som skall skapas</param>
        private void Simulering(int iNumberOfVehicle)
        {
            // Hämta vald garagehandler
            IGarageHandler garageHandler = this.GarageHandlers.FirstOrDefault(g => g.GuidId.Equals(this.SelectedGarageHandlerGuid));

            if (garageHandler != null)
            {
                Ui.WriteLine("Simulerar att fordon parkeras i garaget");
                // Börja skapa lite fordon som parkeras i garaget            
                IVehicleFactory vehicleFactory = new VehicleFactory(this.RegistrationNumberRegister);
                ICanBeParkedInGarage vehicle = null;
                IVehicle tmpVehicle = null;

                for (int i = 0; i < iNumberOfVehicle; i++)
                {
                    vehicle = vehicleFactory.CreateRandomVehicleForGarage();
                    if (garageHandler.ParkVehicle(vehicle))
                    {// Det gick parkera fordonet. Registrerar att registreringsnumret är upptaget

                        tmpVehicle = vehicle as IVehicle;
                        if (tmpVehicle != null)
                            this.RegistrationNumberRegister.AddRegistrationNumber(tmpVehicle.RegistrationNumber);
                    }
                }

                this.RegistrationNumberRegister.PrintRegister(this.Ui);
                garageHandler.PrintInformationAboutGarage();
                Ui.WriteLine("Return för att fortsätta");
                Ui.ReadLine();
               


                // Nu vill jag simulera att fordon lämnar garaget
                // Hämta info om garaget
                var (strId, strName, bIsFull, iCapacity, iNumberOfParkedVehicle) = garageHandler.GetGarageInfo();

                if (iNumberOfParkedVehicle > 1)
                {// Vi har minst ett parkerat fordon. Radera det första i arrayen

                    try
                    {
                        vehicle = garageHandler.Garage[0];
                        if (vehicle != null)
                        {
                            Ui.WriteLine("Simulerar att ett fordon lämnar garaget");

                            tmpVehicle = vehicle as IVehicle;
                            string strRegistrationNumber = String.Empty;
                            if (tmpVehicle != null)
                                strRegistrationNumber = tmpVehicle.RegistrationNumber;

                            if (garageHandler.RemoveVehicle(0))
                                this.RegistrationNumberRegister.RemoveRegistrationNumber(strRegistrationNumber);


                            this.RegistrationNumberRegister.PrintRegister(this.Ui);
                            garageHandler.PrintInformationAboutGarage();

                            Ui.WriteLine("Return för att fortsätta");
                            Ui.ReadLine();
                        }
                    }
                    catch(ArgumentOutOfRangeException)
                    { }                 
                }
            }
        }
    }
}