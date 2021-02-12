using Excercise5Garage.RegistrationNumber.Interface;
using System;
using System.Collections.Generic;
using Excercise5Garage.Extensions;
using Excercise5Garage.UI.Interface;

namespace Excercise5Garage.RegistrationNumber
{
    /// <summary>
    /// Klass med information om vilka registreringsnummer som redan finns och används
    /// Man kan lägga till och radera registreringsnummer
    /// Finns även en metod för att skapa slumpmässigt registreringsnummer som inte är använt
    /// </summary>
    public class RegistrationNumberRegister : IRegistrationNumberRegister
    {
        /// <summary>
        /// Antalet registreringsnummer som finns sparade
        /// </summary>
        public int NumberOfRegistrationNumbers => RegistrationNumbers.Count;

        /// <summary>
        /// Lista med registreringsnummer
        /// </summary>
        private IList<string> RegistrationNumbers { get; }



        /// <summary>
        /// Konstruktor
        /// Skapar en ny lista för registreringsnummer
        /// </summary>
        public RegistrationNumberRegister()
        {
            this.RegistrationNumbers = new List<string>();
        }


        /// <summary>
        /// Metoden skriver ut hur många registreringsnummer som finns i registret
        /// </summary>
        /// <param name="ui">Refernse till ui</param>
        public void PrintRegister(IUI ui)
        {
            ui.WriteLine($"Det finns {RegistrationNumbers.Count} registreringsnummer i registret");
        }


        /// <summary>
        /// Metoden kotrollera så att inte registreringsnumret redan finns
        /// </summary>
        /// <param name="strRegistrationNumber">Sökt registreringsnummer</param>
        /// <returns>true om registreringsnumret redan finns. Annars returneras false</returns>
        public bool CheckIfRegistrationnNumberExcist(string strRegistrationNumber)
        {
            bool bRegisterNumberExcist = false;

            string strTmpRegistrationNumber = strRegistrationNumber.ToUpper();

            bRegisterNumberExcist = this.RegistrationNumbers.Contains(strTmpRegistrationNumber);
            return bRegisterNumberExcist;
        }


        /// <summary>
        /// Metoden sparar registreringsnummer som inte finns sparade
        /// </summary>
        /// <param name="strRegistrationNumber">Registreringsnummer som vi vill spara</param>
        /// <returns>true om det gick spara registreringsnumret. Annars returneras false</returns>
        public bool AddRegistrationNumber(string strRegistrationNumber)
        {
            bool bAddedRegistrationNumber = false;

            if (CheckIfRegistrationnNumberExcist(strRegistrationNumber) == false)
            {
                this.RegistrationNumbers.Add(strRegistrationNumber.ToUpper());
                bAddedRegistrationNumber = true;
            }

            return bAddedRegistrationNumber;
        }


        /// <summary>
        /// Metoden raderar redan sparade registreringsnummer
        /// </summary>
        /// <param name="strRegistrationNumber">Registreringsnummer som vi vill radera</param>
        /// <returns>true om det gick radera registreringsnumret. Annars returneras false</returns>
        public bool RemoveRegistrationNumber(string strRegistrationNumber)
        {
            bool bRemovedRegistrationNumber = false;

            if (CheckIfRegistrationnNumberExcist(strRegistrationNumber) == false)
                bRemovedRegistrationNumber = this.RegistrationNumbers.Remove(strRegistrationNumber.ToUpper());

            return bRemovedRegistrationNumber;
        }


        /// <summary>
        /// Metoden skapar ett slumpmässigt genererat registreringsnummer som inte används tidigare
        /// </summary>
        /// <returns>Slumpmässigt genererat registreringsnummer</returns>
        public string CreateRandomRegistrationNumber()
        {
            bool bRun = true;
            string strRegistrationNumber = String.Empty;
            Random rand = new Random();

            do
            {
                // Skapa nummer delen av registreringsnumret.3 siffror.  000 till 999
                int iNumberPart = rand.Next(0, 1000);
                // Vi vill att siffran skall ha 3 positioner
                string strNumberPart = iNumberPart.ToString("D3");

                // Skapa text delen av registreringsnumret. 3 bokstäver
                string strChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZÅÄÖ";
                string strTextPart = String.Empty;

                for (int i = 0; i < 3; i++)
                    strTextPart += strChars.RandomChar();

                strRegistrationNumber = strTextPart + strNumberPart;

                if (!this.CheckIfRegistrationnNumberExcist(strRegistrationNumber))
                    // Registreringsnumret fanns inte. Det går bra att använda det
                    bRun = false;
            }
            while (bRun);


            return strRegistrationNumber;
        }
    }
}
