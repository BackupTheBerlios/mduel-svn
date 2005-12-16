package server.mediator;

import server.action.Action;
import server.locator.*;
import server.agent.Agent;
import server.agent.AgentFactory;
import java.rmi.*;
import java.util.LinkedList;

public interface Mediator extends Remote {
	public void run() throws RemoteException;

	//method 0
	public void registerAgent(Agent agent, AgentInfo info)
			throws RemoteException;

	//method 1
	public void unregisterAgent(Agent agent) throws RemoteException;

	public AgentFactory getAgentFactory() throws RemoteException;

	public Agent findAgent(Object agentID) throws RemoteException;

	//method 2
	public Action getNextAction(Agent agent) throws RemoteException;

	//method 3
	public void transferActions(Agent dest, Agent orig) throws RemoteException;

	//method 4
	public LinkedList getActionList(Agent agent) throws RemoteException;

	public void interrupt(String agentID, Action action) throws RemoteException;

	//method 5
	public void skipActionList(Agent agent) throws RemoteException;

	public LinkedList getInfo() throws RemoteException;
	
	public void setLocalProxy(String agentID, Proxy agentProxy) throws RemoteException;
	
	public void setFixedProxy(String agentID, Proxy agentProxy) throws RemoteException;
	
	public Proxy getLocalProxy(String agentID) throws RemoteException;
	
	public Proxy getFixedProxy(String agentID) throws RemoteException;
	
}
