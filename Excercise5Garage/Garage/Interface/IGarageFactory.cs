using System;

namespace Excercise5Garage.Garage.Interface
{
    public interface IGarageFactory
    {
        IGarage<ICanBeParkedInGarage> CreateGarage(Guid guid, string strGarageName, int iCapacity);
    }
}