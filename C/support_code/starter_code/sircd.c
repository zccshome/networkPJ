/* To compile: gcc sircd.c rtlib.c rtgrading.c csapp.c -lpthread -osircd */

#include "rtlib.h"
#include "rtgrading.h"
#include "csapp.h"
#include <stdlib.h>
#include "sircd.h"

/* Macros */
#define MAX_MSG_TOKENS 10
#define MAX_MSG_LEN 512

/* Global variables */
u_long curr_nodeID;
rt_config_file_t   curr_node_config_file;  /* The config_file  for this node */
rt_config_entry_t *curr_node_config_entry; /* The config_entry for this node */

/* Function prototypes */
void init_node( int argc, char *argv[] );
size_t get_msg( char *buf, char *msg );
int tokenize( char const *in_buf, char tokens[MAX_MSG_TOKENS][MAX_MSG_LEN+1] );

/* Main */
int main( int argc, char *argv[] )
{
    init_node( argc, argv );
    //rt_args_t args;
    printf( "I am node %lu and I listen on port %d for new users\n", curr_nodeID, curr_node_config_entry->irc_port );
    setup_socket();
    return 0;
}

void setup_socket()
{
    int listen_fd = Open_listenfd(my_port);
    printf("Setting up listen_fd with number: %d\n", listen_fd);
    SA clientaddr;
    while (1)
    {
        socklen_t clientlen = sizeof(clientaddr);
        int conn_fd = Accept(listen_fd, &clientaddr, &clientlen);//If there is a request then accept it
        printf("Accepting with conn_fd: %d\n", conn_fd);
        break;
        /*pthread_t tid;
        Pthread_create(&tid, NULL, thread, (void *)(&connfd));//use a new thread to deal with the request
        Pthread_detach(tid);//detach the thread,not join it*/
        //usleep(200);
        //doit(connfd);
    }
}

/*
 * void init_node( int argc, char *argv[] )
 *
 * Takes care of initializing a node for an IRC server
 * from the given command line arguments
 */
void init_node( int argc, char *argv[] )
{
    int i;

    if( argc < 3 )
    {
        printf( "%s <nodeID> <config file>\n", argv[0] );
        exit( 0 );
    }

    /* Parse nodeID */
    curr_nodeID = atol( argv[1] );

    /* Store  */
    rt_parse_config_file(argv[0], &curr_node_config_file, argv[2] );

    /* Get config file for this node */
    for( i = 0; i < curr_node_config_file.size; ++i )
        if( curr_node_config_file.entries[i].nodeID == curr_nodeID )
             curr_node_config_entry = &curr_node_config_file.entries[i];

    /* Check to see if nodeID is valid */
    if( !curr_node_config_entry )
    {
        printf( "Invalid NodeID\n" );
        exit(1);
    }
}


/*
 * size_t get_msg( char *buf, char *msg )
 *
 * char *buf : the buffer containing the text to be parsed
 * char *msg : a user malloc'ed buffer to which get_msg will copy the message
 *
 * Copies all the characters from buf[0] up to and including the first instance
 * of the IRC endline characters "\r\n" into msg.  msg should be at least as
 * large as buf to prevent overflow.
 *
 * Returns the size of the message copied to msg.
 */
size_t get_msg(char *buf, char *msg)
{
    char *end;
    int  len;

    /* Find end of message */
    end = strstr(buf, "\r\n");

    if( end )
    {
        len = end - buf + 2;
    }
    else
    {
        /* Could not find \r\n, try searching only for \n */
        end = strstr(buf, "\n");
	if( end )
	    len = end - buf + 1;
	else
	    return -1;
    }

    /* found a complete message */
    memcpy(msg, buf, len);
    msg[end-buf] = '\0';

    return len;	
}

/*
 * int tokenize( char const *in_buf, char tokens[MAX_MSG_TOKENS][MAX_MSG_LEN+1] )
 *
 * A strtok() variant.  If in_buf is a space-separated list of words,
 * then on return tokens[X] will contain the Xth word in in_buf.
 *
 * Note: You might want to look at the first word in tokens to
 * determine what action to take next.
 *
 * Returns the number of tokens parsed.
 */
int tokenize( char const *in_buf, char tokens[MAX_MSG_TOKENS][MAX_MSG_LEN+1] )
{
    int i = 0;
    const char *current = in_buf;
    int  done = 0;

    /* Possible Bug: handling of too many args */
    while (!done && (i<MAX_MSG_TOKENS)) {
        char *next = strchr(current, ' ');

	if (next) {
	    memcpy(tokens[i], current, next-current);
	    tokens[i][next-current] = '\0';
	    current = next + 1;   /* move over the space */
	    ++i;

	    /* trailing token */
	    if (*current == ':') {
	        ++current;
		strcpy(tokens[i], current);
		++i;
		done = 1;
	    }
	} else {
	    strcpy(tokens[i], current);
	    ++i;
	    done = 1;
	}
    }

    return i;
}
