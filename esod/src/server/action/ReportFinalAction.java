package server.action;

import server.agent.Agent;

public class ReportFinalAction extends BaseAction {
	private static final long serialVersionUID = 925984428282319399L;
	private String reportHost;
	
	/**
	 * class constructor
	 * 
	 * @param host			where to report
	 * @param trace			regists if the action was sucessful
	 */
	public ReportFinalAction(String host, boolean trace) {
		super(trace);
		reportHost = host;
	}
	
	/**
	 * this method was replaced by the funcionality
	 * of the OutputAction class
	 * 
	 */
	public Object run(Agent agent) {
		try {

		}
		catch (Exception e) {
			e.getMessage();
		}

		return "dummy action";
	}

}
