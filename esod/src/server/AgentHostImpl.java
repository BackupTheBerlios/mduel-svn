package server;

import java.net.InetAddress;
import java.net.MalformedURLException;
import java.net.UnknownHostException;
import java.rmi.*;
import java.rmi.server.*;
import java.util.Hashtable;
import java.util.Stack;
import server.agent.Agent;

public class AgentHostImpl extends UnicastRemoteObject implements AgentHost  {
	private static final long serialVersionUID = 3257001064375988534L;

	private Stack todo = new Stack();
	private Hashtable threadPool = new Hashtable();
	private volatile Thread agentThread;

	public AgentHostImpl() throws RemoteException {
		super();
	}

	public synchronized void accept(Agent agent) throws RemoteException {
		System.out.println("> accepting agent '" + agent.getID() + "'");
		todo.push(agent);
	}
	
	public synchronized void exec() {
		if (todo.empty())
			return;

		Agent agent = (Agent) todo.pop();
		agentThread = new Thread((Runnable) agent);
		threadPool.put(agent.getID(), agentThread);
		agentThread.setDaemon(true);
		agentThread.setPriority(1);
		agentThread.start();
	}

	public void moveTo(Agent agent, String newHost) throws RemoteException {
		AgentHost host = null;

		try {
			host = (AgentHost) Naming.lookup("//" + newHost + "/" + AgentHost.class.getName());
		} catch (MalformedURLException e) {
			e.printStackTrace();
		} catch (RemoteException e) {
			e.printStackTrace();
		} catch (NotBoundException e) {
			e.printStackTrace();
		}

		agentThread = (Thread)threadPool.get(agent.getID());
		if (agentThread != null) {
			Thread.yield();
			agentThread.interrupt();
			agentThread = null;
			threadPool.remove(agent.getID());
			host.accept(agent);
		}
 	}
	
	public synchronized void kill(Agent agent) {
		agentThread = (Thread)threadPool.get(agent.getID());
		if (agentThread != null) {
			Thread.yield();
			agentThread.interrupt();
			agentThread = null;
			threadPool.remove(agent.getID());
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
