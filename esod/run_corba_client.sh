#!/bin/sh

unset CLASSPATH

java -cp bin/ -Djava.security.policy client.CorbaClient -ORBInitialPort 9000
