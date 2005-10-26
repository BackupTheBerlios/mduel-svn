package server.mediator;

import server.agent.Agent;
import java.rmi.*;

public interface Mediator extends Remote {
	public void registerAgent(Agent agent) throws RemoteException;
	public void unregisterAgent(Agent agent) throws RemoteException;
	public Object findAgent(Object agentID) throws RemoteException;
	public void insertTask(Object agentID, Object task) throws RemoteException;
	
}
