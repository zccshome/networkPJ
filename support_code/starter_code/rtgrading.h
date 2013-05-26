/*
 * rtgrading.h
 *
 * Initial Author: Jeff Pang <jeffpang+441@cs.cmu.edu>
 * Class: 15-441 (Spring 2004)
 *
 * Routines you MUST use in your routing daemon. 
 * DO NOT CHANGE ANYTHING IN THIS FILE.
 *
 */

#ifndef __RTGRADING_H__
#define __RTGRADING_H__

#include <sys/types.h>
#include <sys/socket.h>

#ifdef __cplusplus
extern "C" {
#endif

/**
 * Call at the beginning of your routing daemon program.
 *
 * Arguments:
 * argc        - the original argc passed to srouted's main() function.
 * argv        - the original argv passed to srouted's main() function.
 */
void rt_init(int argc, char **argv);

/**
 * You must use this to send UDP packets between routing daemons.
 * Wrapper function for the sendto(...) system call.
 * The parameters are the same as in the sendto syscall, and the semantics
 * are the same.
 */
int rt_sendto(int s, const void *msg, size_t len, int flags,
	      const struct sockaddr *to, socklen_t tolen);

/**
 * You must use this to receive UDP packets sent between routing daemons.
 * Wrapper function for the recvfrom(...) system call.
 * The parameters are the same as in the recvfrom syscall, and the semantics
 * are the same.
 */
int rt_recvfrom(int  s,  void  *buf,  size_t len, int flags,
		struct sockaddr *from, socklen_t *fromlen);

#ifdef __cplusplus
}
#endif

#endif
