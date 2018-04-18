/*
 * UdpSender.h
 *
 *  Created on: Jan 28, 2018
 *      Author: dbeach
 */

#ifndef UDPSENDER_H_
#define UDPSENDER_H_

#include <arpa/inet.h>
#include <sys/socket.h>
#include <netdb.h>
#include <netinet/in.h>
//#include <net/if.h>
#include <sys/ioctl.h>
#include <ifaddrs.h>
#include <linux/if.h>
#include <sys/types.h>
#include <unistd.h>
#include <string>
#include <cstring>
#include <iostream>
#include <vector>

using namespace std;


class UdpSender {
public:
	UdpSender(int port);
	virtual ~UdpSender();
	void send(string message, bool verbose);
	string getIPs();
private:
	int udpHandle;
	int udpPort;
};

#endif /* UDPSENDER_H_ */
