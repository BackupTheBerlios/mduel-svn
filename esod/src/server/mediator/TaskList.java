package server.mediator;

import server.action.Action;
import java.io.Serializable;
import java.util.LinkedList;

public class TaskList implements Serializable {
	private static final long serialVersionUID = 6792247681750179948L;

	private String nextHost;
	private LinkedList tasks;

	/**
	 * class constructor
	 * 
	 * @param host				specifies where the tasks are to be executed
	 */
	public TaskList(String host) {
		nextHost = host;
		tasks = new LinkedList();
	}
	
	/**
	 * sets a new task
	 * 
	 * @param action			action to be set
	 */
	public void addTask(Action action) {
		tasks.add(action);
	}

	/**
	 * gets the next task
	 * 
	 * @return					next action
	 */
	public Action getNextAction() {
		return (Action) tasks.removeFirst();
	}

	/**
	 * gets the nextHost private member
	 * 
	 * @return					nextHost
	 */
	public String getHost() {
		return nextHost;
	}
	
	/**
	 * gets the tasks private member
	 * 
	 * @return					tasks
	 */
	public LinkedList getActions() {
		return tasks;
	}
}
