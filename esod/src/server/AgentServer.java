package server;

import java.rmi.Naming;
import java.rmi.RMISecurityManager;

public class AgentServer {

	public AgentServer() {
		System.setSecurityManager(new RMISecurityManager());
		try {
			AgentHostImpl ah = new AgentHostImpl();
			System.out.println("starting AgentServer on '" + ah.getHostname() + "'...");
			System.out.print("> rebinding... ");
			Naming.rebind(AgentHost.class.getName(), ah);
			System.out.println("ok!");
			System.out.println("> waiting for agents...");
			while (true) {
				ah.exec();
				Thread.sleep(100);
			}
		} catch (Exception e) {
		}		
	}
	
	public static void main(String[] args) {
		new AgentServer();
	}
}
