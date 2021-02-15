using Excercise5Garage.GarageHandler.Interface;
using Excercise5Garage.Menu.Interface;
using Excercise5Garage.RegistrationNumber.Interface;
using Excercise5Garage.UI.Interface;
using Excercise5Garage.Vehicle.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Excercise5Garage.Extensions;
using Excercise5Garage.Vehicle.WheeledVehicle;

namespace Excercise5Garage.Menu
{
    public class SearchVehicleMenu
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
        public SearchVehicleMenu(IMenuFactory menuFactory, IUI ui, IList<IGarageHandler> lsGarageHandlers, Guid guidSelectedGarageHandlerGuid, IRegistrationNumberRegister registrationNumberRegister)
        {
            if (menuFactory == null)
                throw new NullReferenceException("NullReferenceException. SearchVehicleMenu.SearchVehicleMenu(). menuFactory referensen är null");

            if (ui == null)
                throw new NullReferenceException("NullReferenceException. SearchVehicleMenu.SearchVehicleMenu(). ui referensen är null");

            if (lsGarageHandlers == null)
                throw new NullReferenceException("NullReferenceException. SearchVehicleMenu.SearchVehicleMenu(). lsGarageHandlers referensen är null");

            if (registrationNumberRegister == null)
                throw new NullReferenceException("NullReferenceException. SearchVehicleMenu.SearchVehicleMenu(). registrationNumberRegister referensen är null");


            MenuFactory = menuFactory;
            Ui = ui;
            GarageHandlers = lsGarageHandlers;
            SelectedGarageHandlerGuid = guidSelectedGarageHandlerGuid;
            RegistrationNumberRegister = registrationNumberRegister;
        }


        /// <summary>
        /// Metoden visar huvudmenyn där användaren kan söka efter fordon i garaget
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

                this.Ui.WriteLine(this.MenuFactory.GetMenu(MenuType.SOK_VEHICLE_IN_GARAGE_MENU));

                // Hantera inmatning från användaren
                result = HandleInput();
            }
            while (result != MenuInputResult.TO_GARAGE_MENU);

            return result;
        }

        /// <summary>
        /// Metoden hantera input från användaren
        /// </summary>
        /// <returns>enum MenuInputResult med olika värden beroende på användarens kommando</returns>
        private MenuInputResult HandleInput()
        {
            MenuInputResult result = MenuInputResult.NA;

            /* 
                    strBuilder.AppendLine("Sök efter fordon");
                    strBuilder.AppendLine("0. För att återgå till menyn");
                    strBuilder.AppendLine("1. Sök på registreringsnummer");
                    strBuilder.AppendLine("2. Sök på upptagna registreringsnummer");
                    strBuilder.AppendLine("3. Sök på färg");
                    strBuilder.AppendLine("4. Sök på fordonstyp");
                    strBuilder.AppendLine("5. Sök på antal hjul");
                    strBuilder.AppendLine("6. Sök på antal sittande passagerare");
                    strBuilder.AppendLine("7. Sök på text");
                    strMenu = strBuilder.ToString();
            */

            // Hämta vald garagehandler
            //IGarageHandler garageHandler = this.GarageHandlers.FirstOrDefault(g => g.GuidId.Equals(this.SelectedGarageHandlerGuid));

            // Inläsning av information från användaren
            string strInput = this.Ui.ReadLine();

            if (!String.IsNullOrWhiteSpace(strInput))
            {
                strInput = strInput.Trim();
                if (strInput.StartsWith('0'))
                {// Användaren har valt att avsluta programmet. Återgå till menyn

                    result = MenuInputResult.TO_GARAGE_MENU;
                    return result;
                }
                else if (strInput.StartsWith('1'))
                {// Sök på registreringsnummer

                    result = SearchForVehicleWithRegistrationsNumber();
                }
                else if (strInput.StartsWith('2'))
                {// Sök på upptagna registreringsnummer

                    result = SearchForUsedRegistrationsNumber();
                }
                else if(strInput.StartsWith('3'))
                {// Sök på färg

                    result = SearchForVehicleWithColor();
                }
                else if (strInput.StartsWith('4'))
                {// Sök på fordonstyp

                }
                else if (strInput.StartsWith('5'))
                {// Sök på antal hjul

                    result = SearchForVehicleWithNumberOfWheels();
                }
                else if (strInput.StartsWith('6'))
                {// Sök på antal sittande passagerare

                    result = SearchForVehicleWithNumberOfSeatedPassenger();
                }
                else if (strInput.StartsWith('7'))
                {// Sök på en text

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
        /// Metoden söker efter fordon med antal platser för passagerare
        /// </summary>
        /// <returns>enum MenuInputResult med olika värden beroende på användarens kommando</returns>
        private MenuInputResult SearchForVehicleWithNumberOfSeatedPassenger()
        {
            int iNumberOfSeatedPassengers = 0;
            MenuInputResult result = MenuInputResult.NA;

            this.Ui.Clear();

            this.Ui.WriteLine(this.MenuFactory.GetMenu(MenuType.SEARCH_VEHICLE_WITH_NUMBER_OF_WHEELS));

            // Inläsning av sökt registreringsnummer från användaren
            string strInput = this.Ui.ReadLine();
            if (!String.IsNullOrWhiteSpace(strInput))
            {
                strInput = strInput.Trim();
                if (strInput.Length == 1 && strInput.StartsWith('0'))
                {// Användaren har valt att avsluta programmet. Återgå till menyn
                    result = MenuInputResult.TO_GARAGE_MENU;
                    return result;
                }
                else
                {
                    if (Int32.TryParse(strInput, out iNumberOfSeatedPassengers))
                    {// Antal hjul var en siffra

                        // Hämta vald garagehandler
                        IGarageHandler garageHandler = this.GarageHandlers.FirstOrDefault(g => g.GuidId.Equals(this.SelectedGarageHandlerGuid));
                        if (garageHandler != null)
                        {
                            var lsVehicles = garageHandler.Garage.Where(vv => ((WheeledVehicle)vv).NumberOfWheels == iNumberOfSeatedPassengers).ToList();

                            if (lsVehicles?.Count > 0)
                            {
                                foreach (var vehicle in lsVehicles)
                                {
                                    Ui.WriteLine(vehicle.ToString());
                                }
                            }
                            else
                            {
                                Ui.WriteLine($"Hittade inga fordon med {iNumberOfSeatedPassengers} platser");
                            }

                            Ui.WriteLine("Return för att fortsätta");
                            Ui.ReadLine();
                        }
                    }
                    else
                    {
                        result = MenuInputResult.WRONG_INPUT;
                    }
                }
            }
            else
            {
                result = MenuInputResult.WRONG_INPUT;
            }

            return result;
        }


        /// <summary>
        /// Metoden söker efter fordon med ett antal hjul
        /// </summary>
        /// <returns>enum MenuInputResult med olika värden beroende på användarens kommando</returns>
        private MenuInputResult SearchForVehicleWithNumberOfWheels()
        {
            int iNumberOfWheels = 0;
            MenuInputResult result = MenuInputResult.NA;

            this.Ui.Clear();

            this.Ui.WriteLine(this.MenuFactory.GetMenu(MenuType.SEARCH_VEHICLE_WITH_NUMBER_OF_WHEELS));

            // Inläsning av sökt registreringsnummer från användaren
            string strInput = this.Ui.ReadLine();
            if (!String.IsNullOrWhiteSpace(strInput))
            {
                strInput = strInput.Trim();
                if (strInput.Length == 1 && strInput.StartsWith('0'))
                {// Användaren har valt att avsluta programmet. Återgå till menyn
                    result = MenuInputResult.TO_GARAGE_MENU;
                    return result;
                }
                else
                {
                    if(Int32.TryParse(strInput, out iNumberOfWheels))
                    {// Antal hjul var en siffra

                        // Hämta vald garagehandler
                        IGarageHandler garageHandler = this.GarageHandlers.FirstOrDefault(g => g.GuidId.Equals(this.SelectedGarageHandlerGuid));
                        if (garageHandler != null)
                        {
                            var lsVehicles = garageHandler.Garage.Where(vv => ((WheeledVehicle)vv).NumberOfWheels == iNumberOfWheels).ToList();

                            if(lsVehicles?.Count > 0)
                            {
                                foreach (var vehicle in lsVehicles)
                                {
                                    Ui.WriteLine(vehicle.ToString());
                                }
                            }
                            else
                            {
                                Ui.WriteLine($"Hittade inga fordon med {iNumberOfWheels} hjul");
                            }

                            Ui.WriteLine("Return för att fortsätta");
                            Ui.ReadLine();
                        }
                    }
                    else
                    {
                        result = MenuInputResult.WRONG_INPUT;
                    }
                }
            }
            else
            {
                result = MenuInputResult.WRONG_INPUT;
            }

            return result;
        }


        /// <summary>
        /// Metod för att söka fordon med en speciell färg
        /// </summary>
        /// <returns>enum MenuInputResult med olika värden beroende på användarens kommando</returns>
        private MenuInputResult SearchForVehicleWithColor()
        {
            MenuInputResult result = MenuInputResult.NA;

            this.Ui.Clear();

            this.Ui.WriteLine(this.MenuFactory.GetMenu(MenuType.SEARCH_VEHICLE_WITH_COLOR));

            // Inläsning av sökt färg från användaren
            string strInput = this.Ui.ReadLine();
            if (!String.IsNullOrWhiteSpace(strInput))
            {
                strInput = strInput.Trim();
                if (strInput.Length == 1 && strInput.StartsWith('0'))
                {// Användaren har valt att avsluta programmet. Återgå till menyn
                    result = MenuInputResult.TO_GARAGE_MENU;
                    return result;
                }
                else
                {
                    strInput = strInput.ToUpper();

                    // Hämta vald garagehandler
                    IGarageHandler garageHandler = this.GarageHandlers.FirstOrDefault(g => g.GuidId.Equals(this.SelectedGarageHandlerGuid));
                    if (garageHandler != null)
                    {
                        var lsVehicles = garageHandler.Garage.Where(v => ((IVehicle)v).Color.CompareTo(strInput) == 0).ToList();

                        if(lsVehicles?.Count > 0)
                        {
                            foreach(var vehicle in lsVehicles)
                            {
                                Ui.WriteLine(vehicle.ToString());
                            }
                        }
                        else
                        {
                            Ui.WriteLine($"Hittade inga fordon med färgen {strInput.ToLower().FirstToUpper()}");
                        }                        

                        Ui.WriteLine("Return för att fortsätta");
                        Ui.ReadLine();
                    }
                }
            }
            else
            {
                result = MenuInputResult.WRONG_INPUT;
            }

            return result;
        }


        /// <summary>
        /// Metod för att låta användaren söka efter fordon på registreringsnumret
        /// </summary>
        /// <returns>enum MenuInputResult med olika värden beroende på användarens kommando</returns>
        private MenuInputResult SearchForVehicleWithRegistrationsNumber()
        {
            MenuInputResult result = MenuInputResult.NA;
            int iCount = 0;

            this.Ui.Clear();

            this.Ui.WriteLine(this.MenuFactory.GetMenu(MenuType.SEARCH_VEHICLE_WITH_REGISTRATIONNUMBER));

            // Inläsning av sökt registreringsnummer från användaren
            string strInput = this.Ui.ReadLine();
            if (!String.IsNullOrWhiteSpace(strInput))
            {
                strInput = strInput.Trim();                
                if (strInput.Length == 1 && strInput.StartsWith('0'))
                {// Användaren har valt att avsluta programmet. Återgå till menyn
                    result = MenuInputResult.TO_GARAGE_MENU;
                    return result;
                }
                else
                {
                    strInput = strInput.ToUpper();

                    // Hämta vald garagehandler
                    IGarageHandler garageHandler = this.GarageHandlers.FirstOrDefault(g => g.GuidId.Equals(this.SelectedGarageHandlerGuid));
                    if (garageHandler != null)
                    {
                        // Det skall bara finnas ett fordon med registreringsnummer
                        var vehicle = garageHandler.Garage.FirstOrDefault(v => ((IVehicle)v).RegistrationNumber.CompareTo(strInput) == 0);
                        if(vehicle != null)
                        {
                            Ui.WriteLine(vehicle.ToString());
                        }
                        else
                        {
                            Ui.WriteLine("Hittade inga fordon med sökt registreringsnummer");
                        }

                        Ui.WriteLine("Return för att fortsätta");
                        Ui.ReadLine();
                    }
                }
            }
            else
            {
                result = MenuInputResult.WRONG_INPUT;
            }

            return result;
        }



        /// <summary>
        /// Metod för att låta användaren söka efter upptaget registreringsnumret
        /// </summary>
        /// <returns>enum MenuInputResult med olika värden beroende på användarens kommando</returns>
        private MenuInputResult SearchForUsedRegistrationsNumber()
        {
            MenuInputResult result = MenuInputResult.NA;

            this.Ui.Clear();

            this.Ui.WriteLine(this.MenuFactory.GetMenu(MenuType.SEARCH_FOR_USED_REGISTRATIONNUMBER));

            // Inläsning av sökt registreringsnummer från användaren
            string strInput = this.Ui.ReadLine();
            if (!String.IsNullOrWhiteSpace(strInput))
            {
                strInput = strInput.Trim();
                if (strInput.Length == 1 && strInput.StartsWith('0'))
                {// Användaren har valt att avsluta programmet. Återgå till menyn
                    result = MenuInputResult.TO_GARAGE_MENU;
                    return result;
                }
                else
                {
                    strInput = strInput.ToUpper();

                    if(this.RegistrationNumberRegister.CheckIfRegistrationnNumberExists(strInput))
                    {
                        Ui.WriteLine($"Registreringsnummer {strInput} är upptaget");
                    }
                    else
                    {
                        Ui.WriteLine($"Registreringsnummer {strInput} är ledigt");
                    }

                    Ui.WriteLine("Return för att fortsätta");
                    Ui.ReadLine();
                }
            }
            else
            {
                result = MenuInputResult.WRONG_INPUT;
            }

            return result;
        }
    }

}
