package server.action;

import server.agent.Agent;
import java.lang.reflect.*;

public class MobileCodeAction extends BaseAction {
	private static final long serialVersionUID = 1698541644546154950L;

	private String method;
	private Object[] params = null;

	/**
	 * class constructor
	 * 
	 * @param m
	 *            name of the method to be executed
	 * @param trace
	 *            indicates if the action is meant to be traced
	 */
	public MobileCodeAction(String m, boolean trace) {
		super(trace);
		this.method = m;
	}
	
	public void setParams(Object[] p)
	{
		this.params = p;
	}

	/**
	 * executes the method specified by the private member m
	 * 
	 * @param agent
	 *            to execute que action
	 */
	public Object run(Agent agent) {
		int idx = this.method.lastIndexOf(".");
		this.method = this.method.substring(idx + 1);

		try {
			Class c = Class.forName(agent.getClass().getName());
			Method m = c.getMethod(this.method, null);
			return m.invoke(agent, this.params);
		} catch (Exception e) {
			System.out.println("unable to run: " + this.method);
		}

		return "unable to run mobile code!";
	}
}
