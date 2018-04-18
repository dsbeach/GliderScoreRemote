/*
 * CpuStats.h
 *
 *  Created on: Feb 16, 2018
 *      Author: dbeach
 */

#ifndef CPUSTATS_H_
#define CPUSTATS_H_

#include <fstream>
#include <string>
#include <cstring>
#include <iostream>
#include <thread>

class CpuStats {
public:
	CpuStats();
	virtual ~CpuStats();

	double GetTempF();
	double Get5MinuteLoadAvg();
};

#endif /* CPUSTATS_H_ */
