using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Functions;
namespace Unit_Testing
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        //Function name space | String Breakout Class| Normal Test | NumberOfDigit | Test 1
        public void F_S_N_NOB_1()
        {
            int number = StringBreakout.NumberOfDigits("12");
            Assert.AreEqual(number, 2);
        }

        [TestMethod]
        //Function name space | String Breakout Class| Bondary Test | NumberOfDigit | Test 2
        public void F_S_B_NOB_2()
        {
            int number = StringBreakout.NumberOfDigits("1.10");
            Assert.AreEqual(number, 3);
        }


        [TestMethod]
        //Function name space | String Breakout Class| Exception Test | NumberOfDigit | Test 3
        public void F_S_E_NOB_3()
        {
            int number = StringBreakout.NumberOfDigits("asdf");
            Assert.AreEqual(number, 0);
        }

        




        [TestMethod]
        //Function name space | String Breakout Class| Normal Test | NumberOfAlphas | Test 1
        public void F_S_N_NOA_1()
        {
            int number = StringBreakout.NumberOfAlphas("asdf");
            Assert.AreEqual(number, 4);
        }

        [TestMethod]
        //Function name space | String Breakout Class| Exception Test | NumberOfAlphas | Test 2
        public void F_S_E_NOA_2()
        {
            int number = StringBreakout.NumberOfAlphas("2345");
            Assert.AreEqual(number, 0);
        }





        [TestMethod]
        //Function name space | String Breakout Class| Normal Test | NumberOfOther | Test 1
        public void F_S_N_NOO_1()
        {
            int number = StringBreakout.NumberOfOthers(">.<");
            Assert.AreEqual(number, 3);
        }

        [TestMethod]
        //Function name space | String Breakout Class| Exception Test | NumberOfAlphas | Test 2
        public void F_S_E_NOO_2()
        {
            int number = StringBreakout.NumberOfOthers("asd");
            Assert.AreEqual(number, 0);
        }




        [TestMethod]
        //Function name space | String Breakout Class| Normal Test | FindAndExtractDigits | Test 1
        public void F_S_N_NED_1()
        {
            int number = StringBreakout.FindAndExtractDigits("1234a5");
            Assert.AreEqual(number, 12345);
        }

        [TestMethod]
        //Function name space | String Breakout Class| Exception Test | FindAndExtractDigits | Test 2
        public void F_S_E_NED_2()
        {
            int number = StringBreakout.FindAndExtractDigits("asd");
            Assert.AreEqual(number, 0);
        }

        [TestMethod]
        //Function name space | String Breakout Class| Error Handling Test | FindAndExtractDigits | Test 3
        public void F_S_EHT_NED_3()
        {
            int number = StringBreakout.FindAndExtractDigits("asd-+.abcdefhikli");
            Assert.AreEqual(number, 0);
        }








        [TestMethod]
        //Function name space | Factorial Class| Normal Test | Calc | Test 1
        public void F_f_N_Calc_1()
        {
            int number = Factorial.Calc(5);
            Assert.AreEqual(number, 120);
        }

        [TestMethod]
        //Function name space | Factorial Class| Bondary Test | Calc | Test 2
        public void F_f_E_Calc_2()
        {
            int number = Factorial.Calc(0);
            Assert.AreEqual(number, 0);
        }
        

        [TestMethod]
        //Function name space | Factorial Class| Exception | Calc | Test 3
        public void F_f_E_Calc_3()
        {
            int number = Factorial.Calc(-99);
            Assert.AreEqual(number, 0);
        }


        [TestMethod]
        //Function name space | Factorial Class| Error Handling Test | Calc | Test 4
        public void F_f_EHT_Calc_4()
        {
            int number = Factorial.Calc(19);
            Assert.AreEqual(number, 0);
        }








    }
}
