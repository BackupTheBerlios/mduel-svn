package server;

import java.net.InetAddress;
import java.net.UnknownHostException;
import java.rmi.*;
import java.rmi.server.*;
import java.util.LinkedList;
import server.agent.Agent;

public class AgentHostImpl extends UnicastRemoteObject implements AgentHost  {
	private static final long serialVersionUID = 3257001064375988534L;

	private LinkedList agentList;

	public AgentHostImpl() throws RemoteException {
		super();
		agentList = new LinkedList();
	}

	public void accept(Agent agent) throws RemoteException {
		System.out.println("> accepting agent '" + agent.getID() + "'");

		agent.init(this);

		synchronized(agentList) {
			if (!agentList.contains(agent)) {
				agentList.addLast(agent);
				agentList.notify();
			}
		}
		
		new AgentRunner(agent).start();
	}
	
	public void exec() throws RemoteException {
		// void
	}

	public void moveTo(Agent agent) throws RemoteException {
		AgentHost host = null;

		String newHost = agent.getNewHost();
		if (newHost == null) {
			agent.finish();
			return;
		}

		try {
			host = (AgentHost) Naming.lookup("//" + newHost + "/" + AgentHost.class.getName());
			host.accept(agent);
		} catch (Exception e) {
			e.printStackTrace();
		}
 	}
	
	public void kill(Agent agent) {
		synchronized (agentList) {
			agentList.remove(agent);
			agentList.notify();
		}
		
		try {
			moveTo(agent);
		} catch (Exception ex) {
			ex.printStackTrace();
		}
	}

	public String getHostname() throws RemoteException {
		String hostname = null;

		try {
			InetAddress addr = InetAddress.getLocalHost();
			hostname = addr.getHostName();
		} catch (UnknownHostException e) {
			e.printStackTrace();
		}
		
		return hostname;
	}
}
