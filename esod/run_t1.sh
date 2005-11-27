#!/bin/bash

unset CLASSPATH

java -Djava.security.policy=security.policy -Djava.rmi.server.codebase="file:`pwd`/dist/dir_client.jar" -jar dist/dir_client.jar $1 $2
