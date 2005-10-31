package server.agent;

import java.rmi.RemoteException;
import java.rmi.server.UnicastRemoteObject;
import server.AgentHost;

public abstract class AgentFactory extends UnicastRemoteObject implements RemoteFactory {
	public AgentFactory() throws RemoteException {
		super();
	}

	public abstract Agent create(AgentHost host, String scriptFile); 
}
