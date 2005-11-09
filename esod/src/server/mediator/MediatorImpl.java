package server.mediator;

import server.action.Action;
import server.action.CloneAction;
import server.agent.*;

import java.net.MalformedURLException;
import java.rmi.server.*;
import java.rmi.*;
import java.util.*;

public class MediatorImpl extends UnicastRemoteObject implements Mediator {
	private static final long serialVersionUID = -5563370450807830696L;

	private Hashtable agentTable;

	public MediatorImpl() throws RemoteException {
		agentTable = new Hashtable();
		AgentFactory agentFactory = new AgentFactoryImpl();

		try {
			Naming.rebind(AgentFactory.class.getName(), agentFactory);
		} catch (MalformedURLException e) {
			e.printStackTrace();
		}
	}

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
		// TODO: is this needed? schedule for removal
	}

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

	public void run() throws RemoteException {
		// main loop
	}

	public LinkedList getActionList(Agent agent) throws RemoteException {
		AgentInfo ai = (AgentInfo) this.agentTable.get(agent.getID());
		return ai.getActionList();
	}

	public void skipActionList(Agent agent) throws RemoteException {
		AgentInfo ai = (AgentInfo) this.agentTable.get(agent.getID());
		LinkedList list = ai.getActionList();
		list.removeFirst();
		ai.setActionList(list);
		this.agentTable.put(agent.getID(), ai);
	}

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

	// TODO: rename this method to something like bypassActions or injectAction
	public void interrupt(String agentID, Action action) throws RemoteException {
		LinkedList newTask = new LinkedList();
		newTask.add(action);
		((AgentInfo)agentTable.get(agentID)).setActionList(newTask);
	}

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
