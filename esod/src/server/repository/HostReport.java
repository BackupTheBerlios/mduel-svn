package server.repository;

import java.io.Serializable;
import java.util.Iterator;
import java.util.LinkedList;

import server.AgentHost;

public class HostReport implements Serializable {
	
	private String host;
	private LinkedList tasks;

	public HostReport() {
		this.tasks = new LinkedList();
	}
	
	public HostReport(String host) {
		this.host = host;
		this.tasks = new LinkedList();
	}
	
	public void setTasks(LinkedList tasks) {
		this.tasks = tasks;
	}
	
	public void setTask(TaskReport task) {
		tasks.add(task);
	}
	
	public String getHost() {
		return host;
	}
	
	public LinkedList getTasks() {
		return tasks;
	}
	
	public void printReport() {
		
		Iterator i = tasks.listIterator();
		TaskReport tmpTask;
		System.out.println("HOST: " + host);
		while(i.hasNext()) {
			tmpTask = (TaskReport)i.next();
			tmpTask.PrintTask();
		}
	}
}
