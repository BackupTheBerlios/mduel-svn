package server;

import java.rmi.RemoteException;
import server.agent.Agent;

public class AgentRunner extends Thread {

	protected Agent agent;
	
	public AgentRunner(Agent agent) {
		this.agent = agent;
	}
	
	public void run() {
		try {
			agent.start();
		} catch (RemoteException e) {
			e.printStackTrace();
		}
	}
}
