

#include "ssPiI2C.h"
#include "UdpSender.h"
#include <iostream>
#include <chrono>
#include <thread>
#include <unistd.h>
#include <sstream>
#include <iomanip>

#include "WiFiStats.h"
#include "CpuStats.h"


using namespace std;

static void show_usage(std::string name)
{
	std::cerr << "Usage: " << name << " <option(s)> SOURCES"
			<< "Options:\n"
			<< "\t-h, --help\tShow this help message\n"
			<< "\t-a\tWireless adapter name (default:wlan1)\n"
			<< "\t-p\tUDP Port number (default:5724)\n"
			<< "\t-i\tInterval in seconds (default=30)\n"
			<< "\t-v\tVerbose mode for debugging\n"
			<< std::endl;
}

int main( int argc, char *argv[] )
{
	string wirelessAdapter = "wlan1";
	int udpPort = 5724;
	int interval = 30;
	bool verbose = false;

	for (int i = 1; i < argc; ++i) {
		string arg = argv[i];
		if ((arg == "-h") || (arg == "--help")) {
			show_usage(argv[0]);
			return 0;
		} else if (arg == "-a") {
			if (i + 1 < argc) {
				wirelessAdapter = argv[++i];
			} else {
				std::cerr << "-a option requires one argument." << std::endl;
				return 1;
			}
		} else if (arg == "-p") {
			if (i + 1 < argc) { // Make sure we aren't at the end of argv!
				try {
					udpPort = atoi(argv[++i]); // Increment 'i' so we don't get the argument as the next argv[i].
				}
				catch(...) {}
			} else { // Uh-oh, there was no argument to the destination option.
				std::cerr << "-p option requires one argument." << std::endl;
				return 1;
			}
		} else if (arg == "-i") {
			if (i + 1 < argc) { // Make sure we aren't at the end of argv!
				try {
					interval = atoi(argv[++i]); // Increment 'i' so we don't get the argument as the next argv[i].
				}
				catch(...) {}
			} else { // Uh-oh, there was no argument to the destination option.
				std::cerr << "-i option requires one argument." << std::endl;
				return 1;
			}
		} else if (arg == "-v") {
			verbose = true;
		}
	}

	cout << endl << argv[0] << " using wirelessAdapter:" << wirelessAdapter << " UDP Port:" << udpPort << " interval:" << interval << endl << endl;

	SSPiI2C *ssPiI2C = new SSPiI2C();
	WiFiStats *wiFiStrength = new WiFiStats(wirelessAdapter.c_str());
	UdpSender *udpSender = new UdpSender(udpPort);
	CpuStats *cpuStats = new CpuStats();

	char hostname[HOST_NAME_MAX + 1];
	float volts;
	float temp;
	int linkQuality;
	int signalLevel;
	stringstream ss;
	unsigned counter = 5;

	gethostname(hostname, HOST_NAME_MAX);
	while (true) {
		ss.clear();
		ss.str(string());
		ss << "hostname=" << hostname;

		switch (counter++ % 6) {
		case 0:
			ssPiI2C->getVolts(&volts);
			ss <<  ";volts=" << fixed << setprecision(2) << volts;
			break;
		case 1:
			ssPiI2C->getTemp(&temp);
			ss << ";temperature=" << fixed << setprecision(2) << temp;
			break;
		case 2:
			wiFiStrength->getStats(&linkQuality, &signalLevel);
			ss << ";link=" << linkQuality << ";signal=" << signalLevel;
			break;
		case 3:
			ss << ";ips=" << udpSender->getIPs();
			break;
		case 4:
			ss << ";cpuTemp=" << cpuStats->GetTempF();
			break;
		case 5:
			ss << ";5minLoadAvg=" << cpuStats->Get5MinuteLoadAvg();
			break;
		}
		udpSender->send(ss.str(), verbose);
		this_thread::sleep_for(chrono::seconds(interval));
	}
	return 0;
}

