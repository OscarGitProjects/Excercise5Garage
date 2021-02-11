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
        TO_MAIN_MENU = 3
    }

    public enum MainMenuType
    {
        NA = 0,
        MAIN_MENU = 1,
        ADD_VEHICLE = 2,
        CREATE_GARAGE = 3,
        SELECT_GARAGE = 4
    }

    public class MenuFactory : IMenuFactory
    {
        public string GetMainMenu(MainMenuType mainMenuType)
        {
            StringBuilder strBuilder = new StringBuilder();
            string strMenu = String.Empty;

            switch (mainMenuType)
            {
                case MainMenuType.MAIN_MENU:
                    strBuilder.AppendLine("Huvudmeny");
                    strBuilder.AppendLine("0. Avsluta programmet");
                    strBuilder.AppendLine("1. Skapa garage");
                    strBuilder.AppendLine("2. Gå till garage");
                    strBuilder.AppendLine("3. Simulering av skapa garage, skapa och parkera fordon i garaget");
                    strMenu = strBuilder.ToString();
                    break;
                case MainMenuType.CREATE_GARAGE:
                    strBuilder.AppendLine("Skapa ett nytt garage");
                    strBuilder.AppendLine("0. För att återgå till huvudmenyn");
                    strBuilder.AppendLine("Ange namn på garaget");
                    strMenu = strBuilder.ToString();
                    break;
                case MainMenuType.SELECT_GARAGE:
                    strBuilder.AppendLine("Välj siffra för önskat garage?");
                    strBuilder.AppendLine("0. För att återgå till huvudmenyn");
                    strMenu = strBuilder.ToString();
                    break;

            }

            return strMenu;
        }
    }
}
