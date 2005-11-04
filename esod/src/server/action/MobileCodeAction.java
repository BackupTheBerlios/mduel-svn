package server.action;

import server.agent.Agent;
import java.lang.reflect.*;

public class MobileCodeAction implements Action {
	private static final long serialVersionUID = 1698541644546154950L;
	private String method;

	public MobileCodeAction(String m) {
		this.method = m;
	}

	public void run(Agent agent) {
		int idx = this.method.lastIndexOf(".");
		this.method = this.method.substring(idx+1);
		try {
			Class c = Class.forName(agent.getClass().getName());
			Method m = c.getMethod(this.method, null);
			m.invoke(agent, null);
		} catch (Exception e) {
			System.out.println("unable to run: " + this.method);
		}
	}
}
