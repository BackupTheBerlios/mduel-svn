package server.mediator;

import java.util.*;

public class AgentInfo {
	
	private Object agentID;
	private LinkedList pathCompleted;
	private LinkedList taskList;
	private boolean runComplete;
	
	public AgentInfo(Object agentID, LinkedList tasks)
	{
		this.agentID = agentID;
		this.taskList = tasks;
		this.pathCompleted = new LinkedList();
		this.runComplete = false;
	}
	
	public Object getID(){
		return agentID;
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

