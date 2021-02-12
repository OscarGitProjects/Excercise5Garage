using Excercise5Garage.Garage.Interface;
using Excercise5Garage.GarageHandler.Interface;
using Excercise5Garage.Menu.Interface;
using Excercise5Garage.RegistrationNumber.Interface;
using Excercise5Garage.UI.Interface;
using Excercise5Garage.Vehicle;
using Excercise5Garage.Vehicle.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

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
        public GarageMenu(IMenuFactory menuFactory, IUI ui, IList<IGarageHandler> lsGarageHandlers, Guid guidSelectedGarageHandlerGuid, IRegistrationNumberRegister registrationNumberRegister)
        {
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

                // Hämta vald garagehandler
                IGarageHandler garageHandler = this.GarageHandlers.FirstOrDefault(g => g.GuidId.Equals(this.SelectedGarageHandlerGuid));

                if (garageHandler != null)
                {
                    var (strId, strName, bIsFull, iCapacity, iNumberOfParkedVehicle) = garageHandler.GetGarageInfo();

                    // Skapa en lämplig utskrift för menyn
                    string strIsFull = bIsFull ? "Nej" : "Ja";
                    this.Ui.WriteLine($"{strName}. Har lediga platser {strIsFull}. Antal bilar i garaget {iNumberOfParkedVehicle}");
                }
                
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
                {// Parkera fordon 
                    
                    // TODO PARKERA ETT FORDON
                }
                else if (strInput.StartsWith('2'))
                {// Lämna garaget med ett fordon

                    LeaveTheGarageMenu leaveTheGarageMenu = new LeaveTheGarageMenu(this.MenuFactory, this.Ui, this.GarageHandlers, this.SelectedGarageHandlerGuid, this.RegistrationNumberRegister);
                    result = leaveTheGarageMenu.Show();
                }
                else if (strInput.StartsWith('3'))
                {// Skapa ett antal fordon

                    Simulering(6);
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

                    // TODO SÖKNING EFTER FORDON
                }
            }
            else
            {
                result = MenuInputResult.WRONG_INPUT;
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
                if (garageHandler.Garage.Count > 0)
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
            // Hämta vald garagehandler
            IGarageHandler garageHandler = this.GarageHandlers.FirstOrDefault(g => g.GuidId.Equals(this.SelectedGarageHandlerGuid));
            if(garageHandler != null)
            {
                if (garageHandler.Garage.Count() > 0)
                {
                    foreach (IVehicle vehicle in garageHandler.Garage)
                    {
                        Ui.WriteLine($"{vehicle.Color} {vehicle.GetType().Name} med registreringsnummer {vehicle.RegistrationNumber}");
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
        /// Metoden skapar och parkerar önskat antal fordon
        /// </summary>
        /// <param name="iNumberOfVehicle">Antal fordon som skall skapas</param>
        private void Simulering(int iNumberOfVehicle)
        {
            // Hämta vald garagehandler
            IGarageHandler garageHandler = this.GarageHandlers.FirstOrDefault(g => g.GuidId.Equals(this.SelectedGarageHandlerGuid));

            if (garageHandler != null)
            {
                // Börja skapa lite fordon som parkeras i garaget            
                IVehicleFactory vehicleFactory = new VehicleFactory(this.RegistrationNumberRegister);
                ICanBeParkedInGarage vehicle = null;
                IVehicle tmpVehicle = null;

                for (int i = 0; i < iNumberOfVehicle; i++)
                {
                    vehicle = vehicleFactory.CreateRandomVehicleForGarage();
                    if(garageHandler.ParkVehicle(vehicle))
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
            }
        }
    }
}