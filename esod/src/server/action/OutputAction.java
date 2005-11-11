package server.action;

import server.agent.Agent;

public class OutputAction extends BaseAction {
	private static final long serialVersionUID = 2378192140024718644L;

	/**
	 * class constructor
	 * 
	 * @param trace				regists if the action was sucessful
	 */
	public OutputAction(boolean trace) {
		super(trace);
	}

	/**
	 * sends a copy of the agent report to the agent home
	 * 
	 * @param agent				agent to execute the action
	 */
	public Object run(Agent agent) {

		try {
			agent.getRepository().reportHome(agent.getID(), agent.getHome());
		} catch (Exception e) {
			e.getMessage();
		}
		return null;
	}

}
