package server;

import java.io.Serializable;
import java.rmi.*;

import server.agent.Agent;

public interface AgentHost extends Serializable, Remote {
	void accept(Agent agent) throws RemoteException;
	void kill(Agent agent) throws RemoteException;
	void moveTo(Agent agent) throws RemoteException;
	String getHostname() throws RemoteException;
}
