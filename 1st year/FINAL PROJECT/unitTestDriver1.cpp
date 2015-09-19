///*
//File name: unitTestDriver1.cpp
//Project: DS-OOP Assignment
//By: Naween Mehanmal and William Pring
//Date: April 5, 2015
//Description: This test harness is explicitly for the Container class and through numerous vigorous testing, the outcome will determine if the 
//functions/methods have been implemented correctly, meeting the domain/business rules and the program's requirements. 
//*/
//
//
//#include "Champions.h"
//
//using namespace std;
//
//string createChampionTest(string Name, string MainRole, string SubRole, string AD, string AP, string DEF, string DIFF, string IP, string RP);
//void testFailOrPass(string message, int testNum);
//void errorHandling(string message, int testNum);
//
//
//int main()
//{
//	/*********Normal Testing*********/
//
//	string message = "";
//	string typeOfTest = "Normal Testing"; 
//
//	printf("**********|%s|**********\n\n", typeOfTest.c_str());
//
//	message = createChampionTest("Billy", "Tank", "Support", "4", "5", "6", "7", "4500", "600");
//	testFailOrPass(message, 1);
//
//	message = createChampionTest("Harold Johnston", "Mage", "N/A", "5", "3", "4", "2", "3000", "280");
//	testFailOrPass(message, 2);
//
//
//	message = createChampionTest("Steven", "Assassin", "Marksmen", "6", "3", "8", "5", "6000", "500");
//	testFailOrPass(message, 3);
//
//	/*********Boundary Testing*********/
//
//	typeOfTest = "Boundary Testing";
//
//	printf("\n**********|%s|**********\n\n", typeOfTest.c_str());
//
//	message = createChampionTest("abcdefghijklmnop", "Tank", "marksmen", "1", "10", "1", "10", "6300", "260");
//	testFailOrPass(message, 1);
//
//	message = createChampionTest("EARL SWEATSHIRT", "MAGE", "N/A", "10", "1", "10", "1", "450", "975");
//	testFailOrPass(message, 2);
//
//
//	message = createChampionTest("StEvEn", "tank", "N/a", "10", "10", "1", "1", "6229", "974");
//	testFailOrPass(message, 3);
//
//	
//	/*********Exception Testing*********/
//
//
//	typeOfTest = "Exception Testing";
//
//	printf("\n**********|%s|**********\n\n", typeOfTest.c_str());
//
//	message = createChampionTest("abcdefghijklmnopq", "Support", "Support", "-1", "10", "1", "0", "6301", "300");
//	errorHandling(message, 1);
//
//	message = createChampionTest("EARL 5WEATSHIRT", "N/A", "Assassin", "25", "5", "7", "2", "450", "975");
//	errorHandling(message, 2);
//
//
//	message = createChampionTest("s t e v e ", "Hello", "Fighter", "A", "100", " ", "0", "9000", "1");
//	errorHandling(message, 3);
//
//	return 0;
//}
//
//
//string createChampionTest(string Name, string MainRole, string SubRole, string AD, string AP, string DEF, string DIFF, string IP, string RP)
//{
//	Champions trialChamp;
//	string outputMessage = "";
//
//	outputMessage = trialChamp.Champion(Name, MainRole, SubRole, AD, AP, DEF, DIFF, IP, RP);
//
//	return outputMessage;
//}
//
//
//void testFailOrPass(string message, int testNum)
//{
//	string expectedMessage = "You have sucessfully created your new champion!\n";
//
//	if (message == expectedMessage)
//	{
//		printf("[TEST #%d] TEST PASSED!\n", testNum);
//	}
//	else
//	{
//		printf("\n\n[TEST #%d] TEST FAILED\n\n", testNum);
//		printf("%s", message.c_str());
//	}
//}
//
//void errorHandling(string message, int testNum)
//{
//	string expectedMessage = "You have sucessfully created your new champion!\n";
//
//	if (message != expectedMessage)
//	{
//		printf("[TEST #%d] TEST PASSED!\n", testNum);
//		printf("\n%s\n", message.c_str());
//	}
//	else
//	{
//		printf("\n\n[TEST #%d] TEST FAILED\n\n", testNum);
//	}
//}