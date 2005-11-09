package server.repository;

import java.io.Serializable;
import java.rmi.server.UnicastRemoteObject;
import java.rmi.*;
import java.util.*;

import server.AgentHost;
import server.action.Action;
import server.agent.Agent;


public class RepositoryImpl extends UnicastRemoteObject implements Repository, Serializable {
	private static final long serialVersionUID = 6893905241391990022L;
	private Hashtable table;
	
	public RepositoryImpl() throws RemoteException {
		table = new Hashtable(50);
	}

	/*
	public void setActionReport(Object agentID, Action task) throws RemoteException {
		// TODO Auto-generated method stub
	}
	*/

	public void setHostReport(String agentID, HostReport report) throws RemoteException {
		
		if (table.containsKey(agentID))
			((AgentReport)table.get(agentID)).setHostReport(report);
		
		else {
			AgentReport agentReport = new AgentReport(agentID);
			agentReport.setHostReport(report);
			table.put(agentID, agentReport);
		}
		
		System.out.println("> new report added.");
	}
	
	public HostReport getLastReport(String agentID) throws RemoteException {
		return ((AgentReport)table.get(agentID)).getLastReport();
	}
	
	public AgentReport getFinalReport(Object agentID) throws RemoteException {
		
		return (AgentReport)table.get(agentID);
	}
	
	public void reportHome(Agent agent, AgentHost home) throws RemoteException {
		home.reportBack( (AgentReport)table.get(agent.getID()));
	}

	public void run() {	}
	
}
