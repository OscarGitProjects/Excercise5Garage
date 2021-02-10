using System;
using System.Collections.Generic;

namespace Excercise5Garage.Garage
{
    /// <summary>
    /// Interface för Garage
    /// Genereisk typ av element kan sparas i Garaget, men dom måste implementera interfacet ICanBeParkedInGarage
    /// </summary>
    /// <typeparam name="T">Generisk typ av element kan sparas i Garaget</typeparam>
    public interface IGarage<T> : IEnumerable<T>
    {
        string GarageName { get; set; }
        Guid GarageID { get; }
        T this[int iIndex] { get; }
        int Capacity { get; }
        int Count { get; }
        bool IsEmpty { get; }
        bool IsFull { get; }
        bool Add(T vehicle);
        bool Remove(T vehicle);
        bool Remove(int iIndex);
        string PrintAllInformation();
    }
}