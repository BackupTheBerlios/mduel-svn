package server.action;

import server.agent.Agent;

public class MigrateAction extends BaseAction {
	private static final long serialVersionUID = -5809777582121076251L;

	private String hostname;

	/**
	 * class constructor
	 * 
	 * @param newHostname
	 * @param trace
	 *            regists if the action was sucessful
	 */
	public MigrateAction(String newHostname, boolean trace) {
		super(trace);
		this.hostname = newHostname;
	}

	/**
	 * 
	 */
	public Object run(Agent agent) {
		/*
		 * try { agent.getHost().moveTo(agent); } catch (RemoteException e) {
		 * e.printStackTrace(); }
		 */
		return null;
	}
}
