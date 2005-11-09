#!/bin/bash

unset CLASSPATH

java -Djava.security.policy=security.policy -Djava.rmi.server.codebase="file:`pwd`/dist/agentserver.jar" -jar dist/agentserver.jar
