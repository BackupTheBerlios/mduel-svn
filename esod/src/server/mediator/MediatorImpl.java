package server.mediator;

import server.agent.Agent;

import java.rmi.RemoteException;
import java.rmi.server.UnicastRemoteObject;
import java.rmi.*;
import java.util.*;

public class MediatorImpl extends UnicastRemoteObject implements Mediator {

	private Hashtable table;

	
	public MediatorImpl() throws RemoteException
	{
		table = new Hashtable();
	}
	
	public void registerAgent(Agent agent) {
		AgentInfo ai = new AgentInfo(agent.getID(), null, null);
		table.put(ai.getID(), ai);
	}

	public void unregisterAgent(Agent agent) {
		agent.stop();
		table.remove(agent.getID());
	}

	public Object findAgent(Object agentID) throws RemoteException {
		
		Agent a = null;
		try {
			a = (Agent)Naming.lookup((String)agentID);
		} catch (Exception e) {
			e.getClass();
			e.getMessage();
		}
			
		return a;
	}
	
	public void insertTask(Object agentID, Object task) {
		//TODO
		/*
		 * não sei bem como funciona a inserção
		 * de novas tarefas na arvore...
		 */
	}
}
