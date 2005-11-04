package client;

import java.net.*;
import java.rmi.*;
import server.*;
import server.agent.*;
import server.mediator.Mediator;
import server.repository.Repository;

public class Client {
	
	public static void main(String[] args) {
		
		try {
			AgentHost host = (AgentHost) Naming.lookup("//localhost/" + AgentHost.class.getName());
			Mediator mediator = (Mediator) Naming.lookup("//localhost/" + Mediator.class.getName());
			Repository repository = (Repository) Naming.lookup("//localhost/" + Repository.class.getName());

			Agent agent = mediator.getAgentFactory().create(host, args[0]);
			
			if (agent == null) {
				System.out.println("erro ao criar agente");
				return;
			}

			agent.setMediator(mediator);
			agent.setRepository(repository);

			mediator.registerAgent(agent);
			host.accept(agent);
		
		} catch (MalformedURLException e) {
			e.printStackTrace();
		} catch (RemoteException e) {
			e.printStackTrace();
		} catch (NotBoundException e) {
			e.printStackTrace();
		}

	}
}
