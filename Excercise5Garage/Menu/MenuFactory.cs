using Excercise5Garage.Menu.Interface;
using System;
using System.Text;

namespace Excercise5Garage.Menu
{
    /// <summary>
    /// Används för att returnera olika resultat från HandleInput
    /// Talar om vad som skall göras
    /// </summary>
    public enum MenuInputResult
    {
        NA = 0,
        WRONG_INPUT = 1,
        EXIT = 2,
        TO_MAIN_MENU = 3,
        TO_GARAGE_MENU = 4,
        CONTINUE = 5,
        REGISTRATIONNUMBER_EXISTS = 6,
        CREATE_VEHICLE_FAILED = 7
    }


    /// <summary>
    /// Olika menyer
    /// </summary>
    public enum MenuType
    {
        NA = 0,
        MAIN_MENU = 1,
        CREATE_GARAGE_MENU = 2,
        SELECT_GARAGE_MENU = 3,
        GARAGE_MENU = 4,
        SOK_VEHICLE_IN_GARAGE_MENU = 5,
        CREATE_AND_PARK_VEHICLE_MENU = 6,
        LEAVE_WITH_VEHICLE_MENU = 7,
        CREATE_REGISTRATIONNUMBER = 8,
        CREATE_COLOR = 9,
        CREATE_NUMBER_OF_WHEELS = 10,
        CREATE_NUMBER_SEATED_PASSENGERS = 11,
        DELETE_GARAGE = 12

    }


    /// <summary>
    /// Klass som skapar olika menyer
    /// </summary>
    public class MenuFactory : IMenuFactory
    {
        /// <summary>
        /// Metod som returnerar en meny som en textsträng
        /// 
        /// Special för menyerna CREATE_COLOR, CREATE_NUMBER_OF_WHEELS och CREATE_NUMBER_SEATED_PASSENGERS.
        /// Då sista textraden på menyn inte avslutas med nyrad. Det beror på att i de tre fallen måste jag få dit defaulta värden i slutet av meny texten
        /// </summary>
        /// <param name="menuType">MenuType som vill vill ha tillbaka</param>
        /// <returns>Önskad meny som en textsträng</returns>
        public string GetMenu(MenuType menuType)
        {
            StringBuilder strBuilder = new StringBuilder();
            string strMenu = String.Empty;

            switch (menuType)
            {
                case MenuType.MAIN_MENU:
                    strBuilder.AppendLine("Huvudmeny");
                    strBuilder.AppendLine("Välj siffra för önskad funktion");
                    strBuilder.AppendLine("0. Avsluta programmet");
                    strBuilder.AppendLine("1. Skapa garage");
                    strBuilder.AppendLine("2. Gå till garage");
                    strBuilder.AppendLine("3. Radera garage");
                    //strBuilder.AppendLine("3. Simulering av skapa garage, skapa och parkera fordon i garaget");
                    strMenu = strBuilder.ToString();
                    break;
                case MenuType.CREATE_GARAGE_MENU:
                    strBuilder.AppendLine("Skapa ett nytt garage");
                    strBuilder.AppendLine("0. För att återgå till huvudmenyn");
                    strBuilder.AppendLine("Ange namn på garaget");
                    strMenu = strBuilder.ToString();
                    break;
                case MenuType.SELECT_GARAGE_MENU:
                    strBuilder.AppendLine("Välj garage");
                    strBuilder.AppendLine("Välj siffra för önskat garage");
                    strBuilder.AppendLine("0. För att återgå till huvudmenyn");
                    strMenu = strBuilder.ToString();
                    break;
                case MenuType.GARAGE_MENU:
                    strBuilder.AppendLine("Välj siffra för önskad funktion");
                    strBuilder.AppendLine("0. För att återgå till huvudmenyn");
                    strBuilder.AppendLine("1. Skapa och Parkera fordon");
                    strBuilder.AppendLine("2. Lämna med ett fordon");
                    strBuilder.AppendLine("3. Simulering. Skapa och parkera ett antal fordon");
                    strBuilder.AppendLine("   Sedan lämnar ett fordon garaget");
                    strBuilder.AppendLine("4. Lista alla fordon");
                    strBuilder.AppendLine("5. Lista alla fordon per typ");
                    strBuilder.AppendLine("6. Söka efter fordon");
                    strMenu = strBuilder.ToString();
                    break; ;
                case MenuType.SOK_VEHICLE_IN_GARAGE_MENU:

                    // TODO GÖR MENYN FÖR SÖKNING AV FORDON

                    break;
                case MenuType.CREATE_AND_PARK_VEHICLE_MENU:                    
                    strBuilder.AppendLine("Skapa nytt fordon och parkera i garaget");
                    strBuilder.AppendLine("Välj siffra för önskad funktion");
                    strBuilder.AppendLine("0. För att återgå till menyn");
                    strBuilder.AppendLine("1. Skapa en bil");
                    strBuilder.AppendLine("2. Skapa en buss");
                    strBuilder.AppendLine("3. Skapa en motorcykel");
                    strMenu = strBuilder.ToString();
                    break;
                case MenuType.LEAVE_WITH_VEHICLE_MENU:
                    strBuilder.AppendLine("Lämna garaget med fordon");
                    strBuilder.AppendLine("0. För att återgå till menyn");
                    strBuilder.AppendLine("Ange fordonets registreringsnummer");
                    strMenu = strBuilder.ToString();
                    break;
                case MenuType.CREATE_REGISTRATIONNUMBER:
                    strBuilder.AppendLine("Ange registreringsnumret");
                    strBuilder.AppendLine("0. För att återgå till menyn");
                    strBuilder.AppendLine("1. För att automatiskt skapa ett registreringsnummer");
                    strBuilder.AppendLine("Ange fordonets registreringsnummer");
                    strMenu = strBuilder.ToString();
                    break;
                case MenuType.CREATE_COLOR:
                    strBuilder.AppendLine("Ange färg");
                    strBuilder.AppendLine("0. För att återgå till menyn");
                    strBuilder.AppendLine("1. För default värde");
                    strBuilder.Append("Ange fordonets färg");
                    strMenu = strBuilder.ToString();
                    break;
                case MenuType.CREATE_NUMBER_OF_WHEELS:
                    strBuilder.AppendLine("Ange antal hjul");
                    strBuilder.AppendLine("0. För att återgå till menyn");
                    strBuilder.AppendLine("1. För default värde");
                    strBuilder.Append("Ange antal hjul på fordonet");
                    strMenu = strBuilder.ToString();
                    break;
                case MenuType.CREATE_NUMBER_SEATED_PASSENGERS:
                    strBuilder.AppendLine("Ange antal sittande passagerare");
                    strBuilder.AppendLine("0. För att återgå till menyn");
                    strBuilder.AppendLine("1. För default värde");
                    strBuilder.Append("Ange antal sittande passagerare i fordonet");
                    strMenu = strBuilder.ToString();
                    break;
                case MenuType.DELETE_GARAGE:
                    strBuilder.AppendLine("Radera garage");
                    strBuilder.AppendLine("Välj siffra för önskat garage");
                    strBuilder.AppendLine("0. För att återgå till huvudmenyn");
                    strMenu = strBuilder.ToString();
                    break;
            }

            return strMenu;
        }
    }
}
