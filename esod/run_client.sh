#!/bin/bash

unset CLASSPATH

java -Djava.security.policy=security.policy -Djava.rmi.server.codebase="file:`pwd`/dist/client.jar" -jar dist/client.jar $1
