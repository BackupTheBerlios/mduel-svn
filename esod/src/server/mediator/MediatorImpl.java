package server.mediator;

import server.action.Action;
import server.agent.*;
import server.locator.Proxy;

import java.rmi.*;
import java.rmi.activation.*;
import java.util.*;

public class MediatorImpl extends Activatable implements Mediator {
	private static final long serialVersionUID = -5563370450807830696L;

	private Hashtable agentTable;
	
	private AgentFactory agentFactory;

	/**
	 * class constructor
	 * 
	 * @throws RemoteException
	 * 
	 */
	public MediatorImpl(ActivationID id, MarshalledObject data) throws RemoteException {
		super(id, 0);

		agentTable = new Hashtable();
		agentFactory = new AgentFactoryImpl();
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
	
	public AgentFactory getAgentFactory() {
		return agentFactory;
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
		((AgentInfo) agentTable.get(agentID)).setActionList(newTask);
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
			result.add(((AgentInfo) i.next()).getID());
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

	/**
	 * 
	 */
	public void setLocalProxy(String agentID, Proxy agentProxy) throws RemoteException {
		((AgentInfo)agentTable.get(agentID)).setLocalProxy(agentProxy);
	}

	/**
	 * 
	 */
	public void setFixedProxy(String agentID, Proxy agentProxy) throws RemoteException {
		((AgentInfo)agentTable.get(agentID)).setFixedProxy(agentProxy);
	}

	/**
	 * 
	 */
	public Proxy getLocalProxy(String agentID) throws RemoteException {
		return ((AgentInfo)agentTable.get(agentID)).getLocalProxy();
	}

	/**
	 * 
	 */
	public Proxy getFixedProxy(String agentID) throws RemoteException {
		return ((AgentInfo)agentTable.get(agentID)).getFixedProxy();
	}

}
