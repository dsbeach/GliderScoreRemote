#include "ssPiI2C.h"

SSPiI2C::SSPiI2C() {
	handle = open("/dev/i2c-1", O_RDWR);
	if (handle < 0) {
		exit(1);
	}
}

SSPiI2C::~SSPiI2C() {
	close(handle);
}

int SSPiI2C::getTemp(float *temp) {
	if (ioctl(handle, 0x703, 0x18) < 0) // select the temp sensor
	{
		exit(1);
	}
    short bus;

    if ( register_read( 0x05, (unsigned short*)&bus ) != 0 ) // register 0x05 is ambient temp
    {
        return -1;
    }

	unsigned char high = (unsigned char) (bus >> 8);
	// for now (and possibly forever) ignore the flag bits
	high = high & 0x1f;
	unsigned char low= bus & 0xff;
	if ((high & 0x10) == 0x10) {
		high = high & 0x0f; // clear the negative sign bit
		*temp = 256 - ((high * 16.0) + (low / 16.0));
	} else {
		*temp = (high * 16.0) + (low / 16.0);
	}
	*temp = *temp * 1.8 + 32;

	return 0;
}

int SSPiI2C::getVolts(float *volts) {
	if (ioctl(handle, 0x703, 0x40) < 0) // select the voltage sensor
	{
		exit(1);
	}
    short bus;

    if ( register_read( 0x02, (unsigned short*)&bus ) != 0 ) // register 0x02 is bus voltage
    {
        return -1;
    }

    *volts = ( float )( ( bus & 0xFFF8 ) >> 1 ) / 1000.0; // default precision conversion
	return 0;
}

int SSPiI2C::register_read(unsigned char reg, unsigned short *data) {
	int rc = -1;
	unsigned char bite[4];

	bite[0] = reg;
	if (i2c_write(bite, 1) == 0) {
		if (i2c_read(bite, 2) == 0) {
			*data = (bite[0] << 8) | bite[1];
			rc = 0;
		}
	}

	return rc;
}

int SSPiI2C::i2c_read(void *buf, int len) {
	int rc = 0;

	if (read(handle, buf, len) != len) {
		printf("I2C read failed: %s\n", strerror( errno));
		rc = -1;
	}

	return rc;
}

int SSPiI2C::i2c_write(void *buf, int len) {
	int rc = 0;

	if (write(handle, buf, len) != len) {
		printf("I2C write failed: %s\n", strerror( errno));
		rc = -1;
	}

	return rc;
}

