package server;

import java.io.IOException;
import java.rmi.Naming;
import java.rmi.RMISecurityManager;

import server.http.ClassFileServer;

public class AgentServer {
	public static void main(String[] args) {
		try {
			new ClassFileServer(2005, "./bin/");
		} catch (IOException e) {
			e.printStackTrace();
		}

		new AgentServer();
	}

	/**
	 * class constructor
	 */
	public AgentServer() {
		System.setSecurityManager(new RMISecurityManager());
		try {
			AgentHostImpl ah = new AgentHostImpl();
			System.out.println("starting AgentServer on '" + ah.getHostname()
					+ "'...");
			System.out.print("> rebinding... ");
			Naming.rebind(AgentHost.class.getName(), ah);
			System.out.println("ok!");
			System.out.println("> waiting for agents...");
			while (true) {
				ah.exec();
				Thread.sleep(100);
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
	}
}
