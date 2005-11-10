package server.repository;

import java.io.Serializable;
import java.util.Iterator;
import java.util.LinkedList;

public class HostReport implements Serializable {
	private static final long serialVersionUID = 9159528855848695637L;

	private String host;
	private LinkedList tasks;

	/**
	 * class constructor 
	 *
	 */
	public HostReport() {
		this.tasks = new LinkedList();
	}
	
	/**
	 * class constructor
	 * @param host		string containing the host
	 * 					to report
	 */
	public HostReport(String host) {
		this.host = host;
		this.tasks = new LinkedList();
	}
	
	/**
	 * sets a new list of reported tasks
	 * 
	 * @param tasks		list containing the tasks
	 */
	public void setTasks(LinkedList tasks) {
		this.tasks = tasks;
	}
	
	/**
	 * adds a 
	 * @param task
	 */
	public void setTask(TaskReport task) {
		tasks.add(task);
	}
	
	/**
	 * 
	 * @return
	 */
	public String getHost() {
		return host;
	}
	
	/**
	 * 
	 * @return
	 */
	public LinkedList getTasks() {
		return tasks;
	}
	
	/**
	 * 
	 *
	 */
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
