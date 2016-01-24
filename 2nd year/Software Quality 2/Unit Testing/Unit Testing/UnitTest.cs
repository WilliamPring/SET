using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Functions;
namespace Unit_Testing
{
    [TestClass]
    public class UnitTest
    {
        /// <summary>
        /// Test indentifer: F_SB_N_NOB_1  
        /// Test Description: Testing the NumberOfDigit function for Normal Test
        /// Test Method Execution: Normal
        /// Input Data: 123321
        /// Expected output: 6
        /// Observed outputs: 6
        /// Resulting action: Pass
        /// </summary>
        [TestMethod]
        //Function name space | String Breakout Class| Normal Test | NumberOfDigit | Test 1
        public void F_SB_N_NOB_1()
        {
            int number = StringBreakout.NumberOfDigits("123321");
            Assert.AreEqual(number, 6);
        }

 
        /// <summary>
        /// Test indentifer: F_SB_E_NOB_2  
        /// Test Description: Testing the NumberOfDigit function for Exception
        /// Test Method: Exception
        /// Input Data: asdf
        /// Expected output: 0
        /// Observed outputs: 0
        /// Resulting action: Pass
        /// </summary>

        [TestMethod]
        //Function name space | String Breakout Class| Exception Test | NumberOfDigit | Test 3
        public void F_SB_E_NOB_2()
        {
            int number = StringBreakout.NumberOfDigits("asdf");
            Assert.AreEqual(number, 0);
        }


        /// <summary>
        /// Test indentifer: F_SB_N_NOB_3  
        /// Test Description: Testing the NumberOfDigit function for Normal
        /// Test Method: Normal
        /// Input Data: 123456789
        /// Expected output: 9
        /// Observed outputs: 9
        /// Resulting action: Pass
        /// </summary>


        [TestMethod]
        //Function name space | String Breakout Class| Normal Test | NumberOfDigit | Test 3
        public void F_SB_N_NOB_3()
        {
            int number = StringBreakout.NumberOfDigits("123456789");
            Assert.AreEqual(number, 9);
        }




        /// <summary>
        /// Test indentifer: F_SB_E_NOB_4  
        /// Test Description: Testing the NumberOfDigit function for Exception
        /// Test Method: Exception
        /// Input Data: asasdfasdfasdfsdfasdfasdfasdfasdfasdfdf
        /// Expected output: 0
        /// Observed outputs: 0
        /// Resulting action: Pass
        /// </summary>
        [TestMethod]
        //Function name space | String Breakout Class| Exception Test | NumberOfDigit | Test 4
        public void F_SB_E_NOB_4()
        {
            int number = StringBreakout.NumberOfDigits("asasdfasdfasdfsdfasdfasdfasdfasdfasdfdf");
            Assert.AreEqual(number, 0);
        }



        /// <summary>
        /// Test indentifer: F_SB_B_NOB_5  
        /// Test Description: Testing the NumberOfDigit function for Bondary
        /// Test Method: Bondary
        /// Input Data: Empty String
        /// Expected output: 0
        /// Observed outputs: 0
        /// Resulting action: Pass
        /// </summary>

        [TestMethod]
        //Function name space | String Breakout Class| Bondary Test | NumberOfDigit | Test 2
        public void F_SB_B_NOB_5()
        {
            int number = StringBreakout.NumberOfDigits("");
            Assert.AreEqual(number, 0);
        }



        /// <summary>
        /// Test indentifer: F_SB_N_NOA_1  
        /// Test Description: Testing the NumberOfAlphas function for Normal
        /// Test Method: Normal
        /// Input Data: asdf
        /// Expected output: 4
        /// Observed outputs: 4
        /// Resulting action: Pass
        /// </summary>

        [TestMethod]
        //Function name space | String Breakout Class| Normal Test | NumberOfAlphas | Test 1
        public void F_SB_N_NOA_1()
        {
            int number = StringBreakout.NumberOfAlphas("asdf");
            Assert.AreEqual(number, 4);
        }

        /// <summary>
        /// Test indentifer: F_SB_E_NOA_2  
        /// Test Description: Testing the NumberOfAlphas function for Exception
        /// Test Method: Exception
        /// Input Data: 2345
        /// Expected output: 0
        /// Observed outputs: 0
        /// Resulting action: Pass
        /// </summary>

        [TestMethod]
        //Function name space | String Breakout Class| Exception Test | NumberOfAlphas | Test 2
        public void F_SB_E_NOA_2()
        {
            int number = StringBreakout.NumberOfAlphas("2345");
            Assert.AreEqual(number, 0);
        }

        /// <summary>
        /// Test indentifer: F_SB_E_NOA_2  
        /// Test Description: Testing the NumberOfAlphas function for Normal
        /// Test Method: Normal
        /// Input Data: asdfghjklqwertyuiopzxcvbnm
        /// Expected output: 26
        /// Observed outputs: 26
        /// Resulting action: Pass
        /// </summary>

        [TestMethod]
        //Function name space | String Breakout Class| Normal Test | NumberOfAlphas | Test 3
        public void F_SB_N_NOA_3()
        {
            int number = StringBreakout.NumberOfAlphas("asdfghjklqwertyuiopzxcvbnm");
            Assert.AreEqual(number, 26);
        }

        /// <summary>
        /// Test indentifer: F_SB_E_NOA_4  
        /// Test Description: Testing the NumberOfAlphas function for Exception
        /// Test Method: Exception
        /// Input Data: 43280990321409248098230923490243
        /// Expected output: 0
        /// Observed outputs: 0
        /// Resulting action: Pass
        /// </summary>

        [TestMethod]
        //Function name space | String Breakout Class| Exception Test | NumberOfAlphas | Test 4
        public void F_SB_E_NOA_4()
        {
            int number = StringBreakout.NumberOfAlphas("43280990321409248098230923490243");
            Assert.AreEqual(number, 0);
        }
        /// <summary>
        /// Test indentifer: F_SB_B_NOA_5  
        /// Test Description: Testing the NumberOfAlphas function for Bondary
        /// Test Method: Bondary
        /// Input Data: Empty String
        /// Expected output: 0
        /// Observed outputs: 0
        /// Resulting action: Pass
        /// </summary>

        [TestMethod]
        //Function name space | String Breakout Class| Bondary Test | NumberOfAlphas | Test 5
        public void F_SB_B_NOA_5()
        {
            int number = StringBreakout.NumberOfAlphas("");
            Assert.AreEqual(number, 0);
        }




        /// <summary>
        /// Test indentifer: F_SB_N_NOO_1  
        /// Test Description: Testing the NumberOfOthers function for Normal
        /// Test Method: Normal
        /// Input Data: ">.<"
        /// Expected output: 3
        /// Observed outputs: 3
        /// Resulting action: Pass
        /// </summary>


        [TestMethod]
        //Function name space | String Breakout Class| Normal Test | NumberOfOther | Test 1
        public void F_SB_N_NOO_1()
        {
            int number = StringBreakout.NumberOfOthers(">.<");
            Assert.AreEqual(number, 3);
        }


        /// <summary>
        /// Test indentifer: F_SB_E_NOO_2
        /// Test Description: Testing the NumberOfOthers function for Exception
        /// Test Method: Exception
        /// Input Data: asd
        /// Expected output: 0
        /// Observed outputs: 0
        /// Resulting action: Pass
        /// </summary>


        [TestMethod]
        //Function name space | String Breakout Class| Exception Test | NumberOfAlphas | Test 2
        public void F_SB_E_NOO_2()
        {
            int number = StringBreakout.NumberOfOthers("asd");
            Assert.AreEqual(number, 0);
        }

        /// <summary>
        /// Test indentifer: F_SB_E_NOO_3  
        /// Test Description: Testing the NumberOfOthers function for Normal
        /// Test Method: Normal
        /// Input Data: asd
        /// Expected output: 0
        /// Observed outputs: 0
        /// Resulting action: Pass
        /// </summary>

        [TestMethod]
        public void F_SB_N_NOO_3()
        {
            int number = StringBreakout.NumberOfOthers("?????");
            Assert.AreEqual(number, 5);
        }

        /// <summary>
        /// Test indentifer: F_SB_E_NOO_4  
        /// Test Description: Testing the NumberOfOthers function for Exception
        /// Test Method: Exception
        /// Input Data: HeLP
        /// Expected output: 0
        /// Observed outputs: 0
        /// Resulting action: Pass
        /// </summary>

        [TestMethod]
        //Function name space | String Breakout Class| Exception Test | NumberOfAlphas | Test 2
        public void F_SB_E_NOO_4()
        {
            int number = StringBreakout.NumberOfOthers("HeLP");
            Assert.AreEqual(number, 0);
        }


        /// <summary>
        /// Test indentifer: F_SB_B_NOO_5  
        /// Test Description: Testing the NumberOfOthers function for Bondary
        /// Test Method: Bondary
        /// Input Data: Empty String
        /// Expected output: 0
        /// Observed outputs: 0
        /// Resulting action: Pass
        /// </summary>

        [TestMethod]
        //Function name space | String Breakout Class| Bondary | NumberOfAlphas | Test 5
        public void F_SB_B_NOO_5()
        {
            int number = StringBreakout.NumberOfOthers("");
            Assert.AreEqual(number, 0);
        }


        /// <summary>
        /// Test indentifer: F_SB_N_NED_1  
        /// Test Description: Testing the FindAndExtractDigits function for Normal
        /// Test Method: Normal
        /// Input Data: 1234a5
        /// Expected output: 12345
        /// Observed outputs: 12345
        /// Resulting action: Pass
        /// </summary>


        [TestMethod]
        //Function name space | String Breakout Class| Normal Test | FindAndExtractDigits | Test 1
        public void F_SB_N_NED_1()
        {
            int number = StringBreakout.FindAndExtractDigits("1234a5");
            Assert.AreEqual(number, 12345);
        }
        /// <summary>
        /// Test indentifer: F_SB_E_NED_2  
        /// Test Description: Testing the FindAndExtractDigits function for Exception
        /// Test Method: Exception
        /// Input Data: asd
        /// Expected output: 0
        /// Observed outputs: 0
        /// Resulting action: Pass
        /// </summary>
        [TestMethod]
        //Function name space | String Breakout Class| Exception Test | FindAndExtractDigits | Test 2
        public void F_SB_E_NED_2()
        {
            int number = StringBreakout.FindAndExtractDigits("asd");
            Assert.AreEqual(number, 0);
        }
        /// <summary>
        /// Test indentifer: F_SB_E_NED_3  
        /// Test Description: Testing the FindAndExtractDigits function for Normal
        /// Test Method: Normal
        /// Input Data: 123123
        /// Expected output: 123123
        /// Observed outputs: 123123
        /// Resulting action: Pass
        /// </summary>
        [TestMethod]
        //Function name space | String Breakout Class| Normal Test | FindAndExtractDigits | Test 3
        public void F_SB_N_NED_3()
        {
            int number = StringBreakout.FindAndExtractDigits("123123");
            Assert.AreEqual(number, 123123);
        }
        /// <summary>
        /// Test indentifer: F_SB_E_NED_4  
        /// Test Description: Testing the FindAndExtractDigits function for Exception
        /// Test Method: Exception
        /// Input Data: .
        /// Expected output: 0
        /// Observed outputs: 0
        /// Resulting action: Pass
        /// </summary>
        [TestMethod]
        //Function name space | String Breakout Class| Exception Test | FindAndExtractDigits | Test 4
        public void F_SB_E_NED_4()
        {
            int number = StringBreakout.FindAndExtractDigits(".");
            Assert.AreEqual(number, 0);
        }
        /// <summary>
        /// Test indentifer: F_SB_B_NED_5  
        /// Test Description: Testing the FindAndExtractDigits function for Bondary
        /// Test Method: Bondary
        /// Input Data: Empty string
        /// Expected output: 0
        /// Observed outputs: 0
        /// Resulting action: Pass
        /// </summary>
        [TestMethod]
        //Function name space | String Breakout Class| Bondary | FindAndExtractDigits | Test 5
        public void F_SB_B_NED_5()
        {
            int number = StringBreakout.FindAndExtractDigits("");
            Assert.AreEqual(number, 0);
        }










        /// <summary>
        /// Test indentifer: F_F_N_Calc_1  
        /// Test Description: Testing the Calc function for Normal
        /// Test Method: Normal
        /// Input Data: 5
        /// Expected output: 120
        /// Observed outputs: 120
        /// Resulting action: Pass
        /// </summary>
        [TestMethod]
        //Function name space | Factorial Class| Normal Test | Calc | Test 1
        public void F_F_N_Calc_1()
        {
            int number = Factorial.Calc(5);
            Assert.AreEqual(number, 120);
        }
        /// <summary>
        /// Test indentifer: F_F_B_Calc_2  
        /// Test Description: Testing the Calc function for Bondary
        /// Test Method: Bondary
        /// Input Data: 0
        /// Expected output: 1
        /// Observed outputs: 1
        /// Resulting action: Pass
        /// </summary>
        [TestMethod]
        //Function name space | Factorial Class| Bondary Test | Calc | Test 2
        public void F_F_B_Calc_2()
        {
            int number = Factorial.Calc(0);
            Assert.AreEqual(number, 1);
        }

        /// <summary>
        /// Test indentifer: F_F_E_Calc_3  
        /// Test Description: Testing the Calc function for Exception
        /// Test Method: Exception
        /// Input Data: -99
        /// Expected output: 0
        /// Observed outputs: 0
        /// Resulting action: Pass
        /// </summary>
        [TestMethod]
        //Function name space | Factorial Class| Exception | Calc | Test 3
        public void F_F_E_Calc_3()
        {
            int number = Factorial.Calc(-99);
            Assert.AreEqual(number, 0);
        }

        /// <summary>
        /// Test indentifer: F_F_EHT_Calc_4  
        /// Test Description: Testing the Calc function for Error Handling Test
        /// Test Method: Error Handling Test
        /// Input Data: 19
        /// Expected output: 0
        /// Observed outputs: 0
        /// Resulting action: Pass
        /// </summary>
        [TestMethod]
        //Function name space | Factorial Class| Error Handling Test | Calc | Test 4
        public void F_F_EHT_Calc_4()
        {
            int number = Factorial.Calc(19);
            Assert.AreEqual(number, 0);
        }
        /// <summary>
        /// Test indentifer: F_F_N_Calc_5  
        /// Test Description: Testing the Calc function for Normal
        /// Test Method: Normal
        /// Input Data: 5
        /// Expected output: 120
        /// Observed outputs: 120
        /// Resulting action: Pass
        /// </summary>
        [TestMethod]
        //Function name space | Factorial Class| Normal Test | Calc | Test 5
        public void F_F_N_Calc_5()
        {
            int number = Factorial.Calc(5);
            Assert.AreEqual(number, 120);
        }







    }
}
