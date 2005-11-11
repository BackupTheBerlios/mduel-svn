package server.action;

import java.rmi.server.RMIClassLoader;

import server.agent.Agent;
import server.tasks.Task;

public class RemoteCodeAction extends BaseAction {
	private static final long serialVersionUID = 6303424517904872501L;

	private String uri;

	private String task;

	/**
	 * class constructor
	 * 
	 * @param uri
	 *            from where to load the class
	 * @param task
	 *            what task to load
	 * @param trace
	 *            indicates if the action is meant to be traced
	 */
	public RemoteCodeAction(String uri, String task, boolean trace) {
		super(trace);
		this.uri = uri;
		this.task = task;
	}

	/**
	 * executes remote code
	 * 
	 * @param agent
	 *            agent to execute the action
	 */
	public Object run(Agent agent) {
		try {
			Task t = (Task) RMIClassLoader.loadClass(
					"http://" + this.uri + ":2005/", task).newInstance();
			return t.run(agent);
		} catch (Exception e) {
			System.out.println("unable to run: " + this.task + " from "
					+ this.uri);
		}
		return "unable to run remote code!";
	}
}
