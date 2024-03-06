using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Moq;

namespace SimpleCalculator.Business.UnitTest
{
    [TestClass]
    public class CalculatorUnitTest
    {
        Calculator target = null;
        [TestInitialize]
        public void Init()
        {
            target = new Calculator();
        }
        [TestCleanup]
        public void Cleanup() 
        { 
            target = null;  
        }

        [TestMethod]
        public void SumTest_WithValidInput_ShouldGiveCorrectOutput()
        {
            //do not use conditional statements
            //do not use looping statements
            //do not use try...catch blocks

            //plain simple code
            //unit testing approach - AAA
            //Arrange
            //Calculator target = new Calculator();
            int fno = 10;
            int sno = 10;
            int expected = 20;
            //Act 
            int actual = target.Sum(fno, sno);
            //Assert 
            Assert.AreEqual(expected, actual);  

        }
        [TestMethod]
        public void SumTest_WithNegativeInput_ShouldReturnZero()
        {
            //Calculator target = new Calculator();

            int expected = 0;
            int actual = target.Sum(-10, -10);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void SumTest_WithOddInput_ShouldThrowException()
        {
            //Calculator target = new Calculator();
            int actual = target.Sum(1, 1);    
        }
        [TestMethod]
        public void FilterEvensTest_WithValidListOfNumbersInput_ShouldReturnListOfEvens()
        {
            Mock<IRepository> mock = new Mock<IRepository>();
            List<int> numbers = new List<int> { 2, 4, 6 };
            mock.Setup(m => m.Save(numbers));

            Calculator target = new Calculator(mock.Object);
            List<int> list = new List<int> { 1, 2, 3, 4, 5, 6 };
            List<int> expected = new List<int> { 2, 4, 6 };
            List<int> actual = target.FilterEvens(list);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FilterEvensTest_WithValidListOfNumbersInput_ShouldCallSave()
        { 
            Mock<IRepository> mock = new Mock<IRepository>();
            List<int> numbers = new List<int> { 2, 4, 6 };
            mock.Setup(m => m.Save(numbers));

            Calculator target = new Calculator(mock.Object);
            List<int> list = new List<int> { 1, 2, 3, 4, 5, 6 };
            List<int> expected = new List<int> { 2, 4, 6 };
            List<int> actual = target.FilterEvens(list);
            //CollectionAssert.AreEqual(expected, actual);
            mock.Verify(m => m.Save(numbers), Times.AtLeastOnce);
        }

        [TestMethod]
        [ExpectedException(typeof(InputEmptyException))]
        public void FilterEvensTest_WithEmptyListOfNumbersInput_ShouldThrowException()
        {
            //Calculator target = new Calculator();
            List<int> list = new List<int> { };
            List<int> actual = target.FilterEvens(list);
        }
        [TestMethod]
        [ExpectedException(typeof(InputEmptyException))]
        public void FilterEvensTest_WithNullListOfNumbersInput_ShouldThrowException()
        {
            //Calculator target = new Calculator();
            List<int> actual = target.FilterEvens(null);
        }
    }
    
}
