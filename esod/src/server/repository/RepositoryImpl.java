package server.repository;

import java.io.Serializable;
import java.rmi.*;
import java.rmi.activation.*;
import java.rmi.RemoteException;
import java.util.Collection;
import java.util.Hashtable;
import java.util.Iterator;
import java.util.LinkedList;
import server.AgentHost;

public class RepositoryImpl extends Activatable implements Repository,
		Serializable {
	private static final long serialVersionUID = 6893905241391990022L;

	private Hashtable table;

	/**
	 * class constructor 
	 * 
	 * @throws RemoteException
	 */
	public RepositoryImpl(ActivationID id, MarshalledObject data) throws RemoteException {
		super(id, 0);
		table = new Hashtable(50);
	}

	/**
	 * 
	 * @param agentID	the string that identifies the agent
	 * @param report	the report of the agent in the last host
	 */
	public void setHostReport(String agentID, HostReport report)
			throws RemoteException {

		if (table.containsKey(agentID))
			((AgentReport) table.get(agentID)).setHostReport(report);

		else {
			AgentReport agentReport = new AgentReport(agentID);
			agentReport.setHostReport(report);
			table.put(agentID, agentReport);
		}

		System.out.println("> new report added.");
	}

	/**
	 * returns the last report sent by the agent
	 * 
	 * @param agentID	the string that identifies the agent
	 * @return			the last report of the specified agent
	 */
	public HostReport getLastReport(String agentID) throws RemoteException {
		return ((AgentReport) table.get(agentID)).getLastReport();
	}

	/**
	 * returns the full report of an agent
	 * 
	 * @param agentID	the string that identifies the agent
	 * @return			the full report of the specified agent
	 */
	public AgentReport getFinalReport(Object agentID) throws RemoteException {

		return (AgentReport) table.get(agentID);
	}

	/** 
	 * preforms a callback to the agent home to send is report
	 * 
	 * @param agentID	the string that identifies the agent
	 * @param home		agent home to preform a callback
	 */
	public void reportHome(String agentID, AgentHost home)
			throws RemoteException {
		home.reportBack((AgentReport) table.get(agentID));
	}

	public void reportLastHome(String agentID, AgentHost home)
			throws RemoteException {
		home.reportBackLast( (HostReport) ((AgentReport)table.get(agentID)).getLastReport());
	}
	
	/**
	 * 
	 *
	 */
	public void run() {
	}

	/**
	 * preforms a search in the table, returning a list
	 * with the agentID's found
	 * 
	 * @return			a list with agentID's stored
	 * 					in the hashtable (table)
	 */
	public LinkedList getInfo() throws RemoteException {

		Collection e = table.values();
		Iterator i = e.iterator();
		LinkedList result = new LinkedList();

		while (i.hasNext())
			result.add(((AgentReport) i.next()).getID());
		return result;
	}

}
