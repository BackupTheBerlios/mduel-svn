package server.action;

import server.agent.Agent;

public class SleepAction implements Action {
	private static final long serialVersionUID = 3547962867277455423L;
	private long milliseconds;
	
	public SleepAction(long ms) {
		this.milliseconds = ms;
	}

	public void run(Agent agent) {
		System.out.println("sleeping for a while...");
		try {
			Thread.sleep(this.milliseconds);
		} catch (InterruptedException e) {
			e.printStackTrace();
		}
	}
}
