package server.action;

import java.util.Properties;

import server.agent.Agent;

public class UserInfoAction implements Action {

	public void run(Agent agent) {
		Properties properties = new Properties();

		properties.setProperty("User's account name", System.getProperties().getProperty("user.name"));
		properties.setProperty("User's home directory", System.getProperties().getProperty("user.home"));
		properties.setProperty("User's current working directory", System.getProperties().getProperty("user.dir"));
		
		//agent.getReport().addProperties(properties);
	}

}
