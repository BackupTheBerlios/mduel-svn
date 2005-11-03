package server.repository;

import java.util.Iterator;
import java.util.LinkedList;

import server.AgentHost;

public class AgentReport {

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
	
}
