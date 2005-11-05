package server.tasks;

import server.agent.Agent;

public class UnameJNITask implements Task {
	private static final long serialVersionUID = -9031466777376797219L;
	public native String jniuname();

	static {
		System.loadLibrary("uname");
	}

	public void run(Agent agent) {
		String username = jniuname();
		System.out.println(username);
	}
}
