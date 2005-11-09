package server.action;

import server.agent.Agent;

public class OutputAction extends BaseAction {
	private static final long serialVersionUID = 2378192140024718644L;
	
	public OutputAction(boolean trace) {
		super(trace);
	}

	public Object run(Agent agent) {
		
		try {
			agent.getRepository().reportHome(agent, agent.getHome());
		} catch (Exception e) {
			e.getMessage();
		}
		return null;
	}

}
