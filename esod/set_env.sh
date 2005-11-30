#!/bin/sh

rmiregistry -J-Djava.security.policy=security.policy &
rmid -J-Djava.security.policy=security.policy &
orbd -ORBInitialPort 9000 -J-Djava.security.policy=security.policy &

echo "started rmid & rmiregistry & orbd..."
