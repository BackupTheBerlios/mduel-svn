package server.mediator;

import server.action.Action;
import server.agent.Agent;
import server.agent.AgentFactory;
import java.rmi.*;
import java.util.LinkedList;

public interface Mediator extends Remote {
	public void run() throws RemoteException;
	public void registerAgent(Agent agent, AgentInfo info) throws RemoteException;
	public void unregisterAgent(Agent agent) throws RemoteException;
	public AgentFactory getAgentFactory() throws RemoteException;
	public Agent findAgent(Object agentID) throws RemoteException;
	public void insertAction(Object agentID, Action action) throws RemoteException;
	public Action getNextAction(Agent agent) throws RemoteException;
	public void transferActions(Agent dest, Agent orig) throws RemoteException;
	public LinkedList getActionList(Agent agent) throws RemoteException;
	public void interrupt(String agentID, Action action) throws RemoteException;
	public void skipActionList(Agent agent) throws RemoteException;
}
