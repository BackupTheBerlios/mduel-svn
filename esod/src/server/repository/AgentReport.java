package server.repository;

import java.io.Serializable;
import java.util.Iterator;
import java.util.LinkedList;

import server.AgentHost;

public class AgentReport implements Serializable {
	private static final long serialVersionUID = 3065071319622682342L;

	private Object agentID;

	private LinkedList tasks;

	/**
	 * class constructor
	 * @param agentID	the string that identifies the agent
	 */
	public AgentReport(Object agentID) {
		this.agentID = agentID;
		this.tasks = new LinkedList();
	}

	/**
	 * adds a new hostReport to the list
	 * 
	 * @param report	contains the report
	 * 					produced by the agent
	 */
	public void setHostReport(HostReport report) {
		tasks.add(report);
	}

	/**
	 * returns the list of reports produced by the agent
	 * 
	 * @return			the list of reports of the agent
	 */
	public LinkedList getHostReport() {
		return tasks;
	}

	/**
	 * returns the last report produced by the agent
	 * 
	 * @return			last agent host report
	 */
	public HostReport getLastReport() {
		return (HostReport) tasks.getLast();
	}

	/**
	 * returns the agent identifier
	 * 
	 * @return			a string identifying the agent
	 */
	public String getID() {
		return (String) agentID;
	}

	/**
	 * finds a particular report from a specified host
	 * in the report list. Returns -1 if no report is 
	 * found.
	 * 
	 * @param host		report to be found
	 * @return			the position of the report
	 * 					in the list 
	 */
	public int findHostReport(AgentHost host) {

		Iterator i = tasks.listIterator();
		int pos = 0;

		while (i.hasNext()) {
			HostReport tmp = (HostReport) i.next();
			if (tmp.getHost().equals(host))
				return pos;
			pos++;
		}
		return -1;
	}

	/**
	 *
	 *
	 */
	public void printReport() {

		Iterator i = tasks.listIterator();

		System.out.println("AGENT-ID: " + agentID.toString());
		while (i.hasNext()) {
			HostReport tmp = (HostReport) i.next();
			System.out.println(tmp);
		}
	}

}
