package client;

import java.net.*;
import java.rmi.*;
import server.*;
import server.agent.*;

public class Client {

	public static void main(String[] args) {
		try {
			AgentHost host = (AgentHost) Naming.lookup("//192.168.0.1/AgentHost");
			Agent agent = new AgentImpl();
			AgentScript script = new AgentScript("myScript", "mmoura", "10.10.2005", "comment", "obs", "text");
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
