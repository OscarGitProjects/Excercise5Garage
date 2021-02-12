using Excercise5Garage.Garage.Interface;
using Excercise5Garage.GarageHandler.Interface;
using Excercise5Garage.Menu.Interface;
using Excercise5Garage.RegistrationNumber.Interface;
using Excercise5Garage.UI.Interface;
using Excercise5Garage.Vehicle.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Excercise5Garage.Menu
{
    /// <summary>
    /// klass med funktioner för att användaren skall kunna lämna garaget med ett fordon
    /// </summary>
    public class LeaveTheGarageMenu
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
        public LeaveTheGarageMenu(IMenuFactory menuFactory, IUI ui, IList<IGarageHandler> lsGarageHandlers, Guid guidSelectedGarageHandlerGuid, IRegistrationNumberRegister registrationNumberRegister)
        {
            MenuFactory = menuFactory;
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

                this.Ui.WriteLine(this.MenuFactory.GetMenu(MenuType.LEAVE_WITH_VEHICLE_MENU));

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
            MenuInputResult result = MenuInputResult.TO_GARAGE_MENU;
            // Hämta vald garagehandler
            IGarageHandler garageHandler = this.GarageHandlers.FirstOrDefault(g => g.GuidId.Equals(this.SelectedGarageHandlerGuid));

            if (garageHandler != null)
            {
                if (garageHandler.Garage.Count() > 0)
                {// Det finns fordon i Garaget

                    // Inläsning av vald siffra eller registreringsnummer
                    string strInput = this.Ui.ReadLine();

                    if (!String.IsNullOrWhiteSpace(strInput))
                    {
                        strInput = strInput.Trim();

                        if (strInput.Length == 1 && strInput.StartsWith('0'))
                        {// Användaren har valt att avsluta programmet. Återgå till huvudmenyn
                            result = MenuInputResult.TO_GARAGE_MENU;
                        }
                        else
                        {
                            strInput = strInput.ToUpper();

                            IVehicle tmpVehicle = null;
                            // Leta upp sökt fordon i garaget. Radera det. Om det gick radera skall vi även radera registreringsnumret från registret med upptagna registreringsnummer
                            foreach (ICanBeParkedInGarage vehicle in garageHandler.Garage)
                            {
                                tmpVehicle = vehicle as IVehicle;
                                if (tmpVehicle != null)
                                {
                                    if (tmpVehicle.RegistrationNumber.CompareTo(strInput) == 0)
                                    {// Vi har hittat sökt fordon. Radera från garaget

                                        if (garageHandler.RemoveVehicle(vehicle))
                                        {// Vi har raderat fordonet från garaget

                                            // Radera registreringsnumret från registret av använda registreringsnummer
                                            this.RegistrationNumberRegister.RemoveRegistrationNumber(tmpVehicle.RegistrationNumber);
                                        }

                                        Ui.WriteLine("Return för att fortsätta");
                                        Ui.ReadLine();
                                        result = MenuInputResult.TO_GARAGE_MENU;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        result = MenuInputResult.TO_GARAGE_MENU;
                    }
                }
                else
                {
                    Ui.WriteLine("Det finns inga fordon i garaget");
                    Ui.WriteLine("Return för att återgå till menyn");
                    Ui.ReadLine();
                    result = MenuInputResult.TO_GARAGE_MENU;
                }
            }

            return result;
        }
    }
}
