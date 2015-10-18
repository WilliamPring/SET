#include <map>
#include <set>
#include <list>
#include <cmath>
#include <ctime>
#include <climits>
#include <deque>
#include <queue>
#include <stack>
#include <bitset>
#include <cstdio>
#include <limits>
#include <vector>
#include <cstdlib>
#include <fstream>
#include <numeric>
#include <sstream>
#include <iostream>
#include <algorithm>
using namespace std;
int longest_chain(vector < string > w);
int main() {
	int res;

	//int _w_size = 0;
	//cin >> _w_size;
	//cin.ignore(std::numeric_limits<std::streamsize>::max(), '\n');
	vector<string> w;
	string a;

		w.push_back("23");
		w.push_back("23");

		w.push_back("23");
		w.push_back("dfd23");
		w.push_back("23");
		w.push_back("23");
		w.push_back("bdca");
	res = longest_chain(w);

}
int longest_chain(vector < string > w) {
	int subLength = 0;
	int bigLength = w.size();
	for (size_t i = 0; i < w.size() - 1; i++)
	{
		subLength = w[i].length();
		if (subLength > bigLength)
		{
			bigLength = subLength;
		}
		if (i == w.size())
		{
			break;
		}
	}
	return bigLength;

}
