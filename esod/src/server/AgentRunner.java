package server;

import java.rmi.RemoteException;
import server.agent.Agent;

public class AgentRunner extends Thread {

	protected Agent agent;
	
	/**
	 * class constructor
	 * 
	 * @param agent
	 */
	public AgentRunner(Agent agent) {
		this.agent = agent;
	}
	
	/**
	 * starts the execution of an agent
	 */
	public void run() {
		try {
			agent.start();
		} catch (RemoteException e) {
			e.printStackTrace();
		}
	}
}
