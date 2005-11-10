package server.action;

import server.agent.Agent;

public class ReportFinalAction extends BaseAction {
	private static final long serialVersionUID = 925984428282319399L;
	private String reportHost;
	
	public ReportFinalAction(String host, boolean trace) {
		super(trace);
		reportHost = host;
	}
	
	public Object run(Agent agent) {
		try {
			//agent.packReport();

			//agent.getRepository().setHostReport(agent.getID(), agent.getReport());
			/*
			 * A funcionalidade deste método é implementada
			 * pela acção OutputAction 
			 */
		}
		catch (Exception e) {
			e.getMessage();
		}

		return "dummy action";
	}

}
