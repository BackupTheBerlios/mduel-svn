
#include <jni.h>
#include <stdio.h>

#include "server_tasks_JNIGetEnvPathTask.h"

JNIEXPORT jstring JNICALL
Java_server_tasks_JNIGetEnvPathTask_getpath (JNIEnv *env, jobject obj) {
	jstring value;
	const char* buf = (char *)getenv("PATH");

	value = (*env)->NewStringUTF(env, buf);
	return value;
}
