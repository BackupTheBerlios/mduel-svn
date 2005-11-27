#!/bin/bash

unset CLASSPATH

java -cp `pwd`/bin/ -Djava.security.policy=security.policy -Djava.rmi.server.codebase="file:`pwd`/dist/agentserver.jar" server.AgentHostSetup $1
