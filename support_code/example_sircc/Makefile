TARGET		= sircc

CC	 		= gcc
LD	 		= gcc
CFLAGS	 	= -Wall -O2

LDFLAGS	 	= 
DEFS 	 	=
LIB			= -lpthread

OBJECTS		= sircc.o csapp.o

all:	$(TARGET)

$(TARGET): $(OBJECTS)
	$(LD) $(LDFLAGS) $(LIB) -o $@ $^

csapp.o: csapp.c csapp.h
	$(CC) $(DEFS) $(CFLAGS) -c -o $@ $<

sircc.o: sircc.c csapp.h
	$(CC) $(DEFS) $(CFLAGS) -c -o $@ $<

clean:
	rm -f *.o
	rm -f sircc

.PHONY: clean
