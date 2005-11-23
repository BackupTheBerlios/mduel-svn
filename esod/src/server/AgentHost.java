package server;

import java.io.Serializable;
import java.rmi.*;

import server.agent.Agent;
import server.repository.AgentReport;
import server.repository.HostReport;

public interface AgentHost extends Serializable, Remote {
	void accept(Agent agent) throws RemoteException;

	void remove(Agent agent) throws RemoteException;

	void moveTo(Agent agent) throws RemoteException;

	String getHostname() throws RemoteException;

	public void reportBack(AgentReport report) throws RemoteException;
	
	public void reportBackLast(HostReport report) throws RemoteException;
}
