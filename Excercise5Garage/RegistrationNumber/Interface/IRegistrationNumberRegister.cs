using Excercise5Garage.UI.Interface;

namespace Excercise5Garage.RegistrationNumber.Interface
{
    public interface IRegistrationNumberRegister
    {
        int NumberOfRegistrationNumbers { get; }
        bool AddRegistrationNumber(string strRegistrationNumber);
        bool CheckIfRegistrationnNumberExcist(string strRegistrationNumber);
        string CreateRandomRegistrationNumber();
        bool RemoveRegistrationNumber(string strRegistrationNumber);
        void PrintRegister(IUI ui);
    }
}