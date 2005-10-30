package client;

import java.net.*;
import java.rmi.*;

import parser.ASLParser;
import server.*;
import server.agent.*;

public class Client {

	public static void main(String[] args) {
		try {
			AgentHost host = (AgentHost) Naming.lookup("//localhost/" + AgentHost.class.getName());
			Agent agent = new AgentImpl();
			ASLParser parser = new ASLParser();
			AgentScript script = parser.LoadScript(args[0]);

			agent.setHome(host);
			agent.setScript(script);
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
