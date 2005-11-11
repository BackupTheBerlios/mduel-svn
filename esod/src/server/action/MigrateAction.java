package server.action;

import server.agent.Agent;

public class MigrateAction extends BaseAction {
	private static final long serialVersionUID = -5809777582121076251L;

	private String hostname;

	/**
	 * class constructor
	 * 
	 * @param newHostname
	 *            the hostname to migrate
	 * @param trace
	 *            indicates if the action is meant to be traced
	 */
	public MigrateAction(String newHostname, boolean trace) {
		super(trace);
		this.hostname = newHostname;
	}

	/**
	 * deprecated for the time beeing
	 */
	public Object run(Agent agent) {
		/*
		 * try { agent.getHost().moveTo(agent); } catch (RemoteException e) {
		 * e.printStackTrace(); }
		 */
		return null;
	}
}
