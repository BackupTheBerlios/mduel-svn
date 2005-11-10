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
	
	
	public void PrintTask() {
		System.out.println("TASK: " + task);
		System.out.println("OUTPUT: " + output);
		System.out.println("TIMESTAMP: " + timeStamp);
	}
}
