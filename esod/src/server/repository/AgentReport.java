package server.repository;

import java.io.Serializable;
import java.util.Iterator;
import java.util.LinkedList;

import server.AgentHost;

public class AgentReport implements Serializable {
	private static final long serialVersionUID = 3065071319622682342L;
	private Object agentID;
	private LinkedList tasks;
	
	public AgentReport(Object agentID) {
		this.agentID = agentID;
		this.tasks = new LinkedList();
	}
	
	public void setHostReport(HostReport report) {
		tasks.add(report);
	}
	
	public LinkedList getHostReport() {
		return tasks;
	}
	
	public int findHostReport(AgentHost host) {
	
		Iterator i = tasks.listIterator();
		int pos = 0;
		
		while (i.hasNext()) {
			HostReport tmp = (HostReport)i.next();
			if (tmp.getHost().equals(host))
				return pos;
			pos++;
		}
		return -1;
	}
	
	public void printReport() {
		
		Iterator i = tasks.listIterator();
				
		System.out.println("AGENT-ID: " + agentID.toString());
		while (i.hasNext()) {
			HostReport tmp = (HostReport)i.next();
			tmp.printReport();
		}
	}
	
}
