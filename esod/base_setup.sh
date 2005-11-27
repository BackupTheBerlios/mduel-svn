#!/bin/bash

unset CLASSPATH

java -cp `pwd`/bin/ -Djava.security.policy=security.policy -Djava.rmi.server.codebase="file:`pwd`/dist/agentserver.jar" server.AgentHostSetup http://localhost:2005/

java -cp `pwd`/bin/ -Djava.security.policy=security.policy -Djava.rmi.server.codebase="file:`pwd`/dist/mediatorserver.jar" server.mediator.MediatorSetup http://localhost:2005/

java -cp `pwd`/bin/ -Djava.security.policy=security.policy -Djava.rmi.server.codebase="file:`pwd`/dist/repositoryserver.jar" server.repository.RepositorySetup http://localhost:2005/
