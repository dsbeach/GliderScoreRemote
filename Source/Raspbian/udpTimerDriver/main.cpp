#include <iostream>
#include <string>
#include <cstring>
#include <sys/socket.h>
#include <arpa/inet.h>
#include <unistd.h>
using namespace std;

#include "SerialStream.h" // adapted from https://github.com/crayzeewulf/libserial
using namespace LibSerial;


static void show_usage(std::string name)
{
	std::cerr << "Usage: " << name << " <option(s)> SOURCES"
			<< "Options:\n"
			<< "\t-h, --help\tShow this help message\n"
			<< "\t-s\tSerial port name (default:/dev/serial0)\n"
			<< "\t-p\tUDP Port number (default:5723)\n"
			<< "\t-v\tVerbose mode for debugging\n"
			<< std::endl;
}

int main( int argc, char *argv[] ) {
	string serialPort = "/dev/serial0";
	int udpPort = 5723;
	int recvSocket;
	sockaddr_in addr;
	bool verbose = false;

	for (int i = 1; i < argc; ++i) {
		string arg = argv[i];
		if ((arg == "-h") || (arg == "--help")) {
			show_usage(argv[0]);
			return 0;
		} else if (arg == "-s") {
			if (i + 1 < argc) {
				serialPort = argv[++i];
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
		} else if (arg == "-v") {
			verbose = true;
		}
	}

	cout << endl << argv[0] << " using serial port:" << serialPort << " UDP Port:" << udpPort << (verbose?" Verbose":"") << endl << endl;

	if ((recvSocket = socket(AF_INET, SOCK_DGRAM, 0)) < 0) {
		cout << "Unable to create socket, errno=" << errno << endl;
		return -1;
	}

	int enable = 1;
	setsockopt(recvSocket, SOL_SOCKET, SO_REUSEADDR, &enable, sizeof(enable));

	memset(&addr, 0, sizeof(addr));
	addr.sin_family = AF_INET;
	addr.sin_port = htons(udpPort);
	addr.sin_addr.s_addr = htonl(INADDR_ANY);
	if(bind(recvSocket, (struct sockaddr *)&addr, sizeof addr) < 0)
	{
	  cout << "Unable to connect to socket, errno=" << errno << endl;
	  return -1;
	}


	// serial port initialization
    // Open the serial port.

    SerialStream serial_stream;
    serial_stream.Open( serialPort ) ;

    if ( !serial_stream.good() )
    {
        std::cerr << "Error: Could not open serial port "
                  << serialPort
                  << std::endl ;
        exit(1) ;
    }

    // Set the baud rate of the serial port.
    serial_stream.SetBaudRate( SerialStreamBuf::BAUD_9600 ) ;

    if (!serial_stream.good())
    {
        std::cerr << "Error: Could not set the baud rate." << std::endl ;
        exit(1) ;
    }

    // Set the number of data bits.
    serial_stream.SetCharSize( SerialStreamBuf::CHAR_SIZE_8 ) ;

    if ( !serial_stream.good() )
    {
        std::cerr << "Error: Could not set the character size." << std::endl ;
        exit(1) ;
    }

    // Disable parity.
    serial_stream.SetParity( SerialStreamBuf::PARITY_NONE ) ;

    if (!serial_stream.good())
    {
        std::cerr << "Error: Could not disable the parity." << std::endl ;
        exit(1) ;
    }

    // Set the number of stop bits.
    serial_stream.SetNumOfStopBits(1) ;

    if ( !serial_stream.good() )
    {
        std::cerr << "Error: Could not set the number of stop bits."
                  << std::endl ;
        exit(1) ;
    }

    // Turn off hardware flow control.
    serial_stream.SetFlowControl( SerialStreamBuf::FLOW_CONTROL_NONE ) ;

    if ( !serial_stream.good() )
    {
        std::cerr << "Error: Could not use hardware flow control."
                  << std::endl ;
        exit(1) ;
    }


	char recvBytes[512];
	uint recvLen;
	sockaddr saddrRemote;

	while (true) {
		recvLen = recvfrom(recvSocket, recvBytes, sizeof(recvBytes) - 1, 0, (sockaddr *) &saddrRemote, &recvLen);
		if (recvLen > 0) {
			recvBytes[recvLen] = 0;
			if (verbose) {
				string msg = recvBytes;
				cout << "Received:" << msg.substr(0, recvLen-1) << endl;
			}
			serial_stream.write(recvBytes, recvLen);
		}
		else {
			cout << "Receive error, errno=" << errno << endl;
			return -1;
		}
	}

    cout << "Exiting..." << endl;
    close(recvSocket);
    serial_stream.Close();
	return 0;
}

