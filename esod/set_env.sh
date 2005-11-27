#!/bin/sh

rmid -J-Djava.security.policy=security.policy &
rmiregistry &
echo "started rmid & rmiregistry..."
