package server.mediator;

import server.action.Action;
import server.agent.Agent;

import java.rmi.RemoteException;
import java.rmi.server.UnicastRemoteObject;
import java.rmi.*;
import java.util.*;

public class MediatorImpl extends UnicastRemoteObject implements Mediator {
	private static final long serialVersionUID = -5563370450807830696L;

	private Hashtable agentTable;

	public MediatorImpl() throws RemoteException {
		agentTable = new Hashtable();
	}

	public synchronized void registerAgent(Agent agent) {
		if (!agentTable.containsKey(agent.getID())) {
			AgentInfo ai = new AgentInfo(agent.getID(), agent.getScript().getActions());
			agent.setMediator(this);
			agentTable.put(ai.getID(), ai);
			System.out.println("> registered agent " + agent.getID());
		} else {
			System.out.println("> updating agent " + agent.getID() + " info");
		}
	}

	public synchronized void unregisterAgent(Agent agent) {
		
		if (agentTable.containsKey(agent.getID())) {
			AgentInfo ai = (AgentInfo) agentTable.get(agent.getID());
			if (ai.isRunComplete()) {
				agentTable.remove(agent.getID());
				try {
					agent.getHome().kill(agent);
				} catch (RemoteException e) {
					e.printStackTrace();
				}
				System.out.println("> unregistered agent " + agent.getID());
			} else {
			}
		} else { return; }
	}

	public Agent findAgent(Object agentID) throws RemoteException {

		Agent a = null;
		try {
			a = (Agent) Naming.lookup((String) agentID);
		} catch (Exception e) {
			e.getClass();
			e.getMessage();
		}

		return a;
	}

	public void insertAction(Object agentID, Action action) {
		// TODO
		/*
		 * não sei bem como funciona a inserção de novas tarefas na arvore...
		 */
	}

	public synchronized Action getNextAction(Agent a) {
		
		try {
			AgentInfo ai = (AgentInfo) agentTable.get(a.getID());
			LinkedList actions = ai.getActionList();
			
			if (ai.getNextAction() < actions.size() && !ai.isRunComplete()) {
				
				System.out.println("ACTIONLIST: " + actions.size() + "CURRENTACTION: " + ai.getNextAction());
				Action action = (Action)actions.get(ai.getNextAction());
				ai.setAction(ai.getNextAction()+1);
				agentTable.put(a.getID(), ai);
				return action;
			
			} else {
				ai.setRunComplete(true);
				return null;
			}
			
			/*
			if (actions.size() > 0) {
				//Thread.sleep(100);
				Action action = (Action) actions.getFirst();
				actions.removeFirst();
				ai.setActionList(actions);
				agentTable.put(a.getID(), ai);
				return action;
			} else {
				ai.setRunComplete(true);
				return null;
			}
			*/
			
		} catch (Exception ex) {
			//ex.printStackTrace();
			return null;
		}
	}

	public synchronized void run() throws RemoteException {
	}

	public LinkedList getActionList(Agent agent) throws RemoteException {
		AgentInfo ai = (AgentInfo) this.agentTable.get(agent.getID());
		return ai.getActionList();
	}
}
