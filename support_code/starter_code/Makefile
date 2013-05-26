TARGET 	= sircd

CC		= gcc
LD		= gcc

CFLAGS	= -O2 -Wall
LFLAGS	= 
LIBS	= -lpthread

OBJECTS = sircd.o rtlib.o rtgrading.o csapp.o

$(TARGET): $(OBJECTS)
	$(LD) $(LFLAGS) $(LIBS) -o $@ $^

sircd.o: sircd.c sircd.h rtlib.h rtgrading.h csapp.h
	$(CC) $(CFLAGS) -c -o $@ $<

rtlib.o: rtlib.c rtlib.h csapp.h
	$(CC) $(CFLAGS) -c -o $@ $<

rtgrading.o: rtgrading.c rtgrading.h
	$(CC) $(CFLAGS) -c -o $@ $<

csapp.o: csapp.c csapp.h
	$(CC) $(CFLAGS) -c -o $@ $<

%.o: %.c
	$(CC) $(CFLAGS) -c -o $@ $<

clean:
	rm -f *.o
	rm -f $(TARGET)

.PHONY: clean
