package server.agent;

import server.action.Action;
import java.io.Serializable;
import java.util.LinkedList;

public class TaskList implements Serializable {
	private static final long serialVersionUID = 6792247681750179948L;

	private String nextHost;
	private LinkedList tasks;

	public TaskList(String host) {
		nextHost = host;
		tasks = new LinkedList();
	}
	
	public void addTask(Action action) {
		tasks.add(action);
	}

	public Action getNextAction() {
		return (Action) tasks.removeFirst();
	}

	public String getHost() {
		return nextHost;
	}
}
