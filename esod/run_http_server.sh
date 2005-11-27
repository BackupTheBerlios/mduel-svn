#!/bin/sh

unset CLASSPATH

java -cp bin/ server.http.ClassFileServer 2005 bin/ &
