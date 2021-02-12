using Excercise5Garage.Menu.Interface;
using System;
using System.Text;

namespace Excercise5Garage.Menu
{
    public enum MenuInputResult
    {
        NA = 0,
        WRONG_INPUT = 1,
        EXIT = 2,
        TO_MAIN_MENU = 3,
        TO_GARAGE_MENU = 4

    }

    public enum MenuType
    {
        NA = 0,
        MAIN_MENU = 1,
        CREATE_GARAGE_MENU = 2,
        SELECT_GARAGE_MENU = 3,
        GARAGE_MENU = 4,
        SOK_VEHICLE_IN_GARAGE_MENU = 5,
        CREATE_VEHICLE_MENU = 6,
        LEAVE_WITH_VEHICLE_MENU = 7
    }

    public class MenuFactory : IMenuFactory
    {
        public string GetMenu(MenuType menuType)
        {
            StringBuilder strBuilder = new StringBuilder();
            string strMenu = String.Empty;

            switch (menuType)
            {
                case MenuType.MAIN_MENU:
                    strBuilder.AppendLine("Huvudmeny");
                    strBuilder.AppendLine("Välj siffra för önskad funktion?");
                    strBuilder.AppendLine("0. Avsluta programmet");
                    strBuilder.AppendLine("1. Skapa garage");
                    strBuilder.AppendLine("2. Gå till garage");
                    strBuilder.AppendLine("3. Simulering av skapa garage, skapa och parkera fordon i garaget");
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
                    strBuilder.AppendLine("Välj siffra för önskat garage?");
                    strBuilder.AppendLine("0. För att återgå till huvudmenyn");
                    strMenu = strBuilder.ToString();
                    break;
                case MenuType.GARAGE_MENU:
                    strBuilder.AppendLine("Välj siffra för önskad funktion?");
                    strBuilder.AppendLine("0. För att återgå till huvudmenyn");
                    strBuilder.AppendLine("1. Skapa och Parkera fordon");
                    strBuilder.AppendLine("2. Lämna med ett fordon");
                    strBuilder.AppendLine("3. Simulering. Skapa och parkera ett antal fordon");
                    strBuilder.AppendLine("4. Lista alla fordon");
                    strBuilder.AppendLine("5. Lista alla fordon per typ");
                    strBuilder.AppendLine("6. Söka efter fordon");
                    strMenu = strBuilder.ToString();
                    break; ;
                case MenuType.SOK_VEHICLE_IN_GARAGE_MENU:

                    // TODO GÖR MENYN FÖR SÖKNING AV FORDON

                    break;
                case MenuType.CREATE_VEHICLE_MENU:

                    // TODO SKAPA MENY FÖR ATTT SKAPA ETT FORDON

                    break;
                case MenuType.LEAVE_WITH_VEHICLE_MENU:
                    strBuilder.AppendLine("Lämna garaget med fordon");
                    strBuilder.AppendLine("0. För att återgå till menyn");
                    strBuilder.AppendLine("Ange fordonets registreringsnummer");
                    strMenu = strBuilder.ToString();
                    break;

            }

            return strMenu;
        }
    }
}
