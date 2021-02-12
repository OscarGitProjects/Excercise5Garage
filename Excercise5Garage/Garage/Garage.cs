using Excercise5Garage.Garage.Interface;
using Excercise5Garage.Vehicle.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Excercise5Garage.Garage
{
    /// <summary>
    /// Garage klass med funktionalitet för att lägga till och radera items av typen T. Skall vara av typen Vehicle
    /// Anledningen till att jag använder Where T : class är för att kunna sätta arrVehicles[iIndex] = null;  
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Garage<T> : IGarage<T> where T : class, ICanBeParkedInGarage
    {
        /// <summary>
        /// Array med vehicles
        /// </summary>
        private T[] arrVehicles = null;

        /// <summary>
        /// Max antal vehicle som kan parkeras i Garaget
        /// </summary>
        public int Capacity { get; private set; }

        /// <summary>
        /// Nuvarande antal vehicle som är parkerade i Garaget
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Är garaget fullt eller ej
        /// </summary>
        public bool IsFull { get { return Capacity == Count;  } }

        /// <summary>
        /// Är garaget tomt eller ej
        /// </summary>
        public bool IsEmpty { get { return Count == 0;  } }

        /// <summary>
        /// Namn på garaget
        /// </summary>
        public string GarageName { get; set; }

        /// <summary>
        /// Unik identifierare av garaget
        /// </summary>
        public Guid GarageID { get; private set; }



        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="guidId">Unik identifierare av garaget</param>
        /// <param name="strGarageName">Namn på garaget</param>
        /// <param name="iCapacity">Storleken på Array som skall innehålla T objekten. Är iCapacity negativ blir värdet 0</param>
        public Garage(Guid guidId, string strGarageName, int iCapacity)
        {
            GarageID = guidId;
            GarageName = strGarageName;
            Capacity = Math.Max(0, iCapacity);
            arrVehicles = new T[Capacity];
        }


        /// <summary>
        /// Returnerar vehicle vid index
        /// </summary>
        /// <param name="iIndex">index för sökt vehicle</param>
        /// <returns>Vehicle vid önskat index. Kan också vara null</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">Index är utanför arrayen</exception>
        public T this[int iIndex] {
            get 
            {
                if (iIndex < 0 || iIndex >= this.Capacity)
                    throw new ArgumentOutOfRangeException("ArgumentOutOfRangeException. Garage.this[int iIndex]. Index är utanför arrayen");

                return arrVehicles[iIndex];
            }
        }


        /// <summary>
        /// Metoden lägger till ett nytt vehicle
        /// </summary>
        /// <param name="vehicle">Vehicle som skall läggas till i garaget</param>
        /// <returns>true om det gick att lägga till vehicle. Annars returneras false</returns>
        /// <exception cref="System.ArgumentNullException">Kastas om referensen till vehicle är null</exception>
        public bool Add(T vehicle)
        {
            if (vehicle == null)
                throw new ArgumentNullException("ArgumentNullException. Garage.Add(T vehicle). Referensen till vehicle är null");

            if (!IsFull)
            {
                arrVehicles[Count] = vehicle;
                Count++;
                return true;
            }

            return false;
        }


        /// <summary>
        /// Metoden raderar vehicle från garaget
        /// </summary>
        /// <param name="vehicle">Vehicle som skall raderas från garaget</param>
        /// <returns>true om det gick radera vehicle från garaget. Annars returneras false</returns>
        /// <exception cref="System.ArgumentNullException">Kastas om referensen till vehicle är null</exception>
        public bool Remove(T vehicle)
        {
            if (vehicle == null)
                throw new ArgumentNullException("ArgumentNullException. Garage.Remove(T vehicle). Referensen till vehicle är null");

            bool bRemovedObject = false;
            for(int i = 0; i < arrVehicles.Length; i++)
            {
                if(arrVehicles[i] != null)
                    if (vehicle.Equals(arrVehicles[i]))
                        bRemovedObject = Remove(i);
            }

            return bRemovedObject;
        }


        /// <summary>
        /// Metoden raderar vehicle vid index från garaget
        /// </summary>
        /// <param name="iIndex">Index där vi skall radera garaget</param>
        /// <returns>true om det gick radera vehicle från garaget. Annars returneras false</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">Index är utanför arrayen</exception>
        public bool Remove(int iIndex)
        {            
            if (iIndex < 0 || iIndex >= this.Capacity)
                throw new ArgumentOutOfRangeException("ArgumentOutOfRangeException. Garage.Remove(int iIndex). Index är utanför arrayen");

            bool bRemovedObject = false;
            if (arrVehicles[iIndex] != null)
            {
                bRemovedObject = true;
                // Om det fanns ett objekt vid iIndex. Då räknas antalet object i arrayen ned
                Count--;
            }

            arrVehicles[iIndex] = null;

            // Nu vill jag flytta alla allokerade objekt till början av arrayen
            // Vill inte ha null mitt i arrayen
            MoveAllocatedObjectsInArray();

            return bRemovedObject;
        }


        /// <summary>
        /// Metoden flyttar alla allokerade objekt till början av arrayen
        /// Ser till att det inte finns några tomma element, null, mitt i arrayen
        /// </summary>
        private void MoveAllocatedObjectsInArray()
        {
            int iTmpI = 0;
            
            if (arrVehicles != null && !this.IsEmpty)
            {
                for(int i = 0; i < arrVehicles.Length; i++)
                {
                    if (arrVehicles[i] == null)
                    {
                        iTmpI = i;
                        for (int j = i + 1; j < arrVehicles.Length; j++)
                        {
                            arrVehicles[iTmpI] = arrVehicles[j];
                            arrVehicles[j] = null;
                            iTmpI++;
                        }
                    }
                }
            }
        }


        /// <summary>
        /// Metoden returnerar IEnumerator<T>
        /// </summary>
        /// <returns>Returnerra en IEnumerator<T></returns>
        public IEnumerator<T> GetEnumerator()
        {
            foreach(var item in arrVehicles)
            {
                if (item != null)
                    yield return item;
            }
        }


        /// <summary>
        /// Metoden returnerar IEnumerator
        /// </summary>
        /// <returns>Returnerar IEnumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }


        /// <summary>
        /// Metoden skriv ut all information om garaget
        /// Även information vilka fordon som är parkerade i garaget
        /// </summary>
        /// <returns>All information om garaget och de parkerade fordonen</returns>
        public string PrintAllInformation()
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.AppendLine($"Id: {this.GarageID}, Name: {this.GarageName}, Capacity: {this.Capacity}, Count: {this.Count}");

            if (!IsEmpty)
            {// Det finns parkerade fordon. Hämta info om dessa

                foreach (var item in arrVehicles)
                {
                    if (item != null)
                        strBuilder.AppendLine(item.ToString());
                }
            }

            return strBuilder.ToString();
        }


        /// <summary>
        /// Överlagring av ToString()
        /// </summary>
        /// <returns>String med information om objektet</returns>
        public override string ToString()
        {
            return $"{this.GarageName}, Kapacitet: {this.Capacity}, Antal parkerade fordon: {this.Count}";
        }


        /// <summary>
        /// Metoden räknar antalet vehicle som har sökt registreringsnummer
        /// Metoden tar inte hänsyn till om det är stora eller små bokstäver
        /// </summary>
        /// <param name="strRegistrationNumber">Registreringsnummer somm söks</param>
        /// <returns>Antalet vehicle med sökt registreringsnummer</returns>
        public int CountVehicleWithRegistrationNumber(string strRegistrationNumber)
        {
            int iNumberOfVehicleWithRegistrationNumber = 0;
            IVehicle tmpVehicle = null;

            for(int i = 0; i < arrVehicles.Length; i++)
            {
                if(arrVehicles[i] != null)
                {
                    tmpVehicle = arrVehicles[i] as IVehicle;
                    if(tmpVehicle != null)
                    {
                        if (String.Compare(tmpVehicle.RegistrationNumber, strRegistrationNumber, ignoreCase: true) == 0)
                            iNumberOfVehicleWithRegistrationNumber++;
                    }
                }
            }

            return iNumberOfVehicleWithRegistrationNumber;
        }


        /// <summary>
        /// Metoden returnera information om garagets unika id, namn och om garaget är fullt eller ej, kapacitet och antal vehicle som är parkerade
        /// </summary>
        /// <returns>Returnera information om garagets unika id, namn och om garaget är fullt eller ej, kapacitet och antal vehicle som är parkerade</returns>
        public (string strId, string strName, bool bIsFull, int iCapacity, int iNumberOfParkedVehicle) GetGarageInfo()
        {
            return (strId: this.GarageID.ToString(), strName: this.GarageName, bIsFull: this.IsFull, iCapacity: this.Capacity, iNumberOfParkedVehicle: this.Count);
        }
    }
}