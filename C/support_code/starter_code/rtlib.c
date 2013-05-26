/*
 * rtlib.c
 *
 * Initial Author: Jeff Pang <jeffpang+441@cs.cmu.edu>
 * Class: 15-441 (Spring 2005)
 *
 * Some library routines that you may find useful for 
 * Project 1: Routing Protocols.
 * See rtlib.h for documentation.
 */

#include <stdio.h>
#include <stdlib.h>
#include <strings.h>
#include <ctype.h>
#include <unistd.h>
#include <netdb.h>
#include <sys/socket.h>
#include "csapp.h"
#include "rtlib.h"

static const char* const _rt_optstring = "VSi:c:G:y:a:n:r:g:d:s:";

static void parse_long(const char* arg, 
		       unsigned long* value, 
		       const char* prefix,
		       const char* varname);

void rt_parse_command_line(rt_args_t *args, int argc, char *const *argv)
{
    int	c, i, found, old_optind;

    /* set defaults for arguments */
    bzero(args, sizeof(rt_args_t));

    args->nodeID = (unsigned long)-1;
    args->advertisement_cycle_time = 30;
    args->neighbor_timeout = 120;
    args->retransmission_timeout = 3;
    args->lsa_timeout = 120;
    
    /* parse command line */
    old_optind = optind;
    found = 0;
    while ((c = getopt(argc, argv, _rt_optstring)) != -1) {
	switch (c) {
	case 'i':
	    parse_long(optarg, &args->nodeID, argv[0], "nodeID");
	    break;
	case 'c':
	    found = 1;
	    rt_parse_config_file(argv[0], &args->config_file, optarg);
	    break;
	case 'G':
	    /* ignore -- this is only for grading */
	    break;
	case 'a':
	    parse_long(optarg, &args->advertisement_cycle_time, argv[0], 
			    "advertisement_cycle_time");
	    break;
	case 'n':
	    parse_long(optarg, &args->neighbor_timeout, argv[0], 
			    "neighbor_timeout");
	    break;
	case 'r':
	    parse_long(optarg, &args->retransmission_timeout, argv[0], 
			    "route_timeout");
	    break;
	case 't':
	    parse_long(optarg, &args->lsa_timeout, argv[0], 
			    "garbage_collect_timeout");
	    break;
	case '?':
	    exit(255);
	default:
	    fprintf(stderr, "%s: unknown argument -%c\n", argv[0], (char)c);
	    exit(255);
	    break;
	}
    }

    /* validate some of the arguments */
    if (args->nodeID == (unsigned long)-1) {
	fprintf(stderr, "%s: nodeID must be specified\n", argv[0]);
	exit(255);
    }
    if (args->neighbor_timeout % args->advertisement_cycle_time != 0) {
	fprintf(stderr, "%s: warning: neighbor_timeout (%lu) is not a "
		"multiple of advertisement_cycle_time (%lu)\n", argv[0], 
		args->neighbor_timeout, args->advertisement_cycle_time);
    }
    if (args->lsa_timeout % args->advertisement_cycle_time != 0) {
	fprintf(stderr, "%s: warning: lsa_timeout (%lu) is not a "
		"multiple of advertisement_cycle_time (%lu)\n", argv[0], 
		args->lsa_timeout, args->advertisement_cycle_time);
    }
    if (!found) {
	fprintf(stderr, "%s: you must specify a config_file\n", argv[0]);
	exit(0);
    }
    if (args->config_file.size < 2) {
	fprintf(stderr, "%s: warning: this node has no neighbors!\n", argv[0]);
    }
    found = 0;
    for (i=0; i<args->config_file.size; i++) {
	if (args->config_file.entries[i].nodeID == args->nodeID) {
	    found = 1;
	    break;
	}
    }
    if (!found) {
	fprintf(stderr, "%s: this node's nodeID (%lu) wasn't in the "
		"config file!\n", argv[0], args->nodeID);
	exit(255);
    }

    /* reset optind in case the caller wants to getopt some more */
    optind = old_optind;
}


void rt_parse_config_file(const char *cmd, rt_config_file_t *config, 
			  const char *filename)
{
    FILE *file;
    char line[MAX_CONFIG_FILE_LINE_LEN];
    char hostname[MAX_CONFIG_FILE_LINE_LEN];
    struct hostent *host;
    int i, ret;

    file = fopen(filename, "r");
    if (! file) {
	fprintf(stderr, "%s: can't open config_file = %s: ", cmd, filename);
	perror(NULL);
	exit(255);
    }

    config->size = 0;
    
    for (i=0; i<MAX_CONFIG_FILE_LINES; i++) {
	if (! fgets(line, MAX_CONFIG_FILE_LINE_LEN, file) ) {
	    break;
	}
	/* skip blank lines */
	if (line[0] == '\n') {
	    continue;
	}

	ret = sscanf(line, "%lu %s %hu %hu %hu", 
		     &config->entries[i].nodeID,
		     hostname,
		     &config->entries[i].routing_port,
		     &config->entries[i].local_port,
		     &config->entries[i].irc_port);
	if (ret != 5) {
	    fprintf(stderr, "%s: bad line in config_file: %s", cmd, line);
	    exit(255);
	}

	host = gethostbyname(hostname);
	if (host == NULL) {
	    fprintf(stderr, "%s: invalid hostname in config file = %s\n",
		    cmd, hostname);
	    exit(255);
	}
	/* assume that we want to use the first IP address if multiple */
	config->entries[i].ipaddr = ntohl( *(in_addr_t *)host->h_addr );

	++config->size;
    }
    if (getc(file) != EOF) {
	fprintf(stderr, "%s: too many lines in config_file (max = %d)\n",
		cmd, MAX_CONFIG_FILE_LINES);
	exit(255);
    }

    fclose(file);
}


void parse_long(const char* arg, 
		unsigned long* value, 
		const char* prefix,
		const char* varname)
{
  char* endptr;

  *value = strtoul(arg, &endptr, 0);

  if (*endptr != '\0' && !isspace(*endptr)) {

    /* print an error message */
    if (prefix != NULL && varname != NULL) {
	fprintf(stderr, "%s: invalid %s = %s\n", prefix, varname, arg);
	exit(255);
    }
    exit(255);
  }
}
