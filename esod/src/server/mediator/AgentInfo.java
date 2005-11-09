package server.mediator;

import java.io.Serializable;
import java.util.*;

public class AgentInfo implements Serializable {
	private static final long serialVersionUID = 1L;
	private Object agentID;
	private LinkedList pathCompleted;
	private LinkedList taskList;
	private boolean runComplete;
	private int actionCounter;
	
	public AgentInfo(Object agentID, LinkedList tasks)
	{
		this.agentID = agentID;
		this.taskList = tasks;
		this.pathCompleted = new LinkedList();
		this.runComplete = false;
		this.actionCounter = 0;
	}
	
	public Object getID(){
		return agentID;
	}

	public void setAction(int counter) {
		this.actionCounter = counter;
	}
	
	public int getNextAction() {
		return actionCounter;
	}
	
	public LinkedList getActionList() {
		return taskList;
	}
	
	public void setActionList(LinkedList list) {
		this.taskList = list;
	}

	public void linkCompleted(Object agentHostID){
		this.pathCompleted.add(agentHostID);
	}
	
	public boolean isRunComplete() {
		return runComplete;
	}
	
	public void setRunComplete(boolean b) {
		runComplete = b;
	}
}

