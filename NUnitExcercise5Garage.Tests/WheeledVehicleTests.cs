using Excercise5Garage.Vehicle;
using Excercise5Garage.Vehicle.WheeledVehicle;
using NUnit.Framework;

namespace NUnitExcercise5Garage.Tests
{
    /// <summary>
    /// Klassen testar klasser som ärver från WheeledVehicle
    /// </summary>
    public class WheeledVehicleTests
    {
        [SetUp]
        public void Setup()
        {
        }



        /// <summary>
        /// Metoden testar Bus klassens konstruktor fungerar
        /// </summary>
        [Test]
        [Description("Metoden testar Bus klassens konstruktor fungerar")]
        public void Bus_Constructor_Test()
        {
            // Assert.Pass();

            // Arrange
            // expected      
            IVehicle vehicle = new Bus("ABC 123", "Röd", 6, 50);
           

            // Act
            // actual
            string strActualRegistrationNumber = ((Vehicle)vehicle).RegistrationNumber;
            int iActualNumberOfWheels = ((WheeledVehicle)vehicle).NumberOfWheels;
            string strActualColor = ((Vehicle)vehicle).Color;
            int iActualNumberOfSeatedPassengers = ((Bus)vehicle).NumberOfSeatedPassengers;


            // Assert
            Assert.AreEqual("ABC 123", strActualRegistrationNumber);

            Assert.AreEqual("Röd", strActualColor);

            Assert.AreEqual(6, iActualNumberOfWheels);

            Assert.AreEqual(50, iActualNumberOfSeatedPassengers);
        }



        /// <summary>
        /// Metoden testar Car klassens konstruktor fungerar
        /// </summary>
        [Test]
        [Description("Metoden testar Car klassens konstruktor fungerar")]
        public void Car_Constructor_Test()
        {
            // Assert.Pass();

            // Arrange
            // expected      
            IVehicle vehicle = new Car("ABC 123", "Röd", 6, 50);


            // Act
            // actual
            string strActualRegistrationNumber = ((Vehicle)vehicle).RegistrationNumber;
            int iActualNumberOfWheels = ((WheeledVehicle)vehicle).NumberOfWheels;
            string strActualColor = ((Vehicle)vehicle).Color;
            int iActualNumberOfSeatedPassengers = ((Car)vehicle).NumberOfSeatedPassengers;


            // Assert
            Assert.AreEqual("ABC 123", strActualRegistrationNumber);

            Assert.AreEqual("Röd", strActualColor);

            Assert.AreEqual(6, iActualNumberOfWheels);

            Assert.AreEqual(50, iActualNumberOfSeatedPassengers);
        }



        /// <summary>
        /// Metoden testar MotorCycle klassens konstruktor fungerar
        /// </summary>
        [Test]
        [Description("Metoden testar MotorCycle klassens konstruktor fungerar")]
        public void MotorCycle_Constructor_Test()
        {
            // Assert.Pass();

            // Arrange
            // expected      
            IVehicle vehicle = new MotorCycle("ABC 123", "Röd", 6, 50);


            // Act
            // actual
            string strActualRegistrationNumber = ((Vehicle)vehicle).RegistrationNumber;
            int iActualNumberOfWheels = ((WheeledVehicle)vehicle).NumberOfWheels;
            string strActualColor = ((Vehicle)vehicle).Color;
            int iActualNumberOfSeatedPassengers = ((MotorCycle)vehicle).NumberOfSeatedPassengers;


            // Assert
            Assert.AreEqual("ABC 123", strActualRegistrationNumber);

            Assert.AreEqual("Röd", strActualColor);

            Assert.AreEqual(6, iActualNumberOfWheels);

            Assert.AreEqual(50, iActualNumberOfSeatedPassengers);
        }
    }
}