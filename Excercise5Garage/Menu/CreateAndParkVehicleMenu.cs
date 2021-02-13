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
    /// Klass med funktioner för att användaren skall kunna skapa ett fordon och parkera det i garaget
    /// </summary>
    public class CreateAndParkVehicleMenu
    {
        /// <summary>
        /// Factory där man kan skapa menyer
        /// </summary>
        public IMenuFactory MenuFactory { get; }

        /// <summary>
        /// Factory där man kan skapa fordon
        /// </summary>
        public IVehicleFactory VehicleFactory { get;  }

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
        /// <param name="vehicleFactory">referense till en factor där man kan skapa fordon</param>
        /// <param name="ui">Referense till objekt för att skriva och hämta indata</param>
        /// <param name="lsGarageHandlers">lista med olika garagehandlers. Varje garagehandler hanterar ett garage</param>
        /// <param name="guidSelectedGarageHandlerGuid">Guid för vald GarageHandler</param>
        /// <param name="registrationNumberRegister">Referense till register där använda registreringsnummer finns</param>
        /// <exception cref="System.NullReferenceException">Kan kastas om referensen till menuFactory, vehicleFactory, ui, lsGarageHandlers eller registrationNumberRegister är null</exception>
        public CreateAndParkVehicleMenu(IMenuFactory menuFactory, IVehicleFactory vehicleFactory, IUI ui, IList<IGarageHandler> lsGarageHandlers, Guid guidSelectedGarageHandlerGuid, IRegistrationNumberRegister registrationNumberRegister)
        {
            if (menuFactory == null)
                throw new NullReferenceException("NullReferenceException. CreateAndParkVehicleMenu.CreateAndParkVehicleMenu(). menuFactory referensen är null");

            if (vehicleFactory == null)
                throw new NullReferenceException("NullReferenceException. CreateAndParkVehicleMenu.CreateAndParkVehicleMenu(). vehicleFactory referensen är null");

            if (ui == null)
                throw new NullReferenceException("NullReferenceException. CreateAndParkVehicleMenu.CreateAndParkVehicleMenu(). ui referensen är null");

            if (lsGarageHandlers == null)
                throw new NullReferenceException("NullReferenceException. CreateAndParkVehicleMenu.CreateAndParkVehicleMenu(). lsGarageHandlers referensen är null");

            if (registrationNumberRegister == null)
                throw new NullReferenceException("NullReferenceException. CreateAndParkVehicleMenu.CreateAndParkVehicleMenu(). registrationNumberRegister referensen är null");


            MenuFactory = menuFactory;
            VehicleFactory = vehicleFactory;
            Ui = ui;
            GarageHandlers = lsGarageHandlers;
            SelectedGarageHandlerGuid = guidSelectedGarageHandlerGuid;
            RegistrationNumberRegister = registrationNumberRegister;
        }


        /// <summary>
        /// Metoden visar menyn där användaren skall ange registreringsnummer för det fordon som skall lämna garaget
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

                this.Ui.WriteLine(this.MenuFactory.GetMenu(MenuType.CREATE_AND_PARK_VEHICLE_MENU));

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
            IVehicle tmpVehicle = null;
            MenuInputResult result = MenuInputResult.TO_GARAGE_MENU;
            // Hämta vald garagehandler
            IGarageHandler garageHandler = this.GarageHandlers.FirstOrDefault(g => g.GuidId.Equals(this.SelectedGarageHandlerGuid));

            if (garageHandler != null)
            {
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
                    {// Skapa en bil

                        var (menuInputResult, newVehicle) = CreateVehicle(Vehicle_Type.CAR);
                        if(newVehicle != null)
                        {// Vi har skapat ett fordon

                            if(garageHandler.ParkVehicle(newVehicle) == false)
                            {
                                tmpVehicle = newVehicle as IVehicle;
                                if(tmpVehicle != null)
                                    this.RegistrationNumberRegister.RemoveRegistrationNumber(tmpVehicle.RegistrationNumber);
                            }
                        }
                        else
                        {
                            Ui.WriteLine("Det gick inte att skapa ett fordon");
                        }

                        result = menuInputResult;
                    }
                    else if (strInput.StartsWith('2'))
                    {// Skapa en buss

                        var (menuInputResult, newVehicle) = CreateVehicle(Vehicle_Type.BUS);
                        if (newVehicle != null)
                        {// Vi har skapat ett fordon

                            if(garageHandler.ParkVehicle(newVehicle) == false)
                            {
                                tmpVehicle = newVehicle as IVehicle;
                                if (tmpVehicle != null)
                                    this.RegistrationNumberRegister.RemoveRegistrationNumber(tmpVehicle.RegistrationNumber);
                            }
                        }
                        else
                        {
                            Ui.WriteLine("Det gick inte att skapa ett fordon");
                        }

                        result = menuInputResult;
                    }
                    else if (strInput.StartsWith('3'))
                    {// Skapa en motorcykel                   

                        var (menuInputResult, newVehicle) = CreateVehicle(Vehicle_Type.MOTORCYCLE);
                        if (newVehicle != null)
                        {// Vi har skapat ett fordon

                            if (garageHandler.ParkVehicle(newVehicle) == false)
                            {
                                tmpVehicle = newVehicle as IVehicle;
                                if (tmpVehicle != null)
                                    this.RegistrationNumberRegister.RemoveRegistrationNumber(tmpVehicle.RegistrationNumber);
                            }                                
                        }
                        else
                        {
                            Ui.WriteLine("Det gick inte att skapa ett fordon");
                        }

                        result = menuInputResult;
                    }
                    else
                    {
                        result = MenuInputResult.WRONG_INPUT;
                    }

                    Ui.WriteLine("Return för att fortsätta");
                    Ui.ReadLine();
                }
                else
                {
                    result = MenuInputResult.WRONG_INPUT;
                }
            }

            return result;
        }


        /// <summary>
        /// Metoden skapar ett nytt fordon av typ car, bus eller motocycle
        /// </summary>
        /// <param name="enumVehicleType">Enmu som bestämmer vilket fordon vi skall skapa</param>
        /// <returns></returns>
        private (MenuInputResult menuInputResult, ICanBeParkedInGarage newVehicle) CreateVehicle(Vehicle_Type enumVehicleType)
        {
            string strInput = String.Empty;
            MenuInputResult result = MenuInputResult.NA;
            ICanBeParkedInGarage newVehicle = null;

            string strRegistrationNumber = String.Empty;
            string strColor = String.Empty;
            int iNumberOfWheels = 0;
            int iNumberOfSeatedPassengers = 0;

            // Hämta defaulta värden för fordonet som vi skall skapa
            var (strDefaultColor, iDefaultNumberOfWheels, iDefaultNumberOfSeatedPassengers) = this.VehicleFactory.GetDefaultVehicleData(enumVehicleType);

            // Hämta registreringsnumret från användaren
            var (tmpResult, strTmpRegistrationNumber) = GetRegistrationNumber();

            if(tmpResult == MenuInputResult.CONTINUE)
            {// Vi fortsätter och hämtar fordonets färg
                strRegistrationNumber = strTmpRegistrationNumber;

                // Hämta fordonets färg från användaren
                var (tmpResult1, strTmpColor) = GetColor(strDefaultColor);

                if(tmpResult1 == MenuInputResult.CONTINUE)
                {
                    strColor = strTmpColor;

                    // Hämta antal hjul som finns på fordonet
                    var (tmpResult2, iTmpNumberOfWheels) = GetNumberOfWheels(iDefaultNumberOfWheels);

                    if(tmpResult2 == MenuInputResult.CONTINUE)
                    {
                        iNumberOfWheels = iTmpNumberOfWheels;

                        // Hämta antal sittande passagerare
                        var (tmpResult3, iTmpNumberOfSeatedPassengers) = GetNumberOfSeatedPassengers(iDefaultNumberOfSeatedPassengers);

                        if (tmpResult3 == MenuInputResult.CONTINUE)
                        {
                            iNumberOfSeatedPassengers = iTmpNumberOfSeatedPassengers;

                            newVehicle = this.VehicleFactory.CreateVehicle(enumVehicleType, strRegistrationNumber, strColor, iNumberOfWheels, iNumberOfSeatedPassengers);
                            if(newVehicle != null)
                            {
                                // Nu behöver jag lägga till registreringsnumret till de upptagna registreringsnumren
                                this.RegistrationNumberRegister.AddRegistrationNumber(strRegistrationNumber);
                                result = MenuInputResult.TO_GARAGE_MENU;
                            }
                        }
                    }
                }
            }
            
            return (result, newVehicle);
        }

        private (MenuInputResult menuInputResult, int iNumberOfSeatedPassengers) GetNumberOfSeatedPassengers(int iDefaultNumberOfSeatedPassengers)
        {
            MenuInputResult result = MenuInputResult.NA;
            int iNumberOfSeatedPassengers = 0;
            bool bRun = true;            

            do
            {
                this.Ui.Clear();

                if (result == MenuInputResult.WRONG_INPUT)
                    this.Ui.WriteLine("Felaktig inmatning");

                // TODO Implementera GetNumberOfSeatedPassengers
                // this.Ui.WriteLine(this.MenuFactory.GetMenu(MenuType.CREATE_REGISTRATIONNUMBER));

                // Inläsning av fordonets färg från användarna
                string strInput = this.Ui.ReadLine();
                if (!String.IsNullOrWhiteSpace(strInput))
                {
                }
                else
                {
                    result = MenuInputResult.WRONG_INPUT;
                }

            } while (bRun);


            return (result, iNumberOfSeatedPassengers);
        }

        private (MenuInputResult menuInputResult, int iNumberOfWheels) GetNumberOfWheels(int iDefaultNumberOfWheels)
        {
            MenuInputResult result = MenuInputResult.NA;
            int iNumberOfWheels = 0;
            bool bRun = true;            

            do
            {
                this.Ui.Clear();

                if (result == MenuInputResult.WRONG_INPUT)
                    this.Ui.WriteLine("Felaktig inmatning");

                // TODO Implementera GetNumberOfWheels
                // this.Ui.WriteLine(this.MenuFactory.GetMenu(MenuType.CREATE_REGISTRATIONNUMBER));

                // Inläsning av fordonets färg från användarna
                string strInput = this.Ui.ReadLine();
                if (!String.IsNullOrWhiteSpace(strInput))
                {
                }
                else
                {
                    result = MenuInputResult.WRONG_INPUT;
                }

            } while (bRun);

            return (result, iNumberOfWheels);
        }


        /// <summary>
        /// Metoden läser in fordonets färg från användaren. Användaren kan också välja att använd den default färgen
        /// </summary>
        /// <param name="strDefaultColor">Default färg</param>
        /// <returns>MenuInputResult och fordonets färg</returns>
        private (MenuInputResult menuInputResult, string strColor) GetColor(string strDefaultColor)
        {
            MenuInputResult result = MenuInputResult.NA;
            string strColor = strDefaultColor.ToUpper();
            bool bRun = true;

            do
            {
                this.Ui.Clear();

                if (result == MenuInputResult.WRONG_INPUT)
                    this.Ui.WriteLine("Felaktig inmatning");

                // Visa menyn. Lite special för att kunna få med default färg
                string strColorMenu = this.MenuFactory.GetMenu(MenuType.CREATE_REGISTRATIONNUMBER);
                strColorMenu = strColorMenu + ". (Default är " + strColor + ")" + System.Environment.NewLine;
                this.Ui.WriteLine(strColorMenu);

                // Inläsning av fordonets färg från användarna
                string strInput = this.Ui.ReadLine();
                if (!String.IsNullOrWhiteSpace(strInput))
                {
                    strInput = strInput.Trim();
                    if (strInput.Length == 1 && strInput.StartsWith('0'))
                    {// Användaren har valt att avsluta programmet. Återgå till menyn
                        result = MenuInputResult.TO_GARAGE_MENU;
                        return (result, strColor);
                    }
                    else if (strInput.Length == 1 && strInput.StartsWith('1'))
                    {// Användaren har valt den defaulta färgen

                        strInput = strDefaultColor.ToString();
                    }

                    strColor = strInput.ToUpper();
                    result = MenuInputResult.CONTINUE;
                    bRun = false;
                }
                else
                {
                    result = MenuInputResult.WRONG_INPUT;
                }

            } while (bRun);

            return (result, strColor);
        }


        /// <summary>
        /// Metoden läser in registreringsnumret från användaren
        /// Användaren kan också välja att automatisk skapa ett registreringsnummer
        /// </summary>
        /// <returns>MenuInputResult och registreringsnumret</returns>
        private (MenuInputResult menuInputResult, string strRegistrationNumber) GetRegistrationNumber()
        {
            MenuInputResult result = MenuInputResult.NA;
            string strRegistrationNumber = String.Empty;
            string strInput = String.Empty;
            bool bRun = true;

            do
            {
                this.Ui.Clear();

                if (result == MenuInputResult.WRONG_INPUT)
                    this.Ui.WriteLine("Felaktig inmatning");

                if (result == MenuInputResult.REGISTRATIONNUMBER_EXISTS)
                    this.Ui.WriteLine("Registreringsnumret finns redan");

                this.Ui.WriteLine(this.MenuFactory.GetMenu(MenuType.CREATE_REGISTRATIONNUMBER));

                // Inläsning av fordonets registreringsnummer från användaren
                strInput = this.Ui.ReadLine();
                if (!String.IsNullOrWhiteSpace(strInput))
                {
                    strInput = strInput.Trim();
                    if (strInput.Length == 1 && strInput.StartsWith('0'))
                    {// Användaren har valt att avsluta programmet. Återgå till menyn
                        result = MenuInputResult.TO_GARAGE_MENU;
                        return (result, strRegistrationNumber);
                    }
                    else if (strInput.Length == 1 && strInput.StartsWith('1'))
                    {// Användaren har valt att automatsikt skapa ett registreringsnummer

                        strInput = this.RegistrationNumberRegister.CreateRandomRegistrationNumber();
                    }

                    strRegistrationNumber = strInput.ToUpper();

                    bool bRegistrationNumberExists = this.RegistrationNumberRegister.CheckIfRegistrationnNumberExists(strRegistrationNumber);
                    if (bRegistrationNumberExists)
                        result = MenuInputResult.REGISTRATIONNUMBER_EXISTS;
                    else
                    {
                        bRun = false;
                        result = MenuInputResult.CONTINUE;
                    }                        
                }
                else
                {
                    result = MenuInputResult.WRONG_INPUT;
                }

            }while (bRun);

            return (result, strRegistrationNumber);
        }
    }
}
