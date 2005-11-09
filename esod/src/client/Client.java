package client;

import java.io.BufferedReader;
import java.io.DataInputStream;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.MalformedURLException;
import java.rmi.Naming;
import java.rmi.NotBoundException;
import java.rmi.RemoteException;
import java.util.Hashtable;
import java.util.Iterator;
import java.util.LinkedList;

import server.AgentHost;
import server.AgentHostImpl;
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

	private Mediator mediator;
	private Repository repository;
	//private LinkedList fullList;
	private LinkedList list;
	
	
	public Repository getRepository() {
		return repository;
	}
	
	public Mediator getMediator() {
		return mediator;
	}
	
	
	public LinkedList getList() {
		return list;
	}
	
	
	/*
	public Agent getAgent(String agentID) {
		Iterator i = list.iterator();
		Agent a = null;
		while (i.hasNext()) {
			a = (Agent)i.next();
			try {
				if (a.getID().equals(agentID))
					return a;
			} catch (Exception e) {
				e.getMessage();
			}
		}
		return null;
	}
	
	public Agent getAgent(int i) {
		return (Agent)list.get(i);
	}
	*/
	
	/*
	public void setAgent(Agent agent) {
		list.add(agent);
	}
	*/
	
	public int startAgent(String script) {
		try {
			Agent agent = mediator.getAgentFactory().create(this, script);
			agent.setMediator(mediator);
			agent.setRepository(repository);
			mediator.registerAgent(agent, new AgentInfo(agent.getID(), agent.getScript().getActions()));
			agent.setHome(this);
			this.accept(agent);
		} catch (Exception e) {
			e.getMessage();
		}
		return 0;
	}
	
	public void listAgents() {
		int n=0;
		
		try {
			list = mediator.getInfo();
			Iterator i = list.iterator();
			System.out.println("NUM		AGENT-ID");
			while (i.hasNext()) {
				System.out.println(n + "	" + i.next());
				n++;
			}
		} catch (Exception e) {
			e.getMessage();
		}
	}
	
	public Client() throws RemoteException {
		
		System.out.println("> starting Client...");
		System.out.print("> rebinding... ");
		
		try {

			mediator = (Mediator) Naming.lookup("//localhost/" + Mediator.class.getName());
			repository = (Repository) Naming.lookup("//localhost/" + Repository.class.getName());

		} catch (MalformedURLException e) {
			e.printStackTrace();
		} catch (RemoteException e) {
			e.printStackTrace();
		} catch (NotBoundException e) {
			e.printStackTrace();
		}	
	}

	public static void menuInit() {
		System.out.println("--------CONSOLA DE CONTROLO DO AGENTE--------");
		System.out.println("1	lan�ar agente.");
		System.out.println("2	listar agentes activos.");
		System.out.println("3	parar a execu��o de um agente.");
		System.out.println("4	relat�rio completo de um agente.");
		System.out.println("5	�ltimo relat�rio de um agente.");
		System.out.println("--------CONSOLA DE CONTROLO DO AGENTE--------");
	}
	
	
	
	public static void main(String[] args) throws RemoteException {
		
		Client client = new Client();
		char c = '.';
		String script = null;
		
		menuInit();
		while (true) {
			
			c = SavitchIn.readChar();
			switch (c) {
			case '1':
				
				System.out.println("Indique o ficheiro de script pretendido:");
				try {
					script = SavitchIn.readWord();
					client.startAgent(script);
				} catch (Exception e) {
					e.getMessage();
				}

				menuInit();
				break;
				
			case '2':
				
				client.listAgents();
				menuInit();
				break;
				
			case '3':
				System.out.println("Indique o n�mero do agente que quer terminar:");
				client.listAgents();
				c = SavitchIn.readChar();

				try {
					Action migrateHome = new MigrateAction(client.getHostname(), true);
					client.getMediator().interrupt((String)client.getList().get((int)c), migrateHome);
					System.out.println("Agente terminado com sucesso.");
				} catch (Exception e) {
					e.printStackTrace();
				}
				
				menuInit();
				break;
				
			case '4':
				
				System.out.println("Indique o n�mero do agente a reportar:");
				client.listAgents();
				c = SavitchIn.readChar();
				
				AgentReport fullReport = client.getRepository().getFinalReport((String)client.getList().get((int)c));
				fullReport.printReport();
				
				menuInit();
				break;
				
			case '5':
				
				System.out.println("Indique o n�mero do agente a reportar:");
				client.listAgents();
				c = SavitchIn.readChar();
				
				HostReport hostReport = client.getRepository().getLastReport((String)client.getList().get((int)c));
				hostReport.printReport();
				
				menuInit();
				break;
				
			default:
				
				break;
			}	
		}	
	}
}
