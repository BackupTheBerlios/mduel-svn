package server.action;

import java.util.Properties;

import server.agent.Agent;

public class OSDetailsAction implements Action {

	public void run(Agent agent) {
		Properties properties = new Properties();
		 
		 properties.setProperty("OS Name", System.getProperties().getProperty("os.name"));
		 properties.setProperty("OS Architecture", System.getProperties().getProperty("os.arch"));
		 properties.setProperty("OS Version", System.getProperties().getProperty("os.version"));
		 //agent.getReport().addProperties(properties);
	}

}
