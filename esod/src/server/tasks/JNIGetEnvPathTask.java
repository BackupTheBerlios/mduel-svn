package server.tasks;

import server.agent.Agent;

public class JNIGetEnvPathTask implements Task {
	private static final long serialVersionUID = -9031466777376797219L;
	public native String getpath();

	static {
		System.loadLibrary("jnitasks");
	}

	public Object run(Agent agent) {
		String path = getpath();
		return path;
	}
}
