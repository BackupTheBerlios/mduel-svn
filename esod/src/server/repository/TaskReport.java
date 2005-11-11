package server.repository;

import java.io.Serializable;
import server.action.Action;

public class TaskReport implements Serializable {
	private static final long serialVersionUID = 2905688692294421035L;

	private Action task;

	private Object output;

	private Object timeStamp;

	/**
	 * class constructor
	 * 
	 * @param task			object containing a specified action
	 * @param output		the output produced by the action (task)
	 * @param timeStamp		timestamp in miliseconds of when the action
	 * 						was performed
	 */
	public TaskReport(Action task, Object output, Object timeStamp) {
		this.task = task;
		this.output = output;
		this.timeStamp = timeStamp;
	}

	/**
	 * sends a readable copy of the object to
	 * the standard output
	 */
	public String toString() {
		StringBuffer sb = new StringBuffer();
		sb.append("TASK: " + task + "\n");
		sb.append("OUTPUT: " + output + "\n");
		sb.append("TIMESTAMP: " + timeStamp + "\n");
		return sb.toString();
	}
}
