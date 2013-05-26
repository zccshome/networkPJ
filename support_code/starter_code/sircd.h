#ifndef __SIRCD_H__
#define __SIRCD_H__

#define my_port 20989

#define max_client 10
#define max_message_len 512

typedef struct client_info
{
	int conn_fd;
	char nick_name[FD_SETSIZE];
	char user_name[FD_SETSIZE];
	char host_name[FD_SETSIZE];
	char real_name[FD_SETSIZE];
	char channel_name[FD_SETSIZE];

} client;

typedef struct s_channel
{
	int channel_on;
	char channel_name[FD_SETSIZE];
	int client_num;
} channel;

typedef struct s_pool
{
	int maxfd; 		// largest descriptor in sets
	int maxi; 		// largest index of client array
	fd_set read_set; 	// all active read descriptors
	fd_set write_set; 	// all active write descriptors
	fd_set ready_set;	// descriptors ready for reading
	int nready;		// return of select()

	int clientfd[FD_SETSIZE];	// max index in client array
	
	// might want to write this
	rio_t clientrio[FD_SETSIZE]; // set of active read buffers //

	// what else might be helpful for project 1?
	client clients[FD_SETSIZE];
	channel channels[FD_SETSIZE];
} pool;

void setup_socket();
void init_pool(int listen_fd, pool *pool);
void add_client(int conn_fd, struct s_pool *pool);
void check_clients(pool *pool);
int get_command(char (*tokens)[max_message_len+1], int token_len);
int nick(int conn_fd, char *nickname);
int user(int conn_fd, char *username, char* servername, char *realname);
int quit(int conn_fd);
int join(int conn_fd, char *channelname);
int part(int conn_fd, char *channelname);
int list(int conn_fd);
int privmsg(int conn_fd, char *name, char *msg);
int who(int conn_fd, char *name);
#endif