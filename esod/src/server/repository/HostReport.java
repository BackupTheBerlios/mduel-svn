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
	 * adds a task report to the task list
	 * 
	 * @param task		the task to add
	 */
	public void setTask(TaskReport task) {
		tasks.add(task);
	}

	/**
	 * returns the name of the host
	 * 
	 * @return			string containing the
	 * 					name of the host
	 */
	public String getHost() {
		return host;
	}

	/**
	 * returns the list of tasks preformed
	 * 
	 * 
	 * @return			linkedList containing tasks
	 */
	public LinkedList getTasks() {
		return tasks;
	}

	/**
	 * sends a readable copy of the object to
	 * the standard output
	 */
	public String toString() {
		StringBuffer sb = new StringBuffer();
		Iterator i = tasks.listIterator();

		TaskReport tmpTask;
		sb.append("HOST: " + host + "\n");
		while (i.hasNext()) {
			tmpTask = (TaskReport) i.next();
			sb.append(tmpTask);
		}
		return sb.toString();
	}
	
	/**
	 * 
	 *
	 */
	public void printReport() {

		Iterator i = tasks.listIterator();

		System.out.println("AGENT-ID: " + host.toString());
		while (i.hasNext()) {
			TaskReport tmp = (TaskReport) i.next();
			System.out.println(tmp);
		}
	}
}
