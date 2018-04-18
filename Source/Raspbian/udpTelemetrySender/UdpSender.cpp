/*
 * UdpSender.cpp
 *
 *  Created on: Jan 28, 2018
 *      Author: dbeach
 */

#include "UdpSender.h"

UdpSender::UdpSender(int port) {

	// for testing at home I want to send the datagrams to all available interfaces!

	this->udpPort = port;

	udpHandle = socket(AF_INET, SOCK_DGRAM, IPPROTO_UDP);
	int enable=1;
	setsockopt(udpHandle, SOL_SOCKET, SO_BROADCAST, &enable, sizeof(enable));
	//setsockopt(udpHandle, SOL_SOCKET, SO_REUSEADDR, &enable, sizeof(enable));
	//setsockopt(udpHandle, SOL_SOCKET, SO_REUSEPORT, &enable, sizeof(enable));
}

UdpSender::~UdpSender() {
	close(udpHandle);
}

string UdpSender::getIPs()
{
	string result = "";
	ifaddrs *ifap;
	char sourceIP[INET_ADDRSTRLEN];

	if (getifaddrs(&ifap) == 0)
	{
		ifaddrs *p = ifap;
		while (p) {
			if ((p->ifa_flags & IFF_BROADCAST) && (p->ifa_addr->sa_family == AF_INET)) {
				//cout << "Found address: " << inet_ntop(AF_INET, &((struct sockaddr_in *)p->ifa_addr)->sin_addr, sourceIP, INET_ADDRSTRLEN) << endl;
				if (result.length() > 0 ) { result += ", "; }
				inet_ntop(AF_INET, &((struct sockaddr_in *)p->ifa_addr)->sin_addr, sourceIP, INET_ADDRSTRLEN);
				result += sourceIP;
			}
			p = p->ifa_next;
		}
		freeifaddrs(ifap);
	}
	return result;
}

void UdpSender::send(string message, bool verbose)
{
	vector<sockaddr> bcastAddresses;

	sockaddr_in si_other;

	ifaddrs *ifap;
	if (getifaddrs(&ifap) == 0)
	{
		ifaddrs *p = ifap;
		while (p) {
			if ((p->ifa_flags & IFF_BROADCAST) && (p->ifa_addr->sa_family == AF_INET)) {
				// cout << "Adding broadcast interface:" << p->ifa_name << endl;
				bcastAddresses.push_back(*(p->ifa_ifu.ifu_broadaddr)); // the broadcast address for this interface;
			}
			p = p->ifa_next;
		}
		freeifaddrs(ifap);
	}

	for (uint i = 0; i < bcastAddresses.size(); i++) {
		memset((void *) &si_other, 0, sizeof(si_other));
		si_other.sin_family = AF_INET;
		si_other.sin_port = htons(udpPort);
		sockaddr_in *temp = (sockaddr_in*)&bcastAddresses[i];
		si_other.sin_addr.s_addr = temp->sin_addr.s_addr;
		bind(udpHandle, (sockaddr*)&si_other, sizeof(si_other));
		int result = sendto(udpHandle, message.c_str(), message.length(), 0, (struct sockaddr*)&si_other, message.length());
		if (result < 0) {
			cout << "opps.. errno:" << errno << endl;
		} else {
			if (verbose) { cout << "address: " << inet_ntoa(temp->sin_addr) << " message: " << message << endl; }
		}
	}

}
