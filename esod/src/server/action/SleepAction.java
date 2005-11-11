package server.action;

import server.agent.Agent;

public class SleepAction extends BaseAction {
	private static final long serialVersionUID = 3547962867277455423L;
	private long milliseconds;
	
	/**
	 * class constructor
	 * 
	 * @param ms				time in miliseconds
	 * @param trace				regists if the action was sucessful
	 */
	public SleepAction(long ms, boolean trace) {
		super(trace);
		this.milliseconds = ms;
	}

	/**
	 * puts the agent to sleep for a 
	 * specified period of time
	 * 
	 * @param agent				agent to preform the action
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
