package server.repository;

import java.rmi.Remote;
import java.rmi.RemoteException;
import java.util.LinkedList;

import server.AgentHost;
import server.action.Action;
import server.agent.Agent;

public interface Repository extends Remote {
	
	
	//public void setActionReport(Object agentID, Action task) throws RemoteException;
	public void setHostReport(String agentID, HostReport report) throws RemoteException;
	public AgentReport getFinalReport(Object agentID) throws RemoteException;
	public HostReport getLastReport(String agentID) throws RemoteException;
	public void reportHome(Agent agent, AgentHost home) throws RemoteException;
	public LinkedList getInfo() throws RemoteException;
	//public LinkedList getHostReport(Object agentID, AgentHost host) throws RemoteException;

}
