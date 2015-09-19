
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
	string validateStat(int championsStats[6]);
	string validateMainClassSubClass(string subClass, string mainClass);
	string validateName(string inputName[3]);
	Champions(string name, string MainRole, string SubRole, string AD, string AP, string DEF, string DIFF, string IP, string RP);
	string Champion(string name, string MainRole, string SubRole, string AD, string AP, string DEF, string DIFF, string IP, string RP);
	Champions(void);
	void SetAllPurposeStats(string stats);
	string sortChampionChoice(string whatToSortBy);
	bool operator<(const Champions& type) const; 

private:
	int champAD;
	int champAP;
	int champDEF;
	int champDIFF;
	int champIP;
	int champRP;
	string allPurposeStats;
	string champName;
	string champMainRole;
	string champSubRole;
};
#endif