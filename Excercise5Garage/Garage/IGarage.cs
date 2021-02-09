using System;
using System.Collections.Generic;

namespace Excercise5Garage.Garage
{
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
    }
}