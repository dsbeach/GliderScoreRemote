#ifndef _ssPiI2C_
#define _ssPiI2C_

#include <unistd.h>
#include <stdlib.h>
#include <stdio.h>
#include <errno.h>
#include <endian.h>
#include <string.h>
#include <time.h>
#include <getopt.h>
#include <sys/types.h>
#include <sys/stat.h>
#include <sys/ioctl.h>
#include <fcntl.h>
#include <linux/i2c-dev.h>

class SSPiI2C {
public:
	SSPiI2C();
	~SSPiI2C();
	int getVolts(float *volts);
	int getTemp(float *temp); // temp in fahrenheit
private:
	int handle;
	int register_read( unsigned char reg, unsigned short *data );
	int i2c_read( void *buf, int len );
	int i2c_write( void *buf, int len );
};
#endif
