#!/bin/bash

unset CLASSPATH

java -cp `pwd`/bin/ -Djava.security.policy=security.policy -Djava.rmi.server.codebase="http://localhost:2005/" server.AgentHostSetup

java -cp `pwd`/bin/ -Djava.security.policy=security.policy -Djava.rmi.server.codebase="http://localhost:2005/" server.mediator.MediatorSetup

java -cp `pwd`/bin/ -Djava.security.policy=security.policy -Djava.rmi.server.codebase="http://localhost:2005/" server.repository.RepositorySetup
