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

	public synchronized void accept(Agent agent) throws RemoteException {
		System.out.println("> accepting agent '" + agent.getID() + "'");
		agent.setHome(this);
		todo.push(agent);
	}
	
	public synchronized void exec() {
		if (todo.empty())
			return;

		System.out.println(todo);
		Agent agent = (Agent) todo.pop();
		//System.out.println(agent.getMediator().getAgentInfo(agent).getActionList(););
		Thread t = new Thread((Runnable) agent);
		threadPool.put(agent.getID(), t);
		t.setDaemon(true);
		t.setPriority(1);
		t.start();
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

		Thread t = (Thread)threadPool.get(agent.getID());
		if (t != null) {
			t.interrupt();
			threadPool.remove(agent.getID());
			host.accept(agent);
		}
 	}
	
	public synchronized void kill(Agent agent) {
		Thread t = (Thread)threadPool.get(agent.getID());
		if (t != null) {
			t.interrupt();
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
