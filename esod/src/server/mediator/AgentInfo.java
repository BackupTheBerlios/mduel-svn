package server.mediator;

import java.util.*;

public class AgentInfo {
	
	private Object agentID;
	private LinkedList pathCompleted;
	private Object taskTree;
	private Object currentTask;
	
	public AgentInfo(Object agentID, Object taskTree, Object currentTask)
	{
		this.agentID = agentID;
		this.taskTree = taskTree;
		this.pathCompleted = new LinkedList();
		this.currentTask = currentTask;
	}
	
	public Object getID(){
		return agentID;
	}
	
	public Object getTree(){
		return taskTree;
	}
	
	public Object getCurrentTask(){
		return currentTask;
	}
	
	public void setCurrentTask(Object newTask){
		this.currentTask = newTask;
	}
	
	public void setNewTree(Object newTree){
		this.taskTree = newTree;
	}
	
	public void linkCompleted(Object agentHostID){
		this.pathCompleted.add(agentHostID);
	}
}

