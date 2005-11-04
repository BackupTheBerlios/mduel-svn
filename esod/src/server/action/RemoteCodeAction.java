package server.action;

import java.net.URL;
import java.net.URLClassLoader;

import server.agent.Agent;
import server.tasks.Task;

public class RemoteCodeAction implements Action {
	private static final long serialVersionUID = 6303424517904872501L;

	private String uri;
	private String task;

	public RemoteCodeAction(String uri, String task) {
		this.uri = uri;
		this.task = task;
	}

	public void run(Agent agent) {
		try {
			URL url = new URL("http://" + this.uri + ":2005/");
			URLClassLoader ucl = new URLClassLoader(new URL[] {url});
			Task t = (Task) ucl.loadClass(task).newInstance();
			t.run(agent);
		} catch (Exception e) {
			System.out.println("unable to run: " + this.task + " from " + this.uri);
		}
	}
}
