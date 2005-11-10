package server.mediator;

import server.action.Action;
import server.agent.*;

import java.net.MalformedURLException;
import java.rmi.server.*;
import java.rmi.*;
import java.util.*;

public class MediatorImpl extends UnicastRemoteObject implements Mediator {
	private static final long serialVersionUID = -5563370450807830696L;

	private Hashtable agentTable;

	/**
	 * class constructor
	 * 
	 * @throws RemoteException
	 * 
	 */
	public MediatorImpl() throws RemoteException {
		agentTable = new Hashtable();
		AgentFactory agentFactory = new AgentFactoryImpl();

		try {
			Naming.rebind(AgentFactory.class.getName(), agentFactory);
		} catch (MalformedURLException e) {
			e.printStackTrace();
		}
	}

	/**
	 * Regists a particular agent into the mediator.
	 * 
	 * @param agent		the agent to register
	 * @param info		an object containing several
	 * 					information about the agent
	 */
	public void registerAgent(Agent agent, AgentInfo info) {
		try {
				if (!agentTable.containsKey(agent.getID())) {
					agent.setMediator(this);
					agentTable.put(agent.getID(), info);
					System.out.println("> registered agent " + agent.getID());
				} else {
					// something in the way she moves
				}
		} catch (Exception ex) {
			ex.printStackTrace();
		}
	}

	/**
	 * Unregists the agent from the mediator
	 * 
	 * @param agent			the agent to unregister
	 */
	public void unregisterAgent(Agent agent) {
		try {
			if (agentTable.containsKey(agent.getID())) {
				agentTable.remove(agent.getID());
				System.out.println("> unregistered agent " + agent.getID());
			} else {
				return;
			}
		} catch (Exception ex) {
			ex.printStackTrace();
		}
	}
	
	/**
	 * Finds and returns an agent by is agentID
	 * 
	 * @throws RemoteException
	 * @param agentID		agent identifier
	 * @return				the agent found. Null if not found.
	 */
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

	/**
	 * Returns the next actions to be executed by the agent
	 * 
	 * @param a			agent to retreive the action
	 * @return			the next action of the agent (a).
	 * 					Null if none found
	 */
	public Action getNextAction(Agent a) {
		LinkedList actions = null;

		try {
			AgentInfo ai = (AgentInfo) agentTable.get(a.getID());
			actions = ai.getActionList();
			return ((TaskList) actions.getFirst()).getNextAction();
		} catch (Exception ex) {
			if (actions != null && actions.size() != 0)
				actions.removeFirst();
			return null;
		}
	}
	
	/**
	 * 
	 * @throws RemoteException
	 */
	public void run() throws RemoteException {
		// main loop
	}

	/**
	 * Returns the list of actions to be executed by the agent
	 * 
	 * @throws RemoteException
	 * @param agent			agent from witch to retrive the list
	 * @return				a LinkedList containing the actions of the
	 * 						agent (a)
	 */
	public LinkedList getActionList(Agent agent) throws RemoteException {
		AgentInfo ai = (AgentInfo) this.agentTable.get(agent.getID());
		return ai.getActionList();
	}
	
	
	/**
	 * fetches the AgentInfo of a specific agent and removes the
	 * first action of the taskList
	 * 
	 * @throws RemoteException
	 * @param agent			object to get the AgentInfo
	 */
	public void skipActionList(Agent agent) throws RemoteException {
		AgentInfo ai = (AgentInfo) this.agentTable.get(agent.getID());
		LinkedList list = ai.getActionList();
		list.removeFirst();
		ai.setActionList(list);
		this.agentTable.put(agent.getID(), ai);
	}

	/**
	 * returns a factory to create agents
	 * 
	 * @throws RemoteException
	 * @return				factory to create agents
	 */
	public AgentFactory getAgentFactory() throws RemoteException {
		AgentFactory agentFactory = null;

		try {
			agentFactory = (AgentFactory) Naming.lookup("//localhost/"
					+ AgentFactory.class.getName());
		} catch (MalformedURLException e) {
			e.printStackTrace();
		} catch (RemoteException e) {
			e.printStackTrace();
		} catch (NotBoundException e) {
			e.printStackTrace();
		}

		return agentFactory;
	}

	/**
	 * overwrites the agent task list with a specific 
	 * action
	 * 
	 * @throws RemoteException
	 * @param agentID			agent identifier
	 * @param action			new action to set in the agent
	 * 							specified in agentID 
	 */
	public void interrupt(String agentID, Action action) throws RemoteException {
		LinkedList newTask = new LinkedList();
		newTask.add(action);
		((AgentInfo)agentTable.get(agentID)).setActionList(newTask);
	}

	/**
	 * search the agentTable for active agents,
	 * and creates a LinkedList with the identifiers
	 * of the agents found
	 * 
	 * @throws RemoteException
	 * @return				LinkedList of agentID's
	 */
	public LinkedList getInfo() throws RemoteException {
		
		Collection e = agentTable.values();
		Iterator i = e.iterator();
		LinkedList result = new LinkedList();
		
		while (i.hasNext())
			result.add(((AgentInfo)i.next()).getID());
		return result;
	}

	/**
	 * this method suports the agent clone operation
	 * that isn't fully implemented in this version.
	 * 
	 * @throws RemoteException
	 */
	public void transferActions(Agent dest, Agent orig) throws RemoteException {
		AgentInfo ai = (AgentInfo) this.agentTable.get(orig.getID());
		LinkedList ll = ai.getActionList();
		LinkedList newList = new LinkedList();

		TaskList list = (TaskList) ll.getFirst();
		while (list != null) {
			newList.addFirst(list);
			list = (TaskList) ll.removeFirst();
		}
		ai.setActionList(ll);

		AgentInfo newAI = new AgentInfo(dest.getID(), newList);
		registerAgent(dest, newAI);
	}
}
