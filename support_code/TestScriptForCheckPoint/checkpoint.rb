#!/usr/bin/env ruby
#
# 15-441 Checkpoint 1 Script
# 15-441 Staff
#
# Complaints -> email arieshout@gmail.com

require 'socket'
#$SAFE = 1

$SERVER = "127.0.0.1"
$PORT = 9034  

if ARGV.size == 0
  puts "Usage: ./checkpoint2 server_port [server_ip_addr]"
  exit
elsif ARGV.size >= 1

  begin
    $PORT = Integer(ARGV[0])
  rescue
    puts "The port number must be an integer!"
    exit
  end
end

if ARGV.size >= 2
  $SERVER = ARGV[1].to_s()
end

puts "Server address - " + $SERVER + ":" + $PORT.to_s()


class ECHO

  def initialize(server, port, nick, channel)
    @server = server
    @port = port
    @nick = nick
    @channel = channel
  end

  def recv_data_from_server (timeout)
    pending_event = Time.now.to_i
    received_data = Array.new
    k = 0 
    flag = 0
    while flag == 0
      ## check for timeout
      time_elapsed = Time.now.to_i - pending_event
      if (time_elapsed > timeout)
        flag = 1
      end 
      ready = select([@echo], nil, nil, 0.0001)
      next if !ready
      for s in ready[0]
        if s == @echo then
          next if @echo.eof
          s = @echo.gets
          received_data[k] = s
          k= k + 1
        end
      end
    end
    return received_data
  end

  def test_echo (s)
    data=recv_data_from_server(2)
    if(data.size <= 0)
      return false
    else
      puts "<-- " + data[0]
      if (data[0] = s)
        return true
      else
        return false
      end
    end
  end

  def test_silence(timeout)
    data=recv_data_from_server(timeout)
    if (data.size > 0)
      return false
    else
      return true
    end
  end

  def send(s)
    # Send a message to the echo server and print it to the screen
    puts "--> #{s}"
    @echo.send "#{s}\n", 0 
  end

  def connect()
    # Connect to the echo server
    @echo = TCPSocket.open(@server, @port)
  end

  def disconnect()
    @echo.close
  end
end


##
# The main program.  Tests are listed below this point.  All tests
# should call the "result" function to report if they pass or fail.
##

$total_points = 0

def test_name(n)
  puts "\n////// #{n} \\\\\\\\\\\\"
  return n
end

def result(n, passed_exp, failed_exp, passed, points)
  explanation = nil
  if (passed)
    print "(+) #{n} passed"
    $total_points += points
    explanation = passed_exp
  else
    print "(-) #{n} failed"
    explanation = failed_exp
  end

  if (explanation)
    puts ": #{explanation}"
  else
    puts ""
  end
end

def eval_test(n, passed_exp, failed_exp, passed, points = 1)
  result(n, passed_exp, failed_exp, passed, points)
  exit(0) if !passed
end


begin

  echo1 =ECHO.new($SERVER, $PORT, '', '')
  echo2 =ECHO.new($SERVER, $PORT, '', '')
  echo3 =ECHO.new($SERVER, $PORT, '', '')

  #######################################################
  # Test 1: A single user
  #######################################################
  echo1.connect()
  s1 = "This is the first test. Only one user"

  tn = test_name("ECHO_SINGLE_USER")
  echo1.send(s1)
  #puts "<-- Testing echo with a single user..."
  eval_test(tn, nil, nil, echo1.test_echo(s1), 3)

  echo1.disconnect()

  #######################################################
  # Test 2: Two users
  #######################################################
  echo1.connect()
  echo2.connect()

  s1 = "This is the second test. Two users echo. I am xil"
  s2 = "This is the second test. Two users echo. I am xzhang"

  tn = test_name("ECHO_TWO_USERS")
  echo1.send(s1)
  echo2.send(s2)
  eval_test(tn, nil, nil, echo2.test_echo(s2),1)
  eval_test(tn, nil, nil, echo1.test_echo(s1),2)

  echo1.disconnect()
  echo2.disconnect()

  #######################################################
  # Test 3: Three users
  #######################################################
  echo1.connect()
  echo2.connect()
  echo3.connect()

  s1 = "This is the third test Three users echo I am xil"
  s2 = "This is the third test Three users echo I am xzhang"
  s3 = "This is the third test Three users echo I am dan"

  tn = test_name("ECHO_THREE_USERS")
  echo1.send(s1)
  echo2.send(s2)
  echo3.send(s3)
  eval_test(tn, nil, nil, echo3.test_echo(s2),1)
  eval_test(tn, nil, nil, echo2.test_echo(s2),1)
  eval_test(tn, nil, nil, echo1.test_echo(s1),2)

  echo1.disconnect()
  echo2.disconnect()
  echo3.disconnect()

rescue Interrupt
rescue Exception => detail
  puts detail.message()
  print detail.backtrace.join("\n")
ensure
  puts "\nYour score: #{$total_points} / 10"
  puts "Remember to commit code into WORK_UPLOAD/Project_1/YOUR_STUDENT_ID/checkpoint1"
end

