package server.repository;

import java.util.LinkedList;

import server.AgentHost;

public class HostReport {
	
	private AgentHost host;
	private LinkedList tasks;

	public HostReport(AgentHost host) {
		this.host = host;
		this.tasks = new LinkedList();
	}
	
	public void setTasks(LinkedList tasks) {
		this.tasks = tasks;
	}
	
	public AgentHost getHost() {
		return host;
	}
}
