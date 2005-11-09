package server.action;

import server.agent.Agent;

public class SleepAction extends BaseAction {
	private static final long serialVersionUID = 3547962867277455423L;
	private long milliseconds;
	
	public SleepAction(long ms, boolean trace) {
		super(trace);
		this.milliseconds = ms;
	}

	public Object run(Agent agent) {
		try {
			Thread.sleep(this.milliseconds);
		} catch (InterruptedException e) {
			e.printStackTrace();
		}
		return "sleeping for " + String.valueOf(milliseconds) + "ms...";
	}
}
