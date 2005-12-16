package server.repository;

import java.rmi.Remote;
import java.rmi.RemoteException;
import java.util.LinkedList;

import client.FrontEnd;
import server.AgentHost;

public interface Repository extends Remote {

	public void registerFrontEnd(FrontEnd fe) throws RemoteException;
	
	public void unregisterFrontEnd(FrontEnd fe) throws RemoteException;

	public void setHostReport(String agentID, HostReport report)
			throws RemoteException;

	public AgentReport getFinalReport(Object agentID) throws RemoteException;

	public HostReport getLastReport(String agentID) throws RemoteException;

	public void reportHome(String agentID, AgentHost home)
			throws RemoteException;

	public void reportLastHome(String agentID, AgentHost home) throws RemoteException;
	
	public LinkedList getInfo() throws RemoteException;
	
	public void publishReport(String agentID) throws RemoteException;
}
