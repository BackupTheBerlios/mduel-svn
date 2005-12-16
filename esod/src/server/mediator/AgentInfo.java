package server.mediator;

import java.io.Serializable;
import java.util.*;

import server.locator.Proxy;

public class AgentInfo implements Serializable {
	private static final long serialVersionUID = 1L;

	private Object agentID;

	private LinkedList pathCompleted;

	private LinkedList taskList;

	private boolean runComplete;

	private int actionCounter;
	
	private Proxy fixedProxy;
	
	private Proxy localProxy;


	
	/**
	 * class constructor
	 * 
	 * @param agentID			agent identifier
	 * @param tasks				list of tasks to execute
	 */
	public AgentInfo(Object agentID, LinkedList tasks) {
		this.agentID = agentID;
		this.taskList = tasks;
		this.pathCompleted = new LinkedList();
		this.runComplete = false;
		this.actionCounter = 0;
	}

	/**
	 * returns the agent identifier
	 * 
	 * @return				object containing the agentID
	 */
	public Object getID() {
		return agentID;
	}

	/**
	 * sets the action counter to a specific value given
	 * by the variable counter
	 * 
	 * @param counter		value to set the action counter
	 */
	public void setAction(int counter) {
		this.actionCounter = counter;
	}

	/**
	 * returns the value of the private member
	 * actionCounter
	 * 
	 * @return				int containing the actionCounter
	 */
	public int getNextAction() {
		return actionCounter;
	}

	/**
	 * returns the LinkedList of tasks
	 * 
	 * @return				the list of tasks
	 */
	public LinkedList getActionList() {
		return taskList;
	}

	/**
	 * sets the private member taskList to
	 * the argument object list
	 * 
	 * @param list			value to be set
	 */
	public void setActionList(LinkedList list) {
		this.taskList = list;
	}

	/**
	 * marks a specific host as being visited
	 * 
	 * @param agentHostID	visited host
	 */
	public void linkCompleted(Object agentHostID) {
		this.pathCompleted.add(agentHostID);
	}

	/**
	 * verifies if the agent as preformed all
	 * of is tasks
	 * 
	 * @return				false if the agent as more actions,
	 * 						true otherwise.
	 */
	public boolean isRunComplete() {
		return runComplete;
	}

	/**
	 * affects the member runComplete with
	 * the boolean variable b. Used to mark the
	 * agent execution as being over.
	 * 
	 * @param b				variable to set
	 */
	public void setRunComplete(boolean b) {
		runComplete = b;
	}
	
	public void setLocalProxy(Proxy p) {
		this.localProxy = p;
	}
	
	public void setFixedProxy(Proxy p) {
		this.fixedProxy = p;
	}
	
	public Proxy getLocalProxy() {
		return localProxy;
	}
	
	public Proxy getFixedProxy() {
		return fixedProxy;
	}
}
