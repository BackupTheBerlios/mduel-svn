package server.tasks;

import java.util.Properties;
import server.agent.Agent;

public class UserInfoTask implements Task {
	private static final long serialVersionUID = 951845533930705167L;

	/**
	 * sends user properties to the standard output
	 * user.name
	 * user.home
	 * user.dir
	 * 
	 * @return				an object with the printed values
	 */
	public Object run(Agent agent, Object[] params) {
		Properties properties = new Properties();

		properties.setProperty("User's account name", System.getProperties()
				.getProperty("user.name"));
		properties.setProperty("User's home directory", System.getProperties()
				.getProperty("user.home"));
		properties.setProperty("User's current working directory", System
				.getProperties().getProperty("user.dir"));
		return properties;
	}

}
