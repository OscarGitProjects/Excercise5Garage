using Excercise5Garage.GarageHandler.Interface;
using Excercise5Garage.Menu.Interface;
using Excercise5Garage.UI.Interface;
using System;
using System.Collections.Generic;

namespace Excercise5Garage.Menu
{
    /// <summary>
    /// Klass med funktionalitet där användaren kan välja ett garage
    /// </summary>
    public class SelectGarageMenu
    {
        /// <summary>
        /// Factory där man kan skapa menyer
        /// </summary>
        public IMenuFactory MenuFactory { get; }

        /// <summary>
        /// Referens till ui
        /// </summary>
        public IUI Ui { get; }

        /// <summary>
        /// Referens till en lista med handlers av olika garage
        /// </summary>
        public IList<IGarageHandler> GarageHandlers { get; private set; }



        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="menuFactory">Referense till en factory där man kan hämta text till olika menyer</param>
        /// <param name="ui">Referense till objekt för att skriva och hämta indata</param>
        /// <param name="lsGarageHandlers">lista med olika garagehandlers. Varje garagehandler hanterar ett garage</param>
        public SelectGarageMenu(IMenuFactory menuFactory, IUI ui, IList<IGarageHandler> lsGarageHandlers)
        {
            MenuFactory = menuFactory;
            Ui = ui;
            GarageHandlers = lsGarageHandlers;
        }


        /// <summary>
        /// Metoden visar menyn för att välja garage
        /// </summary>
        /// <returns>Index för valt garage eller 0 för ej valt garage</returns>
        public int Show()
        {
            MenuInputResult result = MenuInputResult.NA;
            int iSelectedGarageHandler = 0;

            do
            {
                this.Ui.Clear();

                if (result == MenuInputResult.WRONG_INPUT)
                    this.Ui.WriteLine("Felaktig inmatning");

                this.Ui.WriteLine(this.MenuFactory.GetMenu(MenuType.SELECT_GARAGE_MENU));

                // Nu skall jag skapa meny valen för varje garageHandler
                string strIsFull = String.Empty;
                int iCount = 1;
                if (GarageHandlers.Count > 0)
                {
                    foreach (IGarageHandler garageHandler in GarageHandlers)
                    {
                        // Hämta uppgifter om garaget
                        var (strId, strName, bIsFull, iCapacity, iNumberOfParkedVehicle) = garageHandler.GetGarageInfo();

                        // Skapa en lämplig utskrift för menyn
                        strIsFull = bIsFull ? "Nej" : "Ja";
                        Ui.WriteLine($"{iCount}. {strName}. Har lediga platser {strIsFull}");
                        iCount++;
                    }
                }
                else
                {
                    Ui.WriteLine("Det finns inga garage");
                }

                // Hantera inmatning från användaren
                var (returnResult, iReturnSelectedGarageHandler) = HandleInput(GarageHandlers.Count);

                result = returnResult;
                if (result != MenuInputResult.WRONG_INPUT)
                    iSelectedGarageHandler = iReturnSelectedGarageHandler;
            }
            while (result != MenuInputResult.TO_MAIN_MENU);

            return iSelectedGarageHandler;
        }


        /// <summary>
        /// Metoden hanterar inmatning av kommandon från ui
        /// Hanterar inmatning av val av Garagehandler
        /// </summary>
        /// <param name="iNumberOfGarageHandlers">Antalet garagehandler som det finns</param>
        /// <returns>enum MenuInputResult med olika värden beroende på användarens kommando. Index till vald garagehandler</returns>
        private (MenuInputResult result, int iSelectedGarageHandler) HandleInput(int iNumberOfGarageHandlers)
        {
            MenuInputResult result = MenuInputResult.NA;
            int iSelectedGarageHandler = 0;

            // Inläsning av vald siffra
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
                    if (Int32.TryParse(strInput, out iSelectedGarageHandler) && iSelectedGarageHandler > 0 && iSelectedGarageHandler <= iNumberOfGarageHandlers)
                    {// Användaren har valt ett giltig index till en garageHandler. Nu vill jag återgå till main menu med detta index
                        result = MenuInputResult.TO_MAIN_MENU;
                    }
                    else
                        result = MenuInputResult.WRONG_INPUT;
                }
            }
            else
            {
                result = MenuInputResult.WRONG_INPUT;
            }
            
            return (result: result, iSelectedGarageHandler: iSelectedGarageHandler);
        }
    }
}