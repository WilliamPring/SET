
#ifndef __DOMAIN_H__
#define __DOMAIN_H__
#include <string>
#include <regex>
#define AD_Test   0
#define AP_Test   1
#define DEF_Test  2
#define DIFF_Test 3
#define IP_Test   4
#define RP_Test   5
#pragma warning(disable:4996)

using namespace std;

class Champions
{
public: 
	/*********Constructor*********/

	Champions(void);
	Champions(string name, string MainRole, string SubRole, string AD, string AP, string DEF, string DIFF, string IP, string RP);

	/*********Mutators*********/

	bool SetChampAD(int AD);
	bool SetChampAP(int AP);
	bool SetChampDef(int DEF);
	bool SetChampDiff(int Difficulty);
	bool SetChampRP(int RP);
	bool SetChampIP(int IP);
	bool SetChampMainRole(string mainRole);
	bool SetChampSubRole(string subRole);
	bool SetChampName(string name);

	/*********Accessors*********/

	int GetChampAD(void);
	int GetChampAP(void);
	int GetChampDef(void);
	int GetChampDiff(void);
	int GetChampRP(void);
	int GetChampIP(void);
	string ChampMainRole(void);
	string ChampSubRole(void);
	string GetChampName(void);

	/*********Validation Methods*********/

	string validateStat(int championsStats[6]);
	string validateMainClassSubClass(string subClass, string mainClass);
	string validateName(string inputName[3]);
	string Champion(string name, string MainRole, string SubRole, string AD, string AP, string DEF, string DIFF, string IP, string RP);

	/*********Sorting Methods*********/

	static void SetWhatToSort(string whatYouHaveToSort);


	/*********Overloaded Operator*********/

	bool operator<(const Champions& type) const;
private:
	int champAD;
	int champAP;
	int champDEF;
	int champDIFF;
	int champIP;
	int champRP;
	string champName;
	string champMainRole;
	string champSubRole;

	/*********Other Non-Trivial Methods*********/

	void createSpaces(string& type);

};
#endif