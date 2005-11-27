#!/bin/bash

unset CLASSPATH
export DYLD_LIBRARY_PATH=`pwd`/bin/server/tasks/
export LD_LIBRARY_PATH=`pwd`/bin/server/tasks/

java -cp bin/ -Djava.security.policy=security.policy -jar dist/client.jar
