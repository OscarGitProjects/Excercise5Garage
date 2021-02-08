﻿using Excercise5Garage.Garage;

namespace Excercise5Garage.Vehicle.WheeledVehicle
{
    /// <summary>
    /// Klass med information om en bil
    /// </summary>
    public class Car : WheeledVehicle, ICanBeParkedInGarage
    {
        /// <summary>
        /// Antalet sittande passagerar som fordonet kan ta
        /// </summary>
        public int NumberOfSeatedPassengers { get; set; }


        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="strRegistrationNumber">Bilens registreringsnummer</param>
        /// <param name="strColor">Bilens färg. Default värde är Svart</param>
        /// <param name="iNumberOfWheels">Antal hjul på bilen. Default värde är 4</param>
        public Car(string strRegistrationNumber, string strColor = "Svart", int iNumberOfWheels = 4) : base(strRegistrationNumber, strColor, iNumberOfWheels)
        {
            NumberOfSeatedPassengers = 5;
        }


        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="strRegistrationNumber">Bilens registreringsnummer</param>
        /// <param name="strColor">Bilens färg. Default värde är Svart</param>
        /// <param name="iNumberOfWheels">Antal hjul på bilen. Default värde är 4</param>
        /// <param name="iNumberOfSeatedPassengers">Antal sittande passagerar som fordonet kan ta. Default är 5</param>
        public Car(string strRegistrationNumber, string strColor = "Svart", int iNumberOfWheels = 4, int iNumberOfSeatedPassengers = 5) : base(strRegistrationNumber, strColor, iNumberOfWheels)
        {
            NumberOfSeatedPassengers = iNumberOfSeatedPassengers;
        }

        public override string ToString()
        {
            return $"{this.GetType().Name}. Registreringsnummer: {this.RegistrationNumber}, Färg: {this.Color}, Antal hjul: {this.NumberOfWheels}, Antal sittande passagerare: {this.NumberOfSeatedPassengers}";
        }
    }
}
