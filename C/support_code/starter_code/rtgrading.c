/*
 * rtgrading.c
 *
 * Initial Author: Jeff Pang <jeffpang+441@cs.cmu.edu>
 * Class: 15-441 (Spring 2004)
 *
 * Routines you MUST use in your routing daemon. Don't forget to link this.
 * See rtgrading.h for documentation.
 * DO NOT CHANGE ANYTHING IN THIS FILE.
 *
 */

#include <sys/types.h>
#include <sys/socket.h>
#include "rtgrading.h"

void rt_init(int argc, char **argv)
{
    return;
}

int rt_sendto(int s, const void *msg, size_t len, int flags,
	      const struct sockaddr *to, socklen_t tolen)
{
    return sendto(s, msg, len, flags, to, tolen);
}

int rt_recvfrom(int  s,  void  *buf,  size_t len, int flags,
		struct sockaddr *from, socklen_t *fromlen)
{
    return recvfrom(s, buf, len, flags, from, fromlen);
}
