package server.repository;

import java.io.Serializable;
import java.rmi.server.UnicastRemoteObject;
import java.rmi.*;
import java.util.*;

import server.action.Action;


public class RepositoryImpl extends UnicastRemoteObject implements Repository, Serializable {
	private static final long serialVersionUID = 6893905241391990022L;
	private Hashtable table;
	private HostReport lastReport;
	
	public RepositoryImpl() throws RemoteException {
		table = new Hashtable(50);
	}

	public void setActionReport(Object agentID, Action task) throws RemoteException {
		// TODO Auto-generated method stub
		
	}

	public void setHostReport(String agentID, HostReport report) throws RemoteException {
		
		lastReport = report;
		
		if (table.containsKey(agentID))
			((AgentReport)table.get(agentID)).setHostReport(report);
		
		else {
			AgentReport agentReport = new AgentReport(agentID);
			agentReport.setHostReport(report);
			table.put(agentID, agentReport);
		}
		
		// aki era enviada a resposta para o cliente...
		//como não estava a funcionar.. vai ter de ser feito de outra maneira
		
		System.out.println("> new report added.");
	}
	
	/*
	public int setHostReport(String agentID, String host, LinkedList tasks, Object home) throws RemoteException {
		
		lastReport = new HostReport(host);
		
		lastReport.setTasks(tasks);
		
		System.out.println("> at least i'm in.");
		
		if (table.containsKey(agentID))
			((AgentReport)table.get(agentID)).setHostReport(lastReport);
		
		else {
			AgentReport report = new AgentReport(agentID);
			report.setHostReport(lastReport);
			table.put(agentID, report);
		}
		
		try {
			System.out.println("> new task saved.");
			//TODO
			//mudar de home para AGENTHOST
			((Home)home).getReport(lastReport);
			//((Home)home).reportFinal(host, tasks);
		}	
		catch (Exception e) {
			e.getMessage();
		}
		return 1;
	}
	*/

	public HostReport getLastReport() throws RemoteException {
		return lastReport;
	}
	
	public AgentReport getFinalReport(Object agentID) throws RemoteException {
		
		return (AgentReport)table.get(agentID);
	}
	
	/*
	public LinkedList getHostReport(Object agentID, AgentHost host) throws RemoteException {
		
		int pos = ((AgentReport)table.get(agentID)).findHostReport(host);
		
		if (pos != -1)
			return (LinkedList)((AgentReport)table.get(agentID)).getHostReport().get(pos);
		
		return null;
	}
	*/
	

	public void run() {	}
	
}
