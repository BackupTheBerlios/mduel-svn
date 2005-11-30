#!/bin/sh

rmid -J-Djava.security.policy=security.policy &
rmiregistry &
orbd -ORBInitialPort 9000 -J-Djava.security.policy=security.policy &

echo "started rmid & rmiregistry & orbd..."
