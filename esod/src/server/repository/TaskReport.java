package server.repository;

import java.io.Serializable;
import server.action.Action;

public class TaskReport implements Serializable {
	private static final long serialVersionUID = 2905688692294421035L;
	private Action task;
	private Object output;
	private Object timeStamp;
	
	
	
	public TaskReport(Action task, Object output, Object timeStamp) {
		this.task = task;
		this.output = output;
		this.timeStamp = timeStamp;
	}
	
	public String toString() {
		StringBuffer sb = new StringBuffer();
		sb.append("TASK: " + task + "\n");
		sb.append("OUTPUT: " + output + "\n");
		sb.append("TIMESTAMP: " + timeStamp + "\n");
		return sb.toString();
	}
}
