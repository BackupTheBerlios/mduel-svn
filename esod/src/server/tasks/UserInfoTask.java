package server.tasks;

import java.util.Properties;
import server.agent.Agent;

public class UserInfoTask implements Task {
	private static final long serialVersionUID = 951845533930705167L;

	public void run(Agent agent) {
		Properties properties = new Properties();

		properties.setProperty("User's account name", System.getProperties()
				.getProperty("user.name"));
		properties.setProperty("User's home directory", System.getProperties()
				.getProperty("user.home"));
		properties.setProperty("User's current working directory", System
				.getProperties().getProperty("user.dir"));
		System.out.println(properties);
		// agent.getReport().addProperties(properties);
	}

}
