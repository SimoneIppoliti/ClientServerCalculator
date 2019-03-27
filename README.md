# Client-Server Calculator
Basic calculator using UDP networking protocol.

## Initial setup:
* In order to correctly make the server and clients to communicate, change either the hard-coded values of the IP address or set the server's one the same as that in the code;
* the server's and client's hard-coded IP addresses set in the scripts must be the same;
* the client machine's IP address does NOT need to be modified;
* server project and client project are separated: the one does not need the other to work.

You can set the respective IPs (and ports) in each project's **Program.cs** file.

## How to & functionalities:
1. Once the server and clients have started, type a command number in the client's console window (it must be a number between 0 and 3, one for each operation) and hit _Enter_:

Command | Operation
----- | -----
**0** | _Add_
**1** | _Subtract_
**2** | _Divide_
**3** | _Multiply_

2. Then type two numbers and hit _Enter_ each time;

3. the operation will be sent to the server and the result will eventually be received and print in the client's console.

**/!\\** _The calculator supports both int and float types._
