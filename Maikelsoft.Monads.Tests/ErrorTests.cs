using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Maikelsoft.Monads.Tests
{
    [TestClass]
    public class ErrorTests
    {
        [TestMethod]
        public void FromExceptionCreatesInnerError()
        {
            // Arrange
            Exception outerException = new Exception("Outer exception", 
                new Exception("Inner exception", new Exception("Inner inner exception")));

            // Act
            Error error = Error.FromException(outerException);
            
            // Assert
            Assert.IsNotNull(error.InnerError);
            Assert.AreEqual("Inner exception", error.InnerError.Message);
            Assert.IsNotNull(error.InnerError.InnerError);
            Assert.AreEqual("Inner inner exception", error.InnerError.InnerError.Message);
        }
    }
}