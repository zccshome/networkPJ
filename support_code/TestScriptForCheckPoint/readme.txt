This check point is a ruby script to simulate three client connect to server

To Pass this script, your server need to receive message from any client, and forward this message to the sender only.

To run this script, you should type the command as follow:

ruby checkpoint2 25588 10.132.141.227

where 25588 is your server port
10.132.141.227 is your server ip(No Server IP implies 127.0.0.1)

Output of the test should as following:


[root@localhost ~]# ruby checkpoint.rb 25588
Server address - 127.0.0.1:25588

////// ECHO_SINGLE_USER \\\\\\
--> This is the first test. Only one user
<-- This is the first test. Only one user
(+) ECHO_SINGLE_USER passed

////// ECHO_TWO_USERS \\\\\\
--> This is the second test. Two users echo. I am xil
--> This is the second test. Two users echo. I am xzhang
<-- This is the second test. Two users echo. I am xzhang
(+) ECHO_TWO_USERS passed
<-- This is the second test. Two users echo. I am xil
(+) ECHO_TWO_USERS passed

////// ECHO_THREE_USERS \\\\\\
--> This is the third test Three users echo I am xil
--> This is the third test Three users echo I am xzhang
--> This is the third test Three users echo I am dan
<-- This is the third test Three users echo I am dan
(+) ECHO_THREE_USERS passed
<-- This is the third test Three users echo I am xzhang
(+) ECHO_THREE_USERS passed
<-- This is the third test Three users echo I am xil
(+) ECHO_THREE_USERS passed

Your score: 10 / 10
Remember to commit code into WORK_UPLOAD/Project_1/YOUR_STUDENT_ID/checkpoint1
