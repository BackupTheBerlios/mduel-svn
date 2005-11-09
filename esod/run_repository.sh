#!/bin/bash

unset CLASSPATH

java -Djava.security.policy=security.policy -Djava.rmi.server.codebase="file:`pwd`/dist/repositoryserver.jar" -jar dist/repositoryserver.jar
