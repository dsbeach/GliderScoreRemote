/*
 * WiFiStrength.h
 *
 *  Created on: Jan 28, 2018
 *      Author: dbeach
 */

#ifndef WIFISTATS_H_
#define WIFISTATS_H_

#include <string>
#include <climits>

#include <string.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <unistd.h>
#include <linux/wireless.h>
#include <sys/ioctl.h>

using namespace std;



class WiFiStats {
public:
	WiFiStats(const char *adapterName);
	virtual ~WiFiStats();
	int getStats(int *linkQuality, int *signalLevel);
private:
	string adapterName;
};

#endif /* WIFISTATS_H_ */
