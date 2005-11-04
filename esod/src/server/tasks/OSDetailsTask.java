package server.tasks;

import java.util.Properties;
import server.agent.Agent;

public class OSDetailsTask implements Task {
	private static final long serialVersionUID = 6490718136536673610L;

	public void run(Agent agent) {
		Properties properties = new Properties();
		 
		 properties.setProperty("OS Name", System.getProperties().getProperty("os.name"));
		 properties.setProperty("OS Architecture", System.getProperties().getProperty("os.arch"));
		 properties.setProperty("OS Version", System.getProperties().getProperty("os.version"));
		 System.out.println(properties);
		 // agent.getReport().addProperties(properties);
	}

}
