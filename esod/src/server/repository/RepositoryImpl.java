package server.repository;

import java.rmi.server.UnicastRemoteObject;
import java.rmi.*;
import java.util.*;

import server.AgentHost;
import server.action.Action;


public class RepositoryImpl extends UnicastRemoteObject implements Repository {

	private Hashtable table;
	
	public RepositoryImpl() throws RemoteException {
		table = new Hashtable(50);
	}

	public void setActionReport(Object agentID, Action task) throws RemoteException {
		// TODO Auto-generated method stub
		
	}

	public void setHostReport(Object agentID, AgentHost host, LinkedList tasks) throws RemoteException {
		
		HostReport hReport = new HostReport(host);
		hReport.setTasks(tasks);
		
		if (table.containsKey(agentID))
			((AgentReport)table.get(agentID)).setHostReport(hReport);
		
		else {
			AgentReport report = new AgentReport(agentID);
			report.setHostReport(hReport);
			table.put(agentID, report);
		}
	}

	public LinkedList getHostReport(Object agentID, AgentHost host) throws RemoteException {
		
		int pos = ((AgentReport)table.get(agentID)).findHostReport(host);
		
		if (pos != -1)
			return (LinkedList)((AgentReport)table.get(agentID)).getHostReport().get(pos);
		
		return null;
	}

	public AgentReport getFinalReport(Object agentID) throws RemoteException {
		
		return (AgentReport)table.get(agentID);
	}

	public void run() {
		// TODO Auto-generated method stub
		
	}
	
}
