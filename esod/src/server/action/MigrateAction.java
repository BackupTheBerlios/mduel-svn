package server.action;

import java.rmi.RemoteException;
import server.agent.Agent;

public class MigrateAction implements Action  {
	private static final long serialVersionUID = -5809777582121076251L;
	private String hostname;

	public MigrateAction(String newHostname) {
		this.hostname = newHostname;
	}
	
	public void run(Agent agent) {
		/*try {
			agent.getHome().moveTo(agent, hostname);
		} catch (RemoteException e) {
			e.printStackTrace();
		}*/
	}

}
