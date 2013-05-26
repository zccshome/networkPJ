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
int byte_cnt = 0;
/* Function prototypes */
void init_node( int argc, char *argv[] );
size_t get_msg( char *buf, char *msg );
int tokenize( char const *in_buf, char tokens[MAX_MSG_TOKENS][MAX_MSG_LEN+1] );

struct s_pool p;

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
    printf("%s\n","We are starting zzz...");
    int listen_fd = Open_listenfd(my_port);
    printf("Setting up listen_fd with number: %d\n", listen_fd);
    SA clientaddr;
    /*// Setup your read_set with FD_ZERO and the server socket descriptor
    FD_ZERO(&p.read_set);
    FD_SET(listen_fd, &p.read_set);
    p.maxfd = listen_fd + 1;*/
    init_pool(listen_fd, &p);
    while(1)
    {
        p.ready_set = p.read_set;
        p.nready = select(p.maxfd + 1, &p.ready_set, &p.write_set, NULL, NULL);
        if(FD_ISSET(listen_fd, &p.ready_set))
        {
            socklen_t clientlen = sizeof(clientaddr);
            int conn_fd = Accept(listen_fd, &clientaddr, &clientlen);//If there is a request then accept it
            printf("Accepting with conn_fd: %d\n", conn_fd);
            add_client(conn_fd, &p);
        }
        check_clients(&p);
        /*socklen_t clientlen = sizeof(clientaddr);
        int conn_fd = Accept(listen_fd, &clientaddr, &clientlen);//If there is a request then accept it
        printf("Accepting with conn_fd: %d\n", conn_fd);
        break;*/
    }
}

/*
 * Initialize the pool and set all the client_fd -1.
 */
void init_pool(int listen_fd, struct s_pool *pool)
{
    /* Remember this:
    struct s_pool
    {
        int maxfd;      // largest descriptor in sets
        fd_set read_set;    // all active read descriptors
        fd_set write_set;   // all active write descriptors
        fd_set ready_set;   // descriptors ready for reading
        int nready;     // return of select()

        int clientfd[FD_SETSIZE];   // max index in client array
    
        // might want to write this
        rio_t clientrio[FD_SETSIZE]; // set of active read buffers //

        // what else might be helpful for project 1?
    } pool*/
    bzero(pool, sizeof(struct s_pool));
    pool->maxi = -1;
    int i;
    for(i = 0; i < FD_SETSIZE; i++)
        pool->clientfd[i] = -1;
    FD_ZERO(&pool->read_set);
    FD_SET(listen_fd, &pool->read_set);
    pool->maxfd = listen_fd;
    for(i = 0; i < FD_SETSIZE; i++)
    {
        p.clients[i].conn_fd = -1;
        p.channels[i].channel_on = 0;
        p.channels[i].client_num = 0;
    }
}

/*
 * add a new client to the pool
 */
void add_client(int conn_fd, pool *pool)
{
    pool->nready--;
    /* Find an available block to preserve the client */
    int i;
    for(i = 0; i < FD_SETSIZE; i++)
    {
        if(pool->clientfd[i] < 0)
        {
            pool->clientfd[i] = conn_fd;
            Rio_readinitb(&pool->clientrio[i], conn_fd);

            FD_SET(conn_fd, &pool->read_set);

            /* Change the maxfd and maxi if necessary*/
            if(conn_fd > pool->maxfd)
                pool->maxfd = conn_fd;
            if(i > pool->maxi)
                pool->maxi = i;
            break;
        }
    }
    if(i == FD_SETSIZE)
        app_error("add_client error: Too many clients");

    pool->clients[conn_fd].conn_fd = conn_fd;
    strcpy(pool->clients[conn_fd].nick_name, "passenger");
    strcpy(pool->clients[conn_fd].user_name, "passenger");
    strcpy(pool->clients[conn_fd].host_name, "passenger");
    strcpy(pool->clients[conn_fd].real_name, "passenger");
    strcpy(pool->clients[conn_fd].channel_name, "passenger");
    printf("New client's nickname is: %s\n", pool->clients[conn_fd].nick_name);
    printf("New client's username is: %s\n", pool->clients[conn_fd].user_name);
    printf("New client's hostname is: %s\n", pool->clients[conn_fd].host_name);
    printf("New client's realname is: %s\n", pool->clients[conn_fd].real_name);
    /*client *newclient = Malloc(sizeof(client));
    strcpy(newclient->nickname, "*");
    strcpy(newclient->username, "*");
    newclient->clientfd = connfd;
    newclient->channel_id = -1;
    newclient->nick_is_set = 0;
    newclient->user_is_set = 0;

    client_list[connfd] = newclient;
    client_count++;*/
}

void check_clients(pool *pool)
{
    int i, receiveByteNum;
    char buf[MAXLINE];
    rio_t rio;
    for(i = 0; (i <= pool->maxi) && (pool->nready > 0); i++)
    {
        int conn_fd = pool->clientfd[i];
        /* Check if it is ready */
        if((conn_fd > 0) && (FD_ISSET(conn_fd, &pool->ready_set)))
        {
            pool->nready--;
            rio = pool->clientrio[i];
            if((receiveByteNum = Rio_readlineb(&rio, buf, MAXLINE)) != 0)
            {
                byte_cnt += receiveByteNum;
                printf("Server received %d (%d total) bytes on fd %d\n", receiveByteNum, byte_cnt, conn_fd);
                Rio_writen(conn_fd, buf, receiveByteNum);
                char msg[MAX_MSG_LEN];
                size_t message_len;
                if((message_len =  get_msg(buf, msg)) < 0)
                    app_error("message too long");
                printf("The command you input is: %s\n", msg);
                char tokens[MAX_MSG_TOKENS][MAX_MSG_LEN+1];
                int token_len = tokenize(msg, tokens);
                int command_type = get_command(tokens, token_len);
                switch(command_type)
                {
                    case 1: 
                            if(token_len == 2)
                            {
                                nick(conn_fd, tokens[1]);
                            }
                            else
                            {
                                sprintf(buf, "The command should be like: NICK <nickname>.\n");
                                Write(conn_fd, buf, strlen(buf));
                            }
                            break;
                    case 2: if(token_len == 4)
                            {
                                user(conn_fd, tokens[1], tokens[2], tokens[3]);
                            }
                            else
                            {
                                sprintf(buf, "ERR_NEEDMOREPARAMS. The command should be like: USER <username> <hostname> <servername> <realname>.\n");
                                Write(conn_fd, buf, strlen(buf));
                            }
                            break;
                    case 3: quit(conn_fd); break;
                    case 4: if(token_len == 2)
                            {
                                join(conn_fd, tokens[1]);
                            }
                            else
                            {
                                sprintf(buf, "ERR_NEEDMOREPARAMS. The command should be like: JOIN <channelname>.\n");
                                Write(conn_fd, buf, strlen(buf));
                            }
                            break;
                    case 5: if(token_len == 2)
                            {
                                part(conn_fd, tokens[1]);
                            }
                            else
                            {
                                sprintf(buf, "ERR_NEEDMOREPARAMS. The command should be like: PART <channelname>.\n");
                                Write(conn_fd, buf, strlen(buf));
                            }
                            break;
                    case 6: list(conn_fd);
                            break;
                    case 7: if(token_len == 3)
                            {
                                privmsg(conn_fd, tokens[1], tokens[2]);
                            }
                            else
                            {
                                sprintf(buf, "ERR_NEEDMOREPARAMS. The command should be like: PRIVMSG <nickname/channelname>.\n");
                                Write(conn_fd, buf, strlen(buf));
                            }
                            break;
                    case 8: if(token_len == 2)
                            {
                                who(conn_fd, tokens[1]);
                            }
                            else
                            {
                                sprintf(buf, "ERR_NEEDMOREPARAMS. The command should be like: WHO <nickname/channelname>.\n");
                                Write(conn_fd, buf, strlen(buf));
                            }
                            break;
                    default: 
                            sprintf(buf, "The command you input %s is not understandable!\n", msg);
                            Write(conn_fd, buf, strlen(buf));
                            break;
                }
            }
            /* EOF detected, remove descriptor from pool */
            else
            {
                Close(conn_fd);
                FD_CLR(conn_fd, &pool->read_set);
                pool->clientfd[i] = -1;
                pool->clients[conn_fd].conn_fd = -1;
            }
        }
    }
}

int get_command(char (*tokens)[MAX_MSG_LEN+1], int token_len)
{
    int i;
    for(i = 0; i < token_len; i++)
        printf("The %d token is: %s\n", i, tokens[i]);
    if(strcasecmp(tokens[0], "nick") == 0)
        return 1;
    if(strcasecmp(tokens[0], "user") == 0)
        return 2;
    if(strcasecmp(tokens[0], "quit") == 0)
        return 3;
    if(strcasecmp(tokens[0], "join") == 0)
        return 4;
    if(strcasecmp(tokens[0], "part") == 0)
        return 5;
    if(strcasecmp(tokens[0], "list") == 0)
        return 6;
    if(strcasecmp(tokens[0], "privmsg") == 0)
        return 7;
    if(strcasecmp(tokens[0], "who") == 0)
        return 8;
    return -1;
}

int nick(int conn_fd, char nickname[MAX_MSG_TOKENS])
{
    int i;
    char buf[MAXLINE];
    for(i = 0; i < FD_SETSIZE; i++)
    {
        if(p.clients[i].conn_fd != -1 && strcmp(p.clients[i].nick_name, "passenger") != 0 
            && strcmp(p.clients[i].nick_name, nickname) == 0 && i != conn_fd)
        {
            sprintf(buf, "The nick you input has been used!\n");
            Write(conn_fd, buf, strlen(buf));
            break;
        }
    }
    if(i == FD_SETSIZE)
    {
        sprintf(buf, "Your original nickname is %s\n", p.clients[conn_fd].nick_name);
        Write(conn_fd, buf, strlen(buf));
        strcpy(p.clients[conn_fd].nick_name, nickname);
        sprintf(buf, "Your new nickname is %s\n", p.clients[conn_fd].nick_name);
        Write(conn_fd, buf, strlen(buf));
    }
    return 1;
}

int user(int conn_fd, char *username, char* hostname, char *realname)
{
    char buf[MAXLINE]; 
    if(strcmp(p.clients[conn_fd].user_name, "passenger") == 0 || strcmp(p.clients[conn_fd].host_name, "passenger") == 0
        || strcmp(p.clients[conn_fd].real_name, "passenger") == 0)
    {
        strcpy(p.clients[conn_fd].user_name, username);
        strcpy(p.clients[conn_fd].host_name, hostname);
        strcpy(p.clients[conn_fd].real_name, realname);
        sprintf(buf, "Your username is %s\n", p.clients[conn_fd].user_name);
        Write(conn_fd, buf, strlen(buf));
        sprintf(buf, "Your hostname is %s\n", p.clients[conn_fd].host_name);
        Write(conn_fd, buf, strlen(buf));
        sprintf(buf, "Your realname is %s\n", p.clients[conn_fd].real_name);
        Write(conn_fd, buf, strlen(buf));
    }
    else
    {
        sprintf(buf, "You have confirmed your information!\n");
        Write(conn_fd, buf, strlen(buf));
    }
    return 1;
}

int quit(int conn_fd)
{
    char buf[MAXLINE]; int i;
    sprintf(buf, "Bye bye %s!\n", p.clients[conn_fd].nick_name);
    Write(conn_fd, buf, strlen(buf));
    Close(conn_fd);
    FD_CLR(conn_fd, &p.read_set);
    p.clientfd[conn_fd] = -1;
    p.clients[conn_fd].conn_fd = -1;
    for(i = 0; i < FD_SETSIZE; i++)
    {
        if(p.clients[i].conn_fd != -1)
        {
            sprintf(buf, "User %s has quited!\n", p.clients[conn_fd].nick_name);
            Write(p.clients[i].conn_fd, buf, strlen(buf));
            //break;
        }
    }
    return 1;
}

int join(int conn_fd, char *channelname)
{
    if(strcmp(p.clients[conn_fd].channel_name, "passenger") != 0)
    {
        part(conn_fd, p.clients[conn_fd].channel_name);
    }
    int i; int channel_exist = 0;
    for(i = 0; i < FD_SETSIZE; i++)
    {
        if(p.channels[i].channel_on == 1 && strcmp(p.channels[i].channel_name, channelname) == 0)
        {
            strcpy(p.clients[conn_fd].channel_name, channelname);
            p.channels[i].client_num++;
            channel_exist = 1;
            break;
        }
    }
    if(channel_exist == 0)
    {
        for(i = 0; i < FD_SETSIZE; i++)
        {
            if(p.channels[i].channel_on == 0)
            {
                p.channels[i].channel_on = 1;
                strcpy(p.channels[i].channel_name, channelname);
                strcpy(p.clients[conn_fd].channel_name, channelname);
                p.channels[i].client_num = 1;
                break;
            }
        }
    }
    return 1;
}

int part(int conn_fd, char *channelname)
{
    char buf[MAXLINE];
    if(strcmp(p.clients[conn_fd].channel_name, channelname) != 0)
    {
        sprintf(buf, "User does not join this channel!\n");
        Write(conn_fd, buf, strlen(buf));
        return 1;
    }
    int i; int channel_exist = 0;
    for(i = 0; i < FD_SETSIZE; i++)
    {
        if(p.channels[i].channel_on == 1 && strcmp(p.channels[i].channel_name, channelname) == 0)
        {
            strcpy(p.clients[conn_fd].channel_name, "passenger");
            p.channels[i].client_num--;
            channel_exist = 1;
            if(p.channels[i].client_num == 0)
            {
                p.channels[i].channel_on = 0;
                strcmp(p.channels[i].channel_name, "passenger");
            }
            break;
        }
    }
    if(channel_exist == 0 && i == FD_SETSIZE)
    {
        sprintf(buf, "The channel does noe exist!\n");
        Write(conn_fd, buf, strlen(buf));
        return 1;
    }
    return 1;
}

int list(int conn_fd)
{
    int i; char buf[MAXLINE];
    for(i = 0; i < FD_SETSIZE; i++)
    {
        if(p.channels[i].channel_on == 1)
        {
            sprintf(buf, "Channel: %s\n",p.channels[i].channel_name);
            Write(conn_fd, buf, strlen(buf));
        }
    }
    return 1;
}

int privmsg(int conn_fd, char *name, char *msg)
{
    int i; int j; char buf[MAXLINE];
    for(i = 0; i < FD_SETSIZE; i++)
    {
        if(p.channels[i].channel_on == 1 && strcmp(p.channels[i].channel_name, name) == 0)
        {
            for(j = 0; j < FD_SETSIZE; j++)
            {
                if(strcmp(p.clients[j].channel_name, name) == 0 && j != conn_fd)
                {
                    sprintf(buf, "%s\n",msg);
                    Write(j, buf, strlen(buf));
                }
            }
            break;
        }
    }
    for(i = 0; i < FD_SETSIZE; i++)
    {
        if(strcmp(p.clients[i].nick_name, name) == 0 && i != conn_fd)
        {
            sprintf(buf, "%s\n",msg);
            Write(i, buf, strlen(buf));
            break;
        }
    }
    return 1;
}
int who(int conn_fd, char *name)
{
    int i; int j; char buf[MAXLINE];
    for(i = 0; i < FD_SETSIZE; i++)
    {
        if(p.channels[i].channel_on == 1 && strcmp(p.channels[i].channel_name, name) == 0)
        {
            for(j = 0; j < FD_SETSIZE; j++)
            {
                if(strcmp(p.clients[j].channel_name, name) == 0 && j != conn_fd)
                {
                    sprintf(buf, "Nickname: %s, username: %s, hostname: %s, realname: %s, channelname: %s\n", 
                        p.clients[j].nick_name, p.clients[j].user_name, p.clients[j].host_name, p.clients[j].real_name, name);
                    Write(conn_fd, buf, strlen(buf));
                }
            }
            break;
        }
    }
    for(i = 0; i < FD_SETSIZE; i++)
    {
        if(strcmp(p.clients[i].nick_name, name) == 0 && i != conn_fd)
        {
            sprintf(buf, "Nickname: %s, username: %s, hostname: %s, realname: %s, channelname: %s\n", 
                p.clients[i].nick_name, p.clients[i].user_name, p.clients[i].host_name, p.clients[i].real_name, name);
            Write(conn_fd, buf, strlen(buf));
            break;
        }
    }
    return 1;
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
