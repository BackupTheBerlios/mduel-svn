package server;

import java.rmi.*;

import server.action.Action;
import server.agent.Agent;

public interface AgentHost extends Remote {
	void accept(Agent agent) throws RemoteException;
	void moveTo(Agent agent, String newHost) throws RemoteException;
	String getHostname() throws RemoteException;
}
