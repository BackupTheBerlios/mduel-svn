package client;

import java.net.MalformedURLException;
import java.rmi.Naming;
import java.rmi.NotBoundException;
import java.rmi.RemoteException;
import java.util.LinkedList;

import server.AgentHost;
import server.AgentHostImpl;
import server.agent.TaskList;
import server.action.Action;
import server.action.MigrateAction;
import server.agent.Agent;
import server.mediator.AgentInfo;
import server.mediator.Mediator;
import server.repository.AgentReport;
import server.repository.HostReport;
import server.repository.Repository;

public class Client extends AgentHostImpl {

	private static final long serialVersionUID = -6989670203285660532L;
	/*
	 * tem de ter uma estrutura para possibilitar o lan�amento de v�rios
	 * agentes...
	 * o repositorio e mediador podem estar � parte...
	 * visto s� ser lan�ado um de cada...
	 * 
	 */
	
	private Mediator mediator;
	private Repository repository;
	private Agent agent;
	
	public Client(String[] args) throws Exception {
		
		try {
			
			System.out.println("> starting Client...");
			System.out.print("> rebinding... ");
			
			AgentHost host = (AgentHost) Naming.lookup("//localhost/" + AgentHost.class.getName());
			mediator = (Mediator) Naming.lookup("//localhost/" + Mediator.class.getName());
			repository = (Repository) Naming.lookup("//localhost/" + Repository.class.getName());
			System.out.println("loading script " + args[0]);
			agent = mediator.getAgentFactory().create(host, args[0]);
			
			if (agent == null) {
				System.out.println("erro ao criar agente");
				return;
			}

			agent.setMediator(mediator);
			agent.setRepository(repository);
			mediator.registerAgent(agent, new AgentInfo(agent.getID(), agent.getScript().getActions()));
			agent.setHome(this);
			host.accept(agent);
		} catch (MalformedURLException e) {
			e.printStackTrace();
			throw e;
		} catch (RemoteException e) {
			e.printStackTrace();
			throw e;
		} catch (NotBoundException e) {
			e.printStackTrace();
			throw e;
		}	
	}

	public static void menuInit() {
		System.out.println("--------CONSOLA DE CONTROLO DO AGENTE--------");
		System.out.println("1	parar execu��o e regressar.");
		System.out.println("2	imprimir ultimas tarefas executadas.");
		System.out.println("3	imprimir todas tarefas executadas.");
		System.out.println("--------CONSOLA DE CONTROLO DO AGENTE--------");
	}
	
	public static void main(String[] args) throws RemoteException {
		Client client = null;

		try {
			client = new Client(args);
		} catch (Exception e) {
			System.out.println("agent startup failed!");
			return;
		}
		char c = '.';

		menuInit();
		while (true) {
			
			try {
				c = (char)System.in.read();
			} catch(Exception e) {
				e.getMessage();
			}
			
			switch (c) {
			case '1':
				/* TESTING FASE */
				try {
					Action migrateHome = new MigrateAction(client.getAgent().getHome().getHostname(), true);
					client.getMediator().interrupt(client.getAgent().getID(), migrateHome);
				} catch (Exception e) {
					e.printStackTrace();
				}
				
				break;
				
			case '2':
				HostReport hostReport = client.getRepository().getLastReport();
				hostReport.printReport();
				break;
				
			case '3':
				AgentReport fullReport = client.getRepository().getFinalReport(client.getAgent().getID());
				fullReport.printReport();
				break;
				
			default:
				menuInit();
				break;
			}
			
		}
		
	}
	
	public Repository getRepository() {
		return repository;
	}
	
	public Mediator getMediator() {
		return mediator;
	}
	
	public Agent getAgent() {
		return agent;
	}
}
