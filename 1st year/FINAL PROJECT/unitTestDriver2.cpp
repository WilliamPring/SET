///*
//File name: unitTestDriver2.cpp
//Project: DS-OOP Assignment 
//By: Naween Mehanmal and William Pring
//Date: April 5, 2015
//Description: This test harness is explicitly for the Menu class and through numerous vigorous testing, the outcome will determine if the 
//functions/methods have been implemented correctly, meeting the domain/business rules and the program's requirements. 
//*/
//
//#include "Container.h"
//#include "Menu.h"
//
//void testFailOrPass(string message, int testNum, int champNum);
//void testFailOrPass(string message, int testNum, string champNum);
//string createChampionTest(string numChampions);
//void errorCheckingCondition(string message, int testNum, int champNum);
//void testHandling(string message, int testNum, string champNum);
//
//
//
//int main()
//{
//
//	string typeOfTest = "Normal Testing";
//	string message = "";
//
//	///*********Normal Testing*********/
//
//	printf("**********|%s|**********\n\n", typeOfTest.c_str());
//
//	message = createChampionTest("20");
//	testFailOrPass(message, 1, 20);
//
//	message = createChampionTest("40");
//	testFailOrPass(message, 2, 40);
//
//	message = createChampionTest("100");
//	testFailOrPass(message, 3, 100);
//
//	///*********Boundary Testing*********/
//
//	typeOfTest = "Boundary Testing";
//
//	printf("\n\n**********|%s|**********\n\n", typeOfTest.c_str());
//
//	message = createChampionTest("1");
//	testFailOrPass(message, 1, 1);
//
//	message = createChampionTest("1000000");
//	testFailOrPass(message, 2, 1000000);
//
//	/*********Exception Testing*********/
//	
//	typeOfTest = "Exception Testing";
//
//	printf("\n\n**********|%s|**********", typeOfTest.c_str());
//
//	message = createChampionTest("0");
//	errorCheckingCondition(message, 1, 0);
//
//	message = createChampionTest("-1");
//	errorCheckingCondition(message, 2, -1);
//
//	message = createChampionTest("1a1");
//	testHandling(message, 3, "1a1");
//
//	return 0; 
//}
//
//
//string createChampionTest(string numChampions)
//{
//	Container trialChamp;
//	Menu trialMenu; 
//	string returnMessage = "";
//	
//	returnMessage = trialChamp.createChampions(numChampions);
//
//	return returnMessage;
//}
//
//
//void testFailOrPass(string message, int testNum, int champNum)
//{
//	char expectedMessage[256] = ""; 
//	sprintf(expectedMessage, "You have sucessfully created %d new champions!\n", champNum);
//
//	if (strcmp(message.c_str(), expectedMessage) == 0)
//	{
//		printf("[TEST #%d] TEST PASSED!\n", testNum);
//	}
//	else
//	{
//		printf("\n\n[TEST #%d] TEST FAILED\n\n", testNum);
//		printf("%s", message.c_str());
//	}
//
//	printf("\n");
//}
//
//void testFailOrPass(string message, int testNum, string champNum)
//{
//	char expectedMessage[256] = "";
//	string unexpectedOutputMessage = "Letters are not allowed\n"; 
//	sprintf(expectedMessage, "You have sucessfully created %s new champions!\n", champNum.c_str());
//
//	if (message == unexpectedOutputMessage)
//	{
//		printf("\n\n[TEST #%d] TEST FAILED\n\n", testNum);
//		printf("%s", message.c_str());
//	}
//	else
//	{
//		if (strcmp(message.c_str(), expectedMessage) == 0)
//		{
//			printf("[TEST #%d] TEST PASSED!\n", testNum);
//		}
//		else
//		{
//			printf("\n\n[TEST #%d] TEST FAILED\n\n", testNum);
//		}
//	}
//
//	printf("\n");
//}
//
//
//
//
//void errorCheckingCondition(string message, int testNum, int champNum)
//{
//	char expectedMessage[256] = "";
//	sprintf(expectedMessage, "You have sucessfully created %d new champions!\n", champNum);
//
//	if (strcmp(message.c_str(), expectedMessage) != 0)
//	{
//		printf("\n\n[TEST #%d] TEST PASSED!\n", testNum);
//		printf("%s", message.c_str());
//	}
//	else
//	{
//		printf("\n\n[TEST #%d] TEST FAILED\n\n", testNum);
//	}
//
//	printf("\n");
//}
//
//
//void testHandling(string message, int testNum, string champNum)
//{
//	char expectedMessage[256] = "";
//	string unexpectedOutputMessage = "Letters are not allowed\n";
//	sprintf(expectedMessage, "You have sucessfully created %s new champions!\n", champNum.c_str());
//
//	if (message != unexpectedOutputMessage)
//	{
//		printf("\n[TEST #%d] TEST PASSED\n\n", testNum);
//		printf("%s", message.c_str());
//	}
//	else
//	{
//		if (strcmp(message.c_str(), expectedMessage) != 0)
//		{
//			printf("\n[TEST #%d] TEST PASSED!\n\n", testNum);
//			printf("%s", message.c_str());
//		}
//		else
//		{
//			printf("\n\n[TEST #%d] TEST FAILED\n\n", testNum);
//		}
//	}
//
//	printf("\n");
//}