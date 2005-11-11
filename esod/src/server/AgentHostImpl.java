package server;

import java.net.InetAddress;
import java.net.UnknownHostException;
import java.rmi.*;
import java.rmi.server.*;
import java.util.LinkedList;
import server.agent.Agent;
import server.repository.AgentReport;

public class AgentHostImpl extends UnicastRemoteObject implements AgentHost {
	private static final long serialVersionUID = 3257001064375988534L;

	private LinkedList agentList;

	/**
	 * class constructor
	 * 
	 * @throws RemoteException
	 */
	public AgentHostImpl() throws RemoteException {
		super();
		agentList = new LinkedList();
	}

	/**
	 * accepts an agent in the host
	 * 
	 * @throws RemoteException
	 * @param agent
	 *            agent to be accepted
	 */
	public void accept(Agent agent) throws RemoteException {
		System.out.println("> accepting agent '" + agent.getID() + "'");
		agent.init(this);

		synchronized (agentList) {
			if (!agentList.contains(agent)) {
				agentList.addLast(agent);
				agentList.notify();
			}
		}

		new AgentRunner(agent).start();
	}

	/**
	 * 
	 * @throws RemoteException
	 */
	public void exec() throws RemoteException {
		// void
	}

	/**
	 * moves an agent to the next host
	 * 
	 * @throws RemoteException
	 * @param agent
	 *            agent to be moved
	 */
	public void moveTo(Agent agent) throws RemoteException {
		AgentHost host = null;

		String newHost = agent.getNewHost();
		if (newHost == null) {
			agent.finish();
			return;
		}

		try {
			host = (AgentHost) Naming.lookup("//" + newHost + "/"
					+ AgentHost.class.getName());
			host.accept(agent);
		} catch (Exception e) {
			// TODO: report our failure
			System.out.println("unable to move to host " + newHost);
			agent.getMediator().skipActionList(agent);
			moveTo(agent); // try next host
		}
	}

	/**
	 * removes the agent from the host and sends it to the next node
	 * 
	 * @param agent
	 *            agent to be removed
	 * 
	 */
	public void remove(Agent agent) {
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

	/**
	 * gets the name of the current host
	 * 
	 * @throws RemoteException
	 * @return current hostName
	 */
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

	/**
	 * sends a readable AgentReport object to the standard output
	 * 
	 * @param host
	 *            report to print
	 */
	public void reportBack(AgentReport report) throws RemoteException {
		report.printReport();
	}
}
