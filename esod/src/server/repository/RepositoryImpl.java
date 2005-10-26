package server.repository;

import java.rmi.server.UnicastRemoteObject;
import java.rmi.*;
import java.util.*;


public class RepositoryImpl extends UnicastRemoteObject implements Repository {

	private Hashtable table;
	
	public RepositoryImpl() throws RemoteException {
		table = new Hashtable(50);
	}
	
	
	
	public int setInfo(String agentID, String agentHostID, Object action, String output) {
		
		AgentData data = new AgentData(agentID, agentHostID, action, output);
		this.table.put(agentID.concat(agentHostID), data);
		
		return 0;
	}

	public AgentData readInfo(String agentID, String agentHostID) {

		return (AgentData)table.get(agentID.concat(agentHostID));
	}

}
