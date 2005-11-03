package server.repository;

import server.action.Action;

public class TaskReport {

	private Action task;
	private Object output;
	private Object timeStamp;
	
	public TaskReport(Action task, Object output, Object timeStamp) {
		this.task = task;
		this.output = output;
		this.timeStamp = timeStamp;
	}
}
