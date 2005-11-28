package client;

import java.rmi.Naming;
import java.rmi.RemoteException;
import java.util.Iterator;
import java.util.LinkedList;

import server.AgentHost;
import server.action.Action;
import server.action.MigrateAction;
import server.agent.Agent;
import server.mediator.AgentInfo;
import server.mediator.Mediator;
import server.repository.Repository;

public class FrontEndImpl implements FrontEnd {
	private AgentHost localHost;
	private Mediator mediator;
	private Repository repository;

	public FrontEndImpl() throws Exception {
		localHost = (AgentHost) Naming.lookup("//localhost/"
				+ AgentHost.class.getName());
		mediator = (Mediator) Naming.lookup("//localhost/"
				+ Mediator.class.getName());
		repository = (Repository) Naming.lookup("//localhost/"
				+ Repository.class.getName());
	}

	public boolean validateScript(String script) {
		// TODO: do this
		return false;
	}

	public void startAgent(String script) {
		try {
			Agent agent = mediator.getAgentFactory().create(localHost, script);
			agent.setMediator(mediator);
			agent.setRepository(repository);
			mediator.registerAgent(agent, new AgentInfo(agent.getID(), agent
					.getScript().getActions()));
			AgentHost startHost = (AgentHost) Naming.lookup("//"
					+ agent.getNewHost() + "/" + AgentHost.class.getName());
			agent.setHome(localHost);
			startHost.accept(agent);
		} catch (Exception e) {
			e.printStackTrace();
		}
	}
	
	public void listActiveAgents() {
		int n = 0;

		try {
			LinkedList list = mediator.getInfo();
			Iterator i = list.iterator();
			System.out.println("NUM		AGENT-ID");
			while (i.hasNext()) {
				System.out.println(n + "	" + i.next());
				n++;
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
	}
	
	public void listAvailableReports() {
		int n = 0;

		try {
			LinkedList list = repository.getInfo();
			Iterator i = list.iterator();
			System.out.println("NUM		AGENT-ID");
			while (i.hasNext()) {
				System.out.println(n + "	" + i.next());
				n++;
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	public void killAgent(int idx) {
		try {
			Action migrateHome = new MigrateAction(localHost.getHostname(), true);
			mediator.interrupt(mediator.getInfo().get(idx).toString(), migrateHome);
			System.out.println("Agente terminado com sucesso.");
		} catch (Exception e) {
			e.printStackTrace();
		}

	}
	
	public String getAgentReport(int idx) {
		try {
			return repository.getFinalReport(mediator.getInfo().get(idx).toString()).toString();
		} catch (RemoteException e) {
			e.printStackTrace();
		}
		return null;
	}
	
	public String getHostReport(int idx) {
		try {
			return repository.getLastReport(repository.getInfo().get(idx).toString()).toString();
		} catch (RemoteException e) {
			e.printStackTrace();
		}
		return null;
	}
}
