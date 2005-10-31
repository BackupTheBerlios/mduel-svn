package client;

import java.net.*;
import java.rmi.*;

import server.*;
import server.agent.*;
import server.mediator.Mediator;

public class Client {

	public static void main(String[] args) {
		try {
			AgentHost host = (AgentHost) Naming.lookup("//localhost/" + AgentHost.class.getName());
			Mediator mediator = (Mediator) Naming.lookup("//localhost/" + Mediator.class.getName());
			AgentFactory af = new AgentFactoryImpl();
			Agent agent = af.create(host, args[0]);
			mediator.registerAgent(agent);
			agent.setMediator(mediator);
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
