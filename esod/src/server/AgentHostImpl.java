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

	public AgentHostImpl() throws RemoteException {
		super();
	}

	public void accept(Agent agent) throws RemoteException {
		System.out.println("> accepting agent '" + agent.getID() + "'");
		agent.setHome(this);
		System.out.println(agent.getScript().getActions());
		todo.push(agent);
	}
	
	public synchronized void exec() {
		if (todo.empty())
			return;

		Agent agent = (Agent) todo.pop();
		Thread t = new Thread((Runnable) agent);
		t.setDaemon(true);
		t.setPriority(1);
		t.start();
		threadPool.put(agent.getID(), t);
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

		host.accept(agent);
		Thread t = (Thread)threadPool.get(agent.getID());
		t.interrupt();
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
