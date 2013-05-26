#!/usr/bin/python
#
# Author: Albert Sheu (asheu)
# Last Updated: 20-Sep-2007
#
# Description: 
#
# TODO: File description (above)
# TODO: Built-in assertions
#       assert return :Adonis.local 451 * :...
#       assert noreturn
#       assert irccode 375

import sys, socket
from select import select
from time import sleep
import re

_debug = False
_escapeseq = {'r':'\r', 'n':'\n', 't':'\t', '0':'\0', '\\':'\\'}
_escapeseq_inv = {'\r':'\\r', '\n':'\\n', '\t':'\\t', '\0':'\\0', '\\':'\\\\'}
_defaultport = 6667
_bufsize = 65536
_timeout = 0.2
_autorecv = False

users_by_name = {}
attributes = {}
line_number = 0

class User:
    def __init__(self, name):
        self.name = name
        self.autoflush = True
        self.autonewline = True
        self.sock = None
        self.buffer = ""
        self.server = "<no one>"

    def connect(self, params):
        """Connect to the hostname and port specified.  If no port is given,
        _defaultport is used."""
        if ':' in params and params.find(':') > 0:
            hostname, port = tuple(params.split(':')[:2])
        else:
            hostname, port = params, _defaultport         
        self.server = hostname.split('.')[0]
        if self.server.isdigit():
            self.server = hostname
        try:
            print '%s > %s: <SYN>' % (self.name, self.server)
            self.sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
            self.sock.connect((hostname, int(port)))
        except socket.error:
            self.printerror("socket error during connect()")
        except AttributeError:
            self.printerror("socket has been closed")

    def close(self, params):
        """Send an EOF to the server."""
        try: 
            print '%s > %s: <EOF>' % (self.name, self.server)
            self.sock.close()
            self.sock = None
        except socket.error:
            self.printerror("socket error during close()")
        except AttributeError:
            self.printerror("socket has been closed")

    def set(self, params):
        """Set a parameter."""
        if params == 'autoflush':
            self.autoflush = True
        elif params == 'autonewline':
            self.autonewline = True

    def unset(self, params):
        """Unset a parameter."""
        if params == 'autoflush':
            self.autoflush = False
        elif params == 'autonewline':
            self.autonewline = False

    def send(self, params):
        """Send data to the server.  If newlines are automatic, then add a
        CRLF.  If flushing is automatic, send the data right away.  If
        flushing is not automatic, then queue the data in a buffer and
        wait until self.flush() is called."""
        if self.autonewline:
            params += '\r\n'
        if self.autoflush:
            try:
                print '%s > %s: "%s"' % \
                    (self.name, self.server, unescape(params))
                self.sock.send(params)
            except socket.error:
                self.printerror("socket error during send()")
            except AttributeError:
                self.printerror("socket has been closed")
        else:
            self.buffer += params

    def recv(self, params=_bufsize):
        """Print out the data received from the server."""
        try:
            if not params:
                params = _bufsize
            buffer = self.sock.recv(int(params))
            if len(buffer) == 0:
                print '%s > %s: <EOF>' % (self.server, self.name)
                self.sock = None
                return
            index = 0
            lines = buffer.splitlines(True)
            for line in lines:
                print '%s > %s: "%s"' % \
                      (self.server, self.name, unescape(line))
            return buffer 
        except socket.error:
            self.printerror("socket error during recv()")
            return None
        except AttributeError:
            self.printerror("socket has been closed before recv()")
            return None

    def rematch(self, params):
        try:
            input = self.recv().strip()
            if not re.search(params, input):
                self.printerror("Regexp match failed %s: %s" % (params, input))
        except socket.error:
            self.printerror("socket error during recv()")
        except AttributeError:
            self.printerror("socket has been closed before recv()")

    def flush(self, params):
        """If flushing is not automatic, then this will send all data queued
        on the buffer to the server."""
        try: 
            if len(self.buffer) > 0:
                print '%s > %s: "%s"' % \
                    (self.name, self.server, unescape(self.buffer))
                self.sock.send(self.buffer)
        except socket.error:
            self.printerror("socket error during flush()")
        except AttributeError:
            self.printerror("socket has been closed")
        self.buffer = ""

    def sleep(self, params):
        """User will do nothing but block for the given number of seconds."""
        try:
            timeout = float(params.split()[0])        
            sleep(timeout)
        except IndexError:
            self.printerror("no time specified for sleep command")
        except ValueError:
            self.printerror("invalid sleep parameter: %s" % params)

    def printerror(self, msg):
        print '*** ERROR, Line %d: %s' % (line_number, msg)

def createuser(username):
    users_by_name[username] = User(username)

def escape(line):
    """Turns string literals into printable (escaped) format."""
    line = line.strip()
    for key in attributes:
        line = line.replace('$'+key, attributes[key])
    buffer, ignorenext = "", False
    for (i, ch) in enumerate(line):
        if ignorenext:
            ignorenext = False
            continue
        if ch == '\\':
            buffer += _escapeseq[line[i+1]]
            ignorenext = True
        else:
            buffer += ch
    return buffer

def unescape(line):
    """Turn escaped strings into literals."""
    return ''.join([_escapeseq_inv.get(ch, ch) for ch in line])

def partition(line):
    """Divide the string into the user name, command name, and parameters,
    and returns it as a 3-tuple."""
    words = line.split()
    user = words[0]
    try:
        command = words[1]
        for word in [user, command]:
            line = line.lstrip()
            line = line[len(word):].lstrip()
        return (user, command, line)
    except IndexError:
        return words[0], "", ""

def processline(line):
    """Parse a testfile line and run the appropriate command."""
    if line.startswith('#') or len(line.strip()) == 0:
        return
    username, command, params = partition(line)    
    if username not in users_by_name:
        createuser(username)
    user = users_by_name[username]
    if not command:        
        user.printerror("no command specified")
    commands = {
        'connect' : user.connect,
        'close' : user.close,
        'set' : user.set,
        'unset' : user.unset,
        'send' : user.send,
        'recv' : user.recv,
        'flush' : user.flush,
        'sleep' : user.sleep,
        'match' : user.rematch
        }
    if command not in commands:
        user.printerror("unknown command %s" % command)
        return
    commands[command](params)
    
def processreplies(timeout=_timeout):
    """Wait for replies from the server until the timeout value is hit.
    Once select() returns, each user who received data is processed."""
    clientsocks = [(user.sock, user) for user in users_by_name.values()\
                       if user.sock != None]
    clientsocks = dict(clientsocks)
    ready, _, _ = select(clientsocks.keys(), [], [], timeout)
    for sock in ready:
        user = clientsocks[sock]
        if _autorecv: user.recv()
    return len(ready)

def processfile(filename):
    """Opens file, and then runs each command one by one.  The user pool
    is reset after each file is processed."""
    fp = open(filename, 'r')
    global line_number
    try:
        print '*** Processing file %s' % filename
        for line_number, line in enumerate(fp):
            processline(escape(line))
            processreplies()
        users = users_by_name.values()
        while [user.sock for user in users if user.sock != None]:
            processed = processreplies(2)
            if processed == 0:
                if _debug: print 'Connection timed out waiting for server.'
                break
    finally:
        fp.close()

def tests():
    """Some makeshift unit tests"""
    line1 = 'name command p1 p2 p3'
    line2 = ' name  command p1 p2 p3'
    line3 = 'name\tcommand\tp1 p2 p3'
    for line in [line1, line2, line3]:
        username, command, params = partition(line)
        assert username == 'name'
        assert command == 'command'
        assert params == 'p1 p2 p3'
    global attributes, users_by_name
    attributes = {'key1':'value1',
                  'key2':'value2'}
    assert escape('\\\\\\r') == '\\\r'
    assert escape('$key1 $key2\\r\\n') == 'value1 value2\r\n'
    attributes = {}
    processline('1 unset autoflush')
    processline('2 unset autonewline')
    assert len(users_by_name) == 2
    assert '1' in users_by_name
    assert '2' in users_by_name
    assert not users_by_name['1'].autoflush
    assert not users_by_name['2'].autonewline
    # TODO: other User class unit tests
    if _debug: print "*** Unit tests passed!"

def printusage():
    print ("Usage: %s [--key=<value>] "
           "<testfile> [<testfile>...]" % sys.argv[0])

def configure(args):
    """Process command line arguments, set global variables such as
    filename list for testing, etc."""
    filenames = []
    if '--autorecv' in args:
        global _autorecv
        _autorecv = True
    for arg in args[1:]:
        if arg.startswith('--'):
            arg = arg.lstrip('--')
            index = arg.find('=')
            key, value = arg[:index], arg[index+1:]
            attributes[key] = value
        else:
            filenames.append(arg)
    return filenames
            
if __name__ == '__main__':
    """Parse command line, run unit tests if debugging, and then run each
    test file one by one."""
    if _debug: tests()    
    if len(sys.argv) < 2:
        printusage()
        exit(0)
    filenames = configure(sys.argv)
    for filename in filenames:
        users_by_name = {}
        processfile(filename)
                
