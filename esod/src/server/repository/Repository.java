package server.repository;

import java.rmi.Remote;
import java.rmi.RemoteException;
import java.util.LinkedList;

import server.AgentHost;
import server.action.Action;

public interface Repository extends Remote {
	
	
	public void setActionReport(Object agentID, Action task) throws RemoteException;
	public void setHostReport(Object agentID, AgentHost host, LinkedList tasks) throws RemoteException;

	public LinkedList getHostReport(Object agentID, AgentHost host) throws RemoteException;
	public AgentReport getFinalReport(Object agentID) throws RemoteException;


}
