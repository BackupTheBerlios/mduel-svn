package server.action;

import java.util.*;

import server.agent.Agent;

public class LeafAction implements Action {
	private LinkedList actions;

	public LeafAction() {
		actions = new LinkedList();
	}
	
	public boolean isLeaf() {
		return true;
	}

	public void appendAction(Action action) {
		// ??
	}

	public Object[] getActions() {
		return actions.toArray();
	}

	public void addAction(Action action) {
		actions.add(actions.size()-1, action);
	}
	
	public void removeAction(Action action) {
		actions.remove(action);
	}

	public void setParams(String[] params) {
		// TODO handle action parameters, eg: migrate PARAM1 PARAM2
	}

	public void run(Agent agent) {

	}

}
