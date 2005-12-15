#!/bin/bash

unset CLASSPATH

java -cp `pwd`/bin/ -Djava.security.policy=security.policy -Djava.rmi.server.codebase="$1" server.AgentHostSetup
