using Excercise5Garage.Extensions;
using Excercise5Garage.Garage.Interface;
using Excercise5Garage.GarageHandler.Interface;
using Excercise5Garage.Menu.Interface;
using Excercise5Garage.RegistrationNumber.Interface;
using Excercise5Garage.UI.Interface;
using Excercise5Garage.Vehicle.Interface;
using Excercise5Garage.Vehicle.WheeledVehicle;
using System;
using System.Collections.Generic;
using System.Linq;

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

                    result = SearchForVehicleOfTyp();
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

                    result = SearchForVehicleWithText();
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
        /// Metoden söker efter vehicle med en sök text.
        /// Gör sökningen på ett antal properties och sammanställer resultatet
        /// </summary>
        /// <returns>enum MenuInputResult med olika värden beroende på användarens kommando</returns>
        private MenuInputResult SearchForVehicleWithText()
        {
            // Listor med sök ord som har matchat något i sökningarna efter vehicle
            List<string> lsMatchedRegistrationnumbers = new List<string>();
            List<string> lsMatchedColors = new List<string>();
            List<string> lsMatchedTypes = new List<string>();
            List<int> lsMatchedNumberOfWheels = new List<int>();
            List<int> lsMatchedNumberOfSeatedPassengers = new List<int>();

            // Sammanställd lista med resultat av sökningen
            List<ICanBeParkedInGarage> lsVehicle = new List<ICanBeParkedInGarage>();
            MenuInputResult result = MenuInputResult.NA;

            this.Ui.Clear();

            // Skriv ut menyn
            this.Ui.WriteLine(this.MenuFactory.GetMenu(MenuType.SEARCH_VEHICLE_WITH_TEXT));


            // Läs in data från användaren
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
                    strInput = strInput.ToLower();
                    string[] strArray = strInput.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    int iNumber = 0;

                    if (strArray.Length > 0)
                    {
                        // Hämta vald garagehandler
                        IGarageHandler garageHandler = this.GarageHandlers.FirstOrDefault(g => g.GuidId.Equals(this.SelectedGarageHandlerGuid));
                        if (garageHandler != null)
                        {
                            foreach (string str in strArray)
                            {
                                if (!String.IsNullOrWhiteSpace(str))
                                {// Vi har tecken. Gör en sökning

                                    // Sök på registreringsnummer                                    
                                    var tmpVehiclesRegistrationNumber = garageHandler.Garage.Where(v => ((IVehicle)v).RegistrationNumber.Equals(str, StringComparison.OrdinalIgnoreCase)).ToList();
                                    if (tmpVehiclesRegistrationNumber?.Count > 0)
                                    {
                                        lsVehicle.AddRange(tmpVehiclesRegistrationNumber);
                                        lsMatchedRegistrationnumbers.Add(str);
                                    }

                                    // Sök på färg
                                    var tmpVehiclesColor = garageHandler.Garage.Where(v => ((IVehicle)v).Color.Equals(str, StringComparison.OrdinalIgnoreCase)).ToList();
                                    if (tmpVehiclesColor?.Count > 0)
                                    {
                                        lsVehicle.AddRange(tmpVehiclesColor);
                                        lsMatchedColors.Add(str);
                                    }

                                    // Sök på fordonstyp
                                    var tmpVehiclesType = garageHandler.Garage.Where(v => v.GetType().Name.Equals(str, StringComparison.OrdinalIgnoreCase)).ToList();
                                    if (tmpVehiclesType?.Count > 0)
                                    {
                                        lsVehicle.AddRange(tmpVehiclesType);
                                        lsMatchedTypes.Add(str);
                                    }

                                    if (Int32.TryParse(str, out iNumber))
                                    {
                                        // Sök på antalet hjul
                                        var tmpVehiclesNumberOfWheels = garageHandler.Garage.Where(v => ((WheeledVehicle)v).NumberOfWheels == iNumber).ToList();
                                        if (tmpVehiclesNumberOfWheels?.Count > 0)
                                        {
                                            lsVehicle.AddRange(tmpVehiclesNumberOfWheels);
                                            lsMatchedNumberOfWheels.Add(iNumber);
                                        }

                                        // Sök på antalet sittande passagerare
                                        var tmpVehiclesNumberOfSeatedPassengers = garageHandler.Garage.Where(v => ((WheeledVehicle)v).NumberOfSeatedPassengers == iNumber).ToList();
                                        if (tmpVehiclesNumberOfSeatedPassengers?.Count > 0)
                                        {
                                            lsVehicle.AddRange(tmpVehiclesNumberOfSeatedPassengers);
                                            lsMatchedNumberOfSeatedPassengers.Add(iNumber);
                                        }
                                    }
                                }
                            }// End of foreach strArray


                            // Nu skall vi filtrera listan med fordon som har matchat något i sökningen
                            if(lsVehicle?.Count > 0)
                            {// Vi har hittat några vehicle som matchar något av orden som användaren har sökt på

                                // Se till att alla dubbletter försvinner
                                lsVehicle = lsVehicle.Distinct().ToList();

                                // Nu måste jag kolla om vi har flera sökningar som skall matcha ett resultat
                                List<ICanBeParkedInGarage> lsTmpVehicles = new List<ICanBeParkedInGarage>(lsVehicle.Count);
                                List<ICanBeParkedInGarage> lsTmpFilteredVehicles = null;

                                if (lsMatchedRegistrationnumbers?.Count > 0)
                                {// Vi har haft matchning på  registreringsnummer. Filtrera bort alla som inte har registreringsnummer som har matchat

                                    foreach(string str in lsMatchedRegistrationnumbers)
                                    {
                                        lsTmpFilteredVehicles = lsVehicle.Where(v => ((IVehicle)v).RegistrationNumber.Equals(str, StringComparison.OrdinalIgnoreCase)).ToList();

                                        if (lsTmpFilteredVehicles?.Count > 0)
                                            lsTmpVehicles.AddRange(lsTmpFilteredVehicles);                                            
                                    }                                    
                                }


                                if(lsMatchedColors?.Count > 0)
                                {// Vi har haft matchande färger. Filtrera bort alla som inte har sökt färger

                                    foreach (string str in lsMatchedColors)
                                    {
                                        if(lsTmpVehicles?.Count > 0)    // Vi har tidigare filtreringar av vehicle. Använd den listan med vehicles
                                            lsTmpFilteredVehicles = lsTmpVehicles.Where(v => ((IVehicle)v).Color.Equals(str, StringComparison.OrdinalIgnoreCase)).ToList();
                                        else
                                            lsTmpFilteredVehicles = lsVehicle.Where(v => ((IVehicle)v).Color.Equals(str, StringComparison.OrdinalIgnoreCase)).ToList();

                                        if (lsTmpFilteredVehicles?.Count > 0)
                                            lsTmpVehicles.AddRange(lsTmpFilteredVehicles);
                                    }
                                }


                                if(lsMatchedTypes?.Count > 0)
                                {// Vi har haft matchande typer av fordon. Filtrerar bort dom som inte har matchningar

                                    foreach(string str in lsMatchedTypes)
                                    {
                                        if(lsTmpVehicles?.Count > 0)    // Vi har tidigare filtreringar av vehicle. Använd den listan med vehicles
                                            lsTmpFilteredVehicles = lsTmpVehicles.Where(v => v.GetType().Name.Equals(str, StringComparison.OrdinalIgnoreCase)).ToList();
                                        else 
                                            lsTmpFilteredVehicles = lsVehicle.Where(v => v.GetType().Name.Equals(str, StringComparison.OrdinalIgnoreCase)).ToList();

                                        if (lsTmpFilteredVehicles?.Count > 0)
                                            lsTmpVehicles.AddRange(lsTmpFilteredVehicles);
                                    }
                                }


                                if(lsMatchedNumberOfWheels?.Count > 0)
                                {// Vi har haft matchande antal hjul på fordon. Filtrerar bort dom som inte har matchningen

                                    foreach(int number in lsMatchedNumberOfWheels)
                                    {
                                        if (lsTmpVehicles?.Count > 0)   // Vi har tidigare filtreringar av vehicle. Använd den listan med vehicles
                                            lsTmpFilteredVehicles = lsTmpVehicles.Where(v => ((WheeledVehicle)v).NumberOfWheels == number).ToList();
                                        else
                                            lsTmpFilteredVehicles = lsVehicle.Where(v => ((WheeledVehicle)v).NumberOfWheels == number).ToList();

                                        if (lsTmpFilteredVehicles?.Count > 0)
                                            lsTmpVehicles.AddRange(lsTmpFilteredVehicles);
                                    }
                                }


                                if (lsMatchedNumberOfSeatedPassengers?.Count > 0)
                                {// Vi har haft matchande antal sittande passagerare. Filtrerar bort dom som inte har matchningen

                                    foreach (int number in lsMatchedNumberOfSeatedPassengers)
                                    {
                                        if (lsTmpVehicles?.Count > 0)   // Vi har tidigare filtreringar av vehicle. Använd den listan med vehicles
                                            lsTmpFilteredVehicles = lsTmpVehicles.Where(v => ((WheeledVehicle)v).NumberOfSeatedPassengers == iNumber).ToList();
                                        else
                                            lsTmpFilteredVehicles = lsVehicle.Where(v => ((WheeledVehicle)v).NumberOfSeatedPassengers == iNumber).ToList();

                                        if (lsTmpFilteredVehicles?.Count > 0)
                                            lsTmpVehicles.AddRange(lsTmpFilteredVehicles);
                                    }
                                }

                                // Se till att alla dubbletter försvinner
                                lsVehicle = lsTmpVehicles.Distinct().ToList();

                                if (lsVehicle?.Count > 0)
                                {
                                    // Skriv ut de fordon som sökningen matchar
                                    foreach (IVehicle vehicle in lsVehicle)
                                    {
                                        Ui.WriteLine(vehicle.ToString());
                                    }
                                }
                                else
                                {
                                    Ui.WriteLine($"Hittade inga fordon med sökningen {strInput}");
                                }
                            }
                            else
                            {
                                Ui.WriteLine($"Hittade inga fordon med sökningen {strInput}");
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
        /// Metoden söker efter fordon av en speciell typ
        /// </summary>
        /// <returns>enum MenuInputResult med olika värden beroende på användarens kommando</returns>
        private MenuInputResult SearchForVehicleOfTyp()
        {
            MenuInputResult result = MenuInputResult.NA;

            this.Ui.Clear();

            this.Ui.WriteLine(this.MenuFactory.GetMenu(MenuType.SEARCH_WITH_VEHICLE_TYPE));

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
                    strInput = strInput.ToLower();

                    // Hämta vald garagehandler
                    IGarageHandler garageHandler = this.GarageHandlers.FirstOrDefault(g => g.GuidId.Equals(this.SelectedGarageHandlerGuid));
                    if (garageHandler != null)
                    {
                        var lsVehicles = garageHandler.Garage.Where(v => v.GetType().Name.Equals(strInput, StringComparison.OrdinalIgnoreCase)).ToList();

                        if (lsVehicles?.Count > 0)
                        {
                            foreach (var vehicle in lsVehicles)
                            {
                                Ui.WriteLine(vehicle.ToString());
                            }
                        }
                        else
                        {
                            Ui.WriteLine($"Hittade inga fordon av typen {strInput}");
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
                            var lsVehicles = garageHandler.Garage.Where(vv => ((WheeledVehicle)vv).NumberOfSeatedPassengers == iNumberOfSeatedPassengers).ToList();

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
