#!/bin/sh

java -cp bin/ -Djava.security.policy=security.policy corba.server.AgentPlatform -ORBInitialPort 9000
