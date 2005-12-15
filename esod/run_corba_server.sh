#!/bin/sh

#java -cp bin/ -Djava.security.policy=security.policy corba.server.AgentPlatform -ORBInitialPort 9000

java com.sun.corba.se.internal.Activation.ServerTool -ORBInitialPort 9000 -cmd register -server corba.server.AgentPlatform -applicationName AgentPlatform -classpath ./bin/
