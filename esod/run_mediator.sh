#!/bin/bash

unset CLASSPATH
export DYLD_LIBRARY_PATH=`pwd`/bin/server/tasks/
export LD_LIBRARY_PATH=`pwd`/bin/server/tasks/

java -Djava.security.policy=security.policy -Djava.rmi.server.codebase="file:`pwd`/dist/mediatorserver.jar" -jar dist/mediatorserver.jar
