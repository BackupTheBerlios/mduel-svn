package server.mediator;

import server.action.Action;
import server.agent.Agent;

import java.rmi.RemoteException;
import java.rmi.server.UnicastRemoteObject;
import java.rmi.*;
import java.util.*;

public class MediatorImpl extends UnicastRemoteObject implements Mediator {
	private static final long serialVersionUID = -5563370450807830696L;
	private Hashtable table;
	
	public MediatorImpl() throws RemoteException
	{
		table = new Hashtable();
	}
	
	public void registerAgent(Agent agent) {
		AgentInfo ai = new AgentInfo(agent.getID(), null, null);
		agent.setMediator(this);
		table.put(ai.getID(), ai);
		System.out.println("> registered agent " + agent.getID());
		agent.start();
	}

	public void unregisterAgent(Agent agent) {
		table.remove(agent.getID());
		System.out.println("> unregistered agent " + agent.getID());
	}

	public Agent findAgent(Object agentID) throws RemoteException {
		
		Agent a = null;
		try {
			a = (Agent)Naming.lookup((String)agentID);
		} catch (Exception e) {
			e.getClass();
			e.getMessage();
		}
			
		return a;
	}
	
	public void insertAction(Object agentID, Action action) {
		//TODO
		/*
		 * não sei bem como funciona a inserção
		 * de novas tarefas na arvore...
		 */
	}
	
	public Action getNextAction(Agent a) {
		LinkedList actions = a.getScript().getActions();

		if (actions.size() > 0) {
			Action action = (Action)actions.getFirst();
			actions.removeFirst();
			a.getScript().setActions(actions);
			return action;
		}
		else
			return null;
	}

	public synchronized void run() throws RemoteException {
		//for ()
	}
}
