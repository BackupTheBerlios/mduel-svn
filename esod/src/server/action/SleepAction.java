package server.action;

import server.agent.Agent;

public class SleepAction extends BaseAction {
	private static final long serialVersionUID = 3547962867277455423L;

	private long milliseconds;
	
	private Object[] params = null;

	/**
	 * class constructor
	 * 
	 * @param ms
	 *            time in miliseconds
	 * @param trace
	 *            indicates if the action is meant to be traced
	 */
	public SleepAction(boolean trace) {
		super(trace);
	}

	public void setParams(Object[] p)
	{
		this.params = p;
		this.milliseconds = Integer.parseInt(this.params[0].toString());
	}

	/**
	 * puts the agent to sleep for a specified period of time
	 * 
	 * @param agent
	 *            agent to preform the action
	 */
	public Object run(Agent agent) {
		try {
			Thread.sleep(this.milliseconds);
		} catch (InterruptedException e) {
			e.printStackTrace();
		}
		return "sleeping for " + String.valueOf(milliseconds) + "ms...";
	}
}
