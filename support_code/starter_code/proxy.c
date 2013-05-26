/*
 * proxy.c - Web proxy for Lab 8
 * 
 *
 */

#include <stdio.h>
#include "csapp.h"
#include "proxy.h"
#include "display.h"

#define   FILTER_FILE   "proxy.filter"
#define   LOG_FILE      "proxy.log"

team_struct team = {
/* Full name */
"Zhoulei",
/* Student ID */
"07302010015" };

/*============================================================
 * function declarations
 *============================================================*/

int find_target_address(char * uri, char * target_address, char * path,
		int * port);

void format_log_entry(char * logstring, int sock, char * uri, int size);

int doit(int fd);

void *thread(void *fd);

int writelog(char* log);

sem_t connectioncountmutex;
int currentconnection=0;

sem_t writelogmutex;

/* main function for the proxy program */
int main(int argc, char *argv[]) {
	signal(SIGPIPE, SIG_IGN);
	sem_init(&connectioncountmutex, 0, 1);
	sem_init(&writelogmutex, 0, 1);
	int port;
	if (argc != 2) {
		printf("Usage: ./%s port\n", argv[0]);
		exit(1);
	}

	/* initializes the xlib graphics display 
	 showing the number of connections    */
	init_display_window(argc, argv);

	/*****************************************
	 * fill in proxy code here ;o)           *
	 ******************************************/
	port=atoi(argv[1]);//Get the proxy port
	int listenfd, connfd, clientlen;
	struct sockaddr_in clientaddr;
	listenfd=Open_listenfd(port);//listen on the port

	/* demo code to illustrate how to change the graphic display. */
	/* replace this code                                          */
	/* with your own code                                         */

	while (1) {
		clientlen=sizeof(clientaddr);
		connfd=Accept(listenfd, (SA *)&clientaddr, &clientlen);//If there is a request then accept it
		pthread_t tid;
		Pthread_create(&tid, NULL, thread, (void *)(&connfd));//use a new thread to deal with the request
		Pthread_detach(tid);//detach the thread,not join it
		//usleep(200);
		//doit(connfd);
	}

	/* end of demo code                                             */

	/* destroys xlib display */
	destroy_window();

	return 0;
}

/*This function is used to log each request*/
int writelog(char* log) {
	FILE* file=fopen(LOG_FILE,"a+");
	if (file==NULL) {
		return -1;
	}
	if (fwrite(log, sizeof(char), strlen(log), file)<strlen(log)) {
		return -1;
	}
	fflush(file);
	fclose(file);
}

/*when catch a new request,use a new thread to deal with it*/
void *thread(void *fd) {
	/*Add the count*/
	/*The connection count should not have data race, so use a sem_t to avoid two thread operate the count at one time*/
	P(&connectioncountmutex);
	currentconnection++;
	change_display(currentconnection);
	V(&connectioncountmutex);
	doit((*(int *)fd));//do the job, including send the request to the server and send the response to the proxy client
	//int fail=doit((*(int *)fd));
	//int maxRetry=3;
	//while((fail==-1)&&(maxRetry>=0)){
	//	fail=doit((*(int *)fd));
	//	maxRetry--;
	//	sleep(1);
	//}
	
	
	/*Minus the count*/
	/*The connection count should not have data race, so use a sem_t to avoid two thread operate the count at one time*/
	P(&connectioncountmutex);
	currentconnection--;
	change_display(currentconnection);
	V(&connectioncountmutex);
}

int doit(int fd) {
	char buf[MAXLINE], targetaddress[MAXLINE], path[MAXLINE], uri[MAXLINE],
			method[16], version[16];
	int port;
	rio_t rio;
	Rio_readinitb(&rio, fd);
	Rio_readlineb(&rio, buf, MAXLINE);//Read the proxy request line
	buf[strlen(buf)-2]='\0';//Remove the last \r\n
	//printf("buf=%s\n", buf);
	/*Parse the request proxy command line*/
	char * tmp;
	int i=0;
	tmp=strtok(buf, " ");
	while (tmp!=NULL) {
		switch (i) {
		case 0:
			strncpy(method, tmp, strlen(tmp));
			method[strlen(tmp)]='\0';
			break;
		case 1:
			strncpy(uri, tmp, strlen(tmp));
			uri[strlen(tmp)]='\0';
			break;
		case 2:
			strncpy(version, tmp, strlen(tmp));
			version[strlen(tmp)]='\0';
			if (version[strlen(version)-1]=='\5') {
				version[strlen(version)-1]='\0';
			}
			break;
		}
		tmp=strtok(NULL," ");
		i++;
	}
	//printf("version=%s\n",version);
	/*Parse the uri to several parts*/
	if (find_target_address((char *)&uri, (char *)&targetaddress, (char *)&path, (int *)&port)==-1) {
		printf("Parsing url error.url=%s\n",uri);
		return -1;
	}

	/*Parpare the string which will be send to the web server*/
	char sendcommand[MAXLINE];
	char *pt=(char *)&sendcommand;
	memcpy(pt, method, strlen(method));
	pt+=strlen(method);
	if (path[0]!='/') {
		memcpy(pt, " /", 2);
		pt+=2;
	} else {
		memcpy(pt, " ", 1);
		pt+=1;
	}
	memcpy(pt, path, strlen(path));
	pt+=strlen(path);
	memcpy(pt, " ", 1);
	pt+=1;
	memcpy(pt, "HTTP/1.0\r\n\0",11);//I use HTTP/1.0 to speed the proxy, when I use HTTP/1.1 it will be much slower

	int clientfd;
	rio_t rio2;
	/*connect to the web server*/
	clientfd=Open_clientfd(targetaddress, port);
	/*If failed return error code*/
	if (clientfd==-1) {
		return -1;
	}
	Rio_readinitb(&rio2, clientfd);
	/*Write to the web server*/
	int count=0;
	printf("%s",sendcommand);
	Rio_writen(clientfd, sendcommand, strlen(sendcommand));
	count+=strlen(sendcommand);
	char newbuf[MAXLINE];
	int hasContent=0;
	while((Rio_readlineb(&rio, newbuf, MAXLINE)>2)||(hasContent==1)){
		//if(strcmp(newbuf,"\r\n"))
		printf("##%s$$",newbuf);
		Rio_writen(clientfd, newbuf, strlen(newbuf));
		count+=strlen(newbuf);
		if(strstr(newbuf,"Content-Length")!=NULL){
			hasContent=1;
		}
		if(strstr(newbuf,"--\r\n")!=NULL){
			break;
		}
	}
	if(hasContent==2){
		Rio_readlineb(&rio, newbuf, MAXLINE);
		Rio_writen(clientfd, newbuf, strlen(newbuf));
	}
	Rio_writen(clientfd, "\r\n\r\n", 4);
	printf("\r\n");
	count+=4;
	printf("count=%d\n",count);
	char buf2[MAXLINE];
	int readbytes=0;//Save the bytes read from the server which log requests
	ssize_t readcount=0;
	while ((readcount=Rio_readnb(&rio2, buf2, MAXLINE))!=0) {
		Rio_writen(fd, buf2, readcount);//Write the char array get from the server to the proxy client
		readbytes+=readcount;
	}
	/*Deal with log file*/
	char logline[MAXLINE];
	format_log_entry((char *)&logline, clientfd, (char *)&uri, readbytes);
	
	/*Only one thread can write to the log file at one time, so use sem_t.*/
	P(&connectioncountmutex);
	P(&writelogmutex);
	if(writelog((char *)&logline)==-1){
		printf("Write log file error.\n");
	}
	V(&writelogmutex);
	V(&connectioncountmutex);
	
	/*Close the client socket and the server socket*/
	Close(clientfd);
	Close(fd);
	return 0;
}

/*============================================================
 * url parser:
 *    find_target_address()
 *        Given a url, copy the target web server address to
 *        target_address and the following path to path.
 *        target_address and path have to be allocated before they 
 *        are passed in and should be long enough (use MAXLINE to be 
 *        safe)
 *
 *        Return the port number. 0 is returned if there is
 *        any error in parsing the url.
 *
 *============================================================*/

/*find_target_address - find the host name from the uri */
int find_target_address(char * uri, char * target_address, char * path,
		int * port)

{
	//  printf("uri: %s\n",uri);


	if (strncasecmp(uri, "http://", 7) == 0) {
		char * hostbegin, * hostend, *pathbegin;
		int len;

		/* find the target address */
		hostbegin = uri+7;
		hostend = strpbrk(hostbegin, " :/\r\n");
		if (hostend == NULL) {
			hostend = hostbegin + strlen(hostbegin);
		}

		len = hostend - hostbegin;

		strncpy(target_address, hostbegin, len);
		target_address[len] = '\0';

		/* find the port number ,if no port find use default 80 port*/
		if (*hostend == ':')
			*port = atoi(hostend+1);
		else
			*port=80;

		pathbegin = strchr(hostbegin, '/');
		if ((pathbegin == NULL)||(strlen(pathbegin)==0)) {
			path[0] = '\0';
		} else {
			pathbegin++;
			strcpy(path, pathbegin);
		}
		return 0;
	}
	target_address[0] = '\0';
	path[0]='\0';
	return -1;
}

/*============================================================
 * log utility
 *    format_log_entry
 *       Copy the formatted log entry to logstring
 *============================================================*/

void format_log_entry(char * logstring, int sock, char * uri, int size) {
	time_t now;
	char buffer[MAXLINE];
	struct sockaddr_in addr;
	unsigned long host;
	unsigned char a, b, c, d;
	int len = sizeof(addr);

	now = time(NULL);
	strftime(buffer, MAXLINE, "%a %d %b %Y %H:%M:%S %Z", localtime(&now));

	if (getpeername(sock, (struct sockaddr *) &addr, &len)) {
		return;
	}

	host = ntohl(addr.sin_addr.s_addr);
	a = host >> 24;
	b = (host >> 16) & 0xff;
	c = (host >> 8) & 0xff;
	d = host & 0xff;

	sprintf(logstring, "%s: %d.%d.%d.%d %s %d\n", buffer, a, b, c, d, uri,size);
}
