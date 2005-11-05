
#include <jni.h>
#include <stdio.h>

#include "UnameJNITask.h"

JNIEXPORT jstring JNICALL
Java_server_tasks_UnameJNITask_jniuname (JNIEnv *env, jobject obj) {
	jstring value;
	const char* buf = (char *)getenv("PATH");

	value = (*env)->NewStringUTF(env, buf);
	return value;
}
