package server.repository;

import java.rmi.Remote;
import java.rmi.RemoteException;
import java.util.LinkedList;
import server.AgentHost;

public interface Repository extends Remote {

	public void setHostReport(String agentID, HostReport report)
			throws RemoteException;

	public AgentReport getFinalReport(Object agentID) throws RemoteException;

	public HostReport getLastReport(String agentID) throws RemoteException;

	public void reportHome(String agentID, AgentHost home)
			throws RemoteException;

	public LinkedList getInfo() throws RemoteException;
}
