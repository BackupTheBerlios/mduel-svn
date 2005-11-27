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
import server.repository.AgentReport;
import server.repository.HostReport;
import server.repository.Repository;

public class Client {

	private AgentHost localHost;

	private Mediator mediator;

	private Repository repository;

	private LinkedList list;

	public AgentHost getLocalHost() {
		return localHost;
	}

	public Repository getRepository() {
		return repository;
	}

	public Mediator getMediator() {
		return mediator;
	}

	public LinkedList getList() {
		return list;
	}

	public int startAgent(String script) {
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
		return 0;
	}

	public void listMediatorAgents() {
		int n = 0;

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

	public void listRepositoryAgents() {
		int n = 0;

		try {
			list = repository.getInfo();
			System.out.println("LIST-SIZE: " + list.size());
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

	public Client() throws Exception {
		System.out.println("> starting Client...");
		System.out.println("> rebinding... ");

		localHost = (AgentHost) Naming.lookup("//localhost/"
				+ AgentHost.class.getName());
		mediator = (Mediator) Naming.lookup("//localhost/"
				+ Mediator.class.getName());
		repository = (Repository) Naming.lookup("//localhost/"
				+ Repository.class.getName());
	}

	public static void menuInit() {
		System.out.println("--------CONSOLA DE CONTROLO DO AGENTE--------");
		System.out.println("1	lançar agente.");
		System.out.println("2	listar agentes activos.");
		System.out.println("3	parar a execução de um agente.");
		System.out.println("4	relatório completo de um agente.");
		System.out.println("5	último relatório de um agente.");
		System.out.println("--------CONSOLA DE CONTROLO DO AGENTE--------");
	}

	public static void main(String[] args) throws RemoteException {
		Client client = null;
		
		try {
			client = new Client();
		} catch (Exception ex) {
			ex.printStackTrace();
			return;
		}

		char c = '.';
		String script = null;
		int i;

		menuInit();
		while (true) {

			c = SavitchIn.readNonwhiteChar();
			switch (c) {
			case '1':

				System.out.println("Indique o ficheiro de script pretendido:");
				try {
					script = SavitchIn.readWord();
					client.startAgent(script);
				} catch (Exception e) {
					e.printStackTrace();
				}

				menuInit();
				break;

			case '2':

				client.listMediatorAgents();
				menuInit();
				break;

			case '3':
				System.out
						.println("Indique o número do agente que quer terminar:");
				client.listMediatorAgents();
				i = SavitchIn.readInt();

				try {
					Action migrateHome = new MigrateAction(
							client.getLocalHost().getHostname(), true);
					client.getMediator().interrupt(
							(String) client.getList().get(i), migrateHome);
					System.out.println("Agente terminado com sucesso.");
				} catch (Exception e) {
					e.printStackTrace();
				}

				menuInit();
				break;

			case '4':

				System.out.println("Indique o número do agente a reportar:");
				client.listRepositoryAgents();
				i = SavitchIn.readInt();

				AgentReport fullReport = client.getRepository().getFinalReport(
						(String) client.getList().get(i));
				fullReport.printReport();

				menuInit();
				break;

			case '5':

				System.out.println("Indique o número do agente a reportar:");
				client.listRepositoryAgents();
				i = SavitchIn.readInt();

				HostReport hostReport = client.getRepository().getLastReport(
						(String) client.getList().get(i));
				System.out.println(hostReport);

				menuInit();
				break;

			default:

				break;
			}
		}
	}
}
