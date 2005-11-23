package server.tasks;

import server.agent.Agent;

public class JNIGetEnvPathTask implements Task {
	private static final long serialVersionUID = -9031466777376797219L;

	public native String getpath();

	static {
		System.loadLibrary("jnitasks");
	}

	/**
	 * returns the environment path
	 */
	public Object run(Agent agent, Object[] params) {
		String path = getpath();
		return path;
	}
}
