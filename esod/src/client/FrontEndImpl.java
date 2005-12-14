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
		try {
			return mediator.getAgentFactory().validateScript(script);
		} catch (RemoteException e) {
			e.printStackTrace();
		}

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
	
	public String listActiveAgents() {
		int n = 0;
		StringBuffer sb = new StringBuffer();

		try {
			LinkedList list = mediator.getInfo();
			Iterator i = list.iterator();
			sb.append("NUM\tAGENT-ID\n");
			while (i.hasNext()) {
				n++;
				sb.append(n + "\t" + i.next() + "\n");
			}
		} catch (Exception e) {
			e.printStackTrace();
		}

		return sb.toString();
	}
	
	public String listAvailableReports() {
		int n = 0;
		StringBuffer sb = new StringBuffer();

		try {
			LinkedList list = repository.getInfo();
			Iterator i = list.iterator();
			sb.append("NUM\tAGENT-ID\n");
			while (i.hasNext()) {
				n++;
				sb.append(n + "\t" + i.next() + "\n");
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
		
		return sb.toString();
	}

	public void killAgent(int idx) {
		try {
			idx --;
			Action migrateHome = new MigrateAction(localHost.getHostname(), true);
			mediator.interrupt(mediator.getInfo().get(idx).toString(), migrateHome);
			System.out.println("Agente terminado com sucesso.");
		} catch (Exception e) {
			e.printStackTrace();
		}

	}
	
	public String getAgentReport(int idx) {
		try {
			idx --;
			return repository.getFinalReport(repository.getInfo().get(idx).toString()).toString();
		} catch (RemoteException e) {
			e.printStackTrace();
		}
		return null;
	}
	
	public String getHostReport(int idx) {
		try {
			idx --;
			return repository.getLastReport(repository.getInfo().get(idx).toString()).toString();
		} catch (RemoteException e) {
			e.printStackTrace();
		}
		return null;
	}
}
