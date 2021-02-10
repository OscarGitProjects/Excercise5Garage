using Excercise5Garage.Garage;
using Excercise5Garage.Vehicle;
using Excercise5Garage.Vehicle.WheeledVehicle;
using NUnit.Framework;
using System;

namespace NUnitExcercise5Garage.Tests
{
    /// <summary>
    /// Klassen testar Garage klassen
    /// </summary>
    public class GarageTests
    {

        [SetUp]
        public void Setup()
        {
        }



        /// <summary>
        /// Metoden testar Garage klassens konstruktor fungerar
        /// </summary>
        [Test]
        [Description("Metoden testar Garage klassens konstruktor fungerar")]
        public void Garage_Constructor_Test()
        {
            // Assert.Pass();

            // Arrange
            // expected      
            Guid guid = Guid.NewGuid();

            Garage<ICanBeParkedInGarage> garage = new Garage<ICanBeParkedInGarage>(guid, "Garage 1", 5);
            string strExpectedToString = $"Id: {guid.ToString()}, Name: Garage 1, Capacity: {5}, Count: {0}";


            // Act
            // actual
            string strActualGuid = garage.GarageID.ToString();
            string strActualGarageName = garage.GarageName;
            int iActualGarageCapacity = garage.Capacity;
            int iActualNumberOfVehicles = garage.Count;
            bool bActualIsEmpty = garage.IsEmpty;
            bool bActualIsFull = garage.IsFull;
            string strActualToString = garage.ToString();


            // Assert
            Assert.AreEqual(guid.ToString(), strActualGuid);

            Assert.AreEqual("Garage 1", strActualGarageName);

            Assert.AreEqual(5, iActualGarageCapacity);

            Assert.AreEqual(0, iActualNumberOfVehicles);

            Assert.IsTrue(bActualIsEmpty);

            Assert.IsFalse(bActualIsFull);

            Assert.AreEqual(strExpectedToString, strActualToString);
        }



        /// <summary>
        /// Metoden testar Garage klassens konstruktor fungerar med negativ kapacitet
        /// </summary>
        [Test]
        [Description("Metoden testar Garage klassens konstruktor fungerar med negativ kapacitet")]
        public void Garage_Constructor_Capacity_Negativ_Test()
        {
            // Assert.Pass();

            // Arrange
            // expected      
            Guid guid = Guid.NewGuid();

            Garage<ICanBeParkedInGarage> garage = new Garage<ICanBeParkedInGarage>(guid, "Garage 1", -5);
            string strExpectedToString = $"Id: {guid.ToString()}, Name: Garage 1, Capacity: {0}, Count: {0}";


            // Act
            // actual
            string strActualGuid = garage.GarageID.ToString();
            string strActualGarageName = garage.GarageName;
            int iActualGarageCapacity = garage.Capacity;
            int iActualNumberOfVehicles = garage.Count;
            bool bActualIsEmpty = garage.IsEmpty;
            bool bActualIsFull = garage.IsFull;
            string strActualToString = garage.ToString();


            // Assert
            Assert.AreEqual(guid.ToString(), strActualGuid);

            Assert.AreEqual("Garage 1", strActualGarageName);

            Assert.AreEqual(0, iActualGarageCapacity);

            Assert.AreEqual(0, iActualNumberOfVehicles);

            Assert.IsTrue(bActualIsEmpty);

            Assert.IsTrue(bActualIsFull);

            Assert.AreEqual(strExpectedToString, strActualToString);
        }



        /// <summary>
        /// Metoden testar att det går lägga till Vehicle objekt till Garage klassen
        /// </summary>
        [Test]
        [Description("Metoden testar att det går lägga till Vehicle objekt till Garage klassen")]
        public void Garage_Add_Vehicle_Test()
        {
            // Assert.Pass();

            // Arrange
            // expected      
            Guid guid = Guid.NewGuid();

            Garage<ICanBeParkedInGarage> garage = new Garage<ICanBeParkedInGarage>(guid, "Garage 1", 5);

            ICanBeParkedInGarage vehicle = new Car("AAA 111", "Röd", 4);

            string strExpectedToString = $"Id: {guid.ToString()}, Name: Garage 1, Capacity: {5}, Count: {1}";


            // Act
            // actual
            bool bAddedItem = garage.Add(vehicle);
            int iActualNumberOfVehicles = garage.Count;
            bool bActualIsEmpty = garage.IsEmpty;
            bool bActualIsFull = garage.IsFull;
            string strActualToString = garage.ToString();


            // Assert
            Assert.IsTrue(bAddedItem);

            Assert.AreEqual(1, iActualNumberOfVehicles);

            Assert.IsFalse(bActualIsEmpty);

            Assert.IsFalse(bActualIsFull);

            Assert.AreEqual(strExpectedToString, strActualToString);
        }



        /// <summary>
        /// Metoden testar att det går lägga till flera Vehicle objekt till Garage klassen
        /// </summary>
        [Test]
        [Description("Metoden testar att det går lägga till flera Vehicle objekt till Garage klassen")]
        public void Garage_Add_2_Vehicle_Test()
        {
            // Assert.Pass();

            // Arrange
            // expected      
            Guid guid = Guid.NewGuid();

            Garage<ICanBeParkedInGarage> garage = new Garage<ICanBeParkedInGarage>(guid, "Garage 1", 5);

            ICanBeParkedInGarage vehicle = new Car("AAA 111", "Röd", 4);

            ICanBeParkedInGarage vehicle1 = new Bus("BBB 222", "Grön", 50);

            string strExpectedToString = $"Id: {guid.ToString()}, Name: Garage 1, Capacity: {5}, Count: {2}";


            // Act
            // actual
            bool bAddedItem1 = garage.Add(vehicle);
            bool bAddedItem2 = garage.Add(vehicle1);

            int iActualNumberOfVehicles = garage.Count;
            bool bActualIsEmpty = garage.IsEmpty;
            bool bActualIsFull = garage.IsFull;
            string strActualToString = garage.ToString();


            // Assert
            Assert.IsTrue(bAddedItem1);

            Assert.IsTrue(bAddedItem2);

            Assert.AreEqual(2, iActualNumberOfVehicles);

            Assert.IsFalse(bActualIsEmpty);

            Assert.IsFalse(bActualIsFull);

            Assert.AreEqual(strExpectedToString, strActualToString);
        }



        /// <summary>
        /// Metoden testar att undantaget System.ArgumentNullException kastas om man försöker att lägga till Vehicle objekt som är null till Garage klassen
        /// </summary>
        [Test]
        [Description("Metoden testar att undantaget System.ArgumentNullException kastas om man försöker att lägga till Vehicle objekt som är null till Garage klassen")]
        public void Garage_Add_Vehicle_null_Test()
        {
            // Assert.Pass();

            // Arrange
            // expected      
            Guid guid = Guid.NewGuid();

            Garage<ICanBeParkedInGarage> garage = new Garage<ICanBeParkedInGarage>(guid, "Garage 1", 5);

            ICanBeParkedInGarage vehicle = null;


            // Act
            // actual


            // Assert
            Assert.Throws<ArgumentNullException>(() => garage.Add(vehicle));
        }



        /// <summary>
        /// Metoden testar att det går lägga till max antal Vehicle objekt till Garage klassen
        /// </summary>
        [Test]
        [Description("Metoden testar att det går lägga till max antal Vehicle objekt till Garage klassen")]
        public void Garage_Add_To_Capacity_Vehicle_Test()
        {
            // Assert.Pass();

            // Arrange
            // expected      
            Guid guid = Guid.NewGuid();

            Garage<ICanBeParkedInGarage> garage = new Garage<ICanBeParkedInGarage>(guid, "Garage 1", 5);

            ICanBeParkedInGarage vehicle1 = new Car("AAA 111", "Röd", 4);

            ICanBeParkedInGarage vehicle2 = new Bus("BBB 222", "Grön", 50);

            ICanBeParkedInGarage vehicle3 = new Car("CCC 333", "Grön", 4);

            ICanBeParkedInGarage vehicle4 = new MotorCycle("DDD 444", "Blå", 2);

            ICanBeParkedInGarage vehicle5 = new Car("EEE 555", "Grön", 5);


            string strExpectedToString = $"Id: {guid.ToString()}, Name: Garage 1, Capacity: {5}, Count: {5}";


            // Act
            // actual
            bool bAddedItem1 = garage.Add(vehicle1);
            bool bAddedItem2 = garage.Add(vehicle2);
            bool bAddedItem3 = garage.Add(vehicle3);
            bool bAddedItem4 = garage.Add(vehicle4);
            bool bAddedItem5 = garage.Add(vehicle5);

            int iActualNumberOfVehicles = garage.Count;
            bool bActualIsEmpty = garage.IsEmpty;
            bool bActualIsFull = garage.IsFull;
            string strActualToString = garage.ToString();


            // Assert
            Assert.IsTrue(bAddedItem1);
            Assert.IsTrue(bAddedItem2);
            Assert.IsTrue(bAddedItem3);
            Assert.IsTrue(bAddedItem4);
            Assert.IsTrue(bAddedItem5);

            Assert.AreEqual(5, iActualNumberOfVehicles);

            Assert.IsFalse(bActualIsEmpty);

            Assert.IsTrue(bActualIsFull);

            Assert.AreEqual(strExpectedToString, strActualToString);
        }



        /// <summary>
        /// Metoden testar att det går lägga till flera Vehicle objekt än kapaciteten för för Garage klassen
        /// </summary>
        [Test]
        [Description("Metoden testar att det går lägga till flera Vehicle objekt än kapaciteten för Garage klassen")]
        public void Garage_Add_Over_Capacity_Vehicle_Test()
        {
            // Assert.Pass();

            // Arrange
            // expected      
            Guid guid = Guid.NewGuid();

            Garage<ICanBeParkedInGarage> garage = new Garage<ICanBeParkedInGarage>(guid, "Garage 1", 5);

            ICanBeParkedInGarage vehicle1 = new Car("AAA 111", "Röd", 4);

            ICanBeParkedInGarage vehicle2 = new Bus("BBB 222", "Grön", 50);

            ICanBeParkedInGarage vehicle3 = new Car("CCC 333", "Grön", 4);

            ICanBeParkedInGarage vehicle4 = new MotorCycle("DDD 444", "Blå", 2);

            ICanBeParkedInGarage vehicle5 = new Car("EEE 555", "Grön", 5);

            ICanBeParkedInGarage vehicle6 = new Bus("FFF 666", "Grön", 50);

            string strExpectedToString = $"Id: {guid.ToString()}, Name: Garage 1, Capacity: {5}, Count: {5}";


            // Act
            // actual
            bool bAddedItem1 = garage.Add(vehicle1);
            bool bAddedItem2 = garage.Add(vehicle2);
            bool bAddedItem3 = garage.Add(vehicle3);
            bool bAddedItem4 = garage.Add(vehicle4);
            bool bAddedItem5 = garage.Add(vehicle5);
            bool bAddedItem6 = garage.Add(vehicle6);

            int iActualNumberOfVehicles = garage.Count;
            bool bActualIsEmpty = garage.IsEmpty;
            bool bActualIsFull = garage.IsFull;
            string strActualToString = garage.ToString();


            // Assert
            Assert.IsTrue(bAddedItem1);
            Assert.IsTrue(bAddedItem2);
            Assert.IsTrue(bAddedItem3);
            Assert.IsTrue(bAddedItem4);
            Assert.IsTrue(bAddedItem5);
            Assert.IsFalse(bAddedItem6);

            Assert.AreEqual(5, iActualNumberOfVehicles);

            Assert.IsFalse(bActualIsEmpty);

            Assert.IsTrue(bActualIsFull);

            Assert.AreEqual(strExpectedToString, strActualToString);
        }



        /// <summary>
        /// Metoden testar att det går radera ett Vehicle objekt med index från Garage klassen
        /// </summary>
        [Test]
        [Description("Metoden testar att det går radera ett Vehicle objekt med index från Garage klassen")]
        public void Garage_Remove_Vehicle_At_Index_Test()
        {
            // Assert.Pass();

            // Arrange
            // expected      
            Guid guid = Guid.NewGuid();

            Garage<ICanBeParkedInGarage> garage = new Garage<ICanBeParkedInGarage>(guid, "Garage 1", 5);

            ICanBeParkedInGarage vehicle1 = new Car("AAA 111", "Röd", 4);
            //ICanBeParkedInGarage vehicle2 = new Car("BBB 222");

            string strExpectedToStringBeforeRemove = $"Id: {guid.ToString()}, Name: Garage 1, Capacity: {5}, Count: {1}";
            string strExpectedToStringAfterRemove = $"Id: {guid.ToString()}, Name: Garage 1, Capacity: {5}, Count: {0}";


            // Act
            // actual
            bool bAddedItem1 = garage.Add(vehicle1);
            //bool bAddedItem2 = garage.Add(vehicle2);
            string strActualBeforeToString = garage.ToString();
            int iActualNumberOfVehiclesBeforeRemove = garage.Count;

            bool bRemovedItem2 = garage.Remove(0);

            int iActualNumberOfVehiclesAfterRemove = garage.Count;
            bool bActualIsEmpty = garage.IsEmpty;
            bool bActualIsFull = garage.IsFull;
           
            string strActualAfterToString = garage.ToString();


            // Assert
            Assert.IsTrue(bAddedItem1);

            //Assert.IsTrue(bAddedItem2);

            Assert.AreEqual(strExpectedToStringBeforeRemove, strActualBeforeToString);

            Assert.AreEqual(1, iActualNumberOfVehiclesBeforeRemove);

            Assert.IsTrue(bRemovedItem2);

            Assert.AreEqual(0, iActualNumberOfVehiclesAfterRemove);

            Assert.IsTrue(bActualIsEmpty);

            Assert.IsFalse(bActualIsFull);            

            Assert.AreEqual(strExpectedToStringAfterRemove, strActualAfterToString);
        }



        /// <summary>
        /// Metoden testar att det går radera ett Vehicle objekt med index där det inte finns ett Vehicle objekt från Garage klassen
        /// Då skall Remove metoden returnera false
        /// </summary>
        [Test]
        [Description("Metoden testar att det går radera ett Vehicle objekt med index där det inte finns ett Vehicle objekt från Garage klassen. Då skall Remove metoden returnera false")]
        public void Garage_Remove_Vehicle_At_Index_With_No_Vehicle_Test()
        {
            // Assert.Pass();

            // Arrange
            // expected      
            Guid guid = Guid.NewGuid();

            Garage<ICanBeParkedInGarage> garage = new Garage<ICanBeParkedInGarage>(guid, "Garage 1", 5);

            ICanBeParkedInGarage vehicle1 = new Car("AAA 111", "Röd", 4);
            ICanBeParkedInGarage vehicle2 = new Car("BBB 222");

            string strExpectedToStringBeforeRemove = $"Id: {guid.ToString()}, Name: Garage 1, Capacity: {5}, Count: {2}";
            string strExpectedToStringAfterRemove = $"Id: {guid.ToString()}, Name: Garage 1, Capacity: {5}, Count: {2}";


            // Act
            // actual
            bool bAddedItem1 = garage.Add(vehicle1);
            bool bAddedItem2 = garage.Add(vehicle2);
            string strActualBeforeToString = garage.ToString();
            int iActualNumberOfVehiclesBeforeRemove = garage.Count;

            bool bRemovedItem2 = garage.Remove(3);

            int iActualNumberOfVehiclesAfterRemove = garage.Count;
            bool bActualIsEmpty = garage.IsEmpty;
            bool bActualIsFull = garage.IsFull;

            string strActualAfterToString = garage.ToString();


            // Assert
            Assert.IsTrue(bAddedItem1);

            Assert.IsTrue(bAddedItem2);

            Assert.AreEqual(strExpectedToStringBeforeRemove, strActualBeforeToString);

            Assert.AreEqual(2, iActualNumberOfVehiclesBeforeRemove);

            Assert.IsFalse(bRemovedItem2);

            Assert.AreEqual(2, iActualNumberOfVehiclesAfterRemove);

            Assert.IsFalse(bActualIsEmpty);

            Assert.IsFalse(bActualIsFull);

            Assert.AreEqual(strExpectedToStringAfterRemove, strActualAfterToString);
        }



        /// <summary>
        /// Metoden testar att undantaget ArgumentOutOfRangeException kastas om man försöker att radera Vehicle objekt vid index som är minde än 0 eller större än capacity
        /// </summary>
        [Test]
        [Description("Metoden testar att undantaget ArgumentOutOfRangeException kastas om man försöker att radera Vehicle objekt vid index som är minde än 0 eller större än capacity")]
        public void Garage_Remove_Vehicle_At_Wrong_Index_Test()
        {
            // Assert.Pass();

            // Arrange
            // expected      
            Guid guid = Guid.NewGuid();

            Garage<ICanBeParkedInGarage> garage = new Garage<ICanBeParkedInGarage>(guid, "Garage 1", 5);

            ICanBeParkedInGarage vehicle1 = new Car("AAA 111", "Röd", 4);
            garage.Add(vehicle1);


            // Act
            // actual           


            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => garage.Remove(-1));

            Assert.Throws<ArgumentOutOfRangeException>(() => garage.Remove(5));
        }



        /// <summary>
        /// Metoden testar att det går radera ett Vehicle objekt med ett existerande objekt från Garage klassen
        /// </summary>
        [Test]
        [Description("Metoden testar att det går radera ett Vehicle objekt med ett existerande objekt från Garage klassen")]
        public void Garage_Remove_Vehicle_That_Excist_Test()
        {
            // Assert.Pass();

            // Arrange
            // expected      
            Guid guid = Guid.NewGuid();

            Garage<ICanBeParkedInGarage> garage = new Garage<ICanBeParkedInGarage>(guid, "Garage 1", 5);

            ICanBeParkedInGarage vehicle1 = new Car("AAA 111", "Röd", 4);
            ICanBeParkedInGarage vehicle2 = new Car("BBB 222", "Röd", 4);


            string strExpectedToStringBeforeRemove = $"Id: {guid.ToString()}, Name: Garage 1, Capacity: {5}, Count: {2}";
            string strExpectedToStringAfterRemove = $"Id: {guid.ToString()}, Name: Garage 1, Capacity: {5}, Count: {1}";


            // Act
            // actual           
            bool bActualAddedItem1 = garage.Add(vehicle1);
            bool bActualAddedItem2 = garage.Add(vehicle2);

            string strActualToStringBeforeRemove = garage.ToString();
            int iActualNumberOfItemsBeforeRemove = garage.Count;

            bool bActualRemovedItem1 = garage.Remove(vehicle1);

            string strActualToStringAfterRemove = garage.ToString();
            int iActualNumberOfItemsAfterRemove = garage.Count;


            // Assert
            Assert.IsTrue(bActualAddedItem1);
            Assert.IsTrue(bActualAddedItem2);

            Assert.AreEqual(strExpectedToStringBeforeRemove, strActualToStringBeforeRemove);
            Assert.AreEqual(2, iActualNumberOfItemsBeforeRemove);

            Assert.IsTrue(bActualRemovedItem1);

            Assert.AreEqual(strExpectedToStringAfterRemove, strActualToStringAfterRemove);
            Assert.AreEqual(1, iActualNumberOfItemsAfterRemove);

        }



        /// <summary>
        /// Metoden testar att det går radera ett Vehicle objekt med ett icke existerande objekt från Garage klassen
        /// </summary>
        [Test]
        [Description("Metoden testar att det går radera ett Vehicle objekt med ett icke existerande object från Garage klassen")]
        public void Garage_Remove_Vehicle_That_Not_Excist_Test()
        {
            // Assert.Pass();

            // Arrange
            // expected      
            Guid guid = Guid.NewGuid();

            Garage<ICanBeParkedInGarage> garage = new Garage<ICanBeParkedInGarage>(guid, "Garage 1", 5);

            ICanBeParkedInGarage vehicle1 = new Car("AAA 111", "Röd", 4);
            ICanBeParkedInGarage vehicle2 = new Car("BBB 222", "Röd", 4);

            ICanBeParkedInGarage vehicle3 = new Car("CCC 333", "Röd", 4);


            string strExpectedToStringBeforeRemove = $"Id: {guid.ToString()}, Name: Garage 1, Capacity: {5}, Count: {2}";
            string strExpectedToStringAfterRemove = $"Id: {guid.ToString()}, Name: Garage 1, Capacity: {5}, Count: {2}";


            // Act
            // actual           
            bool bActualAddedItem1 = garage.Add(vehicle1);
            bool bActualAddedItem2 = garage.Add(vehicle2);

            string strActualToStringBeforeRemove = garage.ToString();
            int iActualNumberOfItemsBeforeRemove = garage.Count;

            bool bActualRemovedItem3 = garage.Remove(vehicle3);

            string strActualToStringAfterRemove = garage.ToString();
            int iActualNumberOfItemsAfterRemove = garage.Count;


            // Assert
            Assert.IsTrue(bActualAddedItem1);
            Assert.IsTrue(bActualAddedItem2);

            Assert.AreEqual(strExpectedToStringBeforeRemove, strActualToStringBeforeRemove);
            Assert.AreEqual(2, iActualNumberOfItemsBeforeRemove);

            Assert.IsFalse(bActualRemovedItem3);

            Assert.AreEqual(strExpectedToStringAfterRemove, strActualToStringAfterRemove);
            Assert.AreEqual(2, iActualNumberOfItemsAfterRemove);

        }



        /// <summary>
        /// Metoden testar att undantaget ArgumentNullException kastas om man försöker att radera Vehicle objekt med null
        /// </summary>
        [Test]
        [Description("Metoden testar att undantaget ArgumentNullException kastas om man försöker att radera Vehicle objekt med null")]
        public void Garage_Remove_Vehicle_null_Test()
        {
            // Assert.Pass();

            // Arrange
            // expected      
            Guid guid = Guid.NewGuid();

            Garage<ICanBeParkedInGarage> garage = new Garage<ICanBeParkedInGarage>(guid, "Garage 1", 5);

            ICanBeParkedInGarage vehicle1 = null;


            // Act
            // actual           


            // Assert
            Assert.Throws<ArgumentNullException>(() => garage.Remove(vehicle1));
        }



        /// <summary>
        /// Metoden testar att undantaget ArgumentOutOfRangeException kastas om man att hämta Vehicle objekt med från Garage objektet med [] och index är minde än 0 eller större än capacity
        /// </summary>
        [Test]
        [Description("Metoden testar att undantaget ArgumentOutOfRangeException kastas om man att hämta Vehicle objekt med från Garage objektet med [] och index är minde än 0 eller större än capacity")]
        public void Garage_Returnera_Vehicle_With_this_At_Wrong_Index_Test()
        {
            // Assert.Pass();

            // Arrange
            // expected      
            Guid guid = Guid.NewGuid();

            Garage<ICanBeParkedInGarage> garage = new Garage<ICanBeParkedInGarage>(guid, "Garage 1", 5);
            ICanBeParkedInGarage vehicle1 = new Car("AAA 111", "Röd", 4);
            garage.Add(vehicle1);

            // Act
            // actual           


            // Assert

            try
            {
                var vec = garage[-1];
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException)
            {
                Assert.Pass();
            }

            try
            {
                var vec1 = garage[5];
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException)
            {
                Assert.Pass();
            }
        }



        /// <summary>
        /// Metoden testar att hämta Vehicle objekt från Garage objektet med [] med index 0 och 1
        /// </summary>
        [Test]
        [Description("Metoden testar att hämta Vehicle objekt från Garage objektet med [] med index 0 och 1")]
        public void Garage_Returnera_Vehicle_With_this_Test()
        {
            // Assert.Pass();

            // Arrange
            // expected      
            Guid guid = Guid.NewGuid();

            Garage<ICanBeParkedInGarage> garage = new Garage<ICanBeParkedInGarage>(guid, "Garage 1", 5);

            ICanBeParkedInGarage vehicle1 = new Car("AAA 111", "Röd", 4);
            ICanBeParkedInGarage vehicle2 = new Bus("BBB 222", "Grön", 50);

            garage.Add(vehicle1);
            garage.Add(vehicle2);


            // Act
            // actual           
            ICanBeParkedInGarage actaulVehicle1 = garage[0];
            Vehicle actualVec1 = actaulVehicle1 as Vehicle;

            ICanBeParkedInGarage actaulVehicle2  = garage[1];
            Vehicle actualVec2 = actaulVehicle2 as Vehicle;


            // Assert
            Assert.AreEqual(actualVec1.Color, ((Vehicle)vehicle1).Color);

            Assert.AreEqual(actualVec2.Color, ((Vehicle)vehicle2).Color);

            Assert.AreEqual(actualVec1.RegistrationNumber, ((Vehicle)vehicle1).RegistrationNumber);

            Assert.AreEqual(actualVec2.RegistrationNumber, ((Vehicle)vehicle2).RegistrationNumber);
        }
    }
}
