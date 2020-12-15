using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ComputeClassLibrary.UnitTestProject
{
    [TestClass]
    public class ComputeUnitTest
    {
        [TestMethod]
        //featureToTest_Scenrio_ExpectedResults
        public void FindSum_ValidInput_ValidReuslt()
        {
            //AAA
            //arrange // assert // act

            int a = 10;
            int b = 10;
            int exp = 20;
            Compute compute = new Compute();


            // Act
            int actual = compute.FindSum(a, b);

            // Assert
            // comapring actual against expected
            Assert.AreEqual(exp, actual);


        }

        [TestMethod]
        public void IsPrime_ValidInput_ValidReuslt()
        {
            
            int a = 20;
            bool exp = false;
            Compute compute = new Compute();
            bool actual = compute.IsPrime(a);
            Assert.AreEqual(exp, actual);

        }

    }
}
