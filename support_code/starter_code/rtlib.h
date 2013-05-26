/*
 * rtlib.h
 *
 * Initial Author: Jeff Pang <jeffpang+441@cs.cmu.edu>
 * Class: 15-441 (Spring 2005)
 *
 * Some library routines that you may find useful for
 * Project 1: Routing Protocols.
 */

#ifndef __RTLIB_H__
#define __RTLIB_H__

/**
 * The maximum number of nodes that will be defined in a single config file.
 */
#define MAX_CONFIG_FILE_LINES 32
/**
 * The maximum number of characters of any line in the config file.
 */
#define MAX_CONFIG_FILE_LINE_LEN 255


/**
 * A single entry in the config file which describes a node.
 * All elements in this struct are in host byte-order.
 */
struct rt_config_entry_s {
    unsigned long nodeID; /* the nodeID of the node */
    unsigned long ipaddr; /* the IP address of the node */
    unsigned short routing_port; /* the routing port of the routing daemon */
    unsigned short local_port; /* the local port of the routing daemon */
    unsigned short irc_port; /* the IRC and forwarding port of the IRC Serv */
};
typedef struct rt_config_entry_s rt_config_entry_t;

/**
 * This structure contains an entire configuration file parsed in the 
 * function rt_parse_command_line(...).
 */
struct rt_config_file_s {
    int size; /* the number of entries in the entries field */
    struct rt_config_entry_s entries[MAX_CONFIG_FILE_LINES];
    /* all the entries in the configuration file; only the entries [0,size-1]
       are valid, the remainder should be ignored */
};
typedef struct rt_config_file_s rt_config_file_t;

/**
 * This structure is what is filled by rt_parse_command_line(...). It
 * contains all the command line arguments your routing daemon must know about.
 * All elements of this struct are in host byte-order.
 */
struct rt_args_s {
    /* ===== GENERAL OPTIONS ===== */
    unsigned long nodeID; /* nodeID of the current node */
    struct rt_config_file_s config_file; /* configuration file structure */
    
    /* ===== OSPF OPTIONS ===== */
    unsigned long advertisement_cycle_time; /* -a advertisement cycle */
    unsigned long neighbor_timeout;         /* -n timeout for dead neighbors */
    unsigned long retransmission_timeout;   /* -r timeout for retransmission */
    unsigned long lsa_timeout;              /* -t timeout to expire an LSA */
};
typedef struct rt_args_s rt_args_t;


#ifdef __cplusplus
extern "C" {
#endif

/**
 * Parse the required command line arguments for the routing daemon
 * into the struct pointed to by rt_args_t *args. This function also opens 
 * and parses the configuration file defined by the command line. If there is 
 * an error parsing the command line, this function prints the error to
 * stderr and exits.
 *
 * Arguments:
 * args       - the rt_args_t structure that the function will fill in. 
 *              The caller should allocate it and pass in a pointer.
 * argc       - the original argc passed into srouted's main() function.
 * argv       - the original argv passed into srouted's main() function.
 */
void rt_parse_command_line(rt_args_t *args, int argc, char * const*argv);

/**
 * Parse the config file into the structure config_file_t *config. This
 * function is called automatically by the rt_parse_command_line function,
 * but you can call it separately in your IRC Server. If there is an error
 * parsing the file, this function prints the error to stderr and exits.
 *
 * Arguments:
 * cmd        - a string that will be used as a prefix to all error messages.
 * config     - the rt_config_file_t structure the function will fill in.
 * filename   - the name of the config file to open and parse.
 */
void rt_parse_config_file(const char *cmd, rt_config_file_t *config, 
			  const char *filename);

#ifdef __cplusplus
}
#endif

#endif
