package server.agent;

import server.AgentHost;
import java.rmi.Remote;
import java.rmi.RemoteException;

public interface AgentFactory extends Remote {
	public Agent create(AgentHost host, String scriptFile) throws RemoteException;
}
