package server.action;

import java.rmi.RemoteException;
import java.util.LinkedList;

import server.agent.Agent;
import server.repository.HostReport;

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
			 * testar esta acção... ir buscar ao repositório ou enviar do agente para casa...
			 * a ultima parece-me melhor
			 * 
			 */
			
		}
		catch (Exception e) {
			e.getMessage();
		}
		return null;
	}

}
