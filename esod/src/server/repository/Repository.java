package server.repository;

import java.rmi.Remote;
import java.rmi.RemoteException;
import server.action.Action;

public interface Repository extends Remote {
	
	
	public void setActionReport(Object agentID, Action task) throws RemoteException;
	
	

	//public LinkedList getHostReport(Object agentID, AgentHost host) throws RemoteException;
	
	public void setHostReport(String agentID, HostReport report) throws RemoteException;
	public AgentReport getFinalReport(Object agentID) throws RemoteException;
	public HostReport getLastReport() throws RemoteException;


}
