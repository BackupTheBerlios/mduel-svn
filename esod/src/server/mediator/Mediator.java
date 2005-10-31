package server.mediator;

import server.action.Action;
import server.agent.Agent;
import java.rmi.*;
import java.util.LinkedList;

public interface Mediator extends Remote {
	public void run() throws RemoteException;
	public void registerAgent(Agent agent) throws RemoteException;
	public void unregisterAgent(Agent agent) throws RemoteException;
	public Agent findAgent(Object agentID) throws RemoteException;
	public void insertAction(Object agentID, Action action) throws RemoteException;
	public Action getNextAction(Agent agent) throws RemoteException;
	public LinkedList getActionList(Agent agent) throws RemoteException;
}
