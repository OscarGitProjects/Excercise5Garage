﻿using Excercise5Garage.Extensions;
using Excercise5Garage.Garage.Interface;

namespace Excercise5Garage.Vehicle.WheeledVehicle
{
    /// <summary>
    /// Klass med information om en bil
    /// </summary>
    public class Car : WheeledVehicle, ICanBeParkedInGarage
    {
        /// <summary>
        /// Default värde på antal hjul
        /// </summary>
        public static int DefaultNumberOfWheels = 4;

        /// <summary>
        /// Default värde på antal passagerare
        /// </summary>
        public static int DefaultNumberOfPassengers = 5;

        /// <summary>
        /// Default färg
        /// </summary>
        public static string DefaultColor = "SVART";



        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="strRegistrationNumber">Bilens registreringsnummer</param>
        /// <param name="strColor">Bilens färg. Default värde är Svart</param>
        /// <param name="iNumberOfWheels">Antal hjul på bilen. Default värde är 4</param>
        /// <param name="iNumberOfSeatedPassengers">Antal sittande passagerar som fordonet kan ta. Default är 5</param>
        public Car(string strRegistrationNumber, string strColor = "SVART", int iNumberOfWheels = 4, int iNumberOfSeatedPassengers = 5) : base(strRegistrationNumber, strColor, iNumberOfWheels, iNumberOfSeatedPassengers)
        {
        }


        /// <summary>
        /// Överlagring av ToString()
        /// </summary>
        /// <returns>String med information om objektet</returns>
        public override string ToString()
        {
            return $"{this.GetType().Name}. Registreringsnummer: {this.RegistrationNumber}, Färg: {this.Color?.ToLower()?.FirstToUpper()}, Antal hjul: {this.NumberOfWheels}, Antal sittande passagerare: {this.NumberOfSeatedPassengers}";
        }
    }
}
