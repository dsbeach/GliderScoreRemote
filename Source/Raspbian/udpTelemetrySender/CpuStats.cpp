/*
 * CpuStats.cpp
 *
 *  Created on: Feb 16, 2018
 *      Author: dbeach
 */

#include "CpuStats.h"

using namespace std;

CpuStats::CpuStats() {
	// TODO Auto-generated constructor stub

}

CpuStats::~CpuStats() {
	// TODO Auto-generated destructor stub
}

double CpuStats::GetTempF() {
	string val;
	string preparedTemp;
	double temperature;

	ifstream temperatureFile ("/sys/class/thermal/thermal_zone0/temp");

	if (temperatureFile < 0) {
	    cout << "Failed to open temperature file!\n";
	    return -1;
	}

	// The temperature is stored in 5 digits.  The first two are degrees in C.  The rest are decimal precision.
	temperatureFile >> val;

	temperatureFile.close();

	preparedTemp = val.insert(2, 1, '.');
	temperature = std::stod(preparedTemp);

	return temperature * 1.8 + 32.0;
}

double CpuStats::Get5MinuteLoadAvg() {
	double loadAvg = 0;
	double dummy = 0;
	unsigned cpuCount = std::thread::hardware_concurrency();

	ifstream loadAvgFile ("/proc/loadavg");

	if (loadAvgFile < 0) {
	    cout << "Failed to open loadavg file!\n";
	    return -1;
	}

	// we want the second one
	loadAvgFile >> dummy >> loadAvg;

	loadAvgFile.close();


	return loadAvg / cpuCount * 100;
}
