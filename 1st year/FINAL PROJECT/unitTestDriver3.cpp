/*
File name:
Project:
By:
Date:
Description:
*/
//
//#include "Champions.h"
//
//void passOrFailCondition(bool result, int testNum, string methodTested, string assignValue);
//void errorHandling(bool result, int testNum, string methodTested, string assignValue);
//
//
//int main()
//{
//	Champions trialChamp;
//	bool condition = false;
//
//	string typeOfTest = "Normal Testing"; 
//
//	/*********Normal Testing*********/
//
//	printf("**********|%s|**********\n\n", typeOfTest.c_str());
//
//	condition = trialChamp.SetChampName("Billy");
//	passOrFailCondition(condition, 1, "SetChampName()", "Billy");
//
//	trialChamp.SetChampMainRole("Mage");
//	passOrFailCondition(condition, 2, "SetChampMainRole()", "Mage");
//
//	trialChamp.SetChampSubRole("Support");
//	passOrFailCondition(condition, 3, "SetChampSubRole()", "Support");
//
//	trialChamp.SetChampAD(5);
//	passOrFailCondition(condition, 4, "SetChampAD()", "5");
//
//	trialChamp.SetChampAP(7);
//	passOrFailCondition(condition, 5, "SetChampAP()", "7");
//
//	trialChamp.SetChampDef(6);
//	passOrFailCondition(condition, 6, "SetChampDef()", "6");
//
//	trialChamp.SetChampDiff(3);
//	passOrFailCondition(condition, 7, "SetChampDiff()", "3");
//
//	trialChamp.SetChampIP(2);
//	passOrFailCondition(condition, 8, "SetChampIP()", "2");
//
//	trialChamp.SetChampRP(8);
//	passOrFailCondition(condition, 9, "SetChampRP()", "8");
//
//
//	/*********Boundary Testing*********/
//
//	typeOfTest = "Boundary Testing";
//	printf("\n**********|%s|**********\n\n", typeOfTest.c_str());
//
//	condition = trialChamp.SetChampName("EarL SweaTshirTs");
//	passOrFailCondition(condition, 1, "SetChampName()", "EarL SweaTshirTs");
//
//	trialChamp.SetChampMainRole("AsSASsIN");
//	passOrFailCondition(condition, 2, "SetChampMainRole()", "AsSASsIN");
//
//	trialChamp.SetChampSubRole("n/A");
//	passOrFailCondition(condition, 3, "SetChampSubRole()", "n/A");
//
//	trialChamp.SetChampAD(1);
//	passOrFailCondition(condition, 4, "SetChampAD()", "1");
//
//	trialChamp.SetChampAP(10);
//	passOrFailCondition(condition, 5, "SetChampAP()", "10");
//
//	trialChamp.SetChampDef(1);
//	passOrFailCondition(condition, 6, "SetChampDef()", "1");
//
//	trialChamp.SetChampDiff(10);
//	passOrFailCondition(condition, 7, "SetChampDiff()", "10");
//
//	trialChamp.SetChampIP(450);
//	passOrFailCondition(condition, 8, "SetChampIP()", "450");
//
//	trialChamp.SetChampRP(970);
//	passOrFailCondition(condition, 9, "SetChampRP()", "970");
//
//
//
//	/*********Exception Testing*********/
//
//	typeOfTest = "Exception Testing";
//	printf("\n**********|%s|**********\n\n", typeOfTest.c_str());
//
//	condition = trialChamp.SetChampName("Earl  $weatshirt");
//	errorHandling(condition, 1, "SetChampName()", "Earl  $weatshirt");
//
//	trialChamp.SetChampMainRole("N/A");
//	errorHandling(condition, 2, "SetChampMainRole()", "N/A");
//
//	trialChamp.SetChampSubRole("Hello");
//	errorHandling(condition, 3, "SetChampSubRole()", "Hello");
//
//	trialChamp.SetChampAD(0);
//	errorHandling(condition, 4, "SetChampAD()", "0");
//
//	trialChamp.SetChampAP(100);
//	errorHandling(condition, 5, "SetChampAP()", "100");
//
//	trialChamp.SetChampDef(-1);
//	errorHandling(condition, 6, "SetChampDef()", "-1");
//
//	trialChamp.SetChampDiff(11);
//	errorHandling(condition, 7, "SetChampDiff()", "11");
//
//	trialChamp.SetChampIP(450);
//	errorHandling(condition, 8, "SetChampIP()", "1");
//
//	trialChamp.SetChampRP(970);
//	errorHandling(condition, 9, "SetChampRP()", "9000");
//
//	return 0; 
//}
//
//
//void passOrFailCondition(bool result, int testNum, string methodTested, string assignValue)
//{
//	if (result == true)
//	{
//		printf("[TEST #%d] TEST PASSED! Method: %s Able to set %s!\n", testNum, methodTested.c_str(), assignValue.c_str());
//	}
//	else
//	{
//		printf("[TEST #%d] TEST FAILED! Method: %s Unable to set %s!\n", testNum, methodTested.c_str(), assignValue.c_str());
//	}	
//}
//
//void errorHandling(bool result, int testNum, string methodTested, string assignValue)
//{
//	if (result != true)
//	{
//		printf("[TEST #%d] TEST PASSED! Method: %s Unable to set %s!\n", testNum, methodTested.c_str(), assignValue.c_str());
//	}
//	else
//	{
//		printf("[TEST #%d] TEST FAILED! Method: %s Able to set %s!\n", testNum, methodTested.c_str(), assignValue.c_str());
//	}
//}