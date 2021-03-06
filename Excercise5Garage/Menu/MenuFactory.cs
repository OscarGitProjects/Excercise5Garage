﻿using Excercise5Garage.Menu.Interface;
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
        CREATE_VEHICLE_FAILED = 7,
        TO_SEARCH_MENU = 8

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
        DELETE_GARAGE = 12,
        SEARCH_VEHICLE_WITH_REGISTRATIONNUMBER = 13,
        SEARCH_FOR_USED_REGISTRATIONNUMBER = 14,
        SEARCH_VEHICLE_WITH_COLOR = 15,
        SEARCH_VEHICLE_WITH_NUMBER_OF_WHEELS = 16,
        SEARCH_VEHICLE_WITH_VEHICLE_TYPE = 17,
        SEARCH_VEHICLE_WITH_TEXT = 18,
        SEARCH_VEHICLE_WITH_NUMBER_OF_SEATED_PASSENGERS = 19

    }


    /// <summary>
    /// Klass som skapar olika menyer
    /// </summary>
    public class MenuFactory : IMenuFactory
    {
        /// <summary>
        /// Metod som returnerar en meny som en textsträng
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
                    strBuilder.Append("3. Radera garage");
                    strMenu = strBuilder.ToString();
                    break;
                case MenuType.CREATE_GARAGE_MENU:
                    strBuilder.AppendLine("Skapa ett nytt garage");
                    strBuilder.AppendLine("0. För att återgå till huvudmenyn");
                    strBuilder.Append("Ange namn på garaget");
                    strMenu = strBuilder.ToString();
                    break;
                case MenuType.SELECT_GARAGE_MENU:
                    strBuilder.AppendLine("Välj garage");
                    strBuilder.AppendLine("Välj siffra för önskat garage");
                    strBuilder.Append("0. För att återgå till huvudmenyn");
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
                    strBuilder.Append("6. Söka efter fordon");
                    strMenu = strBuilder.ToString();
                    break; ;
                case MenuType.SOK_VEHICLE_IN_GARAGE_MENU:
                    strBuilder.AppendLine("Sök efter fordon");
                    strBuilder.AppendLine("0. För att återgå till menyn");
                    strBuilder.AppendLine("1. Sök på registreringsnummer");
                    strBuilder.AppendLine("2. Sök på upptagna registreringsnummer");
                    strBuilder.AppendLine("3. Sök på färg");
                    strBuilder.AppendLine("4. Sök på fordonstyp");
                    strBuilder.AppendLine("5. Sök på antal hjul");
                    strBuilder.AppendLine("6. Sök på antal sittande passagerare");
                    strBuilder.Append("7. Sök på text");
                    strMenu = strBuilder.ToString();
                    break;
                case MenuType.CREATE_AND_PARK_VEHICLE_MENU:                    
                    strBuilder.AppendLine("Skapa nytt fordon och parkera i garaget");
                    strBuilder.AppendLine("Välj siffra för önskad funktion");
                    strBuilder.AppendLine("0. För att återgå till menyn");
                    strBuilder.AppendLine("1. Skapa en bil");
                    strBuilder.AppendLine("2. Skapa en buss");
                    strBuilder.Append("3. Skapa en motorcykel");
                    strMenu = strBuilder.ToString();
                    break;
                case MenuType.LEAVE_WITH_VEHICLE_MENU:
                    strBuilder.AppendLine("Lämna garaget med fordon");
                    strBuilder.AppendLine("0. För att återgå till menyn");
                    strBuilder.Append("Ange fordonets registreringsnummer");
                    strMenu = strBuilder.ToString();
                    break;
                case MenuType.CREATE_REGISTRATIONNUMBER:
                    strBuilder.AppendLine("Ange registreringsnumret");
                    strBuilder.AppendLine("0. För att återgå till menyn");
                    strBuilder.AppendLine("1. För att automatiskt skapa ett registreringsnummer");
                    strBuilder.Append("Ange fordonets registreringsnummer");
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
                    strBuilder.Append("0. För att återgå till huvudmenyn");
                    strMenu = strBuilder.ToString();
                    break;
                case MenuType.SEARCH_VEHICLE_WITH_REGISTRATIONNUMBER:
                    strBuilder.AppendLine("Sök fordon på registreringsnummer");
                    strBuilder.AppendLine("0. För att återgå till huvudmenyn");
                    strBuilder.Append("Ange registreringsnummer");
                    strMenu = strBuilder.ToString();
                    break;
                case MenuType.SEARCH_FOR_USED_REGISTRATIONNUMBER:
                    strBuilder.AppendLine("Sök registreringsnummer i registret med använda registreringsnummer");
                    strBuilder.AppendLine("0. För att återgå till huvudmenyn");
                    strBuilder.Append("Ange registreringsnummer");
                    strMenu = strBuilder.ToString();
                    break;
                case MenuType.SEARCH_VEHICLE_WITH_COLOR:
                    strBuilder.AppendLine("Sök fordon med färgen");
                    strBuilder.AppendLine("0. För att återgå till huvudmenyn");
                    strBuilder.Append("Ange färg");
                    strMenu = strBuilder.ToString();
                    break;
                case MenuType.SEARCH_VEHICLE_WITH_NUMBER_OF_WHEELS:
                    strBuilder.AppendLine("Sök fordon med antal hjul");
                    strBuilder.AppendLine("0. För att återgå till huvudmenyn");
                    strBuilder.Append("Ange antal hjul");
                    strMenu = strBuilder.ToString();
                    break;
                case MenuType.SEARCH_VEHICLE_WITH_VEHICLE_TYPE:
                    strBuilder.AppendLine("Sök fordon av typ");
                    strBuilder.AppendLine("0. För att återgå till huvudmenyn");
                    strBuilder.Append("Ange vilken typ av fordon som söks");
                    strMenu = strBuilder.ToString();
                    break;
                case MenuType.SEARCH_VEHICLE_WITH_TEXT:
                    strBuilder.AppendLine("Sök fordon. Returnerar alla som matchar något uttryck som ni har sökt på");
                    strBuilder.AppendLine("0. För att återgå till huvudmenyn");
                    strBuilder.AppendLine("Ange en text med vad ni söker"); 
                    strBuilder.Append("ex färg, typ av fordon, registreringsnummer, antal hjul, antal passagerare");
                    strMenu = strBuilder.ToString();
                    break;
                case MenuType.SEARCH_VEHICLE_WITH_NUMBER_OF_SEATED_PASSENGERS:
                    strBuilder.AppendLine("Sök fordon med antal sittande passagerare");
                    strBuilder.AppendLine("0. För att återgå till huvudmenyn");
                    strBuilder.Append("Ange antal sittande passagerare");
                    strMenu = strBuilder.ToString();
                    break;
            }

            return strMenu;
        }
    }
}
