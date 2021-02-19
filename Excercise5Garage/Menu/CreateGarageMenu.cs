using Excercise5Garage.Garage;
using Excercise5Garage.Garage.Interface;
using Excercise5Garage.GarageHandler.Interface;
using Excercise5Garage.Menu.Interface;
using Excercise5Garage.UI.Interface;
using System;
using System.Collections.Generic;

namespace Excercise5Garage.Menu
{
    /// <summary>
    /// Klass med funktioner för att kunna hantera skapandet av ett nytt garage
    /// Garaget kommer att hanteras av en garagehandler
    /// </summary>
    public class CreateGarageMenu
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
        /// Konstruktor
        /// </summary>
        /// <param name="menuFactory">Referense till en factory där man kan hämta text till olika menyer</param>
        /// <param name="ui">Referense till objekt för att skriva och hämta indata</param>
        /// <param name="lsGarageHandlers">lista med olika garagehandlers. Varje garagehandler hanterar ett garage</param>
        /// <exception cref="System.NullReferenceException">Kan kastas om referensen till menuFactory eller ui är null</exception>
        public CreateGarageMenu(IMenuFactory menuFactory, IUI ui, IList<IGarageHandler> lsGarageHandlers)
        {
            if (menuFactory == null)
                throw new NullReferenceException("NullReferenceException. GarageHandler.GarageHandler(). menuFactory referensen är null");

            if (ui == null)
                throw new NullReferenceException("NullReferenceException. GarageHandler.GarageHandler(). ui referensen är null");


            MenuFactory = menuFactory;
            Ui = ui;

            // Om vi inte har en lista med för garagehandlers. Skapa listan
            if (lsGarageHandlers == null)
                lsGarageHandlers = new List<IGarageHandler>();

            GarageHandlers = lsGarageHandlers;
        }


        /// <summary>
        /// Metoden visar menyerna där användaren kan skapa ett nytt garage
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

                this.Ui.WriteLine(this.MenuFactory.GetMenu(MenuType.CREATE_GARAGE_MENU));

                // Hantera inmatning från användaren
                result = HandleInput();
            }
            while (result != MenuInputResult.TO_MAIN_MENU);

            return result;
        }


        /// <summary>
        /// Metoden hanterar inmatning av kommandon från ui
        /// Hantering av inmatning av namn på garage och dess kapacitet
        /// </summary>
        /// <returns>enum MenuInputResult med olika värden beroende på användarens kommando</returns>
        private MenuInputResult HandleInput()
        {
            MenuInputResult result = MenuInputResult.NA;

            // Inläsning av namnet på garaget
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
                    // Nu har vi namnet på garaget
                    string strGarageName = strInput;

                    // Nu skall vi läsa in kapacitet
                    Ui.WriteLine("0. För att återgå till huvudmenyn");
                    Ui.WriteLine("Ange antal platser i garaget ");

                    int iCapacity = 0;

                    // Inläsning av garagets kapacitet
                    strInput = this.Ui.ReadLine();

                    if (!String.IsNullOrWhiteSpace(strInput))
                    {
                        strInput = strInput.Trim();

                        if (strInput.StartsWith('0'))
                        {// Användaren har valt att avsluta programmet. Återgå till huvudmenyn
                            result = MenuInputResult.TO_MAIN_MENU;
                        }
                        else
                        {                            
                            string strCapacity = strInput;

                            if (Int32.TryParse(strCapacity, out iCapacity) && iCapacity > 0)
                            {// Vi har en capacity

                                // Skapa ett nytt garage
                                IGarageFactory garageFactory = new GarageFactory();                                
                                IGarage<ICanBeParkedInGarage> garage = garageFactory.CreateGarage(Guid.NewGuid(), strGarageName, iCapacity);                                

                                // Skapa en handler som skall hantera det nya garaget
                                IGarageHandler garageHandler = new Excercise5Garage.GarageHandler.GarageHandler(garage, this.Ui);

                                // Lägg till handlern till en lista med olika garagehandlers
                                this.GarageHandlers.Add(garageHandler);
                                Ui.WriteLine("Skapade garage. " + garage);

                                Ui.WriteLine("Return för att återgå till huvudmenyn");
                                this.Ui.ReadLine();
                                result = MenuInputResult.TO_MAIN_MENU;
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
                }
            }
            else
            {
                result = MenuInputResult.TO_MAIN_MENU;
            }

            return result;
        }
    }
}