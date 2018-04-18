/*
 * WiFiStrength.cpp
 *
 *  Created on: Jan 28, 2018
 *      Author: dbeach
 */

#include "WiFiStats.h"

WiFiStats::WiFiStats(const char *adapterName) {
	this->adapterName = adapterName;
}

WiFiStats::~WiFiStats() {
}

int WiFiStats::getStats(int *linkQuality, int *signalLevel)
{
	iwreq req;
	iw_statistics stats;

	// initialization
	*linkQuality = 0;
	*signalLevel = 0;


	//have to use a socket for ioctl
	int sockfd = socket(AF_INET, SOCK_DGRAM, 0);

	//this will gather the signal strength
	memset(&stats, 0, sizeof(stats));
	memset(&req, 0, sizeof(req));
	strcpy((char *)(req.ifr_ifrn.ifrn_name), adapterName.c_str());
	req.u.data.pointer=&stats;
	req.u.data.length=sizeof(stats);
	req.u.data.flags = 1;
	if(ioctl(sockfd, SIOCGIWSTATS, &req) == -1) {
		//die with error, invalid interface
		return(-1);
	}

	*linkQuality = stats.qual.qual;
	*signalLevel = stats.qual.level;

	close(sockfd);
	return 0;
}
