using Excercise5Garage.GarageHandler.Interface;
using Excercise5Garage.Menu.Interface;
using Excercise5Garage.UI.Interface;
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
        /// Konstruktor
        /// </summary>
        /// <param name="menuFactory">Referense till en factory där man kan hämta text till olika menyer</param>
        /// <param name="ui">Referense till objekt för att skriva och hämta indata</param>
        /// <param name="lsGarageHandlers">lista med olika garagehandlers. Varje garagehandler hanterar ett garage</param>
        /// <param name="guidSelectedGarageHandlerGuid">Guid för vald GarageHandler</param>
        public GarageMenu(IMenuFactory menuFactory, IUI ui, IList<IGarageHandler> lsGarageHandlers, Guid guidSelectedGarageHandlerGuid)
        {
            MenuFactory = menuFactory;
            Ui = ui;
            GarageHandlers = lsGarageHandlers;
            this.SelectedGarageHandlerGuid = guidSelectedGarageHandlerGuid;
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
                    this.Ui.WriteLine($"{strName}. Har lediga platser {strIsFull}");
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
                else
                {
                    // TODO Implementera GarageMenu HandleInput

                    // TODO SKAPA MENYN I MenuFactory
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