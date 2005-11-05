#!/bin/sh

cc -bundle -o libuname.jnilib -framework JavaVM -I/System/Library/Frameworks/JavaVM.framework/Versions/1.4.2/Headers/ UnameJNITask.c
mv libuname.jnilib ../../../bin/server/tasks
